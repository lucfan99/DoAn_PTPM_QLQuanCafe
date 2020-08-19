using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class Nuoc_BLL
    {
        QL_QuanCafeDataContext da = new QL_QuanCafeDataContext();
        public Nuoc_BLL()
        {
        }
        public IQueryable getNuoc()
        {
            var nuoc = from n in da.NUOCs select new { n.MANUOC, n.TENNUOC, n.GIABAN, n.MALOAI, n.HINH };
            return nuoc;
        }
        public List<NUOC> getDSNuoc()
        {
            var nuoc = da.NUOCs.ToList();
            return nuoc;
        }
        public List<LOAINUOC> getLoaiNuoc()
        {
            var l = da.LOAINUOCs.ToList();
            return l;
        }
        public void ThemNuocUong(string ma, string ten, float dg, string maloai, byte[] hinh)
        {
            NUOC n = new NUOC();
            n.MANUOC = ma;
            n.TENNUOC = ten;
            n.GIABAN = dg;
            n.MALOAI = maloai;
            n.HINH = hinh;

            da.NUOCs.InsertOnSubmit(n);
            da.SubmitChanges();
        }
        public void SuaNuocUong(string ma, string ten, float dg, string maloai, byte[] hinh)
        {
            NUOC n = da.NUOCs.Where(t => t.MANUOC == ma).SingleOrDefault();
            n.TENNUOC = ten;
            n.GIABAN = dg;
            n.MALOAI = maloai;
            n.HINH = hinh;

            da.SubmitChanges();
        }
        public void XoaNuoc(string ma)
        {
            NUOC n = da.NUOCs.Where(t => t.MANUOC == ma).SingleOrDefault();
            da.NUOCs.DeleteOnSubmit(n);
            da.SubmitChanges();
        }
        public float GetDonGiaNuoc(string ma)
        {
            NUOC n = da.NUOCs.Where(t => t.MANUOC == ma).SingleOrDefault();
            return (float)n.GIABAN;
        }
    }
}
