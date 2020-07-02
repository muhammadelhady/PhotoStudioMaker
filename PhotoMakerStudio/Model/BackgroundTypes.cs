using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Model
{
    public class BackgroundTypes
    {
        [Key]
        public int Id { get; set; }
        public string BackgroundType  { get; set; }
    }
}
