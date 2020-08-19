using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL_BLL;
using System.Drawing.Imaging;

namespace QL_QuanCafe
{
    public partial class frmQLNuoc : Form
    {
        OpenFileDialog ofd = null;
        string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        string fullFilePath;
        Nuoc_BLL nuoc = new Nuoc_BLL();
        bool themmoi;
        public frmQLNuoc()
        {
            InitializeComponent();
        }
      
        public void setnull()
        {
            txtMaNuoc.Text = "";
            txtTenNuoc.Text = "";
            txtDongia.Text = "0";
            cboMaLoai.SelectedIndex = 0;
            //imghinhanh.Image = QuanLyCuaHangTivi.Properties.Resources.no;
        }
        public void un_locktext()
        {
            txtMaNuoc.Enabled = true;
            txtTenNuoc.Enabled = true;
            txtDongia.Enabled = true;
            cboMaLoai.Enabled = true;

            btnThemMoi.Enabled = false;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        public void locktext()
        {
            txtMaNuoc.Enabled = false;
            txtTenNuoc.Enabled = false;
            txtDongia.Enabled = false;
            cboMaLoai.Enabled = false;


            btnThemMoi.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void btnThemHinh_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog f = new OpenFileDialog())
                {
                    f.ShowDialog();
                    if (string.IsNullOrEmpty(f.FileName))
                        return;
                    image.Image = Image.FromFile(f.FileName);
                    //btnThemHinh.Text = f.FileName;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoaHinh_Click(object sender, EventArgs e)
        {
            
            FileInfo file = new FileInfo(fullFilePath);

            if (!IsFileLocked(file))
                file.Delete(); 
        }

        private void frmQLNuoc_Load(object sender, EventArgs e)
        {
            cboMaLoai.Items.Add("");
            
            cboMaLoai.DataSource = nuoc.getLoaiNuoc();
            cboMaLoai.DisplayMember = "TENLOAI";
            cboMaLoai.ValueMember = "MALOAI";
            locktext();
            dgvNuoc.DataSource = nuoc.getNuoc();
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            themmoi = true;
            un_locktext();
            //btnLuu.Enabled = true;
            //btnThemMoi.Enabled = false;
            //btnSua.Enabled = false;
            //btnXoa.Enabled = false;
            setnull();
            txtMaNuoc.Enabled = true;
            txtMaNuoc.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaNuoc.Text != "")
            {
                if (txtTenNuoc.Text != "")
                {
                    if (cboMaLoai.Text != "")
                    {
                        if (image.Image != null)
                        {
                            if (themmoi == true)
                            {
                                try
                                {
                                    MemoryStream st = new MemoryStream();
                                    image.Image.Save(st, ImageFormat.Png);
                                    byte[] hinh = st.ToArray();
                                    //string tenhinh = txtMaNuoc.Text.Trim() + ".jpg";
                                    //string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                                    //string filePath = Path.Combine(projectPath, "Resources");
                                    //string pathstring = System.IO.Path.Combine(filePath, tenhinh);
                                    //Image a = image.Image;
                                    //a.Save(pathstring);
                                    nuoc.ThemNuocUong(txtMaNuoc.Text, txtTenNuoc.Text, float.Parse(txtDongia.Text), cboMaLoai.SelectedValue.ToString(), hinh);

                                    locktext();
                                    dgvNuoc.DataSource = nuoc.getNuoc();
                                    MessageBox.Show("Đã Lưu Thành Công", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString(), "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                                try
                                {
                                    MemoryStream st = new MemoryStream();
                                    image.Image.Save(st, ImageFormat.Png);
                                    byte[] hinh = st.ToArray();
                                    //string tenhinh = txtMaNuoc.Text.Trim() + ".jpg";
                                    //string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                                    //string filePath = Path.Combine(projectPath, "Resources");
                                    //string pathstring = System.IO.Path.Combine(filePath, tenhinh);
                                    //Image a = image.Image;
                                    //a.Save(pathstring);
                                    nuoc.SuaNuocUong(txtMaNuoc.Text, txtTenNuoc.Text, float.Parse(txtDongia.Text), cboMaLoai.SelectedValue.ToString(), hinh);

                                    MessageBox.Show("Đã Sửa Thành Công Thành Công", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString(), "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            txtMaNuoc.Enabled = true;
                            locktext();
                            dgvNuoc.DataSource = nuoc.getNuoc();
                        }
                        else
                        {
                            MessageBox.Show("Bạn phải chọn hình !!");
                            return;  
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã Không được để trống", "Chú Ý", MessageBoxButtons.OK);
                        cboMaLoai.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Tên Không được để trống", "Chú Ý", MessageBoxButtons.OK);
                    txtTenNuoc.Focus();
                }
            }
            else
            {
                MessageBox.Show("Mã Không được để trống", "Chú Ý", MessageBoxButtons.OK);
                txtMaNuoc.Focus();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            themmoi = false;
            un_locktext();
            txtMaNuoc.Enabled = false;
            txtTenNuoc.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa dữ liệu này?", "Chú Ý", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    nuoc.XoaNuoc(txtMaNuoc.Text);
                    MessageBox.Show("Đã Xóa Thành Công", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvNuoc.DataSource = nuoc.getNuoc();
                    setnull();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi");
                }
            }
        }

        private void dgvNuoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int dong = e.RowIndex;
                txtMaNuoc.Text = dgvNuoc.Rows[dong].Cells[0].Value.ToString();
                txtTenNuoc.Text = dgvNuoc.Rows[dong].Cells[1].Value.ToString();
                txtDongia.Text = dgvNuoc.Rows[dong].Cells[2].Value.ToString();
                cboMaLoai.SelectedValue = dgvNuoc.Rows[dong].Cells[3].Value.ToString();
                string path = dgvNuoc.Rows[dong].Cells[4].Value.ToString();
                image.Image = Image.FromFile(path);

                fullFilePath = dgvNuoc.Rows[dong].Cells[4].Value.ToString();
                locktext();
            }
            catch
            {
                return;
            }
        }
        public static Boolean IsFileLocked(FileInfo path)
        {
            FileStream stream = null;
            try
            { //Don't change FileAccess to ReadWrite, 
                //because if a file is in readOnly, it fails. 
                stream = path.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            { //the file is unavailable because it is: 
                //still being written to or being processed by another thread 
                //or does not exist (has already been processed) 
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            //file is not locked 
            return false;
        } 
    }

}
