using ClosedXML.Excel;

namespace IMDbTop250Scraper.Services
{
    public class ExcelExportService
    {
        public void ExportMovies(
            List<Movie> movies,
            string fileName)
        {
            using var workbook =
                new XLWorkbook();

            var worksheet =
                workbook.Worksheets.Add(
                    "IMDb Top 250");

            // Headers
            worksheet.Cell(1, 1).Value = "Position";
            worksheet.Cell(1, 2).Value = "Title";
            worksheet.Cell(1, 3).Value = "Rating";
            worksheet.Cell(1, 4).Value = "Votes";
            worksheet.Cell(1, 5).Value = "Year";
            worksheet.Cell(1, 6).Value = "Poster";
            worksheet.Cell(1, 7).Value = "Link";

            // Header style
            var headerRange =
                worksheet.Range(1, 1, 1, 7);

            headerRange.Style.Font.Bold = true;

            int row = 2;

            foreach(var movie in movies)
            {
                worksheet.Cell(row, 1).Value =
                    movie.Position;

                worksheet.Cell(row, 2).Value =
                    movie.Title;

                worksheet.Cell(row, 3).Value =
                    movie.Rating;

                worksheet.Cell(row, 4).Value =
                    movie.VoteCount;

                worksheet.Cell(row, 5).Value =
                    movie.Year;

                worksheet.Cell(row, 6).Value =
                    movie.Poster;

                worksheet.Cell(row, 7).Value =
                    movie.Link;


                worksheet.Cell(row, 7).Value = "Open IMDb";

                worksheet.Cell(row, 7).SetHyperlink(
                    new XLHyperlink(movie.Link));

                row++;
            }

            worksheet.SheetView.FreezeRows(1);
            headerRange.SetAutoFilter();
            worksheet.RangeUsed().Style.Border.OutsideBorder =
    XLBorderStyleValues.Thin;
            

            worksheet.Columns()
                .AdjustToContents();

            workbook.SaveAs(fileName);
        }
    }
}