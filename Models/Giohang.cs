using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TH1.Models
{
    public class Giohang
    {
        QLBANSACHEntities data = new QLBANSACHEntities();

        public int iMasach { set; get; }
        public string sTensach { set; get; }
        public string sAnhbia { set; get; }
        public double dDongia { set; get; }
        public int iSoluong { set; get; }

        public double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }

        // Khởi tạo giỏ hàng theo Masach được truyền vào với Soluong mặc định là 1
        public Giohang(int Masach)
        {
            iMasach = Masach;
            SACH sach = data.SACHes.Single(n => n.Masach == iMasach);
            sTensach = sach.Tensach;
            sAnhbia = sach.Anhbia;
            dDongia = double.Parse(sach.Giaban.ToString());
            iSoluong = 1;
        }
    }
}