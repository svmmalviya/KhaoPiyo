namespace KhaoPiyo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer_Master
    {
        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal AutoCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Biz_Id { get; set; }

        [StringLength(50)]
        public string Store_Id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UPR_Order_Id { get; set; }

        [StringLength(50)]
        public string Order_Id { get; set; }

        [StringLength(250)]
        public string Customer_Name { get; set; }

        [StringLength(250)]
        public string Customer_Address { get; set; }

        [StringLength(250)]
        public string Customer_Phone { get; set; }

        public string Customer_Email { get; set; }

        [StringLength(250)]
        public string Customer_Landmark { get; set; }
    }
}
