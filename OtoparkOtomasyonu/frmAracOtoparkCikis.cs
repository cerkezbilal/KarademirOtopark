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
    public partial class frmAracOtoparkCikis : Form
    {
        public frmAracOtoparkCikis()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-05BE747;Initial Catalog=KarademirOtopark;Integrated Security=True");
        private void frmAracOtoparkCikis_Load(object sender, EventArgs e)
        {
            DoluParkYeriGetir();
            DoluParkYeriPlakaListele();
            lblSure.Text = "";
            lblUcret.Text = "";
            lblGirisSaati.Text = "";
            
            timer1.Enabled = true;
        }

        private void DoluParkYeriPlakaListele()
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select * from arac_otopark_kaydi ", baglan);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                cmbPlakaNo.Items.Add(read["plaka"].ToString());
                
            }
            baglan.Close();
        }

        private void DoluParkYeriGetir()
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select * from aracdurumu where durumu='DOLU'", baglan);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                cmbParkYeri.Items.Add(read["parkyeri"]);
                
            }
            baglan.Close();
        }

        private void cmbPlakaNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select * from arac_otopark_kaydi where plaka='"+cmbPlakaNo.Text+"'  ", baglan);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtParkYeri1.Text = read["parkyeri"].ToString();
            }
            baglan.Close();
        }

        private void cmbParkYeri_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select * from arac_otopark_kaydi where parkyeri='" + cmbParkYeri.Text + "'  ", baglan);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtPlaka.Text = read["plaka"].ToString();
                txtParkyeri2.Text = read["parkyeri"].ToString();
                txtParkYeri1.Text = read["parkyeri"].ToString();
                txtTc.Text = read["tc"].ToString();
                txtAd.Text = read["ad"].ToString();
                txtSoyad.Text = read["soyad"].ToString();
                txtMarka.Text = read["marka"].ToString();
                txtSeri.Text = read["seri"].ToString();
                txtRenk.Text = read["renk"].ToString();

                lblGirisSaati.Text = read["tarih"].ToString();
            }
            baglan.Close();
            DateTime gelis, cikis;
            gelis = DateTime.Parse(lblGirisSaati.Text);
            cikis = DateTime.Parse(lblCikisSaati.Text);
            TimeSpan fark;
            fark = cikis - gelis;
            lblSure.Text =Math.Ceiling(fark.TotalHours) .ToString();
           lblUcret.Text =Math.Ceiling((double.Parse(lblSure.Text)) * (0.2)).ToString();
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblCikisSaati.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("delete from arac_otopark_kaydi where plaka='"+cmbPlakaNo.Text+"'", baglan);
            komut.ExecuteNonQuery();

            SqlCommand komut2 = new SqlCommand("update aracdurumu set durumu='BOŞ' where parkyeri='" + txtParkyeri2.Text + "'", baglan);
            komut2.ExecuteNonQuery();

            SqlCommand komut3 = new SqlCommand("insert into satis(parkyeri,plaka,gelis_tarihi,cikis_tarihi,sure,tutar) values(@parkyeri,@plaka,@gelis_tarihi,@cikis_tarihi,@sure,@tutar)", baglan);
            komut3.Parameters.AddWithValue("@parkyeri", txtParkyeri2.Text);
            komut3.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut3.Parameters.AddWithValue("@gelis_tarihi", lblGirisSaati.Text);
            komut3.Parameters.AddWithValue("@cikis_tarihi", lblCikisSaati.Text);
            komut3.Parameters.AddWithValue("@sure",double.Parse(lblSure.Text));
            komut3.Parameters.AddWithValue("@tutar",double.Parse(lblUcret.Text));
            komut3.ExecuteNonQuery();

            baglan.Close();
            MessageBox.Show("Araç Çıkışı Yapıldı", "Çıkış");
            foreach (Control item in grupKisiBilgileri.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                    txtParkYeri1.Text = "";
                    cmbParkYeri.Text = "";
                    cmbPlakaNo.Text = "";

                }
            }

            cmbParkYeri.Items.Clear();
            cmbPlakaNo.Items.Clear();
            DoluParkYeriGetir();
            DoluParkYeriPlakaListele();
            lblSure.Text = "";
            lblUcret.Text = "";
            lblGirisSaati.Text = "";


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
