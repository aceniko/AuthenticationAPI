using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Data.Models
{
    [Table("Device")]
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeviceId { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string? ActivationCode { get; set; }
        public byte[]? SecretKey { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; set; }
        public int Status { get; set; }
        public int TokenId { get; set; }
        public Token Token { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    
    }
}
