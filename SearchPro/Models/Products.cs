using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SearchPro.Models
{
    public class Products
    {
        [Key]
        [Display(Name = "Product ID")]
        public int ProductID { get; set; }
        [Display(Name = "Gym ID")]
        public int GymID { get; set; }
        [Display(Name = "Name:")]
        public string ProductName { get; set; }
        [Display(Name = "How Much?")]
        public int ProductPrice { get; set; }
        [Display(Name = "Rate:")]
        public int ProductRate { get; set; }
        [Display(Name = "Type:")]
        public string ProductType { get; set; }

        [ForeignKey("GymID")]
        public virtual Gyms GYMS { get; set; }
    }
}