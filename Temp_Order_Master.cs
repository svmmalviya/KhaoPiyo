namespace KhaoPiyo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Temp_Order_Master
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal AutoCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Biz_Id { get; set; }

        [StringLength(50)]
        public string Store_Id { get; set; }

        [StringLength(50)]
        public string Channel { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UPR_Order_Id { get; set; }

        [StringLength(50)]
        public string Order_Id { get; set; }

        public double? Order_Subtotal { get; set; }

        public double? Order_Tax { get; set; }

        public double? Order_Total { get; set; }

        [StringLength(50)]
        public string OrderTime { get; set; }

        [StringLength(50)]
        public string DeliveryTime { get; set; }

        public byte? OrderValue { get; set; }

        public double? Order_Charges { get; set; }

        [StringLength(250)]
        public string Order_Coupon { get; set; }

        [StringLength(250)]
        public string Order_Instructions { get; set; }

        [StringLength(250)]
        public string Charges_Title { get; set; }

        public double? Charges_Value { get; set; }

        public double? Order_Discount { get; set; }

        public double? External_Discount { get; set; }

        [StringLength(50)]
        public string Order_Type { get; set; }

        [StringLength(50)]
        public string Delivery_Type { get; set; }

        [StringLength(50)]
        public string Payment_Type { get; set; }
    }
}
