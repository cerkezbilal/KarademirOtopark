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
    public partial class frmSatisListeleme : Form
    {
        public frmSatisListeleme()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-05BE747;Initial Catalog=KarademirOtopark;Integrated Security=True");
        DataSet daset = new DataSet();
        private void frmSatisListeleme_Load(object sender, EventArgs e)
        {
            SatislariListele();
            Hesapla();
        }

        private void Hesapla()
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("select sum(tutar) from satis", baglan);
            label2.Text = komut.ExecuteScalar() + " TL";
            baglan.Close();
        }

        private void SatislariListele()
        {
            baglan.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis", baglan);
            adtr.Fill(daset,"satis");
            dataGridView1.DataSource = daset.Tables["satis"];
            baglan.Close();
        }
    }
}
