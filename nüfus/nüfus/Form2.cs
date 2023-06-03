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
    public partial class Form2 : Form
    {
        
        public Form2() 
        {
            InitializeComponent();
        }

        private void harfal(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar)==true && e.KeyChar !=8)
            {
                e.Handled = true;
                
                
            }
        }

        private void cbulke_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=BERTUB\SQLEXPRESS;Initial Catalog=il ilce mah;Integrated Security=True");
            baglan.Open();
            SqlCommand komut = new SqlCommand("select SehirAdi from Sehirler order by SehirAdi asc",baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cbil.Items.Add(oku["SehirAdi"]);
            }
            baglan.Close();
        }

        private void cbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbilce.Items.Clear();
            cbilce.Text = "";
            SqlConnection baglan = new SqlConnection(@"Data Source=BERTUB\SQLEXPRESS;Initial Catalog=il ilce mah;Integrated Security=True");
            baglan.Open();
            SqlCommand komut = new SqlCommand("select IlceAdi from Ilceler where SehirAdi = @degisken order by IlceAdi asc", baglan);
            komut.Parameters.AddWithValue("@degisken",cbil.SelectedItem);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cbilce.Items.Add(oku["IlceAdi"]);
            }
            baglan.Close();
        }

        private void cbilce_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbmah.Items.Clear();
            cbmah.Text = "";
            SqlConnection baglan = new SqlConnection(@"Data Source=BERTUB\SQLEXPRESS;Initial Catalog=il ilce mah;Integrated Security=True");
            baglan.Open();
            SqlCommand komut = new SqlCommand("select MahalleAdi from SemtMah where SemtAdi = @degisken order by MahalleAdi asc", baglan);
            komut.Parameters.AddWithValue("@degisken", cbilce.SelectedItem);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cbmah.Items.Add(oku["MahalleAdi"]);
            }
            baglan.Close();
        }
        bool durum;
        bool durumseri;
        void tekrar()
        {
            
            SqlConnection baglan = new SqlConnection(@"Data Source=BERTUB\SQLEXPRESS;Initial Catalog=il ilce mah;Integrated Security=True");
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select tckimlik from KayıtlıNüfus where tckimlik ='" + tcno.Text+"' ", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                durum = true;
            }
            else
            {
                durum = false;
               
            }
          
            baglan.Close();
        }
        void tekrarSeri() {

            SqlConnection baglan = new SqlConnection(@"Data Source=BERTUB\SQLEXPRESS;Initial Catalog=il ilce mah;Integrated Security=True");
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select SeriNo from KayıtlıNüfus where SeriNo ='" + txtserino.Text + "' ", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                durumseri = true;
            }
            else
            {
                durumseri = false;

            }

        }
        private void tcolusturbtn_Click(object sender, EventArgs e)
        {
                
           tcolusturbtn.Enabled = false;
           Random rst = new Random();
           int tek = 0, cift = 0 , toplam = 0;
           int[] sayi = new int[11];
           sayi[0] = rst.Next(1, 10);
           for (int i = 1; i < 9; i++)
           {
               sayi[i] = rst.Next(0,10);
           }

           for (int i = 0; i < 9; i+=2)
           {
               tek = tek + sayi[i];
           }

           for (int i = 1; i < 9; i+=2)
           {
               cift = cift + sayi[i];
           }

           sayi[9] = (((tek * 7) - cift) % 10);

           for (int i = 0; i < 10; i++)
           {
               toplam = toplam + sayi[i];
           }
           sayi[10] = (toplam % 10);

           foreach (int a in sayi)
           {
               tcno.Text += a.ToString();
           }                       
            tekrar();
            if (durum == false)
            {

            }
            else
            {
                tcno.Text = "";
                tcolusturbtn.Enabled = false;                               
                sayi[0] = rst.Next(1, 10);
                for (int i = 1; i < 9; i++)
                {
                    sayi[i] = rst.Next(0, 10);
                }

                for (int i = 0; i < 9; i += 2)
                {
                    tek = tek + sayi[i];
                }

                for (int i = 1; i < 9; i += 2)
                {
                    cift = cift + sayi[i];
                }

                sayi[9] = (((tek * 7) - cift) % 10);

                for (int i = 0; i < 10; i++)
                {
                    toplam = toplam + sayi[i];
                }
                sayi[10] = (toplam % 10);

                foreach (int a in sayi)
                {
                    tcno.Text += a.ToString();
                }
            }
        }
      
        private void Form2_Load(object sender, EventArgs e)
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=BERTUB\SQLEXPRESS;Initial Catalog=il ilce mah;Integrated Security=True");
            SqlCommand komut = new SqlCommand("select * from defresim where id = 1 " , baglan);
            
            
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                if (oku["defaultkimlikrsm"].ToString().Length > 10)
                {
                    Byte[] datadefkimlik = (Byte[])(oku["defaultkimlikrsm"]);
                    MemoryStream kimlikdefhafıza = new MemoryStream(datadefkimlik);
                    tckimlikresim.Image = Image.FromStream(kimlikdefhafıza);

                }
                if (oku["defaultimzarsm"].ToString().Length > 10)
                {
                    Byte[] datadefimza = (Byte[])(oku["defaultimzarsm"]);
                    MemoryStream imzadefhafıza = new MemoryStream(datadefimza);
                    imzaresim.Image = Image.FromStream(imzadefhafıza);
                }
            }

           baglan.Close();
            groupBox2.Enabled = false;
            resimeklebtn.Enabled = false;
            imzaEklebtn.Enabled = false; 
        }

        private void buttondegis_Click(object sender, EventArgs e)
        {
            

            if (groupBox1.Enabled ==true)
            {
                
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
                resimeklebtn.Enabled = true;
                imzaEklebtn.Enabled = true;
                txtad.Text = "";
                txtsoyad.Text = "";
                txtbabaAd.Text = "";
                txtserino.Text = "";
                txtanneAd.Text = "";
                tcno.Text = "";
                cbcinsiyet.Text = "";
                cbulke.Text = "";
                cbil.Text = "";
                cbilce.Text = "";
                cbmah.Text = "";
                dtdogum.Value = DateTime.Today;
                tckimlikresim.Image = null;
                imzaresim.Image = null;
                cbil.Items.Clear();
                cbilce.Items.Clear();
                cbmah.Items.Clear();
            }
            else if (groupBox2.Enabled == true)
            {
                SqlConnection baglan = new SqlConnection(@"Data Source=BERTUB\SQLEXPRESS;Initial Catalog=il ilce mah;Integrated Security=True");
                SqlCommand komut = new SqlCommand("select * from defresim where id = 1 ", baglan);


                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    if (oku["defaultkimlikrsm"].ToString().Length > 10)
                    {
                        Byte[] datadefkimlik = (Byte[])(oku["defaultkimlikrsm"]);
                        MemoryStream kimlikdefhafıza = new MemoryStream(datadefkimlik);
                        tckimlikresim.Image = Image.FromStream(kimlikdefhafıza);

                    }
                    if (oku["defaultimzarsm"].ToString().Length > 10)
                    {
                        Byte[] datadefimza = (Byte[])(oku["defaultimzarsm"]);
                        MemoryStream imzadefhafıza = new MemoryStream(datadefimza);
                        imzaresim.Image = Image.FromStream(imzadefhafıza);
                    }
                }
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
                resimeklebtn.Enabled = false;
                imzaEklebtn.Enabled= false;
                tcgno.Text = "";
                txtgad.Text = "";
                txtgsoyad.Text = "";
                cbgcinsiyet.Text = "";
                txtganneAd.Text = "";
                txtgbabaAd.Text = "";
                txtgserino.Text = "";
                cbgulke.Text = "";
                cbgsehir.Text = "";
                cbgilce.Text = "";
                cbgmah.Text = "";
                dtdogum.Value = DateTime.Today;
                
            }
        }
        insan kisi = new insan();
        insan kisig = new insan();
        insan insangetirguncel()
        {            
            kisig.ad = txtgad.Text;
            kisig.soyad = txtgsoyad.Text;
            kisig.tc = tcgno.Text;
            kisig.cinsiyet = cbgcinsiyet.Text;
            kisig.annead = txtganneAd.Text;
            kisig.babaad = txtgbabaAd.Text;
            kisig.serino = txtgserino.Text;
            kisig.ülke = cbgulke.Text;
            kisig.sehir = cbgsehir.Text;
            kisig.ilce = cbgilce.Text;
            kisig.mahalle = cbgmah.Text;
            kisig.dogum = dtgdogum.Value;
            

            return kisig;


        }
        insan insangetir() {

            kisi.ad = txtad.Text;
            kisi.soyad = txtsoyad.Text;
            kisi.tc = tcno.Text;
            kisi.cinsiyet = cbcinsiyet.Text;
            kisi.annead = txtanneAd.Text;
            kisi.babaad = txtbabaAd.Text;
            kisi.serino = txtserino.Text;
            kisi.ülke = cbulke.Text;
            kisi.sehir = cbil.Text;
            kisi.ilce = cbilce.Text;
            kisi.mahalle = cbmah.Text;
            kisi.dogum = dtdogum.Value;
            

            return kisi;
        }
        private void btnekle_Click(object sender, EventArgs e)
        {

            insangetir();

            try
            {


                SqlConnection baglan = new SqlConnection(@"Data Source=BERTUB\SQLEXPRESS;Initial Catalog=il ilce mah;Integrated Security=True");
                SqlCommand komut = new SqlCommand("Insert into KayıtlıNüfus(ad,soyad,cinsiyet,tckimlik,dogum,anneAd,babaAd,SeriNo,ülke,şehir,ilçe,mahalle,kimlikresim,imza) values(@ad,@soyad,@cinsiyet,@tckimlik,@dogum,@anneAd,@babaAd,@SeriNo,@ülke,@şehir,@ilçe,@mahalle,@kimlikresim,@imza)", baglan);
                komut.Parameters.AddWithValue("@ad", kisi.ad);
                komut.Parameters.AddWithValue("@soyad", kisi.soyad);
                komut.Parameters.AddWithValue("@cinsiyet", kisi.cinsiyet);
                komut.Parameters.AddWithValue("@tckimlik", kisi.tc);
                komut.Parameters.AddWithValue("@dogum", kisi.dogum);
                komut.Parameters.AddWithValue("@anneAd", kisi.annead);
                komut.Parameters.AddWithValue("@babaAd", kisi.babaad);
                komut.Parameters.AddWithValue("@SeriNo", kisi.serino);
                komut.Parameters.AddWithValue("@ülke", kisi.ülke);
                komut.Parameters.AddWithValue("@şehir", kisi.sehir);
                komut.Parameters.AddWithValue("@ilçe", kisi.ilce);
                komut.Parameters.AddWithValue("@mahalle", kisi.mahalle);
            
                MemoryStream mskimlik = new MemoryStream();
                tckimlikresim.Image.Save(mskimlik, ImageFormat.Jpeg);
                byte[] photo_araykimlik = new byte[mskimlik.Length];
                mskimlik.Position = 0;
                mskimlik.Read(photo_araykimlik, 0, photo_araykimlik.Length);
                komut.Parameters.AddWithValue("@kimlikresim", photo_araykimlik);
            
            
                MemoryStream msimza = new MemoryStream();
                imzaresim.Image.Save(msimza, ImageFormat.Jpeg);
                byte[] photo_arayimza = new byte[msimza.Length];
                msimza.Position = 0;
                msimza.Read(photo_arayimza, 0, photo_arayimza.Length);
                komut.Parameters.AddWithValue("@imza", photo_arayimza);
            
            baglan.Open();
                komut.ExecuteNonQuery();
                baglan.Close();
                
                
                txtad.Text = "";
                txtsoyad.Text = "";
                txtbabaAd.Text = "";
                txtserino.Text = "";
                txtanneAd.Text = "";
                tcno.Text = "";
                cbcinsiyet.Text = "";
                cbulke.Text = "";
                cbil.Text = "";
                cbilce.Text = "";
                cbmah.Text = "";
                dtdogum.Value = DateTime.Today;
                cbil.Items.Clear();
                cbilce.Items.Clear();
                cbmah.Items.Clear();
                MessageBox.Show("Kayıt İşlemi Başarıyla Gerçekleşti...");

            }
            catch
            {
                MessageBox.Show("Kayıt Sırasında Bir hata Oluştu Lütfen Tekrar Deneyin...");
                txtad.Text = "";
                txtsoyad.Text = "";
                txtbabaAd.Text = "";
                txtserino.Text = "";
                txtanneAd.Text = "";
                tcno.Text = "";
                cbcinsiyet.Text = "";
                cbulke.Text = "";
                cbil.Text = "";
                cbilce.Text = "";
                cbmah.Text = "";
                dtdogum.Value = DateTime.Today;
                cbil.Items.Clear();
                cbilce.Items.Clear();
                cbmah.Items.Clear();
                tcolusturbtn.Enabled = true;
                sclolusturbtn.Enabled = true;
            }
        }

        private void sclolusturbtn_Click(object sender, EventArgs e)
        {
            sclolusturbtn.Enabled = false;
            Random rst = new Random();
            string[] rakam = new string[9];            
            char harf;
             for (int i = 0; i < 1; i++)
              {
                  harf = Convert.ToChar(rst.Next(65, 66));
                  rakam[i] = harf.ToString();
              }

              for (int i = 3; i < 4; i++)
              { 
                  harf = Convert.ToChar(rst.Next(65,91));
                  rakam[i] = harf.ToString();
              }
              for (int i = 0; i < 9; i++)
              {
                  if (i==0 || i ==3)
                  {
                      continue;
                  }
                  rakam[i] = rst.Next(0, 10).ToString();
              }

              foreach (string b in rakam)
              {
                  txtserino.Text += b.ToString();
              }
            
            tekrarSeri();
            if (durumseri == false)
            {
                
            }
            else
            {
                txtserino.Text = "";
                for (int i = 0; i < 1; i++)
                {
                    harf = Convert.ToChar(rst.Next(65, 66));
                    rakam[i] = harf.ToString();
                }

                for (int i = 3; i < 4; i++)
                {
                    harf = Convert.ToChar(rst.Next(65, 91));
                    rakam[i] = harf.ToString();
                }
                for (int i = 0; i < 9; i++)
                {
                    if (i == 0 || i == 3)
                    {
                        continue;
                    }
                    rakam[i] = rst.Next(0, 10).ToString();
                }

                foreach (string b in rakam)
                {
                    txtserino.Text += b.ToString();
                }
            }

        }
        
        private void gtrbtn_Click(object sender, EventArgs e)
        {
            
            kisig.tc = tcgno.Text;

            SqlConnection baglan = new SqlConnection(@"Data Source=BERTUB\SQLEXPRESS;Initial Catalog=il ilce mah;Integrated Security=True");
            baglan.Open();
            SqlCommand komut = new SqlCommand("select * from KayıtlıNüfus where tckimlik like '"+kisig.tc+"'",baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                txtgad.Text = oku["ad"].ToString();
                txtgsoyad.Text = oku["soyad"].ToString();
                cbgcinsiyet.Text = oku["cinsiyet"].ToString();
                dtgdogum.Text = oku["dogum"].ToString();
                txtganneAd.Text= oku["anneAd"].ToString();
                txtgbabaAd.Text= oku["babaAd"].ToString();
                txtgserino.Text = oku["SeriNo"].ToString();
                cbgulke.Text = oku["ülke"].ToString(); 
                cbgsehir.Text = oku["şehir"].ToString();
                cbgilce.Text= oku["ilçe"].ToString();
                cbgmah.Text = oku["mahalle"].ToString();
                if (oku["kimlikresim"].ToString().Length>10)
                {
                    Byte[] datakimlik = (Byte[])(oku["kimlikresim"]);
                    MemoryStream kimlikhafıza = new MemoryStream(datakimlik);
                    tckimlikresim.Image = Image.FromStream(kimlikhafıza);
                    
                }
                if (oku["imza"].ToString().Length > 10)
                {
                    Byte[] dataimza = (Byte[])(oku["imza"]);
                    MemoryStream imzahafıza = new MemoryStream(dataimza);
                    imzaresim.Image = Image.FromStream(imzahafıza);
                }
                

            }

            baglan.Close();
            try
            {
                if (kisig.tc != string.Empty)
                {
                    kimlikyazdırbtn.Enabled = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Beklenmedik Bir Hata Oluştu");
                
            }
        }

        public  void btngnc_Click(object sender, EventArgs e)
        {
            insangetirguncel();

            try
            {
                SqlConnection baglan = new SqlConnection(@"Data Source=BERTUB\SQLEXPRESS;Initial Catalog=il ilce mah;Integrated Security=True");
                SqlCommand komut = new SqlCommand("update KayıtlıNüfus set ad=@ad, soyad=@soyad, cinsiyet=@cinsiyet, dogum=@dogum ,anneAd=@anneAd, babaAd=@babaAd, SeriNo=@SeriNo, ülke=@ülke, şehir=@şehir, ilçe=@ilçe, mahalle=@mahalle, kimlikresim=@kimlikresim, imza=@imza where tckimlik like '" + tcgno.Text + "'", baglan);
                komut.Parameters.AddWithValue("@ad", kisig.ad);
                komut.Parameters.AddWithValue("@soyad", kisig.soyad);
                komut.Parameters.AddWithValue("@cinsiyet", kisig.cinsiyet);
                komut.Parameters.AddWithValue("@dogum", kisig.dogum);
                komut.Parameters.AddWithValue("@anneAd",kisig.annead);
                komut.Parameters.AddWithValue("@babaAd", kisig.babaad);
                komut.Parameters.AddWithValue("@SeriNo", kisig.serino);
                komut.Parameters.AddWithValue("@ülke", kisig.ülke);
                komut.Parameters.AddWithValue("@şehir", kisig.sehir);
                komut.Parameters.AddWithValue("@ilçe", kisig.ilce);
                komut.Parameters.AddWithValue("@mahalle", kisig.mahalle);
            if (tckimlikresim.Image !=null)

            {

                MemoryStream mskimlik = new MemoryStream();
                tckimlikresim.Image.Save(mskimlik, ImageFormat.Jpeg);
                byte[] photo_araykimlik = new byte[mskimlik.Length];
                mskimlik.Position = 0;
                mskimlik.Read(photo_araykimlik, 0, photo_araykimlik.Length);
                komut.Parameters.AddWithValue("@kimlikresim", photo_araykimlik);
            }
            if (imzaresim.Image != null)
            {
                MemoryStream msimza = new MemoryStream();
                imzaresim.Image.Save(msimza, ImageFormat.Jpeg);
                byte[] photo_arayimza = new byte[msimza.Length];
                msimza.Position = 0;
                msimza.Read(photo_arayimza, 0, photo_arayimza.Length);
                komut.Parameters.AddWithValue("@imza", photo_arayimza);
            }                                 
                baglan.Open();
                komut.ExecuteNonQuery();
                baglan.Close();
                tcgno.Text = "";
                txtgad.Text = "";
                txtgsoyad.Text = "";
                cbgcinsiyet.Text = "";
                txtganneAd.Text = "";
                txtgbabaAd.Text = "";
                txtgserino.Text = "";
                cbgulke.Text = "";
                cbgsehir.Text = "";
                cbgilce.Text = "";
                cbgmah.Text = "";
                dtdogum.Value = DateTime.Today;
                tckimlikresim.Image = null;
                imzaresim.Image = null;
                
                MessageBox.Show("Kimlik Güncelleme Başarılı");
            }
            catch
            {
                MessageBox.Show("Bir Hata Oluştu");
            }
        }

        private void resimeklebtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)

            {
                tckimlikresim.Image = Image.FromFile(openFileDialog1.FileName);
                kisig.kimlikresim = openFileDialog1.FileName;
            }
            }

        private void imzaEklebtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                imzaresim.Image = Image.FromFile(openFileDialog2.FileName);
                kisig.imzaresim = openFileDialog2.FileName;
            }
            
        }

       
        private void kimlikyazdırbtn_Click(object sender, EventArgs e)
        {
            insangetirguncel();
            kimlikform frm = new kimlikform();
            frm.labelönAD.Text = kisig.ad.ToUpper();
            frm.labelönSoyad.Text = kisig.soyad.ToUpper();
            frm.labelönTc.Text = kisig.tc;
            frm.labelönSeri.Text = kisig.serino;

            if (cbgcinsiyet.Text == "Erkek")
            {
                frm.labelönCins.Text = "E/M".ToUpper();
            }
            else if (cbgcinsiyet.Text == "Kadın") {

                frm.labelönCins.Text = "K/F".ToUpper();
            }

            
            frm.labelönDogum.Text = kisig.dogum.ToString("M/d/yyyy");
            frm.önKimlikresim.Image = tckimlikresim.Image;
            frm.önİmzaresim.Image = imzaresim.Image;

            frm.labelarkaAd.Text =kisig.ad.ToUpper();
            frm.labelarkaAnneAd.Text = kisig.annead.ToUpper();
            frm.labelarkaBabaAd.Text = kisig.babaad.ToUpper();
            frm.labelarkaSeri.Text = kisig.serino;
            frm.labelarkaTc.Text = kisig.tc;
            frm.labelarkaSoyad.Text= kisig.soyad.ToUpper();

            Random rstgele = new Random();
            string[] dizirand = new string[15];
            char harf;

            for (int i = 7; i < 8; i++)
            {
                harf = Convert.ToChar(rstgele.Next(65, 91));
                dizirand[i] = harf.ToString();
            }
            for (int i = 0; i < 15; i++)
            {
                if (i==7)
                {
                    continue;
                }
                
                
                    dizirand[i] = rstgele.Next(0, 10).ToString();
                
                
            }
            string randomsayi = "".ToString();
            foreach (string c in dizirand )
            {
                randomsayi += c.ToString();
            }

           frm.labelarkaRand.Text = randomsayi;
            frm.Show();
            this.Hide();
        }

    }
}
