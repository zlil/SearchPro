using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SearchPro.Models
{
    public class Gyms
    {
        [Key]
        [Display(Name = "Gym ID")]
        public int GymID { get; set; }
        [Display(Name = "Name:")]
        public string GymName { get; set; }
        [Display(Name = "How Much?")]
        public int GymPrice { get; set; }
        [Display(Name = "Address:")]
        public string GymAddress { get; set; }
        [Display(Name = "Open At:")]
        public string GymOpenTime { get; set; }
        [Display(Name = "Close At:")]
        public string GymCloseTime { get; set; }

        public virtual ICollection<Courses> COURSES { get; set; }
        public virtual ICollection<Products> PRODUCTS { get; set; }

    }
}