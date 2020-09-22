using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
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
    }
}
