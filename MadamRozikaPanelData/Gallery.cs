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
    
    public partial class Gallery
    {
        public Gallery()
        {
            this.CategoryGalleryRelations = new HashSet<CategoryGalleryRelation>();
            this.CommentGalleryRelations = new HashSet<CommentGalleryRelation>();
            this.TagGalleryRelations = new HashSet<TagGalleryRelation>();
        }
    
        public int GalleryId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string TitleUrl { get; set; }
        public string SeoTitle { get; set; }
        public string SeoTitleUrl { get; set; }
        public System.DateTime PublishDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public byte Status { get; set; }
        public int ItemCount { get; set; }
        public string ImageUrl { get; set; }
        public string Tags { get; set; }
        public string FilePath { get; set; }
    
        public virtual ICollection<CategoryGalleryRelation> CategoryGalleryRelations { get; set; }
        public virtual ICollection<CommentGalleryRelation> CommentGalleryRelations { get; set; }
        public virtual ICollection<TagGalleryRelation> TagGalleryRelations { get; set; }
    }
}
