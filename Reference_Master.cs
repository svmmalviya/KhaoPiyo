namespace KhaoPiyo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reference_Master
    {
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal AutoCode { get; set; }

        [Key]
        [StringLength(250)]
        public string Reference_Id { get; set; }

        [StringLength(500)]
        public string sError { get; set; }
    }
}
