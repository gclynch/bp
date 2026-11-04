"""Configuration - replaces appsettings.json, appsettings.Development.json and
the ASPNETCORE_ENVIRONMENT switch from Properties/launchSettings.json."""
import os
from typing import ClassVar


class Config:
    # "Production" settings (appsettings.json); ASP.NET Core also defaults to Production
    DEBUG = False
    TESTING = False
    LOG_LEVEL = "INFO"
    # Key for the anti-forgery (CSRF) token. ASP.NET Core generates its Data Protection
    # key automatically; here we read it from the environment or generate one per process.
    SECRET_KEY = os.environ.get("SECRET_KEY") or os.urandom(32)
    WTF_CSRF_ENABLED = True
    # commit SHA baked into the image by the pipeline; "dev" when run locally
    APP_VERSION = os.environ.get("APP_VERSION", "dev")
    # session cookie: HTTPS only, and not sent on cross-site requests
    SESSION_COOKIE_SECURE = True
    SESSION_COOKIE_SAMESITE = "Lax"
    # sent on every response (see MIGRATION.md F8)
    SECURITY_HEADERS: ClassVar[dict[str, str]] = {
        # img-src allows data: because Bootstrap's CSS has inline SVG images
        "Content-Security-Policy": (
            "default-src 'self'; img-src 'self' data:; object-src 'none'; "
            "base-uri 'self'; form-action 'self'; frame-ancestors 'none'"
        ),
        "X-Frame-Options": "DENY",
        "X-Content-Type-Options": "nosniff",
        "Strict-Transport-Security": "max-age=31536000; includeSubDomains",
        # keeps the Referer on same-site POSTs, which Flask-WTF can require over HTTPS
        "Referrer-Policy": "strict-origin-when-cross-origin",
        "Permissions-Policy": "camera=(), microphone=(), geolocation=()",
        "Cross-Origin-Opener-Policy": "same-origin",
        "Cross-Origin-Resource-Policy": "same-origin",
        "Cross-Origin-Embedder-Policy": "require-corp",
    }


class DevelopmentConfig(Config):
    # appsettings.Development.json
    DEBUG = True
    LOG_LEVEL = "DEBUG"
    SESSION_COOKIE_SECURE = False   # the dev server is plain HTTP


class TestingConfig(Config):
    TESTING = True
    WTF_CSRF_ENABLED = False
    SESSION_COOKIE_SECURE = False   # the test client is plain HTTP


CONFIGS = {
    "Production": Config,
    "Development": DevelopmentConfig,
    "Testing": TestingConfig,
}


def get_config(env_name=None):
    # APP_ENV plays the role of ASPNETCORE_ENVIRONMENT
    return CONFIGS[env_name or os.environ.get("APP_ENV", "Production")]
