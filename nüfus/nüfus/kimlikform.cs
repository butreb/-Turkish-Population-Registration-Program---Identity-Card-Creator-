using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;

namespace nüfus
{
    public partial class kimlikform : Form
    {
        public kimlikform()
        {
            InitializeComponent();
        }

               
        private void kimlikform_Load(object sender, EventArgs e)
        {

    }
        public void pdfresimsil() {
            try
            {
                String outputFiletcrsm = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "kimlikresim.jpg");
                File.Delete(outputFiletcrsm);
                pdf.pdfsil();
                Application.Restart();
            }
            catch (Exception)
            {

                Application.Restart();
            }
                 
        }

        pdfyazdır pdf = new pdfyazdır();

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            using (var bmp = new Bitmap(panel1.Width, panel1.Height)) {
                panel1.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                String saversm = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "kimlikresim.jpg");
                bmp.Save(saversm);
                
                pdf.pdfyaz();

            }
        }

        private void kimlikform_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult Cikis;
            Cikis = MessageBox.Show("Bu işlem sonrası oluşturulan kimlik dosyaları silinecektir. Kapatmak istediğinize emin misiniz?", "Kapatma Uyarısı!", MessageBoxButtons.YesNo);
            if (Cikis == DialogResult.Yes)
            {   pdfresimsil();
                
            }
            if (Cikis == DialogResult.No)
            {
               Form2 frm2 = new Form2();
                frm2.Show();
                pdf.pdfsil();
                String outputFiletcrsm = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "kimlikresim.jpg");
                File.Delete(outputFiletcrsm);
            }
        }
    }
}
