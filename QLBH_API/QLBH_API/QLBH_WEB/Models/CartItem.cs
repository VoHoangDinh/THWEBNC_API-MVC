using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLBH_WEB.Models;

namespace QLBH_WEB.Models
{
    public class CartItem
    {
        public SanPhamViewModel SanPham { get; set; }
        public int SoLuong { get; set; }

        // Thuộc tính tính toán thành tiền
        public decimal ThanhTien
        {
            get { return (SanPham.DONGIA ?? 0) * SoLuong; }
        }

        public CartItem(SanPhamViewModel sanPham, int soLuong)
        {
            SanPham = sanPham;
            SoLuong = soLuong;
        }
    }
}