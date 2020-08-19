using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DAL_BLL;
using System.IO;

namespace QL_QuanCafe
{
    public partial class frmCTHoaDon : Form
    {
        HoaDon_BLL hd = new HoaDon_BLL();
        Nuoc_BLL nuoc = new Nuoc_BLL();
        NhanVien_BLL nv = new NhanVien_BLL();
        Ban_BLL b = new Ban_BLL();
        string ma;
        int dong = 0;
        public frmCTHoaDon(string ma)
        {
            this.ma = ma;
            InitializeComponent();
        }

        private void frmCTHoaDon_Load(object sender, EventArgs e)
        {
            txtMaHD.Enabled = false;
            txtNgayLap.Enabled = false;
            cboBan.Enabled = false;
            cboNV.Enabled = false;
            txtTinhTrang.Enabled = false;
            txtTongTien.Enabled = false;
            cboNV.DataSource = nv.getDSNhanVien();
            cboNV.DisplayMember = "HOTEN";
            cboNV.ValueMember = "MANV";
            cboBan.DataSource = b.getDSBan();
            cboBan.DisplayMember = "TENBAN";
            cboBan.ValueMember = "MABAN";
            dgvCTHD.DataSource = hd.GetCTHD(ma);
            txtMaHD.Text = hd.getTTHoaDon(ma).MAHD;
            txtNgayLap.Text = hd.getTTHoaDon(ma).NGAYLAP.ToString();
            cboNV.SelectedValue = hd.getTTHoaDon(ma).MANV;
            cboBan.SelectedValue = hd.getTTHoaDon(ma).MABAN;
            txtTinhTrang.Text = hd.getTTHoaDon(ma).TINHTRANG.ToString();
            txtTongTien.Text = hd.TinhTong(ma).ToString();
            LoadNuoc();
            dgvCTHD.MouseClick += new MouseEventHandler(dgvCTHD_MouseClick);
        }
        int nuttam = -1;
        private void dgvCTHD_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
            else
            {
                ContextMenuStrip menu = new System.Windows.Forms.ContextMenuStrip();
                int position_xy_mouse_row = dgvCTHD.HitTest(e.X, e.Y).RowIndex;
                nuttam = position_xy_mouse_row;
                if (position_xy_mouse_row >= 0)
                {
                    menu.Items.Add("Xóa").Name = "Xoa";
                    menu.Items.Add("Sửa").Name = "Sua";
                }
                menu.Show(dgvCTHD, new Point(e.X, e.Y));
                menu.ItemClicked += new ToolStripItemClickedEventHandler(menu_ItemClicked);
            }
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name.ToString())
            {
                case "Xoa":

                    string manuoc = dgvCTHD.Rows[nuttam].Cells[0].Value.ToString();
                    hd.XoaNuocRaKhoiHD(ma, manuoc);
                    MessageBox.Show("Xóa thành công");
                    txtTongTien.Text = hd.TinhTong(ma).ToString();
                    dgvCTHD.DataSource = hd.GetCTHD(ma);
                    break;
                case "Sua":
                    string man = dgvCTHD.Rows[nuttam].Cells[0].Value.ToString();
                    string tennuoc = dgvCTHD.Rows[nuttam].Cells[1].Value.ToString();
                    string sl = dgvCTHD.Rows[nuttam].Cells[2].Value.ToString();
                    frmSuaSoLuong frm = new frmSuaSoLuong(ma, man, tennuoc, sl);
                    frm.ShowDialog();
                    dgvCTHD.DataSource = hd.GetCTHD(ma);

                    this.Refresh();
                    this.Hide();
                    this.Close();
                    frmCTHoaDon frm1 = new frmCTHoaDon(ma);
                    frm1.ShowDialog();
                    txtTongTien.Text = hd.TinhTong(ma).ToString();
                    break;
            }
            //try
            //{
            //    string manuoc = dgvCTHD.Rows[nuttam].Cells[0].Value.ToString();
            //    hd.XoaNuocRaKhoiHD(ma, manuoc);
            //    MessageBox.Show("Xóa thành công");
            //    txtTongTien.Text = hd.TinhTong(ma).ToString();
            //    dgvCTHD.DataSource = hd.GetCTHD(ma);
            //}
            //catch
            //{
            //    return;
            //}
        }
        private void LoadNuoc()
        {
            int height = 160;
            foreach (var item in nuoc.getDSNuoc())
            {
                PanelControl pnl = new PanelControl();
                pnl.BackColor = Color.Red;
                pnl.Height = height;
                PanelControl pnltop = new PanelControl();
                pnltop.Dock = DockStyle.Top;
                pnltop.Height = 120;
                PictureBox pc = new PictureBox();
                pc.Dock = DockStyle.Fill;
                pc.SizeMode = PictureBoxSizeMode.StretchImage;
                if (item.HINH != null)
                {
                    MemoryStream st = new MemoryStream(item.HINH.ToArray());
                    pc.Image = Image.FromStream(st);
                }
                pnltop.Controls.Add(pc);
                Label lbl = new Label();
                lbl.Text = item.TENNUOC;
                lbl.Font = new Font(lbl.Font.Name, lbl.Font.Size, FontStyle.Bold);
                lbl.Dock = DockStyle.Bottom;

                pnltop.Controls.Add(lbl);
                pnl.Controls.Add(pnltop);

                PanelControl pnlbuttom = new PanelControl();
                pnlbuttom.Dock = DockStyle.Bottom;
                pnlbuttom.Height = 40;
                Button btnDM1 = new Button();
                btnDM1.Text = "Thêm vào";
                btnDM1.Cursor = Cursors.Hand;
                btnDM1.Dock = DockStyle.Fill;
                btnDM1.TextAlign = ContentAlignment.BottomCenter;
                btnDM1.Tag = item;
                btnDM1.BackColor = Color.DodgerBlue;
                btnDM1.ForeColor = Color.White;
                btnDM1.Font = new Font(btnDM1.Font.Name, btnDM1.Font.Size, FontStyle.Bold);
                pnlbuttom.Controls.Add(btnDM1);


                pnl.Controls.Add(pnlbuttom);
                pnl.Dock = DockStyle.Top;
                pnlNuoc.Controls.Add(pnl);
                this.pnlNuoc.AutoScroll = true;

                btnDM1.Click += btnDM_Click;

            }
        }

        private void btnDM_Click(object sender, EventArgs e)
        {
            Button btnDM = sender as Button;
            TextBox txt = sender as TextBox;
            string MaNuoc = ((sender as Button).Tag as NUOC).MANUOC;
            if (hd.KTraNuocTonTai(ma, MaNuoc) == true)
            {
                hd.ThemNuocVaoHD(ma, MaNuoc, 1, nuoc.GetDonGiaNuoc(MaNuoc));
                dgvCTHD.DataSource = hd.GetCTHD(ma);
                txtTongTien.Text = hd.TinhTong(ma).ToString();

            }
            else
            {
                int sl = hd.GetSL(ma, MaNuoc) + 1;
                hd.CapNhatSL(ma, MaNuoc, sl);
                dgvCTHD.DataSource = hd.GetCTHD(ma);
                txtTongTien.Text = hd.TinhTong(ma).ToString();
            }
        }

        private void dgvCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dong = e.RowIndex;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn thanh toán bàn này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.Yes)
            {
                try
                {
                    float tt = hd.TinhTong(ma);
                    hd.ThanhToanHD(ma, tt);
                    MessageBox.Show("Thanh toán thành công");
                    InHD();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Loi");
                }
            }
        }
        public void InHD()
        {
            ExcelExport ex = new ExcelExport();
            if (dgvCTHD.Rows.Count == 0)
            {
                MessageBox.Show("Khong co du lieu de Xuat");
                return;
            }
            List<INHOADON> plistdiem = new List<INHOADON>();
            int Stt = 1;
            string path = "";
            foreach (DataGridViewRow item in dgvCTHD.Rows)
            {
                INHOADON d = new INHOADON();
                d.MANUOC = item.Cells[0].Value.ToString();
                d.TENNUOC = item.Cells[1].Value.ToString();
                d.SOLUONG = int.Parse(item.Cells[2].Value.ToString());
                d.DONGIA = double.Parse(item.Cells[3].Value.ToString());
                d.THANHTIEN = double.Parse(item.Cells[4].Value.ToString());
                d.STT = Stt.ToString();
                Stt++;
                plistdiem.Add(d);

                path = string.Empty;
                ex.ExportKhoa(plistdiem, ref path, false, txtMaHD.Text, txtNgayLap.Text, cboNV.Text, cboBan.Text, double.Parse(txtTongTien.Text));
            }
            ex.OpenFile(path);
        }
    }
}
