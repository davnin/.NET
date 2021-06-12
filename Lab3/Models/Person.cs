using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3NET.Models
{
    public class Person
    {
        [Required]
        public string fName
        {
            get;
            set;
        }

        [Required]
        public string lName
        {
            get;
            set;
        }

        [Required]
        public int age
        {
            get;
            set;
        }

        [Required]
        public string email
        {
            get;
            set;
        }

        [Required]
        public int dofb
        {
            get;
            set;
        }

        [Required]
        public int password
        {
            get;
            set;
        }

        [Required]
        public string descrpt
        {
            get;
            set;
        }
    }
}