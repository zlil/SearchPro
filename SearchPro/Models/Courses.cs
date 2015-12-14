using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SearchPro.Models
{
    public class Courses
    {

        [Key]
        [Display(Name = "Course ID")]
        public int courseID { get; set; }
        [Display(Name = "Gym ID")]
        public int GymID { get; set; }
        [Display(Name = "Name:")]
        public string courseName { get; set; }
        [Display(Name = "Starts At:")]
        public string courseTime { get; set; }
        [Display(Name = "Minutes")]
        public int courseLength { get; set; }
        [Display(Name = "Instructor:")]
        public string courseInstructor { get; set; }
        [Display(Name = "Type:")]
        public string courseType { get; set; }

        [ForeignKey("GymID")]
        public virtual Gyms GYMS { get; set; }
    }
}