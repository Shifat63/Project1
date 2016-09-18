using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project1.Models
{
    public class DepositSearch
    {
        [DataType(DataType.Text)]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "From")]
        public String StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "To")]
        public String EndDate { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Verification Status")]
        public String IsVerified { get; set; }
    }
}