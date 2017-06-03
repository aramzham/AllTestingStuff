using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Template
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Users\HP\Desktop\MyExcelFile.xlsx";
            ReadExcelFileDOM(path);
            foreach (var bloggerse in ReadExcelFileDOM(path))
            {
                Console.WriteLine($"{bloggerse.FullName}\t{bloggerse.CompanyName}\t{bloggerse.Country}\t{bloggerse.Email}");
            }
            Console.ReadKey(true);
        }

        static List<ContactRequestModel> ReadExcelFileDOM(string filename)
        {
            var strProperties = new string[5];
            var contactRequestModels = new List<ContactRequestModel>();
            ContactRequestModel model = null;
            var j = 0;
            using (var myDoc = SpreadsheetDocument.Open(filename, true))
            {
                var workbookPart = myDoc.WorkbookPart;
                var Sheets = myDoc.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                var relationshipId = Sheets?.First().Id.Value;
                var worksheetPart = (WorksheetPart)myDoc.WorkbookPart.GetPartById(relationshipId);
                var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                var i = 1;
                string value;
                foreach (var r in sheetData.Elements<Row>())
                {
                    if (i != 1)
                    {
                        foreach (var c in r.Elements<Cell>())
                        {
                            if (c == null) continue;

                            value = c.InnerText;
                            if (c.DataType != null)
                            {
                                var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                                if (stringTable != null)
                                {
                                    value = stringTable.SharedStringTable.
                                      ElementAt(int.Parse(value)).InnerText;
                                }
                            }
                            strProperties[j] = value;
                            j = j + 1;
                        }
                    }
                    j = 0;
                    i = i + 1;
                    model = new ContactRequestModel { FullName = strProperties[0], CompanyName = strProperties[1], Position = strProperties[2], Country = strProperties[3], Email = strProperties[4] };
                    contactRequestModels.Add(model);
                }
                return contactRequestModels;
            }
        }

        static void Get(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var doc = SpreadsheetDocument.Open(fs, false))
                {
                    var workbookPart = doc.WorkbookPart;
                    var sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                    var sst = sstpart.SharedStringTable;

                    var worksheetPart = workbookPart.WorksheetParts.First();
                    var sheet = worksheetPart.Worksheet;

                    var cells = sheet.Descendants<Cell>();
                    var rows = sheet.Descendants<Row>();

                    Console.WriteLine("Row count = {0}", rows.LongCount());
                    Console.WriteLine("Cell count = {0}", cells.LongCount());

                    //// One way: go through each cell in the sheet
                    //foreach (Cell cell in cells)
                    //{
                    //    if ((cell.DataType != null) && (cell.DataType == CellValues.SharedString))
                    //    {
                    //        int ssid = int.Parse(cell.CellValue.Text);
                    //        string str = sst.ChildElements[ssid].InnerText;
                    //        Console.WriteLine("Shared string {0}: {1}", ssid, str);
                    //    }
                    //    else if (cell.CellValue != null)
                    //    {
                    //        Console.WriteLine("Cell contents: {0}", cell.CellValue.Text);
                    //    }
                    //}

                    // Or... via each row
                    foreach (var row in rows)
                    {
                        foreach (var c in row.Elements<Cell>())
                        {
                            if ((c.DataType != null) && (c.DataType == CellValues.SharedString))
                            {
                                var ssid = int.Parse(c.CellValue.Text);
                                var str = sst.ChildElements[ssid].InnerText;
                                Console.WriteLine("Shared string {0}: {1}", ssid, str);
                            }
                            else if (c.CellValue != null)
                            {
                                Console.WriteLine("Cell contents: {0}", c.CellValue.Text);
                            }
                        }
                    }
                }
            }
        }
    }

    internal class ContactRequestModel
    {
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
    }
}
