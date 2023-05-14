using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultiTracks.API.Models
{
    public class ArtistModel
    {
        
        public int artistID { get; set; }
        [Required]
        public string title { get; set; }
        public string dateCreation { get; set; }
        [Required]
        public string biography { get; set; }
        [Required]
        [Url]
        public string imageURL { get; set; }
        [Required]
        [Url]
        public string heroURL { get; set; }
    }
}