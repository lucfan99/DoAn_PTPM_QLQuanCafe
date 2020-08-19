using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class NguyenLieu_BLL
    {
        QL_QuanCafeDataContext da = new QL_QuanCafeDataContext();
        public NguyenLieu_BLL() { }

        public IQueryable getDSNL()
        {
            var nv = from n in da.NGUYENLIEUs select new { n.MANL, n.TENNL, n.DONGIA, n.MANCC };
            return nv;
        }
        public bool KTraNLTonTai(string ma)
        {
            int nv = da.NGUYENLIEUs.Where(t => t.MANL == ma).ToList().Count;
            if (nv > 0)
                return false;//nhân viên này đã tồn tại
            return true;//chưa tồn tại
        }
        public void ThemNL(string ma, string ten, float dongia, string mancc)
        {
            NGUYENLIEU nv = new NGUYENLIEU();
            nv.MANL = ma;
            nv.TENNL = ten;
            nv.DONGIA = dongia;
            nv.MANCC = mancc;

            da.NGUYENLIEUs.InsertOnSubmit(nv);
            da.SubmitChanges();
        }
        public void XoaNL(string ma)
        {
            NGUYENLIEU nv = da.NGUYENLIEUs.Where(t => t.MANL == ma).SingleOrDefault();
            da.NGUYENLIEUs.DeleteOnSubmit(nv);
            da.SubmitChanges();
        }
        public void SuaNL(string ma, string ten, float dongia, string mancc)
        {
            NGUYENLIEU nv = da.NGUYENLIEUs.Where(t => t.MANL == ma).SingleOrDefault();
            nv.TENNL = ten;
            nv.DONGIA = dongia;
            nv.MANCC = mancc;

            da.SubmitChanges();
        }
        public string getMANLLast()
        {
            List<NGUYENLIEU> a = da.NGUYENLIEUs.ToList();
            if (a.Count == 0)// neu chua co nhan vien nao
            { return ""; }
            //da co ma nhan vien
            NGUYENLIEU b = da.NGUYENLIEUs.ToList().OrderByDescending(t => t.MANL).First();
            return b.MANL;
        }
        public bool KTraKhoaNgoai(string ma)
        {

            int phieunhap = da.CTPHIEUNHAPs.Where(t => t.MANL == ma).ToList().Count;
            if (phieunhap > 0)
                return true;
            return false;
        }
        public IQueryable TimKiemTheoTen(string ten)
        {
            var tk = from t in da.NGUYENLIEUs where t.TENNL.Contains(ten) == true select new { t.MANL, t.TENNL, t.DONGIA, t.MANCC };
            return tk;
        }
        public IQueryable TimKiemTheoMaT(string ma)
        {
            var tk = from t in da.NGUYENLIEUs where t.MANL.Contains(ma) == true select new { t.MANL, t.TENNL, t.DONGIA, t.MANCC };
            return tk;
        }
    }
}
