//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteDienNghien.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class cart_product
    {
        public int cartid { get; set; }
        public int productid { get; set; }
        public int quantity { get; set; }
    
        public virtual cart cart { get; set; }
        public virtual product product { get; set; }
    }
}
