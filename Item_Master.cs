namespace KhaoPiyo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item_Master
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

        [StringLength(50)]
        public string Item_Cd { get; set; }

        [StringLength(250)]
        public string Item_Name { get; set; }

        public double? Rate { get; set; }

        public double? Qty { get; set; }

        public double? Amount { get; set; }

        public double? Item_Tax { get; set; }

        public double? Item_Total { get; set; }

        public double? Charges { get; set; }

        public double? Discount { get; set; }

        [StringLength(50)]
        public string Food_Type { get; set; }

        [StringLength(250)]
        public string Options_To_Add_Title { get; set; }

        [StringLength(250)]
        public string Options_To_Add_Rate { get; set; }

        [StringLength(250)]
        public string Options_To_Remove { get; set; }
    }
}
