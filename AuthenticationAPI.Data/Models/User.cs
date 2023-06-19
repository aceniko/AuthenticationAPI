using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string? Name { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string ExternalId { get; set; }

        public Device Device { get; set; }
        
    }
}
