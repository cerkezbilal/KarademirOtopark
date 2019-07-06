using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtoparkOtomasyonu
{
    public partial class frmAnasayfa : Form
    {
        public frmAnasayfa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAracOtoparkKayit kayit = new frmAracOtoparkKayit();
            kayit.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAracOtoparkYerleri yerler = new frmAracOtoparkYerleri();
            yerler.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmAracOtoparkCikis cikis = new frmAracOtoparkCikis();
            cikis.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            frmSatisListeleme satis = new frmSatisListeleme();
            satis.ShowDialog();
        }
    }
}
