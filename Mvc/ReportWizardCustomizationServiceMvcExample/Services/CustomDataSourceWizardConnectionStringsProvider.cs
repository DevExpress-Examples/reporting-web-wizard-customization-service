using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using DevExpress.DataAccess.Web;
using DevExpress.DataAccess.ConnectionParameters;
using System.Web;

namespace ReportWizardCustomizationServiceMvcExample.Services
{
    public class CustomDataSourceWizardConnectionStringsProvider : IDataSourceWizardConnectionStringsProvider {
        Dictionary<string, string> connectionStrings;
        public CustomDataSourceWizardConnectionStringsProvider() {
            connectionStrings = new Dictionary<string, string>();
            connectionStrings.Add("Northwind", ConfigurationManager.ConnectionStrings["NWindConnectionString"].ConnectionString);
        }
        Dictionary<string, string> IDataSourceWizardConnectionStringsProvider.GetConnectionDescriptions() {
            return connectionStrings.ToDictionary(k => k.Key, k => k.Key);
        }
        DataConnectionParametersBase IDataSourceWizardConnectionStringsProvider.GetDataConnectionParameters(string name) {
            return new CustomStringConnectionParameters(connectionStrings[name]);
        }
    }
}
