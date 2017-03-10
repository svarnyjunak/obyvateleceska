using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SvarnyJunak.CeskeObce.DataParser.Utils
{
    public class ExcelReader
    {
        public IEnumerable<DataRow> ReadData(string file, string sheetName)
        {
            var fileInfo = new FileInfo(file);

            if (!fileInfo.Exists)
            {
                throw new ArgumentException($"File {file} doesn't exist.");
            }

            using (var excelPackage = new ExcelPackage(fileInfo))
            {
                var worksheet = excelPackage.Workbook.Worksheets[sheetName];
                if (worksheet == null)
                    throw new ArgumentException($"Excel file doesn't contain worksheet {sheetName}.");

                var firstRowIndex = worksheet.Dimension.Start.Row;
                var lastRowIndex = worksheet.Dimension.End.Row;
                var firstColumnIndex = worksheet.Dimension.Start.Column;
                var lastColumnIndex = worksheet.Dimension.End.Column;

                for (var row = firstRowIndex; row <= lastRowIndex; row++)
                {
                    var columns = new List<object>();
                    for (var column = firstColumnIndex; column <= lastColumnIndex; column++)
                    {
                        columns.Add(worksheet.Cells[row, column].Value);
                    }

                    yield return new DataRow
                    {
                        Columns = columns.ToArray(),
                    };
                }
            }
        }
    }

    public class DataRow
    {
        public object[] Columns { get; set; }
    }
}
