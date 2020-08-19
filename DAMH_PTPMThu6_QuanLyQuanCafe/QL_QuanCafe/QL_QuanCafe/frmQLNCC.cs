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
using System.Text.RegularExpressions;
using DAL_BLL;

namespace QL_QuanCafe
{
    public partial class frmQLNCC : Form
    {
        
        NhaCungCap_BLL ncc = new NhaCungCap_BLL();
        public frmQLNCC()
        {
            InitializeComponent();
        }

        private void frmQLNCC_Load(object sender, EventArgs e)
        {
            dgvNCC.DataSource = ncc.getDSNCC();
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
            txtDT.Enabled = false;
            txtDiaChi.Enabled = false;
        }
        public void unblockButtonTextbox()
        {
            btnThemMoi.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;

            txtMa.Enabled = true;
            txtTen.Enabled = true;
            txtDT.Enabled = true;
            txtDiaChi.Enabled = true;
        }
        public string MaTuDong()
        {
            string kq = "";
            if (ncc.getMANCCLast() == "")
            {
                kq = "NCC0001";
            }
            else
            {
                int so = int.Parse(ncc.getMANCCLast().Remove(0, 3));

                so = so + 1;
                if (so < 10)
                {
                    kq = "NCC" + "000";
                }
                else if (so < 100)
                {
                    kq = "NCC" + "00";
                }
                else if (so < 1000)
                {
                    kq = "NCC" + "0";
                }
                kq = kq + so.ToString();
            }
            return kq;
        }
        public void ClearDL()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtDiaChi.Text = "";
            txtDT.Text = "";
        }
        public static bool IsValidPhone(string value)
        {
            value = value ?? string.Empty;
            string pattern = @"^-*[0-9,\.?\-?\(?\)?\ ]+$";
            Regex re = new Regex(pattern);
            if (re.IsMatch(value) && value.Length == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
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
                        MessageBox.Show("Mã NCC không được để trống!!");
                        txtMa.Focus();
                        return;
                    }
                    if (txtTen.Text == "")
                    {
                        MessageBox.Show("Tên NCC không được để trống!!");
                        txtTen.Focus();
                        return;
                    }

                    if (txtDT.Text == "")
                    {
                        MessageBox.Show("Số ĐT không được để trống!!");
                        txtDT.Focus();
                        return;
                    }
                    if (IsValidPhone(txtDT.Text) == false)
                    {
                        MessageBox.Show("Số ĐT không hợp lệ!!");
                        txtDT.Focus();
                        return;
                    }
                    if (txtDiaChi.Text == "")
                    {
                        MessageBox.Show("Địa chỉ không được để trống!!");
                        txtDiaChi.Focus();
                        return;
                    }
                    if (ncc.KTraNCCTonTai(txtMa.Text) == true)
                    {
                        ncc.ThemNCC(txtMa.Text, txtTen.Text, txtDT.Text, txtDiaChi.Text);
                        dgvNCC.DataSource = ncc.getDSNCC();
                        blockButtonTextbox();
                    }
                    else
                    {
                        MessageBox.Show("NCC " + txtTen.Text + "đã tồn tại rồi!!");
                        return;
                    }
                }
                else
                {
                    ncc.SuaNCC(txtMa.Text, txtTen.Text, txtDT.Text, txtDiaChi.Text);
                    dgvNCC.DataSource = ncc.getDSNCC();
                    blockButtonTextbox();
                    MessageBox.Show("Sửa thành công ");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi!! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                blockButtonTextbox();
                int index = e.RowIndex;
                txtMa.Text = dgvNCC.Rows[index].Cells[0].Value.ToString();
                txtTen.Text = dgvNCC.Rows[index].Cells[1].Value.ToString();
                txtDiaChi.Text = dgvNCC.Rows[index].Cells[2].Value.ToString();
                txtDT.Text = dgvNCC.Rows[index].Cells[3].Value.ToString();
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
                        MessageBox.Show("Mã NCC không được để trống!!");
                        txtMa.Focus();
                        return;
                    }
                    if (ncc.KTraKhoaNgoai(txtMa.Text) == false)
                    {
                        ncc.XoaNCC(txtMa.Text);
                        MessageBox.Show("Xóa thành công");
                        dgvNCC.DataSource = ncc.getDSNCC();
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

        private void txtTimKiem_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTimKiem.Text.Trim() == "")
                {
                    dgvNCC.DataSource = ncc.getDSNCC();
                }
                else
                {
                    if (rdTen.Checked)
                    {
                        dgvNCC.DataSource = ncc.TimKiemTheoTen(txtTimKiem.Text);
                    }
                    else
                    {
                        dgvNCC.DataSource = ncc.TimKiemTheoSDT(txtTimKiem.Text);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTimKiem.Text.Trim() == "")
                {
                    dgvNCC.DataSource = ncc.getDSNCC();
                }
                else
                {
                    if (rdTen.Checked)
                    {
                        dgvNCC.DataSource = ncc.TimKiemTheoTen(txtTimKiem.Text);
                    }
                    else
                    {
                        dgvNCC.DataSource = ncc.TimKiemTheoSDT(txtTimKiem.Text);
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
