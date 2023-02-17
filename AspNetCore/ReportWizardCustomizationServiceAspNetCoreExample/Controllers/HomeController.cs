using System.Collections.Generic;
using System.Threading.Tasks;
using DevExpress.DataAccess.Sql;
using Microsoft.AspNetCore.Mvc;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.ReportDesigner;
using DevExpress.AspNetCore.Reporting.QueryBuilder;
using DevExpress.AspNetCore.Reporting.ReportDesigner;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer;


namespace ReportWizardCustomizationServiceAspNetCoreExample.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult Error() {
            Models.ErrorModel model = new Models.ErrorModel();
            return View(model);
        }

        public async Task<IActionResult> Designer(
            [FromServices] IReportDesignerClientSideModelGenerator clientSideModelGenerator,
            [FromQuery] string reportName) {
            Models.ReportDesignerCustomModel model = new Models.ReportDesignerCustomModel();
            model.ReportDesignerModel = await CreateDefaultReportDesignerModel(clientSideModelGenerator, reportName, null);
            model.ReportDesignerModel.DataSourceSettings.AllowAddDataSource = false;
            model.ReportDesignerModel.DataSourceSettings.AllowEditDataSource = false;
            model.ReportDesignerModel.DataSourceSettings.AllowRemoveDataSource = false;
            return View(model);
        }

         public static Dictionary<string, object> GetAvailableDataSources() {
            var dataSources = new Dictionary<string, object>();
            // Create a SQL data source with the specified connection string.
            SqlDataSource ds = new SqlDataSource("NWindConnectionString");
            // Create a SQL query to access the Products data table.
            SelectQuery query = SelectQueryFluentBuilder.AddTable("Products").SelectAllColumnsFromTable().Build("Products");
            ds.Queries.Add(query);
            ds.RebuildResultSchema();
            dataSources.Add("Northwind", ds);
            return dataSources;
        }

        public static async Task<ReportDesignerModel> CreateDefaultReportDesignerModel(IReportDesignerClientSideModelGenerator clientSideModelGenerator, string reportName, XtraReport report) {
            reportName = string.IsNullOrEmpty(reportName) ? "TestReport" : reportName;
            var dataSources = GetAvailableDataSources();
            if(report != null) {
                return await clientSideModelGenerator.GetModelAsync(report, dataSources, ReportDesignerController.DefaultUri, WebDocumentViewerController.DefaultUri, QueryBuilderController.DefaultUri);
            }
            return await clientSideModelGenerator.GetModelAsync(reportName, dataSources, ReportDesignerController.DefaultUri, WebDocumentViewerController.DefaultUri, QueryBuilderController.DefaultUri);
        }
    }
}
