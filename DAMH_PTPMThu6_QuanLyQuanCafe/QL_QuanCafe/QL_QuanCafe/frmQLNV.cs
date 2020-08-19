using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DAL_BLL;

namespace QL_QuanCafe
{
    public partial class frmQLNV : Form
    {
        NhanVien_BLL nv = new NhanVien_BLL();
        public frmQLNV()
        {
            InitializeComponent();
        }

        private void frmQLNV_Load(object sender, EventArgs e)
        {
            dgvNhanVien.DataSource = nv.getDSNhanVien();
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
            txtSDT.Enabled = false;
            txtDiachi.Enabled = false;
        }
        public void unblockButtonTextbox()
        {
            btnThemMoi.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;

            txtMa.Enabled = true;
            txtTen.Enabled = true;
            txtSDT.Enabled = true;
            txtDiachi.Enabled = true;
        }
        public string MaTuDong()
        {
            string kq = "";
            if (nv.getMANVLast() == "")
            {
                kq = "NV00001";
            }
            else
            {
                int so = int.Parse(nv.getMANVLast().Remove(0, 2));

                so = so + 1;
                if (so < 10)
                {
                    kq = "NV" + "0000";
                }
                else if (so < 100)
                {
                    kq = "NV" + "000";
                }
                else if (so < 1000)
                {
                    kq = "NV" + "00";
                }
                else if (so < 10000)
                {
                    kq = "NV" + "0";
                }
                kq = kq + so.ToString();
            }
            return kq;
        }
        public void ClearDL()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtDiachi.Text = "";
            txtSDT.Text = "";
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
                    
                    if (txtSDT.Text == "")
                    {
                        MessageBox.Show("Số ĐT không được để trống!!");
                        txtSDT.Focus();
                        return;
                    }
                    if (IsValidPhone(txtSDT.Text) == false)
                    {
                        MessageBox.Show("Số ĐT không hợp lệ!!");
                        txtSDT.Focus();
                        return;
                    }
                    if (txtDiachi.Text == "")
                    {
                        MessageBox.Show("Địa chỉ không được để trống!!");
                        txtDiachi.Focus();
                        return;
                    }
                    if (nv.KTraNhanVienTonTai(txtMa.Text) == true)
                    {
                        nv.ThemNhanVien(txtMa.Text, txtTen.Text, txtSDT.Text, txtDiachi.Text);
                        dgvNhanVien.DataSource = nv.getDSNhanVien();
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
                    nv.SuaNhanVien(txtMa.Text, txtTen.Text, txtSDT.Text, txtDiachi.Text);
                    dgvNhanVien.DataSource = nv.getDSNhanVien();
                    blockButtonTextbox();
                    MessageBox.Show("Sửa thành công ");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi!! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                blockButtonTextbox();
                int index = e.RowIndex;
                txtMa.Text = dgvNhanVien.Rows[index].Cells[0].Value.ToString();
                txtTen.Text = dgvNhanVien.Rows[index].Cells[1].Value.ToString();
                txtSDT.Text = dgvNhanVien.Rows[index].Cells[2].Value.ToString();
                txtDiachi.Text = dgvNhanVien.Rows[index].Cells[3].Value.ToString();
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
                    if (nv.KTraKhoaNgoai(txtMa.Text) == false)
                    {
                        nv.XoaNhanVien(txtMa.Text);
                        MessageBox.Show("Xóa thành công");
                        dgvNhanVien.DataSource = nv.getDSNhanVien();
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
                    dgvNhanVien.DataSource = nv.getDSNhanVien();
                }
                else
                {
                    if (rdTen.Checked)
                    {
                        dgvNhanVien.DataSource = nv.TimKiemTheoTen(txtTimKiem.Text);
                    }
                    else
                    {
                        dgvNhanVien.DataSource = nv.TimKiemTheoSDT(txtTimKiem.Text);
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
                    dgvNhanVien.DataSource = nv.getDSNhanVien();
                }
                else
                {
                    if (rdTen.Checked)
                    {
                        dgvNhanVien.DataSource = nv.TimKiemTheoTen(txtTimKiem.Text);
                    }
                    else
                    {
                        dgvNhanVien.DataSource = nv.TimKiemTheoSDT(txtTimKiem.Text);
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
