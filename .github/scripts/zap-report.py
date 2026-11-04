#!/usr/bin/env python3
"""Build one HTML page from a ZAP baseline scan, showing passed and failed checks.

ZAP's own reports only list the problems; the passed checks appear only in its
console output. This merges the two.

Usage: zap-report.py <console output> <ZAP JSON report> <output HTML>
Prints a Markdown summary to stdout for the GitHub run summary.
"""
import html
import json
import os
import re
import sys
from datetime import UTC, datetime

# e.g. "WARN-NEW: Missing Anti-clickjacking Header [10020] x 3"
RULE_LINE = re.compile(r"^(PASS|WARN|FAIL|INFO|IGNORE)(?:-NEW|-INPROG)?: (.+?) \[(\d+)\]")
# e.g. "FAIL-NEW: 0   FAIL-INPROG: 0  WARN-NEW: 10    WARN-INPROG: 0  INFO: 0 IGNORE: 0   PASS: 55"
TOTALS_LINE = re.compile(r"^FAIL-NEW: \d+\s+FAIL-INPROG:")

RISKS = {3: "High", 2: "Medium", 1: "Low", 0: "Informational"}
MAX_INSTANCES = 10

CSS = """
:root { --text: #1f2933; --muted: #5f6c7b; --line: #d9e0e7; --bg: #fff; --panel: #f5f7fa;
        --pass: #1a7f37; --warn: #b35900; --fail: #c62828; --info: #1f6feb; }
* { box-sizing: border-box; }
body { margin: 0 auto; max-width: 960px; padding: 24px 16px;
       background: var(--bg); color: var(--text);
       font: 16px/1.5 system-ui, -apple-system, "Segoe UI", Roboto, sans-serif; }
h1 { font-size: 28px; margin: 0 0 4px; }
h2 { font-size: 20px; margin: 32px 0 12px; padding-bottom: 6px;
     border-bottom: 1px solid var(--line); }
.meta { color: var(--muted); font-size: 14px; overflow-wrap: anywhere; }
.tiles { display: grid; grid-template-columns: repeat(auto-fit, minmax(120px, 1fr));
         gap: 12px; margin: 20px 0; }
.tile { background: var(--panel); border-radius: 8px; padding: 12px; }
.tile b { display: block; font-size: 28px; line-height: 1.2; }
.tile span { color: var(--muted); font-size: 14px; }
details { border: 1px solid var(--line); border-radius: 8px; margin: 10px 0; padding: 10px 14px; }
summary { cursor: pointer; font-weight: 600; }
.badge { border-radius: 4px; color: #fff; font-size: 12px; margin-right: 6px; padding: 2px 6px; }
.PASS { background: var(--pass); } .WARN { background: var(--warn); }
.FAIL { background: var(--fail); } .INFO, .IGNORE { background: var(--info); }
h3 { font-size: 14px; margin: 14px 0 2px; }
p { margin: 4px 0; }
.scroll { overflow-x: auto; }
table { border-collapse: collapse; font-size: 14px; width: 100%; }
th, td { border-bottom: 1px solid var(--line); padding: 6px 8px;
         text-align: left; vertical-align: top; }
td { overflow-wrap: anywhere; }
code { font-size: 13px; }
"""


def parse_console(text):
    """Return ({rule id: (status, name)}, totals line) from ZAP's console output."""
    rules, totals = {}, ""
    for line in text.splitlines():
        line = line.strip()
        if TOTALS_LINE.match(line):
            totals = " ".join(line.split())
            continue
        match = RULE_LINE.match(line)
        if match:
            status, name, rule_id = match.groups()
            rules[rule_id] = (status, name)
    return rules, totals


def parse_alerts(report):
    """Return {rule id: alert} from ZAP's JSON report, merged across all sites."""
    alerts = {}
    for site in report.get("site", []):
        for alert in site.get("alerts", []):
            rule_id = str(alert.get("pluginid", ""))
            if rule_id in alerts:
                alerts[rule_id]["instances"].extend(alert.get("instances", []))
            else:
                alerts[rule_id] = {**alert, "instances": list(alert.get("instances", []))}
    return alerts


def clean(text):
    """Turn ZAP's HTML fragments into plain text, ready to put in the page."""
    return html.escape(html.unescape(re.sub(r"<[^>]+>", " ", text or "")).strip())


