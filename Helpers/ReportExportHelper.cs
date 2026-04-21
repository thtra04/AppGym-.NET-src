using AppGym.Models;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace AppGym.Helpers
{
    public static class ReportExportHelper
    {
        public static void ExportThanhToanReport(
            string filePath,
            IEnumerable<ThanhToanReportItem> invoiceItems,
            IEnumerable<DangKyUnpaidItem> unpaidItems,
            bool includeInvoiceSheet,
            bool includeUnpaidSheet)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("\u0110\u01B0\u1EDDng d\u1EABn file kh\u00F4ng h\u1EE3p l\u1EC7.", nameof(filePath));

            var invoices = invoiceItems?.ToList() ?? new List<ThanhToanReportItem>();
            var unpaid = unpaidItems?.ToList() ?? new List<DangKyUnpaidItem>();

            if (!includeInvoiceSheet && !includeUnpaidSheet)
                throw new ArgumentException("Ph\u1EA3i ch\u1ECDn \u00EDt nh\u1EA5t m\u1ED9t sheet \u0111\u1EC3 xu\u1EA5t.");

            using var workbook = new XLWorkbook();

            if (includeInvoiceSheet)
            {
                var ws = workbook.Worksheets.Add("HoaDon");
                AddInvoiceSheet(ws, invoices);
            }

            if (includeUnpaidSheet)
            {
                var ws = workbook.Worksheets.Add("ChuaThanhToan");
                AddUnpaidSheet(ws, unpaid);
            }

            workbook.SaveAs(filePath);
        }

        private static void AddInvoiceSheet(IXLWorksheet ws, List<ThanhToanReportItem> items)
        {
            var headers = new[]
            {
                "MaHD", "MaDK", "MaHV", "HoTen", "MaGoi", "TenGoi",
                "NgayBatDau", "NgayHetHan", "NgayThanhToan", "SoTien",
                "HinhThucTT", "NguoiLap", "NguoiThanhToan", "GhiChu"
            };

            for (var i = 0; i < headers.Length; i++)
                ws.Cell(1, i + 1).Value = headers[i];

            var row = 2;
            foreach (var item in items)
            {
                ws.Cell(row, 1).Value = item.MaHD;
                ws.Cell(row, 2).Value = item.MaDK;
                ws.Cell(row, 3).Value = item.MaHV;
                ws.Cell(row, 4).Value = item.HoTen;
                ws.Cell(row, 5).Value = item.MaGoi;
                ws.Cell(row, 6).Value = item.TenGoi;
                ws.Cell(row, 7).Value = item.NgayBatDau;
                ws.Cell(row, 8).Value = item.NgayHetHan;
                ws.Cell(row, 9).Value = item.NgayThanhToan;
                ws.Cell(row, 10).Value = item.SoTien;
                ws.Cell(row, 11).Value = item.HinhThucTT;
                ws.Cell(row, 12).Value = item.TenNguoiLap;
                ws.Cell(row, 13).Value = item.TenNguoiThanhToan;
                ws.Cell(row, 14).Value = item.GhiChu;

                ws.Cell(row, 7).Style.DateFormat.Format = "dd/MM/yyyy";
                ws.Cell(row, 8).Style.DateFormat.Format = "dd/MM/yyyy";
                ws.Cell(row, 9).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
                ws.Cell(row, 10).Style.NumberFormat.Format = "#,##0";
                row++;
            }

            ws.Columns().AdjustToContents();
            ws.Rows().AdjustToContents();
        }

        private static void AddUnpaidSheet(IXLWorksheet ws, List<DangKyUnpaidItem> items)
        {
            var headers = new[]
            {
                "MaDK", "MaHV", "HoTen", "MaGoi", "TenGoi",
                "NgayBatDau", "NgayHetHan", "TongDaThanhToan", "GiaGoi", "ConThieu"
            };

            for (var i = 0; i < headers.Length; i++)
                ws.Cell(1, i + 1).Value = headers[i];

            var row = 2;
            foreach (var item in items)
            {
                ws.Cell(row, 1).Value = item.MaDK;
                ws.Cell(row, 2).Value = item.MaHV;
                ws.Cell(row, 3).Value = item.HoTen;
                ws.Cell(row, 4).Value = item.MaGoi;
                ws.Cell(row, 5).Value = item.TenGoi;
                ws.Cell(row, 6).Value = item.NgayBatDau;
                ws.Cell(row, 7).Value = item.NgayHetHan;
                ws.Cell(row, 8).Value = item.TongDaThanhToan;
                ws.Cell(row, 9).Value = item.GiaGoi;
                ws.Cell(row, 10).Value = item.ConThieu;

                ws.Cell(row, 6).Style.DateFormat.Format = "dd/MM/yyyy";
                ws.Cell(row, 7).Style.DateFormat.Format = "dd/MM/yyyy";
                ws.Cell(row, 8).Style.NumberFormat.Format = "#,##0";
                ws.Cell(row, 9).Style.NumberFormat.Format = "#,##0";
                ws.Cell(row, 10).Style.NumberFormat.Format = "#,##0";
                row++;
            }

            ws.Columns().AdjustToContents();
            ws.Rows().AdjustToContents();
        }

        public static void ExportSingleInvoice(string filePath, HoaDon hd)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("\u0110\u01B0\u1EDDng d\u1EABn file kh\u00F4ng h\u1EE3p l\u1EC7.", nameof(filePath));

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("HoaDon");

            ws.Range("A1:D1").Merge();
            ws.Cell("A1").Value = "HOA DON THANH TOAN";
            ws.Cell("A1").Style.Font.Bold = true;
            ws.Cell("A1").Style.Font.FontSize = 16;
            ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws.Range("A2:D2").Merge();
            ws.Cell("A2").Value = "Phong tap Gym";
            ws.Cell("A2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell("A2").Style.Font.FontSize = 11;

            var row = 4;
            var fields = new (string Label, string Value)[]
            {
                ("Ma hoa don:", $"HD-{hd.MaHD:D5}"),
                ("Hoc vien:", hd.TenHV),
                ("Goi tap:", hd.TenGoi),
                ("Ngay thanh toan:", hd.NgayThanhToan?.ToString("dd/MM/yyyy HH:mm") ?? "N/A"),
                ("Hinh thuc TT:", hd.HinhThucTT),
                ("Nguoi lap:", hd.TenNguoiLap),
                ("Nguoi thanh toan:", hd.TenNguoiThanhToan),
                ("Ghi chu:", hd.GhiChu)
            };

            foreach (var field in fields)
            {
                ws.Cell(row, 1).Value = field.Label;
                ws.Cell(row, 1).Style.Font.Bold = true;
                ws.Range(row, 2, row, 4).Merge();
                ws.Cell(row, 2).Value = field.Value;
                row++;
            }

            row++;
            ws.Cell(row, 1).Value = "TONG TIEN:";
            ws.Cell(row, 1).Style.Font.Bold = true;
            ws.Cell(row, 1).Style.Font.FontSize = 13;
            ws.Range(row, 2, row, 4).Merge();
            ws.Cell(row, 2).Value = hd.SoTien ?? 0;
            ws.Cell(row, 2).Style.NumberFormat.Format = "#,##0 \u0111";
            ws.Cell(row, 2).Style.Font.Bold = true;
            ws.Cell(row, 2).Style.Font.FontSize = 13;

            row += 2;
            ws.Cell(row, 3).Value = $"Ngay {DateTime.Now:dd} thang {DateTime.Now:MM} nam {DateTime.Now:yyyy}";
            ws.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            row++;
            ws.Cell(row, 1).Value = "Nguoi nop tien";
            ws.Cell(row, 1).Style.Font.Bold = true;
            ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(row, 3).Value = "Nguoi lap phieu";
            ws.Cell(row, 3).Style.Font.Bold = true;
            ws.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws.Column(1).Width = 20;
            ws.Column(2).Width = 18;
            ws.Column(3).Width = 18;
            ws.Column(4).Width = 18;
            ws.PageSetup.PrintAreas.Add("A1:D" + (row + 3));
            ws.PageSetup.PageOrientation = XLPageOrientation.Portrait;
            ws.PageSetup.FitToPages(1, 1);

            workbook.SaveAs(filePath);
        }

        public static void ExportSingleInvoicePdf(string filePath, HoaDon hd)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("\u0110\u01B0\u1EDDng d\u1EABn file kh\u00F4ng h\u1EE3p l\u1EC7.", nameof(filePath));

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Column(column =>
                    {
                        column.Spacing(4);
                        column.Item().Text("HOA DON THANH TOAN")
                            .SemiBold().FontSize(20).FontColor("#13233F");
                        column.Item().Text("AppGym - He thong quan ly phong tap")
                            .FontSize(11).FontColor("#6B7280");
                        column.Item().PaddingTop(8).LineHorizontal(1).LineColor("#D4D8E1");
                    });

                    page.Content().PaddingVertical(18).Column(column =>
                    {
                        column.Spacing(14);

                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Column(left =>
                            {
                                left.Spacing(6);
                                left.Item().Text(text =>
                                {
                                    text.Span("Ma hoa don: ").SemiBold();
                                    text.Span($"HD-{hd.MaHD:D5}");
                                });
                                left.Item().Text(text =>
                                {
                                    text.Span("Ngay thanh toan: ").SemiBold();
                                    text.Span(hd.NgayThanhToan?.ToString("dd/MM/yyyy HH:mm") ?? "N/A");
                                });
                            });

                            row.ConstantItem(180).AlignRight().Column(right =>
                            {
                                right.Item().Background("#F5E7D1").Padding(12).Column(box =>
                                {
                                    box.Spacing(4);
                                    box.Item().Text("Tong thanh toan").SemiBold().FontColor("#7C5A20");
                                    box.Item().Text(FormatCurrency(hd.SoTien ?? 0))
                                        .SemiBold().FontSize(18).FontColor("#13233F");
                                });
                            });
                        });

                        column.Item().Border(1).BorderColor("#D4D8E1").Padding(16).Column(details =>
                        {
                            details.Spacing(10);
                            details.Item().Text("Thong tin giao dich").SemiBold().FontSize(13).FontColor("#13233F");
                            details.Item().Element(c => ComposeDetailRow(c, "Hoc vien", hd.TenHV));
                            details.Item().Element(c => ComposeDetailRow(c, "Goi tap", hd.TenGoi));
                            details.Item().Element(c => ComposeDetailRow(c, "Hinh thuc thanh toan", hd.HinhThucTT));
                            details.Item().Element(c => ComposeDetailRow(c, "Nguoi lap hoa don", hd.TenNguoiLap));
                            details.Item().Element(c => ComposeDetailRow(c, "Nguoi thanh toan", hd.TenNguoiThanhToan));
                            details.Item().Element(c => ComposeDetailRow(c, "Ghi chu", string.IsNullOrWhiteSpace(hd.GhiChu) ? "Khong co" : hd.GhiChu));
                        });

                        column.Item().Border(1).BorderColor("#D4D8E1").Padding(16).Column(notes =>
                        {
                            notes.Spacing(8);
                            notes.Item().Text("Xac nhan").SemiBold().FontSize(13).FontColor("#13233F");
                            notes.Item().Text("Hoa don nay duoc xuat tu he thong AppGym va co gia tri doi chieu noi bo.")
                                .FontColor("#4B5563");
                        });
                    });

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Ngay in: ");
                        text.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                        text.Span("   |   Trang ");
                        text.CurrentPageNumber();
                    });
                });
            }).GeneratePdf(filePath);
        }

        private static void ComposeDetailRow(IContainer container, string label, string value)
        {
            container.Row(row =>
            {
                row.ConstantItem(140).Text(label).SemiBold().FontColor("#374151");
                row.RelativeItem().Text(value);
            });
        }

        private static string FormatCurrency(decimal amount) => string.Format("{0:#,##0} VND", amount);
    }
}
