using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project1.Models
{
    public class SavePDF
    {
        public int SearchStatus { get; set; }
        public DepositSearch depositSearch { get; set; }
    }
}