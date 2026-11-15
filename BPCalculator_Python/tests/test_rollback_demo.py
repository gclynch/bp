"""Rollback demo (MIGRATION.md F12): /Privacy logs an exception only where
telemetry is on (prod), so test, stage and E2E still pass."""
import logging


def simulated_failures(caplog):
    return [r for r in caplog.records if r.getMessage() == "Simulated failure"]


def test_no_failure_without_telemetry(client, caplog, monkeypatch):
    monkeypatch.delenv("APPLICATIONINSIGHTS_CONNECTION_STRING", raising=False)
    caplog.set_level(logging.ERROR, logger="pages")
    assert client.get("/Privacy").status_code == 200
    assert simulated_failures(caplog) == []


def test_privacy_logs_exception_with_telemetry(client, caplog, monkeypatch):
    monkeypatch.setenv("APPLICATIONINSIGHTS_CONNECTION_STRING", "InstrumentationKey=test")
    caplog.set_level(logging.ERROR, logger="pages")
    response = client.get("/Privacy")
    assert response.status_code == 200
    assert "Privacy Policy" in response.get_data(as_text=True)
    [record] = simulated_failures(caplog)
    assert record.exc_info[0] is RuntimeError


def test_other_pages_unaffected_with_telemetry(client, caplog, monkeypatch):
    monkeypatch.setenv("APPLICATIONINSIGHTS_CONNECTION_STRING", "InstrumentationKey=test")
    caplog.set_level(logging.ERROR, logger="pages")
    assert client.get("/").status_code == 200
    assert simulated_failures(caplog) == []
