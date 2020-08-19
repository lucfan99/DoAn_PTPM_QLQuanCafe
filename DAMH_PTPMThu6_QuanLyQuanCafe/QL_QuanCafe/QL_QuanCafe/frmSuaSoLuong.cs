using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL_BLL;

namespace QL_QuanCafe
{
    public partial class frmSuaSoLuong : Form
    {
        HoaDon_BLL hd = new HoaDon_BLL();
        string ma;
        string manuoc;
        string tennuoc;
        string sl;
        public frmSuaSoLuong(string ma,string manuoc,string tennuoc,string sl)
        {
            this.ma = ma;
            this.manuoc = manuoc;
            this.tennuoc = tennuoc;
            this.sl = sl;
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                int soluong = int.Parse(txtSoLuong.Text);
                hd.CapNhatSL(ma, manuoc, soluong);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi");
            }
        }

        private void frmSuaSoLuong_Load(object sender, EventArgs e)
        {
            txtMa.Text = manuoc;
            txtTen.Text = tennuoc;
            txtSoLuong.Text = sl;
        }
    }
}
