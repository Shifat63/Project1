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
    
    public partial class tbl_UserType
    {
        public tbl_UserType()
        {
            this.tbl_UserInfo = new HashSet<tbl_UserInfo>();
        }

        [Required]
        [Display(Name = "User Type ID")]
        public int UserTypeID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "User Type")]
        public string UserTypeName { get; set; }
    
        public virtual ICollection<tbl_UserInfo> tbl_UserInfo { get; set; }
    }
}
