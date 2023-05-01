using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

Console.WriteLine("Enter the first pdf file");
var file1 = Console.ReadLine();
Console.WriteLine("Enter the second pdf file");
var file2 = Console.ReadLine();

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

using (PdfDocument one = PdfReader.Open(file1, PdfDocumentOpenMode.Import))
using (PdfDocument two = PdfReader.Open(file2, PdfDocumentOpenMode.Import))
using (PdfDocument outPdf = new PdfDocument())
{
    CopyPages(one, outPdf);
    CopyPages(two, outPdf);

    outPdf.Save(new FileInfo(file1).DirectoryName + "\\" + FilenameOnly(file1) + "_and_" + FilenameOnly(file2));
}

string FilenameOnly(string fileNameWithExtension)
{
    FileInfo b = new FileInfo(fileNameWithExtension);
    return Path.GetFileNameWithoutExtension(b.FullName);
}

void CopyPages(PdfDocument from, PdfDocument to)
{
    for (int i = 0; i < from.PageCount; i++)
    {
        to.AddPage(from.Pages[i]);
    }
}