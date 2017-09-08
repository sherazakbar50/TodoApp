using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserToDoApp.Data.Core
{
    internal class ConnectionStringBuilder
    {
        const string K_PROVIDER = "System.Data.SqlClient";
        const string K_MODEL = @"metadata=res://*/Model.UserToDoAppModel.csdl|res://*/Model.UserToDoAppModel.ssdl|res://*/Model.UserToDoAppModel.msl";

        string _cnnstr = string.Empty;

        internal ConnectionStringBuilder(string connectionstr)
        {
            _cnnstr = connectionstr;
        }
        string Build(string cnnstr, string modelMetadata)
        {
            cnnstr = AppendParam(cnnstr, "multipleactiveresultsets", "True");
            cnnstr = AppendParam(cnnstr, "App", System.Reflection.Assembly.GetExecutingAssembly().FullName.Split(',')[0]);

            EntityConnectionStringBuilder builder = new EntityConnectionStringBuilder
            {
                Provider = K_PROVIDER,
                ProviderConnectionString = cnnstr,
                Metadata = modelMetadata
            };

            return builder.ToString();
        }

        internal string UserToDoAPPString { get { return Build(_cnnstr, K_MODEL); } }

        public override string ToString() { return UserToDoAPPString; }

        string AppendParam(string core, string name, string value)
        {
            if (!core.Contains(name))
                core += core.EndsWith(";") ? string.Format("{0}={1}", name, value) : string.Format(";{0}={1}", name, value);
            return core;
        }
    }
}
