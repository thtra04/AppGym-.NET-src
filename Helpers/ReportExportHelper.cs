using AppGym.Models;
using ClosedXML.Excel;

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
            {
                throw new ArgumentException("Đường dẫn file không hợp lệ.", nameof(filePath));
            }

            var invoices = invoiceItems?.ToList() ?? new List<ThanhToanReportItem>();
            var unpaid = unpaidItems?.ToList() ?? new List<DangKyUnpaidItem>();

            if (!includeInvoiceSheet && !includeUnpaidSheet)
            {
                throw new ArgumentException("Phải chọn ít nhất một sheet để xuất.");
            }

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
                "HinhThucTT", "GhiChu"
            };

            for (var i = 0; i < headers.Length; i++)
            {
                ws.Cell(1, i + 1).Value = headers[i];
            }

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
                ws.Cell(row, 12).Value = item.GhiChu;

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
            {
                ws.Cell(1, i + 1).Value = headers[i];
            }

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

        /// <summary>
        /// Xuất hóa đơn riêng cho 1 người (1 dòng HoaDon) ra file Excel có định dạng phiếu thu.
        /// </summary>
        public static void ExportSingleInvoice(string filePath, HoaDon hd)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Đường dẫn file không hợp lệ.", nameof(filePath));

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("HoaDon");

            // Title
            ws.Range("A1:D1").Merge();
            ws.Cell("A1").Value = "HÓA ĐƠN THANH TOÁN";
            ws.Cell("A1").Style.Font.Bold = true;
            ws.Cell("A1").Style.Font.FontSize = 16;
            ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws.Range("A2:D2").Merge();
            ws.Cell("A2").Value = "Phòng tập Gym";
            ws.Cell("A2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell("A2").Style.Font.FontSize = 11;

            // Invoice info
            int row = 4;
            var fields = new (string label, string value)[]
            {
                ("Mã hóa đơn:", $"HD-{hd.MaHD:D5}"),
                ("Học viên:", hd.TenHV),
                ("Gói tập:", hd.TenGoi),
                ("Ngày thanh toán:", hd.NgayThanhToan?.ToString("dd/MM/yyyy HH:mm") ?? "N/A"),
                ("Hình thức TT:", hd.HinhThucTT),
                ("Ghi chú:", hd.GhiChu),
            };

            foreach (var f in fields)
            {
                ws.Cell(row, 1).Value = f.label;
                ws.Cell(row, 1).Style.Font.Bold = true;
                ws.Range(row, 2, row, 4).Merge();
                ws.Cell(row, 2).Value = f.value;
                row++;
            }

            // Amount
            row++;
            ws.Cell(row, 1).Value = "TỔNG TIỀN:";
            ws.Cell(row, 1).Style.Font.Bold = true;
            ws.Cell(row, 1).Style.Font.FontSize = 13;
            ws.Range(row, 2, row, 4).Merge();
            ws.Cell(row, 2).Value = hd.SoTien ?? 0;
            ws.Cell(row, 2).Style.NumberFormat.Format = "#,##0 đ";
            ws.Cell(row, 2).Style.Font.Bold = true;
            ws.Cell(row, 2).Style.Font.FontSize = 13;

            // Footer
            row += 2;
            ws.Cell(row, 3).Value = $"Ngày {DateTime.Now:dd} tháng {DateTime.Now:MM} năm {DateTime.Now:yyyy}";
            ws.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            row++;
            ws.Cell(row, 1).Value = "Người nộp tiền";
            ws.Cell(row, 1).Style.Font.Bold = true;
            ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(row, 3).Value = "Người lập phiếu";
            ws.Cell(row, 3).Style.Font.Bold = true;
            ws.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws.Column(1).Width = 20;
            ws.Column(2).Width = 18;
            ws.Column(3).Width = 18;
            ws.Column(4).Width = 18;

            // Print area
            ws.PageSetup.PrintAreas.Add("A1:D" + (row + 3));
            ws.PageSetup.PageOrientation = XLPageOrientation.Portrait;
            ws.PageSetup.FitToPages(1, 1);

            workbook.SaveAs(filePath);
        }
    }
}
