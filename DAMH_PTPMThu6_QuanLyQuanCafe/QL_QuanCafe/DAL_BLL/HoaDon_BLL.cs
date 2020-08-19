using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class HoaDon_BLL
    {
        QL_QuanCafeDataContext da = new QL_QuanCafeDataContext();
        public HoaDon_BLL() { }
        public IQueryable GetCTHD(string ma)
        {
            var ct = from cthd in da.CTHOADONs
                     join n in da.NUOCs on cthd.MANUOC equals n.MANUOC
                     where cthd.MAHD == ma
                     select new
                     {
                         cthd.MANUOC,
                         n.TENNUOC,
                         cthd.SOLUONG,
                         cthd.DONGIA,
                         cthd.THANHTIEN
                     };
            return ct;
        }

        public void ThemHoaDon(string mahd, string manv, string maban, float tongtien)
        {
            HOADON hd = new HOADON();
            hd.MAHD = mahd;
            hd.NGAYLAP = DateTime.Now;
            hd.MANV = manv;
            hd.MABAN = maban;
            hd.TINHTRANG = false;
            hd.TONGTIEN = tongtien;

            da.HOADONs.InsertOnSubmit(hd);
            da.SubmitChanges();

        }

        public string getMAHDLast()
        {
            List<HOADON> a = da.HOADONs.ToList();
            if (a.Count == 0)
            { return ""; }
            HOADON b = da.HOADONs.ToList().OrderByDescending(t => t.MAHD).First();
            return b.MAHD;
            //IEnumerable<HOADON> hd = from hoadon in da.HOADONs orderby hoadon.MAHD descending select hoadon.MAHD.First();
            //return 
        }
        public bool KTraBan(string maban)
        {
            int hd = da.HOADONs.Where(t => t.MABAN == maban && t.TINHTRANG == false).ToList().Count;
            if (hd > 0)
                return false;// bàn có người ngồi 
            return true;// bàn chưa có người ngồi
        }
        public string getMaHD(string maban)
        {
            HOADON hd = da.HOADONs.Where(t => t.MABAN == maban && t.TINHTRANG == false).SingleOrDefault();
            return hd.MAHD;
        }
        public HOADON getTTHoaDon(string mahd)
        {
            HOADON hd = da.HOADONs.Where(t => t.MAHD == mahd).SingleOrDefault();
            return hd;
        }
        public bool KTraNuocTonTai(string mahd, string manuoc)
        {
            int ct = da.CTHOADONs.Where(t => t.MAHD == mahd && t.MANUOC == manuoc).ToList().Count;
            if (ct > 0)
                return false;// đã tồn tại
            return true;// chưa tồn tại
        }
        public void ThemNuocVaoHD(string mahd, string manuoc, int sl, float dg)
        {
            CTHOADON ct = new CTHOADON();
            ct.MAHD = mahd;
            ct.MANUOC = manuoc;
            ct.SOLUONG = sl;
            ct.DONGIA = dg;
            ct.THANHTIEN = sl * dg;

            da.CTHOADONs.InsertOnSubmit(ct);
            da.SubmitChanges();
        }
        public void XoaNuocRaKhoiHD(string mahd, string manuoc)
        {
            CTHOADON ct = da.CTHOADONs.Where(t => t.MAHD == mahd && t.MANUOC == manuoc).SingleOrDefault();
            da.CTHOADONs.DeleteOnSubmit(ct);
            da.SubmitChanges();
        }
        public int GetSL(string mahd, string manuoc)
        {
            CTHOADON ct = da.CTHOADONs.Where(t => t.MAHD == mahd && t.MANUOC == manuoc).SingleOrDefault();

            return (int)ct.SOLUONG;
        }
        public void CapNhatSL(string mahd, string manuoc, int sl)
        {
            CTHOADON ct = da.CTHOADONs.Where(t => t.MAHD == mahd && t.MANUOC == manuoc).SingleOrDefault();
            ct.SOLUONG = sl;
            ct.THANHTIEN = sl * ct.DONGIA;

            da.SubmitChanges();
        }
        public float TinhTong(string mahd)
        {
            List<CTHOADON> ct = da.CTHOADONs.Where(t => t.MAHD == mahd).ToList();
            return (float)ct.Sum(t => t.THANHTIEN);
        }
        public void ThanhToanHD(string mahd, float tongtien)
        {
            HOADON hd = da.HOADONs.Where(t => t.MAHD == mahd).SingleOrDefault();
            hd.TONGTIEN = tongtien;
            hd.TINHTRANG = true;
            da.SubmitChanges();
        }
    }
}
