using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class Ban_BLL
    {
        QL_QuanCafeDataContext da = new QL_QuanCafeDataContext();
        public Ban_BLL()
        { }
        public List<BAN> getDSTatCaBan()
        {
            var b = da.BANs.ToList();
            return b;
        }
        public IQueryable getDSBan()
        {
            var ban = from n in da.BANs select new { n.MABAN, n.TENBAN };
            return ban;
        }
        public bool KTraBanTonTai(string ma)
        {
            int b = da.BANs.Where(t => t.MABAN == ma).ToList().Count;
            if (b > 0)
                return false;//nhân viên này đã tồn tại
            return true;//chưa tồn tại
        }
        public void ThemBan(string ma, string ten)
        {
            BAN b = new BAN();
            b.MABAN = ma;
            b.TENBAN = ten;


            da.BANs.InsertOnSubmit(b);
            da.SubmitChanges();
        }
        public void XoaBan(string ma)
        {
            BAN b = da.BANs.Where(t => t.MABAN == ma).SingleOrDefault();
            da.BANs.DeleteOnSubmit(b);
            da.SubmitChanges();
        }
        public void SuaBan(string ma, string ten)
        {
            BAN b = da.BANs.Where(t => t.MABAN == ma).SingleOrDefault();
            b.TENBAN = ten;

            da.SubmitChanges();
        }
        public string getMABanLast()
        {
            List<BAN> a = da.BANs.ToList();
            if (a.Count == 0)// neu chua co nhan vien nao
            { return ""; }
            //da co ma nhan vien
            BAN b = da.BANs.ToList().OrderByDescending(t => t.MABAN).First();
            return b.MABAN;
        }
        public bool KTraKhoaNgoai(string ma)
        {
            int hoadon = da.HOADONs.Where(t => t.MABAN == ma).ToList().Count;

            if (hoadon > 0)
                return true;
            return false;
        }
        public IQueryable TimKiemTheoTen(string ten)
        {
            var tk = from t in da.BANs where t.TENBAN.Contains(ten) == true select new { t.MABAN, t.TENBAN };
            return tk;
        }
        public IQueryable TimKiemTheoMa(string ma)
        {
            var tk = from t in da.BANs where t.MABAN.Contains(ma) == true select new { t.MABAN, t.TENBAN };
            return tk;
        }

    }
}
