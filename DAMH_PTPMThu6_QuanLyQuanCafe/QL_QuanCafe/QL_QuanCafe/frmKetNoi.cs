using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QL_QuanCafe;
using System.IO;
using DAL_BLL;

namespace QL_QuanCafe
{
    public partial class frmKetNoi : Form
    {
        DangNhap dn = new DangNhap();
        public frmKetNoi()
        {
            InitializeComponent();
        }

        private void cboServername_DropDown(object sender, EventArgs e)
        {
            cboServername.DataSource = dn.GetServerName();
            cboServername.DisplayMember = "ServerName";
        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("Sinfo"))
                {
                    File.Delete("Sinfo");
                    StreamWriter write = new StreamWriter("Sinfo");
                    write.WriteLine("SV=:" + txtUser.Text);
                    write.WriteLine("DB=:" + txtPass.Text);
                    write.Close();
                    MessageBox.Show("Đã Thiết Lập xong", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    StreamWriter write = new StreamWriter("Sinfo");
                    write.WriteLine("SV=:" + txtUser.Text);
                    write.Close();
                    MessageBox.Show("Đã Thiết Lập xong", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Question);

                }

                MessageBox.Show("Kết Nối Thành Công Tới Sever " + txtUser.Text + ". Bạn sẻ phải khởi động lại chương trình đối với lần kết nối đầu tiên", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch
            {
                MessageBox.Show("Không thiết lập được", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmKetNoi_Load(object sender, EventArgs e)
        {

        }
    }
}
