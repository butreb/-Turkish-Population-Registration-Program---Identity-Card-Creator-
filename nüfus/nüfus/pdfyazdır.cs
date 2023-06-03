using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
namespace nüfus
{
    internal class pdfyazdır
    {
        public void pdfyaz()
        {
            String outputFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "kimlik.pdf");
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A3, 15, 15, 0, 0);
            PdfWriter.GetInstance(document, new FileStream(outputFile, System.IO.FileMode.Create));
            if (document.IsOpen() == false)
            {
                document.Open();
                String resimyazdır = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "kimlikresim.jpg");
                iTextSharp.text.Image resim = iTextSharp.text.Image.GetInstance(resimyazdır);
                document.Add(resim);
                document.Close();

            }
        }
        public void pdfsil() {
            String outputFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "kimlik.pdf");
            File.Delete(outputFile);
        
        }
    }
}
