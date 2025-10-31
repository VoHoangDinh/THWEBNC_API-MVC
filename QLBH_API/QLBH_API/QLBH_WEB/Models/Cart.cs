using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLBH_WEB.Models;

namespace QLBH_WEB.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; }

        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }

        // Phương thức thêm sản phẩm vào giỏ hàng
        public void AddItem(SanPhamViewModel sanPham, int soLuong = 1)
        {
            // Nếu sản phẩm đã có trong giỏ, chỉ tăng số lượng
            if (Items.ContainsKey(sanPham.MASANPHAM))
            {
                Items[sanPham.MASANPHAM].SoLuong += soLuong;
            }
            // Nếu chưa có, thêm mới
            else
            {
                var newItem = new CartItem(sanPham, soLuong);
                Items.Add(sanPham.MASANPHAM, newItem);
            }
        }

        // Phương thức cập nhật số lượng
        public void UpdateItem(int maSanPham, int soLuong)
        {
            if (Items.ContainsKey(maSanPham))
            {
                if (soLuong > 0)
                {
                    Items[maSanPham].SoLuong = soLuong;
                }
                else
                {
                    RemoveItem(maSanPham);
                }
            }
        }

        // Phương thức xóa sản phẩm khỏi giỏ
        public void RemoveItem(int maSanPham)
        {
            Items.Remove(maSanPham);
        }

        // Phương thức tính tổng tiền
        public decimal GetTotal()
        {
            return Items.Values.Sum(item => item.ThanhTien);
        }

        // Phương thức lấy tổng số lượng sản phẩm
        public int GetTotalQuantity()
        {
            return Items.Values.Sum(item => item.SoLuong);
        }

        // Phương thức xóa sạch giỏ hàng
        public void Clear()
        {
            Items.Clear();
        }
    }
}