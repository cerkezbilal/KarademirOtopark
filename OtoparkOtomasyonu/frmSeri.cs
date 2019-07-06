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
    public partial class frmSeri : Form
    {
        public frmSeri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-05BE747;Initial Catalog=KarademirOtopark;Integrated Security=True");
        private void frmSeri_Load(object sender, EventArgs e)
        {
            MarkalariGetir();
        }

        private void MarkalariGetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from markabilgileri ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["marka"].ToString());
            }

            baglanti.Close();
        }

        private void btnSeriEkle_Click(object sender, EventArgs e)
        {
            SeriEkle();
            MarkalariGetir();
        }

        private void SeriEkle()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into seribilgileri(seri,marka) values(@seri,@marka)", baglanti);
            komut.Parameters.AddWithValue("@seri", textBox1.Text);
            komut.Parameters.AddWithValue("@marka", comboBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Marka bağlı araç serisi Eklendi", "Eklendi");
            textBox1.Clear();
            comboBox1.Text = "";
            comboBox1.Items.Clear();
        }
    }
}
