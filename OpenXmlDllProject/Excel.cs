using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;

namespace OpenXmlDllProject
{
    public class Excel
    {
        public Excel(string sheetName,string path,List<string[]> v)
        {
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
                Cell header1 = new Cell() { CellReference = "A1", CellValue = new CellValue("Interval Period Timestamp"), DataType = CellValues.String };
                row.Append(header1);
                Cell header2 = new Cell() { CellReference = "B1", CellValue = new CellValue("Settlement Interval"), DataType = CellValues.String };
                row.Append(header2);
                Cell header3 = new Cell() { CellReference = "C1", CellValue = new CellValue("Aggregated Consumption Factor"), DataType = CellValues.String };
                row.Append(header3);
                Cell header4 = new Cell() { CellReference = "D1", CellValue = new CellValue("Loss Adjusted Aggregated Consumption"), DataType = CellValues.String };
                row.Append(header4);

                sheetData.Append(row);

                sheets.Append(sheet);

                workbookpart.Workbook.Save();

                // Close the document.
                spreadsheetDocument.Close();
            }
        }


    }

}
