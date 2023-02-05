using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace XXiang.Models
{
    //[Authorize(Roles = "Admin")]身分驗證
    //https://ithelp.ithome.com.tw/articles/10205112

    public class TProductMetadata
    {
        public TProductMetadata()
        {
            TCorders = new HashSet<TCorder>();
            TPsites = new HashSet<TPsite>();
        }
        public int ProductId { get; set; }
        public int? SupplierId { get; set; }
        [DisplayName("註冊商品名稱")]
        [Required(ErrorMessage = "{0}欄位不可為空!")]
        public string Name { get; set; } = null!;
        [DisplayName("業者")]
        [Required(ErrorMessage = "{0}欄位不可為空!")]
        public string SupplierName { get; set; }

        public virtual TSupplier? Supplier { get; set; }
        public virtual ICollection<TCorder> TCorders { get; set; }
        public virtual ICollection<TPsite> TPsites { get; set; }
    }



    public class TPsiteRoomMetadata
    {
        public TPsiteRoomMetadata()
        {
            TCorderDetails = new HashSet<TCorderDetail>();
            TEvaluations = new HashSet<TEvaluation>();
        }

        public int RoomId { get; set; }
        public int? SiteId { get; set; }
        public int? CategoryId { get; set; }
        [DisplayName("以時計費")]
        public decimal? HourPrice { get; set; }
        [DisplayName("以日計費")]
        public decimal? DatePrice { get; set; }
        [DisplayName("坪數")]
        public int? Ping { get; set; }
        [DisplayName("房間圖片")]
        public string? Image { get; set; }
        [DisplayName("租借狀態")]
        public bool? Status { get; set; }
        [DisplayName("描述")]
        public string? Description { get; set; }
        public virtual TCategory? Category { get; set; }
        public virtual TPsite? Site { get; set; }
        public virtual ICollection<TCorderDetail> TCorderDetails { get; set; }
        public virtual ICollection<TEvaluation> TEvaluations { get; set; }
    }




    public class TPsiteMetadata
    {
        public TPsiteMetadata()
        {
            TPsiteRooms = new HashSet<TPsiteRoom>();
        }

        public int SiteId { get; set; }
        public int? ProductId { get; set; }
        [DisplayName("站點名稱")]
        public string Name { get; set; } = null!;
        [DisplayName("站點圖片")]
        public string? Image { get; set; }
        [DisplayName("開幕時間")]
        public string? OpenTime { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        [DisplayName("地址")]
        public string? Address { get; set; }
        [DisplayName("站點描述")]
        public string? Description { get; set; }
        public virtual TProduct? Product { get; set; }
        public virtual ICollection<TPsiteRoom> TPsiteRooms { get; set; }
    }




    public class TAdvertiseMetadata
    {
        public TAdvertiseMetadata()
        {
            TAorders = new HashSet<TAorder>();
        }

        public int AdvertiseId { get; set; }
        [DisplayName("廣告類型")]
        public string Name { get; set; } = null!;
        public decimal? DatePrice { get; set; }

        public virtual ICollection<TAorder> TAorders { get; set; }
    }




    public class TAorderMetadata
    {
        public int AorderId { get; set; }
        public int? SupplierId { get; set; }
        public int? AdvertiseId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Clicks { get; set; }
        public decimal? Price { get; set; }

        public virtual TAdvertise? Advertise { get; set; }
        public virtual TSupplier? Supplier { get; set; }
    }




    public class TCategoryMetadata
    {
        public TCategoryMetadata()
        {
            TPsiteRooms = new HashSet<TPsiteRoom>();
        }

        public int CategoryId { get; set; }
        [DisplayName("租借空間類型")]
        public string Name { get; set; } = null!;

        public virtual ICollection<TPsiteRoom> TPsiteRooms { get; set; }
    }




    public class TCorderMetadata
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? TakeDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual TCustomer? Customer { get; set; }
        public virtual TProduct? Product { get; set; }
    }




    public class TCorderDetailMetadata
    {
        public int OrderId { get; set; }
        public int? CouponId { get; set; }
        public int? RoomId { get; set; }
        public decimal? Price { get; set; }

        public virtual TCoupon? Coupon { get; set; }
        public virtual TPsiteRoom? Room { get; set; }
    }




    public class TCouponMetadata
    {
        public TCouponMetadata()
        {
            TCorderDetails = new HashSet<TCorderDetail>();
        }

        public int CouponId { get; set; }
        [DisplayName("優惠卷代碼")]
        public string? Code { get; set; }
        [DisplayName("折扣(輸入整數，ex：85折輸入85)")]
        public decimal? Discount { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? Quantity { get; set; }
        public bool? Available { get; set; }

        public virtual ICollection<TCorderDetail> TCorderDetails { get; set; }
    }




    public class TCustomerMetadata
    {
        public TCustomerMetadata()
        {
            TCorders = new HashSet<TCorder>();
            TEvaluations = new HashSet<TEvaluation>();
        }

        [DisplayName("編號")]
        public int CustomerId { get; set; }
        [DisplayName("名稱")]
        public string Name { get; set; } = null!;
        [DisplayName("性別")]
        public bool? Sex { get; set; }
        public string? Email { get; set; }
        [DisplayName("手機")]
        public string? Phone { get; set; }
        [DisplayName("密碼")]
        public string? Password { get; set; }
        [DisplayName("生日")]
        public DateTime? Birth { get; set; }
        [DisplayName("信用卡")]
        public string? CreditCard { get; set; }
        [DisplayName("信用積分")]
        public int? CreditPoints { get; set; }
        [DisplayName("黑名單")]
        public bool? BlackListed { get; set; }

        public virtual ICollection<TCorder> TCorders { get; set; }
        public virtual ICollection<TEvaluation> TEvaluations { get; set; }
    }




    public class TEvaluationMetadata
    {
        public int EvaluationId { get; set; }
        public int? CustomerId { get; set; }
        public int? RoomId { get; set; }
        public DateTime? Date { get; set; }
        [DisplayName("主題")]
        public string? Title { get; set; }
        [DisplayName("概述")]
        public string? Description { get; set; }
        public string? Response { get; set; }
        public int? Star { get; set; }

        public virtual TCustomer? Customer { get; set; }
        public virtual TPsiteRoom? Room { get; set; }
    }




    public class TManagerMetadata
    {
        public int ManagerId { get; set; }
        [DisplayName("管理者名稱")]
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
    }




    public class TSupplierMetadata
    {
        public TSupplierMetadata()
        {
            TAorders = new HashSet<TAorder>();
            TProducts = new HashSet<TProduct>();
        }

        public int SupplierId { get; set; }
        [DisplayName("業者名稱")]
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        [DisplayName("電話號碼")]
        public string? Phone { get; set; }
        [DisplayName("密碼")]
        public string? Password { get; set; }
        [DisplayName("地址")]
        public string? Address { get; set; }
        [DisplayName("信用積分")]
        public int? CreditPoints { get; set; }
        [DisplayName("黑名單")]
        public bool? BlackListed { get; set; }

        public virtual ICollection<TAorder> TAorders { get; set; }
        public virtual ICollection<TProduct> TProducts { get; set; }
    }



}
