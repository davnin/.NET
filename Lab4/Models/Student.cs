﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab4.Models
{
    public class Student
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Required]
        [Display(Name = "Last Name")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }


        public string FullName
        {
            get
            {
                return LastName + " , " + FirstName;
            }
        }

        public ICollection<CommunityMembership> Memberships { get; set; }

    }
}