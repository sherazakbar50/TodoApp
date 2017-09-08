using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserToDoApp.ViewModels
{
    public class UserToDoListVm
    {
        public int utd_id { get; set; }
        public int utd_order { get; set; }
        public string utd_title { get; set; }
        public Nullable<System.DateTime> utd_date { get; set; }
        public string utd_priority { get; set; }
        public Nullable<System.DateTime> utd_created_date { get; set; }
        public string utd_created_by { get; set; }
        public string date { get; set; }
    }
}