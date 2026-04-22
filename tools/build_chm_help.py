from __future__ import annotations

from datetime import datetime
from html import escape
from pathlib import Path

import markdown


ROOT = Path(__file__).resolve().parent.parent
CHM_DIR = ROOT / "help" / "chm"


def read_markdown(path: Path) -> str:
    return path.read_text(encoding="utf-8")


def render_page(title: str, body_html: str) -> str:
    generated_at = datetime.now().strftime("%Y-%m-%d %H:%M")
    return f"""<html>
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
  <title>{escape(title)}</title>
  <link rel="stylesheet" href="style.css">
</head>
<body>
  <div class="page">
    <div class="topbar">
      <div class="brand">AppGym Help</div>
      <div class="meta">Generated: {escape(generated_at)}</div>
    </div>
    {body_html}
  </div>
</body>
</html>
"""


def render_markdown_page(source: Path, title: str) -> str:
    body = markdown.markdown(
        read_markdown(source),
        extensions=["fenced_code", "tables", "sane_lists"],
        output_format="html5",
    )
    return render_page(title, body)


def write_text(path: Path, content: str) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(content, encoding="utf-8")


def write_ascii(path: Path, content: str) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(content, encoding="ascii")


def build_index() -> str:
    body = """
<h1>AppGym Help</h1>
<p>Tài liệu trợ giúp dạng CHM cho AppGym.</p>

<h2>Nội dung chính</h2>
<ul>
  <li><a href="huong-dan-cai-dat.html">Hướng dẫn cài đặt và cấu hình</a></li>
  <li><a href="logic-nghiep-vu.html">Logic nghiệp vụ</a></li>
</ul>

<h2>Phạm vi</h2>
<ul>
  <li>Cài đặt từ bộ setup hoặc chạy từ source.</li>
  <li>Cấu hình kết nối SQL Server và tạo database ban đầu.</li>
  <li>Vai trò người dùng, thực thể dữ liệu và luồng nghiệp vụ chính.</li>
</ul>
"""
    return render_page("AppGym Help", body)


def build_hhp() -> str:
    return """[OPTIONS]
Compatibility=1.1 or later
Compiled file=..\\..\\help_output\\AppGym_Help.chm
Default topic=index.html
Display compile progress=No
Full-text search=No
Language=0x409 English (United States)
Title=AppGym Help

[FILES]
index.html
huong-dan-cai-dat.html
logic-nghiep-vu.html
style.css
"""


def build_hhc() -> str:
    return """<!DOCTYPE HTML PUBLIC "-//IETF//DTD HTML//EN">
<HTML>
<HEAD>
  <meta name="GENERATOR" content="Codex">
</HEAD>
<BODY>
  <OBJECT type="text/site properties">
    <param name="ImageType" value="Folder">
  </OBJECT>
  <UL>
    <LI>
      <OBJECT type="text/sitemap">
        <param name="Name" value="Trang chu">
        <param name="Local" value="index.html">
      </OBJECT>
    <LI>
      <OBJECT type="text/sitemap">
        <param name="Name" value="Huong dan cai dat">
        <param name="Local" value="huong-dan-cai-dat.html">
      </OBJECT>
    <LI>
      <OBJECT type="text/sitemap">
        <param name="Name" value="Logic nghiep vu">
        <param name="Local" value="logic-nghiep-vu.html">
      </OBJECT>
  </UL>
</BODY>
</HTML>
"""


def build_hhk() -> str:
    return """<!DOCTYPE HTML PUBLIC "-//IETF//DTD HTML//EN">
<HTML>
<HEAD>
  <meta name="GENERATOR" content="Codex">
</HEAD>
<BODY>
  <UL>
    <LI>
      <OBJECT type="text/sitemap">
        <param name="Name" value="AppGym">
        <param name="Local" value="index.html">
      </OBJECT>
    </LI>
    <LI>
      <OBJECT type="text/sitemap">
        <param name="Name" value="Cai dat">
        <param name="Local" value="huong-dan-cai-dat.html">
      </OBJECT>
    </LI>
    <LI>
      <OBJECT type="text/sitemap">
        <param name="Name" value="Database">
        <param name="Local" value="huong-dan-cai-dat.html">
      </OBJECT>
    </LI>
    <LI>
      <OBJECT type="text/sitemap">
        <param name="Name" value="Dang ky goi">
        <param name="Local" value="logic-nghiep-vu.html">
      </OBJECT>
    </LI>
    <LI>
      <OBJECT type="text/sitemap">
        <param name="Name" value="Hoa don">
        <param name="Local" value="logic-nghiep-vu.html">
      </OBJECT>
    </LI>
    <LI>
      <OBJECT type="text/sitemap">
        <param name="Name" value="Logic nghiep vu">
        <param name="Local" value="logic-nghiep-vu.html">
      </OBJECT>
    </LI>
    <LI>
      <OBJECT type="text/sitemap">
        <param name="Name" value="Phan cong PT">
        <param name="Local" value="logic-nghiep-vu.html">
      </OBJECT>
    </LI>
    <LI>
      <OBJECT type="text/sitemap">
        <param name="Name" value="SQL Server">
        <param name="Local" value="huong-dan-cai-dat.html">
      </OBJECT>
    </LI>
    <LI>
      <OBJECT type="text/sitemap">
        <param name="Name" value="Tai khoan">
        <param name="Local" value="logic-nghiep-vu.html">
      </OBJECT>
    </LI>
  </UL>
</BODY>
</HTML>
"""


def main() -> None:
    write_text(CHM_DIR / "index.html", build_index())
    write_text(
        CHM_DIR / "huong-dan-cai-dat.html",
        render_markdown_page(ROOT / "README.md", "Hướng dẫn cài đặt"),
    )
    write_text(
        CHM_DIR / "logic-nghiep-vu.html",
        render_markdown_page(ROOT / "LOGIC_NGHIEP_VU.md", "Logic nghiệp vụ"),
    )
    write_ascii(CHM_DIR / "AppGymHelp.hhp", build_hhp())


if __name__ == "__main__":
    main()