def build_results(rules, alerts):
    """Return (problems sorted by risk, passed check names)."""
    problems = []
    for rule_id, alert in alerts.items():
        status = rules.get(rule_id, ("WARN", ""))[0]
        problems.append({"id": rule_id, "status": status, "alert": alert,
                         "risk": int(alert.get("riskcode", 0))})
    # a rule ZAP warned about that the JSON report does not describe
    for rule_id, (status, name) in rules.items():
        if status != "PASS" and rule_id not in alerts:
            problems.append({"id": rule_id, "status": status, "risk": 0,
                             "alert": {"name": name, "instances": []}})
    problems.sort(key=lambda p: (-p["risk"], p["alert"].get("name", "")))
    passed = sorted((name, rule_id) for rule_id, (status, name) in rules.items()
                    if status == "PASS" and rule_id not in alerts)
    return problems, passed


def render_problem(problem):
    alert = problem["alert"]
    instances = alert["instances"]
    rows = "".join(
        f"<tr><td>{html.escape(i.get('method', ''))}</td><td>{html.escape(i.get('uri', ''))}</td>"
        f"<td><code>{html.escape(i.get('evidence', '') or i.get('param', ''))}</code></td></tr>"
        for i in instances[:MAX_INSTANCES])
    more = len(instances) - MAX_INSTANCES
    table = ""
    if rows:
        table = ("<h3>Affected URLs</h3><div class='scroll'><table>"
                 "<tr><th>Method</th><th>URL</th><th>Evidence</th></tr>" + rows + "</table></div>")
    if more > 0:
        table += f"<p class='meta'>and {more} more</p>"
    cwe = alert.get("cweid", "")
    reference = ""
    if cwe not in ("", "-1"):
        reference = f"<h3>Reference</h3><p>CWE-{html.escape(str(cwe))}</p>"
    return (
        f"<details><summary><span class='badge {problem['status']}'>{problem['status']}</span>"
        f"{RISKS[problem['risk']]}: {html.escape(alert.get('name', ''))} [{problem['id']}]"
        f" ({len(instances)} found)</summary>"
        f"<h3>Problem</h3><p>{clean(alert.get('desc'))}</p>"
        f"<h3>How to fix it</h3><p>{clean(alert.get('solution'))}</p>"
        f"{table}{reference}</details>")


def render_page(report, problems, passed, totals):
    sites = ", ".join(html.escape(s.get("@name", "")) for s in report.get("site", []))
    commit = os.environ.get("GITHUB_SHA", "local")
    when = datetime.now(UTC).strftime("%Y-%m-%d %H:%M UTC")
    tiles = [("Passed", len(passed))] + [
        (RISKS[risk], sum(1 for p in problems if p["risk"] == risk)) for risk in RISKS]
    tile_html = "".join(f"<div class='tile'><b>{count}</b><span>{label}</span></div>"
                        for label, count in tiles)
    problem_html = "".join(render_problem(p) for p in problems) or "<p>No problems found.</p>"
    passed_rows = "".join(f"<tr><td><span class='badge PASS'>PASS</span>{html.escape(name)}</td>"
                          f"<td>{rule_id}</td></tr>" for name, rule_id in passed)
    passed_html = ("<div class='scroll'><table><tr><th>Check</th><th>Rule</th></tr>"
                   + passed_rows + "</table></div>") if passed else "<p>No passed checks found.</p>"
    return f"""<!doctype html>
<html lang="en">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>ZAP Security Report</title>
<style>{CSS}</style>
</head>
<body>
<h1>OWASP ZAP security report</h1>
<p class="meta">Target: {sites or "unknown"}<br>Commit: <code>{html.escape(commit)}</code><br>
Scanned: {when} with {html.escape(str(report.get("@version", "ZAP")))}<br>
ZAP totals: {html.escape(totals) or "not found"}</p>
<div class="tiles">{tile_html}</div>
<h2>Problems found ({len(problems)})</h2>
{problem_html}
<h2>Passed checks ({len(passed)})</h2>
{passed_html}
</body>
</html>
"""


def render_summary(problems, passed, totals):
    lines = ["## OWASP ZAP scan (stage)", "| Result | Checks |", "|---|---|",
             f"| Passed | {len(passed)} |"]
    lines += [f"| {RISKS[risk]} | {sum(1 for p in problems if p['risk'] == risk)} |"
              for risk in RISKS]
    if totals:
        lines += ["", f"ZAP totals: `{totals}`"]
    return "\n".join(lines)


def main():
    if len(sys.argv) != 4:
        sys.exit(__doc__)
    console_path, json_path, output_path = sys.argv[1:]
    with open(console_path, encoding="utf-8", errors="replace") as f:
        rules, totals = parse_console(f.read())
    with open(json_path, encoding="utf-8") as f:
        report = json.load(f)
    problems, passed = build_results(rules, parse_alerts(report))
    with open(output_path, "w", encoding="utf-8") as f:
        f.write(render_page(report, problems, passed, totals))
    print(render_summary(problems, passed, totals))


if __name__ == "__main__":
    main()
