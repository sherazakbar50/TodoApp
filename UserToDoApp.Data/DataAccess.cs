using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserToDoApp.Data.Core;
using UserToDoApp.Data.Model;
using UserToDoApp.Data.Actions;
using System.Net.Http;

namespace UserToDoApp.Data
{
    public class DataAccess
    {

        internal UserToDoAppDBEntities _ctx = null;
        static DataAccess self = null;
        //public object ModulePermission;

        private DataAccess()
        {
            var tmp = new ConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["UserToDoAppConnection"].ConnectionString);
            _ctx = new UserToDoAppDBEntities(tmp.UserToDoAPPString);

            // temporarily dissabling the EF entity validation - it should be removed in the next code sync.
            _ctx.Configuration.ValidateOnSaveEnabled = false;
        }
        public static DataAccess Instance
        {
            get
            {
                var context = HttpContext.Current;
                if (context != null)
                {
                    const string kApplicationSettings = "ApplicationObject";
                    if (context != null && context.Items[kApplicationSettings] != null)
                    {
                        var da = context.Items[kApplicationSettings] as DataAccess;
                        return da;
                    }

                    self = new DataAccess();
                    context.Items[kApplicationSettings] = self;
                }
                else
                {
                    self = new DataAccess();
                }
                return self;
            }
        }
        public void Save()
        {
            _ctx.SaveChanges();
        }
        internal Model.UserToDoAppDBEntities Ctx { get { return _ctx; } }
        public void Dispose()
        {
            if (self != null)
            {
                //DataAccess.Instance.Dispose();
                const string kApplicationSettings = "ApplicationObject";
                var context = HttpContext.Current;
                if (context != null && context.Items[kApplicationSettings] != null)
                    context.Items[kApplicationSettings] = null;
                _ctx.Dispose();
                _ctx = null;
                GC.SuppressFinalize(this);
                self = null;
            }
        }

        public UserToDoApp.Data.Model.UserToDoAppDBEntities Context
        {
            get
            {
                return this._ctx;
            }
        }

          public UserToDoListActions UsersToDoActions
        {
            get
            {
                UserToDoListActions Ans = new UserToDoListActions();
                Ans.SetContainer(this);
                return Ans;
            }
        }
    }
}
