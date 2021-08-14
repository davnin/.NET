using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class Advertisements
    {
        [Key]
        public int AdId 
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "File Name")]
        public string FileName
        {
            get; set;
        }
        
        [Required]
        [Url]
        public string Url
        {
            get; set;
        }

        /*[Required]
        public Question Question
        {
            get; set;
        }*/
    }
}
