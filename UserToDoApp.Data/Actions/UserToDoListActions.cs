using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserToDoApp.Data.Core;
using UserToDoApp.Data.Model;
using EntityFramework.Extensions;

namespace UserToDoApp.Data.Actions
{
   public class UserToDoListActions : BaseActions<UserToDoList>
    {
        public override IQueryable<UserToDoList> GetAll()
        {
            return ctx.UserToDoLists;
        }


        public override UserToDoList Get(long id)
        {
            return ctx.UserToDoLists.First(it => it.utd_id == id);
        }


        public override void Delete(long id)
        {
            ctx.UserToDoLists.Where(x => x.utd_id == id).Delete();
            Save();
        }
        public override void Change(UserToDoList t, Guid by)
        {
            Save();
        }

        public override void Add(UserToDoList t, Guid by)
        {
            ctx.UserToDoLists.Add(t);
            Save();
        }
    }
}
