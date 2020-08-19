using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DAL_BLL;

namespace QL_QuanCafe
{
    public partial class frmDanhSachBan : Form
    {
        Ban_BLL b = new Ban_BLL();

        HoaDon_BLL hd = new HoaDon_BLL();
        public frmDanhSachBan()
        {
            InitializeComponent();
        }
        private void frmDanhSachBan_Load(object sender, EventArgs e)
        {

            LoadDanhMucBan();
        }
        private void LoadDanhMucBan()
        {
            int width = 130;
            int height = 100;
            foreach (var item in b.getDSTatCaBan())
            {
                Button btnDM = new Button();
                btnDM.Width = width;
                btnDM.Height = height;
                btnDM.Text = item.TENBAN;
                btnDM.Cursor = Cursors.Hand;
                btnDM.Dock = DockStyle.Top;
                btnDM.TextAlign = ContentAlignment.BottomCenter;
                btnDM.BackgroundImage = Properties.Resources.table_64px;
                btnDM.BackgroundImageLayout = ImageLayout.Center;
                btnDM.Tag = item;

                pnlDMBan.Controls.Add(btnDM);
                if (hd.KTraBan(item.MABAN) == true)
                {
                    btnDM.BackColor = Color.LightSlateGray;
                }
                else
                {
                    btnDM.BackColor = Color.Red;
                }
                btnDM.Click += btnDM_Click;

            }
        }
        public string MaHDTuDong()
        {
            string kq = "";
            if (hd.getMAHDLast() == "")
            {
                kq = "HD00001";
            }
            else
            {
                int so = int.Parse(hd.getMAHDLast().Remove(0, 2));

                so = so + 1;
                if (so < 10)
                {
                    kq = "HD" + "0000";
                }
                else if (so < 100)
                {
                    kq = "HD" + "000";
                }
                else if (so < 1000)
                {
                    kq = "HD" + "00";
                }
                else if (so < 1000000)
                {
                    kq = "HD" + "0";
                }
                kq = kq + so.ToString();
            }
            return kq;
        }
        private void btnDM_Click(object sender, EventArgs e)
        {
            Button btnDM = sender as Button;
            string tableID = ((sender as Button).Tag as BAN).MABAN;
            if (hd.KTraBan(tableID) == true)
            {
                DialogResult rs = MessageBox.Show("Bạn có muốn chọn bàn này không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rs == DialogResult.Yes)
                {
                    string s = MaHDTuDong();
                    hd.ThemHoaDon(s, frmDangNhap.LuuThongTin.manv, tableID, 0);
                    string mahd = hd.getMaHD(tableID);
                    btnDM.BackColor = Color.Red;
                    frmCTHoaDon frm = new frmCTHoaDon(mahd);
                    frm.ShowDialog();
                    if (hd.KTraBan(tableID) == true)
                    {
                        btnDM.BackColor = Color.LightSlateGray;
                    }
                    else
                    {
                        btnDM.BackColor = Color.Red;
                    }
                }
            }
            else
            {

                string mahd = hd.getMaHD(tableID);


                frmCTHoaDon frm = new frmCTHoaDon(mahd);
                frm.ShowDialog();
                if (hd.KTraBan(tableID) == true)
                {
                    btnDM.BackColor = Color.LightSlateGray;
                }
                else
                {
                    btnDM.BackColor = Color.Red;
                }
            }
            //LoadDanhMucBan(Convert.ToInt32(btnDM.Tag));
        }
    }
}
