using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Models
{
    public enum Question
    {
        Computer, Earth
    }
    public class AnswerImage
    {

        public int AnswerImageId
        {
            get; set;
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

        [Required]
        public Question Question
        {
            get; set;
        }


        /*  public IActionResult Index()
          {
              return View();
          }*/
    }
}
