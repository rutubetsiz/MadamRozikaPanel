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
    
    public partial class Headline
    {
        public int HeadlineId { get; set; }
        public int ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlPrivate { get; set; }
        public int UserId { get; set; }
        public string Site { get; set; }
        public System.DateTime DateTime { get; set; }
        public Nullable<short> Rank { get; set; }
        public Nullable<byte> Status { get; set; }
    }
}