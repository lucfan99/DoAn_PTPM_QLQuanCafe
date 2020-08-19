using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class NhanVien_BLL
    {
        QL_QuanCafeDataContext da = new QL_QuanCafeDataContext();
        public NhanVien_BLL() { }

        public IQueryable getDSNhanVien()
        {
            var nv = from n in da.NHANVIENs select new { n.MANV, n.HOTEN, n.DIENTHOAI, n.DIACHI };
            return nv;
        }
        public bool KTraNhanVienTonTai(string ma)
        {
            int nv = da.NHANVIENs.Where(t => t.MANV == ma).ToList().Count;
            if (nv > 0)
                return false;//nhân viên này đã tồn tại
            return true;//chưa tồn tại
        }
        public void ThemNhanVien(string ma, string ten, string diachi, string dienthoai)
        {
            NHANVIEN nv = new NHANVIEN();
            nv.MANV = ma;
            nv.HOTEN = ten;
            nv.DIACHI = diachi;
            nv.DIENTHOAI = dienthoai;

            da.NHANVIENs.InsertOnSubmit(nv);
            da.SubmitChanges();
        }
        public void XoaNhanVien(string ma)
        {
            NHANVIEN nv = da.NHANVIENs.Where(t => t.MANV == ma).SingleOrDefault();
            da.NHANVIENs.DeleteOnSubmit(nv);
            da.SubmitChanges();
        }
        public void SuaNhanVien(string ma, string ten, string diachi, string dienthoai)
        {
            NHANVIEN nv = da.NHANVIENs.Where(t => t.MANV == ma).SingleOrDefault();
            nv.HOTEN = ten;
            nv.DIACHI = diachi;
            nv.DIENTHOAI = dienthoai;

            da.SubmitChanges();
        }
        public string getMANVLast()
        {
            List<NHANVIEN> a = da.NHANVIENs.ToList();
            if (a.Count == 0)// neu chua co nhan vien nao
            { return ""; }
            //da co ma nhan vien
            NHANVIEN b = da.NHANVIENs.ToList().OrderByDescending(t => t.MANV).First();
            return b.MANV;
        }
        public bool KTraKhoaNgoai(string ma)
        {
            int hoadon = da.HOADONs.Where(t => t.MANV == ma).ToList().Count;
            int phieunhap = da.PHIEUNHAPs.Where(t => t.MANV == ma).ToList().Count;
            int taikhoan = da.TAIKHOANs.Where(t => t.MANV == ma).ToList().Count;
            if (hoadon > 0 || phieunhap > 0 || taikhoan > 0)
                return true;
            return false;
        }
        public IQueryable TimKiemTheoTen(string ten)
        {
            var tk = from t in da.NHANVIENs where t.HOTEN.Contains(ten) == true select new { t.MANV, t.HOTEN, t.DIENTHOAI, t.DIACHI };
            return tk;
        }
        public IQueryable TimKiemTheoSDT(string sdt)
        {
            var tk = from t in da.NHANVIENs where t.DIENTHOAI.Contains(sdt) == true select new { t.MANV, t.HOTEN, t.DIENTHOAI, t.DIACHI };
            return tk;
        }
        public string GetTenNV(string ma)
        {
            NHANVIEN nv = da.NHANVIENs.Where(t => t.MANV == ma).SingleOrDefault();
            return nv.HOTEN;
        }
    }
}
