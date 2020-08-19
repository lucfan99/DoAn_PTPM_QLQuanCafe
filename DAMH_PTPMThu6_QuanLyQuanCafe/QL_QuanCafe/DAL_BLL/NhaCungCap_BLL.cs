using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class NhaCungCap_BLL
    {
        QL_QuanCafeDataContext da = new QL_QuanCafeDataContext();
        public NhaCungCap_BLL() { }

        public IQueryable getDSNCC()
        {
            var ncc = from n in da.NHACUNGCAPs select new { n.MANCC, n.TENNCC, n.DIACHI, n.SDT };
            return ncc;
        }
        public bool KTraNCCTonTai(string ma)
        {
            int ncc = da.NHACUNGCAPs.Where(t => t.MANCC == ma).ToList().Count;
            if (ncc > 0)
                return false;//nhân viên này đã tồn tại
            return true;//chưa tồn tại
        }
        public void ThemNCC(string ma, string ten, string diachi, string dienthoai)
        {
            NHACUNGCAP ncc = new NHACUNGCAP();
            ncc.MANCC = ma;
            ncc.TENNCC = ten;
            ncc.DIACHI = diachi;
            ncc.SDT = dienthoai;

            da.NHACUNGCAPs.InsertOnSubmit(ncc);
            da.SubmitChanges();
        }
        public void XoaNCC(string ma)
        {
            NHACUNGCAP ncc = da.NHACUNGCAPs.Where(t => t.MANCC == ma).SingleOrDefault();
            da.NHACUNGCAPs.DeleteOnSubmit(ncc);
            da.SubmitChanges();
        }
        public void SuaNCC(string ma, string ten, string diachi, string dienthoai)
        {
            NHACUNGCAP ncc = da.NHACUNGCAPs.Where(t => t.MANCC == ma).SingleOrDefault();
            ncc.TENNCC = ten;
            ncc.DIACHI = diachi;
            ncc.SDT = dienthoai;

            da.SubmitChanges();
        }
        public string getMANCCLast()
        {
            List<NHACUNGCAP> a = da.NHACUNGCAPs.ToList();
            if (a.Count == 0)// neu chua co nhan vien nao
            { return ""; }
            //da co ma nhan vien
            NHACUNGCAP b = da.NHACUNGCAPs.ToList().OrderByDescending(t => t.MANCC).First();
            return b.MANCC;
        }
        public bool KTraKhoaNgoai(string ma)
        {
            int nguyenlieu = da.NGUYENLIEUs.Where(t => t.MANCC == ma).ToList().Count;

            if (nguyenlieu > 0)
                return true;
            return false;
        }
        public IQueryable TimKiemTheoTen(string ten)
        {
            var tk = from t in da.NHACUNGCAPs where t.TENNCC.Contains(ten) == true select new { t.MANCC, t.TENNCC, t.DIACHI, t.SDT };
            return tk;
        }
        public IQueryable TimKiemTheoSDT(string sdt)
        {
            var tk = from t in da.NHACUNGCAPs where t.SDT.Contains(sdt) == true select new { t.MANCC, t.TENNCC, t.DIACHI, t.SDT };
            return tk;
        }

    }
}
