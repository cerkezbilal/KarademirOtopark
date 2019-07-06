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
    public partial class frmMarka : Form
    {
        public frmMarka()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-05BE747;Initial Catalog=KarademirOtopark;Integrated Security=True");
        private void frmMarka_Load(object sender, EventArgs e)
        {

        }

        private void btnMarkaEkle_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("insert into markabilgileri(marka) values(@marka)", baglan);
            komut.Parameters.AddWithValue("@marka", txtMarka.Text);
            komut.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("Marka Eklendi", "Eklendi");
            txtMarka.Text = "";

        }
    }
}
