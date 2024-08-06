using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySach
{
    public partial class ThongTinSach : Form
    {
        public ThongTinSach()
        {
            InitializeComponent();
        }

        private void ThongTinSach_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'duLieu.Sach' table. You can move, or remove it, as needed.
            this.sachTableAdapter.Fill(this.duLieu.Sach);

        }

        private void ThongTinSach_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'duLieu.Sach' table. You can move, or remove it, as needed.
            this.sachTableAdapter.Fill(this.duLieu.Sach);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TheLoai formTheLoai = new TheLoai();
            formTheLoai.ShowDialog();
        }
    }
}
