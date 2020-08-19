using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class DangNhap
    {
        QL_QuanCafeDataContext da = new QL_QuanCafeDataContext();
        public DangNhap() { }
        public int Check_Config()
        {
            if (Properties.Settings.Default.QL_CafeConnectionString == string.Empty)
                return 1;// Chuỗi cấu hình không tồn tại 
            SqlConnection _Sqlconn = new SqlConnection(Properties.Settings.Default.QL_CafeConnectionString);
            try
            {
                if (_Sqlconn.State == System.Data.ConnectionState.Closed)
                    _Sqlconn.Open();
                return 0;
                // Kết nối thành công chuỗi cấu hình hợp lệ 
            }
            catch
            {
                return 2;// Chuỗi cấu hình không phù hợp. 
            }
        }
        public int Check_User(string pUser, string pPass)
        {
            int dn = da.TAIKHOANs.Where(t => t.TenTK == pUser && t.MatKhau == pPass).ToList().Count;
            var dt = da.TAIKHOANs.Where(t => t.TenTK == pUser && t.MatKhau == pPass).FirstOrDefault();
            if (dn == 0)
                return 1000;
            else if (dt.HoatDong == null || dt.HoatDong.ToString() == "False")
                return 2000;
            return 3000;
        }
        public DataTable GetServerName()
        {
            DataTable dt = new DataTable();
            dt = SqlDataSourceEnumerator.Instance.GetDataSources();
            return dt;
        }
        public DataTable GetDBName(string pServer, string pUser, string pPass)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select name from sys.Databases", "Data Source =" + pServer + ";Initial Catalog =master; User ID =" + pUser + "; pwd =" + pPass);
            da.Fill(dt);
            return dt;
        }
        public void SaveConfig(string pServer, string pUser, string pPass, string pDataBase)
        {
            //DAL_BLL.Properties.Settings.Default.QL_cafeConnectionString = "Data Source=" + pServer + ";Initial Catalog=" + pDataBase + ";User ID=" + pUser + ";pwd = " + pPass + "";
            //DAL_BLL.Properties.Settings.Default.Save();
        }
        public string getMaNV(string tentk)
        {
            TAIKHOAN nd = da.TAIKHOANs.Where(t => t.TenTK == tentk).SingleOrDefault();
            return nd.MANV;
        }
    }
}
