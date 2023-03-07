using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace YogIT.LargeAzFileUpload.Models
{
    [Table("YogITLargeAzFileUpload")]
    public class LargeAzFileUpload : IAuditable
    {
        [Key]
        public int LargeAzFileUploadId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
