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
    public partial class frmAracOtoparkYerleri : Form
    {
        public frmAracOtoparkYerleri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-05BE747;Initial Catalog=KarademirOtopark;Integrated Security=True");
        private void frmAracOtoparkYerleri_Load(object sender, EventArgs e)
        {
            AlanlariDoldur();
            DoluBosDurumu();

            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from arac_otopark_kaydi", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == read["parkyeri"].ToString() )
                        {
                            item.Text = read["plaka"].ToString();
                        }
                    }
                }
            }

            }

        public void DoluBosDurumu()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from aracdurumu", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == read["parkyeri"].ToString() && read["durumu"].ToString() == "DOLU")
                        {
                            item.BackColor = Color.Red;
                        }


                       


                    }
                }
            }

            baglanti.Close();
        }

        public void AlanlariDoldur()
        {
            int sayac = 1;
            foreach (Control btn in Controls)
            {
                if (btn is Button)
                {
                    btn.Text = "P-" + sayac;
                    btn.Name = "P-" + sayac;
                    sayac++;
                }

            }
        }
    }
}
