using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserToDoApp.Data.Model
{
    public partial class UserToDoAppDBEntities : DbContext
    {
        public UserToDoAppDBEntities(string cnnstr) : base(cnnstr) { }
    }
}
