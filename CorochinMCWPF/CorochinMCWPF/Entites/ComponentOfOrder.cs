//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CorochinMCWPF.Entites
{
    using System;
    using System.Collections.Generic;
    
    public partial class ComponentOfOrder
    {
        public int ComponentId { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }
    
        public virtual Component Component { get; set; }
        public virtual Order Order { get; set; }
    }
}
