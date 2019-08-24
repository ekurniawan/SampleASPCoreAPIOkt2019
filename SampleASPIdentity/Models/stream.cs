using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleASPIdentity.Models
{
    public class stream
    {
        [Key]
        public int streamid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string userId { get; set; }
    }
}
