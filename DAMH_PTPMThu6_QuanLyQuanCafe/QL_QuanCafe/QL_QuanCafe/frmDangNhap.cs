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
    public partial class frmDangNhap : Form
    {
        DangNhap dn = new DangNhap();
        public frmDangNhap()
        {
            InitializeComponent();
        }
        public class LuuThongTin
        {
            static public string tendn;
            static public string manv;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void ProcessConfig()
        {
            if (Program.frmkn == null)
            {
                Program.frmkn = new frmKetNoi();
            }
            this.Visible = false;
            Program.frmkn.Show();
        }
        public void ProcessLogin()
        {
            int result;
            result = dn.Check_User(txtUser.Text, txtPass.Text);
            if (result == 1000)
            {
                MessageBox.Show("Sai " + lblUsername.Text + " or " + lblPass.Text);
                return;
            }
            else if (result == 2000)
            {
                MessageBox.Show("Tai khoan bi khoa !!");
                return;
            }
            LuuThongTin.tendn = txtUser.Text.Trim();
            LuuThongTin.manv = dn.getMaNV(txtUser.Text);
            frmMain frm = new frmMain();
            frm.ShowDialog();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text.Trim()))
            {
                MessageBox.Show("Không được bỏ trống" + lblUsername.Text.ToLower());
                this.txtUser.Focus(); return;
            }
            if (string.IsNullOrEmpty(this.txtPass.Text))
            {
                MessageBox.Show("Không được bỏ trống" + lblPass.Text.ToLower());
                this.txtPass.Focus();
                return;
            }
            int kq = dn.Check_Config(); //hàm Check_Config() thuộc Class QL_NguoiDung 
            if (kq == 0)
            {
                ProcessLogin();// Cấu hình phù hợp xử lý đăng nhập 
            }
            if (kq == 1)
            {
                MessageBox.Show("Chuỗi cấu hình không tồn tại");// Xử lý cấu hình 
                ProcessConfig();
                return;
            }
            if (kq == 2)
            {
                MessageBox.Show("Chuỗi cấu hình không phù hợp");// Xử lý cấu hình 
                ProcessConfig();
                return;
            }
        }

        private void txtPass_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void lblPass_Click(object sender, EventArgs e)
        {

        }

        private void txtUser_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }
    }
}
