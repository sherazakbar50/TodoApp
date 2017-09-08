using UserToDoApp.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DBG = System.Diagnostics.Debug;

namespace UserToDoApp.Data.Core
{
    public abstract class BaseActions<Entity> where Entity : new()
    {
        DataAccess _da = null;
        internal void SetContainer(DataAccess parent)

        {
            DBG.Assert(parent != null);
            _da = parent;
        }

        internal UserToDoAppDBEntities ctx { get { return _da.Ctx; } }
        // internal NLog.Logger Log{ get{ return _da.Log; }}

        internal void Save() { _da.Save(); }

        public abstract IQueryable<Entity> GetAll();
        public abstract Entity Get(long id);
        public abstract void Delete(long id);
        public abstract void Change(Entity t, Guid by);
        public abstract void Add(Entity t, Guid by);


        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

    }
}
