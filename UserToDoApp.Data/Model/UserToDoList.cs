//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserToDoApp.Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserToDoList
    {
        public int utd_id { get; set; }
        public int utd_order { get; set; }
        public string utd_title { get; set; }
        public Nullable<System.DateTime> utd_date { get; set; }
        public string utd_priority { get; set; }
        public Nullable<System.DateTime> utd_created_date { get; set; }
        public string utd_created_by { get; set; }
    }
}