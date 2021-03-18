namespace KhaoPiyo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DataCopy")]
    public partial class DataCopy
    {
        [Key]
        [StringLength(50)]
        public string TableName { get; set; }

        public byte? iComp_Cd { get; set; }

        public byte? iBus_Cd { get; set; }
    }
}
