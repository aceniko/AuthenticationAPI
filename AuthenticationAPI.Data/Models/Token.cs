using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Data.Models
{
    public class Token
    {
        public int TokenId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string TokenSerial { get; set; }

        public bool IsActive { get; set; }
        public Device Device { get; set; }

    }
}
