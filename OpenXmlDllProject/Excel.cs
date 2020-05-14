using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;

namespace OpenXmlDllProject
{
    public class Excel
    {
        public Excel(string sheetName,string path,List<string[]> body,string[] header)
        {
            if (body[0].Length != header.Length)
                throw new System.Exception("Le colonne sono diverse!");
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook))
            {
                // Add a WorkbookPart to the document.
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                // Add Sheets to the Workbook.
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                // Append a new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = sheetName
                };

                Row row = new Row() { RowIndex = 1 };
                for (int i = 0; i < header.Length; i++)
                {
                    Cell c = new Cell() { CellValue = new CellValue(header[i]), DataType = CellValues.String };
                    row.Append(c);
                }
                sheetData.Append(row);

                foreach (var v in body)
                {
                    Row r = new Row();
                    for (int i = 0; i < v.Length; i++)
                    {
                        Cell c = new Cell() {CellValue = new CellValue(v[i]), DataType = CellValues.String };
                        r.Append(c);
                    }
                    sheetData.Append(r);
                }

                sheets.Append(sheet);

                workbookpart.Workbook.Save();

                // Close the document.
                spreadsheetDocument.Close();
            }
        }


    }

}
