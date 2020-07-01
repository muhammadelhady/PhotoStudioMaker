using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Model
{
    public class GalleryPhoto
    {
        [Key]
        public int PhotoId { get; set; }
        public string PhotoName { get; set; }
        public string PhotoLocation { get; set; }
        public int PhotoViewCounter { get; set; }
        public Category Category { get; set; }
        [ForeignKey("Category")]
        public int CategoryFK { get; set; }
    }
}
