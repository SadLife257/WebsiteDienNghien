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
    
    public partial class cart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cart()
        {
            this.cart_product = new HashSet<cart_product>();
        }
    
        public int id { get; set; }
        public int accountid { get; set; }
        public System.DateTime datebegin { get; set; }
        public double total { get; set; }
        public bool isOrder { get; set; }
    
        public virtual account account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart_product> cart_product { get; set; }
    }
}