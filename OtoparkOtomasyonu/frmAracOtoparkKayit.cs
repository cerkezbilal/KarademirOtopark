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
namespace OtoparkOtomasyonu
{
    public partial class frmAracOtoparkKayit : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-05BE747;Initial Catalog=KarademirOtopark;Integrated Security=True");
        public frmAracOtoparkKayit()
        {
            InitializeComponent();
        }

        private void frmAracOtoparkKayit_Load(object sender, EventArgs e)
        {
            Yenile();

           

            MarkaEkle();


           

        }

        private void MarkaEkle()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from markabilgileri ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                cmbMarka.Items.Add(read["marka"].ToString());
            }

            baglanti.Close();
        }

        private void Yenile()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from aracdurumu where durumu ='BOŞ'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                cmbParkYeri.Items.Add(read["parkyeri"].ToString());
            }

            baglanti.Close();
        }

        private void btniptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKayit_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into arac_otopark_kaydi(tc,ad,soyad,telefon,email,plaka,marka,seri,renk,parkyeri,tarih) values(@tc,@ad,@soyad,@telefon,@email,@plaka,@marka,@seri,@renk,@parkyeri,@tarih)", baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);
            komut.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut.Parameters.AddWithValue("@marka", cmbMarka.Text);
            komut.Parameters.AddWithValue("@seri", cmbSeri.Text);
            komut.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut.Parameters.AddWithValue("@parkyeri", cmbParkYeri.Text);
            komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());

            komut.ExecuteNonQuery();

            SqlCommand komut2 = new SqlCommand("update aracdurumu set durumu='DOLU' where parkyeri='" + cmbParkYeri.SelectedItem + "'", baglanti);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Araç kaydı oluşturuldu", "Kayıt");
            cmbParkYeri.Items.Clear();

            Yenile();
            cmbMarka.Items.Clear();
            MarkaEkle();
            cmbSeri.Items.Clear();

            ControlTemizle();

        }

        private void ControlTemizle()
        {
            foreach (Control item in grupKisi.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            foreach (Control item in grupArac.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            foreach (Control item in grupArac.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }

            foreach (Control item in grupKisi.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnMarka_Click(object sender, EventArgs e)
        {
            frmMarka marka = new frmMarka();
            marka.ShowDialog();
        }

        private void btnSeri_Click(object sender, EventArgs e)
        {
            frmSeri seri = new frmSeri();
            seri.ShowDialog();
        }

        private void cmbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSeri.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka,seri from seribilgileri where marka='"+cmbMarka.Text+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                cmbSeri.Items.Add(read["seri"].ToString());
            }

            baglanti.Close();
        }
    }
}
