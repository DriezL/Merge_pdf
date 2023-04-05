using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using System.Collections.Generic;
using System.IO;

namespace MergePDF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var manual = File.ReadAllBytes(@"C:\temp\pdf_one.pdf");
            var receipt = File.ReadAllBytes(@"C:\temp\pdf_two.pdf");
            var pdfList = new List<byte[]> { manual, receipt };
            var result = Combine(pdfList);
            PdfWriter pdfWriter = new PdfWriter(@"c:\temp\together.pdf");
            pdfWriter.Write(result);
        }


        public static byte[] Combine(IEnumerable<byte[]> pdfs)
        {
            using (var writerMemoryStream = new MemoryStream())
            {
                using (var writer = new PdfWriter(writerMemoryStream))
                {
                    using (var mergedDocument = new PdfDocument(writer))
                    {
                        var merger = new PdfMerger(mergedDocument);

                        foreach (var pdfBytes in pdfs)
                        {
                            using (var copyFromMemoryStream = new MemoryStream(pdfBytes))
                            {
                                using (var reader = new PdfReader(copyFromMemoryStream))
                                {
                                    using (var copyFromDocument = new PdfDocument(reader))
                                    {
                                        merger.Merge(copyFromDocument, 1, copyFromDocument.GetNumberOfPages());
                                    }
                                }
                            }
                        }
                    }
                }

                return writerMemoryStream.ToArray();
            }
        }
    }
}
