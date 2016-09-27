//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    
    public partial class tbl_Deposit
    {
        [Remote("IsPaymentIDExist", "Validation", HttpMethod = "POST", ErrorMessage = "Payment ID already exists. Please enter a different one.")]
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Payment ID")]
        public string PaymentID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Required]
        [Range(0.01, 100000000.00, ErrorMessage = "Amount must be greater than 0")]
        [Display(Name = "Deposit Amount")]
        public double Amount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Payment Date")]
        public System.DateTime PaymentDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Entry Date")]
        public System.DateTime EntryDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Source of Fund")]
        public string SourceOfFund { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Required]
        [Display(Name = "Verification Status")]
        public bool IsVerified { get; set; }
    
        public virtual tbl_UserInfo tbl_UserInfo { get; set; }
    }
}
