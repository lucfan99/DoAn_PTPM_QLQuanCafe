using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace QL_QuanCafe
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private Form KiemTraTonTai(Type ptype)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == ptype)
                {
                    return f;
                }
            }
            return null;
        }
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = KiemTraTonTai(typeof(frmDanhSachBan));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmDanhSachBan f = new frmDanhSachBan();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn muốn đăng xuất tài khoản không ?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rs == DialogResult.Yes)
                e.Cancel = false;

        }

        private void btnBan_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = KiemTraTonTai(typeof(frmQLBan));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmQLBan f = new frmQLBan();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnNguyenLieu_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = KiemTraTonTai(typeof(frmQLNguyenLieu));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmQLNguyenLieu f = new frmQLNguyenLieu();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnNCC_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = KiemTraTonTai(typeof(frmQLNCC));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmQLNCC f = new frmQLNCC();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnNV_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = KiemTraTonTai(typeof(frmQLNV));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmQLNV f = new frmQLNV();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form frm = KiemTraTonTai(typeof(frmQLNuoc));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmQLNuoc f = new frmQLNuoc();
                f.MdiParent = this;
                f.Show();
            }
        }

        private void btnLoaiNuoc_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Form frm = KiemTraTonTai(typeof(frmDanhSachBan));
            if (frm != null)
            {
                frm.Activate();
            }
            else
            {
                frmDanhSachBan f = new frmDanhSachBan();
                f.MdiParent = this;
                f.Show();
            }
        }


    }
}