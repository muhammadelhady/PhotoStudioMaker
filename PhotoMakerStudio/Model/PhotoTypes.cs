using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Model
{
    public class PhotoTypes
    {
        [Key]
        public int Id { get; set; }
        public string PhotoTybe { get; set; }
    }
}
