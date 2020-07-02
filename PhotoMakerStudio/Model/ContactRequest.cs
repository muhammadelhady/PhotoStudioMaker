using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Model
{
    public class ContactRequest
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string BusinessType { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PhotoType { get; set; }
        public string NumbersOfPhotos { get; set; }
        public string PhotoBackground { get; set; }
        public string AttachedFileLocation { get; set; }
    }
}
