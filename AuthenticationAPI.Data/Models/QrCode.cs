using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Data.Models
{
    [Table("QrCode")]
    public class QrCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QrCodeId { get; set; }
        public string Content { get; set; }
        public DateTime Expire { get; set; }
        public bool IsValidated { get; set; }
    }
}
