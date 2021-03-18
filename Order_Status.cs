namespace KhaoPiyo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_Status
    {
        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal AutoCode { get; set; }

        [StringLength(50)]
        public string Store_Id { get; set; }

        [StringLength(50)]
        public string Channel { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UPR_Order_Id { get; set; }

        [StringLength(50)]
        public string Order_Id { get; set; }

        [StringLength(250)]
        public string dUpdate_Dt { get; set; }

        [Column("Order_Status")]
        [StringLength(250)]
        public string Order_Status1 { get; set; }

        [StringLength(250)]
        public string Order_Message { get; set; }

        [StringLength(50)]
        public string Order_Status_Pre { get; set; }
    }
}
