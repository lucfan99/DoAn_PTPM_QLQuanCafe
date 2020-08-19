using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BLL
{
    public class INHOADON
    {
        double _THANHTIEN;

        public double THANHTIEN
        {
            get { return _THANHTIEN; }
            set { _THANHTIEN = value; }
        }
        string _MANUOC;

        public string MANUOC
        {
            get { return _MANUOC; }
            set { _MANUOC = value; }
        }
        string _STT;

        public string STT
        {
            get { return _STT; }
            set { _STT = value; }
        }
        string _TENNUOC;

        public string TENNUOC
        {
            get { return _TENNUOC; }
            set { _TENNUOC = value; }
        }
        int _SOLUONG;

        public int SOLUONG
        {
            get { return _SOLUONG; }
            set { _SOLUONG = value; }
        }
        double _DONGIA;

        public double DONGIA
        {
            get { return _DONGIA; }
            set { _DONGIA = value; }
        }

    }
}
