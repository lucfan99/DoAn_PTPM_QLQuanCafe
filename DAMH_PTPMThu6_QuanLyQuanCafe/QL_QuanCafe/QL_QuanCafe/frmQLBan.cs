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
    public partial class frmQLBan : Form
    {
        Ban_BLL b = new Ban_BLL();
        public frmQLBan()
        {
            InitializeComponent();
        }

        private void frmQLBan_Load(object sender, EventArgs e)
        {
            dgvBan.DataSource = b.getDSBan();
            blockButtonTextbox();
        }
        public void blockButtonTextbox()
        {
            btnThemMoi.Enabled = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = false;

            txtMa.Enabled = false;
            txtTen.Enabled = false;
        }
        public void unblockButtonTextbox()
        {
            btnThemMoi.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;

            txtMa.Enabled = true;
            txtTen.Enabled = true;
        }
        public string MaTuDong()
        {
            string kq = "";
            if (b.getMABanLast() == "")
            {
                kq = "BAN001";
            }
            else
            {
                int so = int.Parse(b.getMABanLast().Remove(0, 3));

                so = so + 1;
                if (so < 10)
                {
                    kq = "BAN" + "00";
                }
                else if (so < 100)
                {
                    kq = "BAN" + "0";
                }

                kq = kq + so.ToString();
            }
            return kq;
        }
        public void ClearDL()
        {
            txtMa.Text = "";
            txtTen.Text = "";
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            ClearDL();
            unblockButtonTextbox();
            txtMa.Text = MaTuDong();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMa.Enabled == true)
                {
                    if (txtMa.Text == "")
                    {
                        MessageBox.Show("Mã nhân viên không được để trống!!");
                        txtMa.Focus();
                        return;
                    }
                    if (txtTen.Text == "")
                    {
                        MessageBox.Show("Tên nhân viên không được để trống!!");
                        txtTen.Focus();
                        return;
                    }


                    if (b.KTraBanTonTai(txtMa.Text) == true)
                    {
                        b.ThemBan(txtMa.Text, txtTen.Text);
                        dgvBan.DataSource = b.getDSBan();
                        blockButtonTextbox();
                    }
                    else
                    {
                        MessageBox.Show("Nhân viên " + txtTen.Text + "đã tồn tại rồi!!");
                        return;
                    }
                }
                else
                {
                    b.SuaBan(txtMa.Text, txtTen.Text);
                    dgvBan.DataSource = b.getDSBan();
                    blockButtonTextbox();
                    MessageBox.Show("Sửa thành công ");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi!! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                blockButtonTextbox();
                int index = e.RowIndex;
                txtMa.Text = dgvBan.Rows[index].Cells[0].Value.ToString();
                txtTen.Text = dgvBan.Rows[index].Cells[1].Value.ToString();

                btnLuu.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
            catch
            {
                return;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            unblockButtonTextbox();
            txtMa.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = MessageBox.Show("Bạn có muốn xóa chứ ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (rs == DialogResult.Yes)
                {
                    if (txtMa.Text == "")
                    {
                        MessageBox.Show("Mã nhân viên không được để trống!!");
                        txtMa.Focus();
                        return;
                    }
                    if (b.KTraKhoaNgoai(txtMa.Text) == false)
                    {
                        b.XoaBan(txtMa.Text);
                        MessageBox.Show("Xóa thành công");
                        dgvBan.DataSource = b.getDSBan();
                        blockButtonTextbox();
                    }
                    else
                    {
                        MessageBox.Show("Dữ liệu này đang được sử dụng không thể xóa!!");
                        blockButtonTextbox();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Lỗi!! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTimKiem.Text.Trim() == "")
                {
                    dgvBan.DataSource = b.getDSBan();
                }
                else
                {
                    if (rdTen.Checked)
                    {
                        dgvBan.DataSource = b.TimKiemTheoTen(txtTimKiem.Text);
                    }
                    else
                    {
                        dgvBan.DataSource = b.TimKiemTheoMa(txtTimKiem.Text);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTimKiem.Text.Trim() == "")
                {
                    dgvBan.DataSource = b.getDSBan();
                }
                else
                {
                    if (rdTen.Checked)
                    {
                        dgvBan.DataSource = b.TimKiemTheoTen(txtTimKiem.Text);
                    }
                    else
                    {
                        dgvBan.DataSource = b.TimKiemTheoMa(txtTimKiem.Text);
                    }
                }
            }
            catch
            {
                return;
            }
        }
    }
}
