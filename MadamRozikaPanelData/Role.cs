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
    
    public partial class Role
    {
        public Role()
        {
            this.RoleUserRelations = new HashSet<RoleUserRelation>();
        }
    
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public Nullable<byte> IsActive { get; set; }
    
        public virtual ICollection<RoleUserRelation> RoleUserRelations { get; set; }
    }
}
