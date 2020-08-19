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
    public partial class frmQLNguyenLieu : Form
    {
        NguyenLieu_BLL nl = new NguyenLieu_BLL();
        NhaCungCap_BLL ncc = new NhaCungCap_BLL();
        public frmQLNguyenLieu()
        {
            InitializeComponent();
        }

        private void frmQLNguyenLieu_Load(object sender, EventArgs e)
        {
            cboNCC.DataSource = ncc.getDSNCC();
            cboNCC.DisplayMember = "TENNCC";
            cboNCC.ValueMember = "MANCC";
            dgvNL.DataSource = nl.getDSNL();
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
            txtDG.Enabled = false;
            cboNCC.Enabled = false;
        }
        public void unblockButtonTextbox()
        {
            btnThemMoi.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;

            txtMa.Enabled = true;
            txtTen.Enabled = true;
            txtDG.Enabled = true;
            cboNCC.Enabled = true;
        }
        public string MaTuDong()
        {
            string kq = "";
            if (nl. getMANLLast() == "")
            {
                kq = "NL00001";
            }
            else
            {
                int so = int.Parse(nl.getMANLLast().Remove(0, 2));

                so = so + 1;
                if (so < 10)
                {
                    kq = "NL" + "0000";
                }
                else if (so < 100)
                {
                    kq = "NL" + "000";
                }
                else if (so < 1000)
                {
                    kq = "NL" + "00";
                }
                else if (so < 1000)
                {
                    kq = "NL" + "0";
                }
                kq = kq + so.ToString();
            }
            return kq;
        }
        public void ClearDL()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtDG.Text = "";
            cboNCC.Text = "";
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
                        MessageBox.Show("Mã nguyên liệu không được để trống!!");
                        txtMa.Focus();
                        return;
                    }
                    if (txtTen.Text == "")
                    {
                        MessageBox.Show("Tên nguyên liệu không được để trống!!");
                        txtTen.Focus();
                        return;
                    }

                    if (txtDG.Text == "")
                    {
                        MessageBox.Show("Đơn giá không được để trống!!");
                        txtDG.Focus();
                        return;
                    }
                    
                    if (cboNCC.Text == "")
                    {
                        MessageBox.Show("Nhà cung cấp không được để trống!!");
                        return;
                    }
                    if (nl.KTraNLTonTai(txtMa.Text) == true)
                    {
                        nl.ThemNL(txtMa.Text, txtTen.Text, float.Parse(txtDG.Text), cboNCC.SelectedValue.ToString());
                        dgvNL.DataSource = nl.getDSNL();
                        blockButtonTextbox();
                    }
                    else
                    {
                        MessageBox.Show("Nguyên liệu " + txtTen.Text + "đã tồn tại rồi!!");
                        return;
                    }
                }
                else
                {
                    nl.SuaNL(txtMa.Text, txtTen.Text, float.Parse(txtDG.Text), cboNCC.SelectedValue.ToString());
                    dgvNL.DataSource = nl.getDSNL();
                    blockButtonTextbox();
                    MessageBox.Show("Sửa thành công ");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi!! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvNL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                blockButtonTextbox();
                int index = e.RowIndex;
                txtMa.Text = dgvNL.Rows[index].Cells[0].Value.ToString();
                txtTen.Text = dgvNL.Rows[index].Cells[1].Value.ToString();
                txtDG.Text = dgvNL.Rows[index].Cells[2].Value.ToString();
                cboNCC.SelectedValue = dgvNL.Rows[index].Cells[3].Value.ToString();
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
                        MessageBox.Show("Mã NL không được để trống!!");
                        txtMa.Focus();
                        return;
                    }
                    if (nl.KTraKhoaNgoai(txtMa.Text) == false)
                    {
                        nl.XoaNL(txtMa.Text);
                        MessageBox.Show("Xóa thành công");
                        dgvNL.DataSource = nl.getDSNL();
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
                    dgvNL.DataSource = nl.getDSNL();
                }
                else
                {
                    if (rdTen.Checked)
                    {
                        dgvNL.DataSource = nl.TimKiemTheoTen(txtTimKiem.Text);
                    }
                    else
                    {
                        dgvNL.DataSource = nl.TimKiemTheoMaT(txtTimKiem.Text);
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
