using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.IO;
using A = DocumentFormat.OpenXml.Drawing;
using Color = DocumentFormat.OpenXml.Wordprocessing.Color;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace OpenXmlDllProject
{
    public class WordParameter
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public FileInfo Image { get; set; }
    }
    public class Word
    {
        public Word() { }

        public void InsertPicture(WordprocessingDocument wordprocessingDocument, string fileName)
        {
            MainDocumentPart mainPart = wordprocessingDocument.MainDocumentPart;
            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                imagePart.FeedData(stream);
            }
            AddImageToBody(wordprocessingDocument, mainPart.GetIdOfPart(imagePart));
        }

        public WordprocessingDocument DOC { get; set; }

        public WordprocessingDocument CreateWordFile(string title, string path)
        {
            WordprocessingDocument doc = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document);

            using (WordprocessingDocument docTemplate = WordprocessingDocument.Open(@"./Templates/t.docx", true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(docTemplate.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("mainTitle", title);

                using (StreamWriter sw = new StreamWriter(doc.AddMainDocumentPart().GetStream(FileMode.Create)))
                {
                    doc.MainDocumentPart.Document = new Document();
                    doc.MainDocumentPart.Document.AppendChild<Body>(new Body());

                    sw.Write(docText);
                }

            }
            return doc;

        }

        public Table CreateTable(string[][] content)
        {
            string xml;
            int n = content[0].Length;
            Table t = new Table();
            t.AppendChild(GetTableProperties("#000000", BorderValues.Thick, "5000"));
            for (int i = 0; i < content.Length; i++)
            {
                TableRow r = new TableRow();
                for (int j = 0; j < content[i].Length; j++)
                {
                    xml = File.ReadAllText(@".\Templates\Cell.txt").Replace("{{cellText}}", content[i][j]).Replace("{{cellWidth}}", (10000 / n).ToString());
                    r.InnerXml += xml;
                }
                t.AppendChild(r);
            }
            return t;
        }

        public Paragraph CreateParagraph(string text)
        {
            Paragraph p = new Paragraph(new Justification() { Val = JustificationValues.Left });
            p.InnerXml += File.ReadAllText(@".\Templates\Paragraph.txt").Replace("{{content}}", text);
            return p;
        }

        private void AddImageToBody(WordprocessingDocument wordDoc, string relationshipId)
        {
            // Define the reference of the image.
            var element =
                 new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = 990000L, Cy = 792000L },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 0L,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = 1U,
                             Name = "Picture 1"
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties()
                                         {
                                             Id = 0U,
                                             Name = "New Bitmap Image.jpg"
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension()
                                                 {
                                                     Uri =
                                                        "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 })
                                         )
                                         {
                                             Embed = relationshipId,
                                             CompressionState =
                                             A.BlipCompressionValues.Print
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = 990000L, Cy = 792000L }),
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         )
                                         { Preset = A.ShapeTypeValues.Rectangle }))
                             )
                             { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     )
                     {
                         DistanceFromTop = 0U,
                         DistanceFromBottom = 0U,
                         DistanceFromLeft = 0U,
                         DistanceFromRight = 0U,
                         EditId = "50D07946"
                     });

            // Append the reference to body, the element should be in a Run.
            wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new Run(element)));
        }

        public void AddStyle(MainDocumentPart mainPart, string styleId, string styleName, string fontName, int fontSize, string rgbColor, bool isBold, bool isItalic, bool isUnderline)
        {
            // we have to set the properties
            RunProperties rPr = new RunProperties();
            Color color = new Color() { Val = rgbColor };
            RunFonts rFont = new RunFonts
            {
                Ascii = fontName
            };
            rPr.Append(color);
            rPr.Append(rFont);
            if (isBold) rPr.Append(new Bold());
            if (isItalic) rPr.Append(new Italic());
            if (isUnderline) rPr.Append(new Underline() { Val = UnderlineValues.Single });
            rPr.Append(new FontSize() { Val = (fontSize * 2).ToString() });

            Style style = new Style
            {
                StyleId = styleId
            };
            if (styleName == null || styleName.Length == 0) styleName = styleId;
            style.Append(new Name() { Val = styleName });
            style.Append(rPr); //we are adding properties previously defined

            // we have to add style that we have created to the StylePart
            StyleDefinitionsPart stylePart;
            if (mainPart.StyleDefinitionsPart == null)
            {
                stylePart = mainPart.AddNewPart<StyleDefinitionsPart>();
                stylePart.Styles = new Styles();
            }
            else stylePart = mainPart.StyleDefinitionsPart;

            stylePart.Styles.Append(style);
            stylePart.Styles.Save(); // we save the style part
        }

        public Paragraph CreateParagraphWithStyle(string styleId, JustificationValues justification)
        {
            Paragraph paragraph = new Paragraph();

            ParagraphProperties pp = new ParagraphProperties
            {
                // we set the style
                ParagraphStyleId = new ParagraphStyleId() { Val = styleId },
                // we set the alignement
                Justification = new Justification() { Val = justification }
            };
            paragraph.Append(pp);
            return paragraph;
        }

        public void AddTextToParagraph(Paragraph paragraph, string content)
        {
            Run r = new Run();
            Text t = new Text(content);
            r.Append(t);
            paragraph.Append(r);
        }

        public List<Paragraph> CreateBulletList(string[] v)
        {
            List<Paragraph> retVal = new List<Paragraph>();
            SpacingBetweenLines sbl = new SpacingBetweenLines() { After = "0" };
            Indentation indent = new Indentation() { Left = "100", Hanging = "200" };
            NumberingProperties np = new NumberingProperties(
                new NumberingLevelReference() { Val = 0 },
                new NumberingId() { Val = 1 }
            );
            ParagraphProperties ppUnordered = new ParagraphProperties(np, sbl, indent);
            ppUnordered.ParagraphStyleId = new ParagraphStyleId() { Val = "ListParagraph" };
            for (int i = 0; i < v.Length; i++)
            {
                // Pargraph
                Paragraph p1 = new Paragraph();
                p1.ParagraphProperties = new ParagraphProperties(ppUnordered.OuterXml);
                p1.Append(new Run(new Text(v[i])));
                retVal.Add(p1);
            }
            return retVal;
        }

        public void InsertParagraphInList(List<Paragraph> paragraphs, ParagraphProperties ppUnordered, string text)
        {
            Paragraph p = new Paragraph
            {
                ParagraphProperties = new ParagraphProperties(ppUnordered.OuterXml)
            };
            p.Append(new Run(new Text(text)));
            paragraphs.Add(p);
        }

        public Table CreateTable(string[][] contenuto, TableProperties tableProperties)
        {
            Table table = new Table();
            table.AppendChild(tableProperties);
            for (int i = 0; i < contenuto.Length; i++)
                table.Append(CreateRow(contenuto[i]));
            return table;
        }

        public TableProperties GetTableProperties(
            string borderColor = "#0000000",
            BorderValues b = BorderValues.None,
            string tableWidth = "2500"
            )
        {
            TableProperties tblProperties = new TableProperties();
            TableBorders tblBorders = new TableBorders();

            TableCellVerticalAlignment tcVA = new TableCellVerticalAlignment
            {
                Val = TableVerticalAlignmentValues.Center
            };
            tblBorders.AppendChild(tcVA);


            TopBorder topBorder = new TopBorder
            {
                Val = new EnumValue<BorderValues>(b),
                Color = borderColor
            };
            tblBorders.AppendChild(topBorder);

            BottomBorder bottomBorder = new BottomBorder
            {
                Val = new EnumValue<BorderValues>(b),
                Color = borderColor
            };
            tblBorders.AppendChild(bottomBorder);

            RightBorder rightBorder = new RightBorder
            {
                Val = new EnumValue<BorderValues>(b),
                Color = borderColor
            };
            tblBorders.AppendChild(rightBorder);

            LeftBorder leftBorder = new LeftBorder
            {
                Val = new EnumValue<BorderValues>(b),
                Color = borderColor
            };
            tblBorders.AppendChild(leftBorder);

            InsideHorizontalBorder insideHBorder = new InsideHorizontalBorder
            {
                Val = new EnumValue<BorderValues>(b),
                Color = borderColor
            };
            tblBorders.AppendChild(insideHBorder);

            InsideVerticalBorder insideVBorder = new InsideVerticalBorder
            {
                Val = new EnumValue<BorderValues>(b),
                Color = borderColor
            };
            tblBorders.AppendChild(insideVBorder);

            tblProperties.AppendChild(tblBorders);
            TableWidth width = new TableWidth
            {
                Width = tableWidth,
                Type = TableWidthUnitValues.Pct
            };

            tblProperties.AppendChild(width);
            return tblProperties;
        }

        private TableRow CreateRow(string[] s, string fontSize = "22", bool isBold = false)
        {
            TableRow row = new TableRow();
            row.Append(new TableRowHeight() { Val = 20 });

            for (int i = 0; i < s.Length; i++)
            {

                TableCell cell = new TableCell(new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center });
                Paragraph p = new Paragraph();
                Run run = new Run();
                RunProperties runProperties = new RunProperties
                {
                    Bold = isBold ? new Bold() : null,
                    FontSize = new FontSize()
                };
                runProperties.FontSize.Val = fontSize;
                p.Append(new TextAlignment() { Val = VerticalTextAlignmentValues.Center });

                run.Append(runProperties);
                run.Append(new Text(s[i]));
                p.Append(run);
                cell.Append(p);
                row.Append(cell);
            }
            return row;
        }

        public void AddHeadingStyle(MainDocumentPart mainPart)
        {
            // we have to set the properties
            RunProperties rPr = new RunProperties();
            Color color = new Color() { Val = "000066" }; // the color is red
            RunFonts rFont = new RunFonts
            {
                Ascii = "Segoe UI" // the font is Arial
            };
            rPr.Append(color);
            rPr.Append(rFont);
            //rPr.Append(new Bold()); // it is Bold
            rPr.Append(new FontSize() { Val = "50" }); //font size (in 1/72 of an inch)

            Style style = new Style
            {
                StyleId = "MyHeading1" //this is the ID of the style
            };
            style.Append(new Name() { Val = "My Heading 1" }); //this is the name of the new style
            style.Append(rPr); //we are adding properties previously defined

            // we have to add style that we have created to the StylePart
            StyleDefinitionsPart stylePart = mainPart.AddNewPart<StyleDefinitionsPart>();
            stylePart.Styles = new Styles();
            stylePart.Styles.Append(style);
            stylePart.Styles.Save(); // we save the style part
        }

        public Paragraph CreateHeading(string content)
        {
            Paragraph heading = new Paragraph();
            Run r = new Run();
            Text t = new Text(content);
            ParagraphProperties pp = new ParagraphProperties
            {
                // we set the style
                ParagraphStyleId = new ParagraphStyleId() { Val = "MyHeading1" },
                // we set the alignement
                Justification = new Justification() { Val = JustificationValues.Center }
            };
            heading.Append(pp);
            r.Append(t);
            heading.Append(r);
            return heading;
        }
    }

}