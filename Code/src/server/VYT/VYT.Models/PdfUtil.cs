using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VYT.Models
{
    public static class PdfUtil
    {
        public static int GetTotalPages(string pdfFile)
        {
            var pdfReader = new PdfReader(pdfFile);
            try
            {
                return pdfReader.NumberOfPages;
            }
            finally
            {
                pdfReader.Close();
            }
        }

        public static string GetTextFromAllPages(string pdfPath)
        {
            PdfReader reader = new PdfReader(pdfPath);

            StringWriter output = new StringWriter();

            for (int i = 1; i <= reader.NumberOfPages; i++)
                output.WriteLine(PdfTextExtractor.GetTextFromPage(reader, i, new SimpleTextExtractionStrategy()));

            return output.ToString();
        }
    }
}
