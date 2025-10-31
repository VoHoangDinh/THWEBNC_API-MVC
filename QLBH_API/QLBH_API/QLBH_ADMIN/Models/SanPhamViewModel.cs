using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLBH_ADMIN.Models
{
    public class SanPhamViewModel
    {
        [Display(Name = "Mã sản phẩm")]
        public int MASANPHAM { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [Display(Name = "Tên sản phẩm")]
        public string TENSANPHAM { get; set; }

        [Display(Name = "Giá bán")]
        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal? GIA { get; set; }

        [Display(Name = "Mô tả")]
        public string MOTA { get; set; }

        [Display(Name = "Hình ảnh")]
        public string HINHANH { get; set; }

        [Display(Name = "Danh mục")]
        [Required(ErrorMessage = "Bạn phải chọn danh mục")]
        public int? MADANHMUC { get; set; }

        // Thuộc tính này chỉ để hiển thị tên danh mục (không bắt buộc)
        public string TenDanhMuc { get; set; }
    }
}