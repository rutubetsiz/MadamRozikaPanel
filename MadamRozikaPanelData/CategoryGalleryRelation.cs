//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MadamRozikaPanelData
{
    using System;
    using System.Collections.Generic;
    
    public partial class CategoryGalleryRelation
    {
        public int CategoryGalleryId { get; set; }
        public Nullable<int> GalleryId { get; set; }
        public Nullable<int> CategoryId { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Gallery Gallery { get; set; }
    }
}
