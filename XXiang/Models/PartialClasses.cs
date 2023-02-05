using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XXiang.Models
{
    [ModelMetadataType(typeof(TProductMetadata))]
    public partial class TProduct
    {
        [NotMapped]
        public string SupplierName { get; set; } = null!;
    }
    [ModelMetadataType(typeof(TPsiteRoomMetadata))]
    public partial class TPsiteRoom
    {
        [NotMapped]
        public IFormFile photo { get; set; }
    }
    [ModelMetadataType(typeof(TPsiteMetadata))]
    public partial class TPsite
    {
        [NotMapped]
        public IFormFile photo { get; set; }
    }
    [ModelMetadataType(typeof(TAdvertiseMetadata))]
    public partial class TAdvertise
    {
    }
    [ModelMetadataType(typeof(TAorderMetadata))]
    public partial class TAorder
    {
        [NotMapped]
        public string? advertiseName { get; set; }
        [NotMapped]
        public string? supplierName { get; set; }
    }
    [ModelMetadataType(typeof(TCategoryMetadata))]
    public partial class TCategory
    {
    }
    [ModelMetadataType(typeof(TCorderMetadata))]
    public partial class TCorder
    {
    }
    [ModelMetadataType(typeof(TCorderDetailMetadata))]
    public partial class TCorderDetail
    {
    }
    [ModelMetadataType(typeof(TCouponMetadata))]
    public partial class TCoupon
    {
    }
    [ModelMetadataType(typeof(TCustomerMetadata))]
    public partial class TCustomer
    {
    }
    [ModelMetadataType(typeof(TEvaluationMetadata))]
    public partial class TEvaluation
    {
    }
    [ModelMetadataType(typeof(TManagerMetadata))]
    public partial class TManager
    {
    }
    [ModelMetadataType(typeof(TSupplierMetadata))]
    public partial class TSupplier
    {
    }
}
