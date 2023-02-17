using DevExpress.Compatibility.System.Web;
using DevExpress.DataAccess.Sql;
using System.Collections.Generic;
using DevExpress.AspNetCore.Reporting.QueryBuilder;
using DevExpress.AspNetCore.Reporting.ReportDesigner;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer.Native.Services;
using DevExpress.AspNetCore.Reporting.ReportDesigner.Native.Services;
using DevExpress.AspNetCore.Reporting.QueryBuilder.Native.Services;
using DevExpress.XtraReports.Web.ReportDesigner;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ReportWizardCustomizationServiceAngularExample.Controllers {
    public class CustomWebDocumentViewerController : WebDocumentViewerController {
        public CustomWebDocumentViewerController(IWebDocumentViewerMvcControllerService controllerService) : base(controllerService) {
        }
    }

    public class CustomReportDesignerController : ReportDesignerController {
        public CustomReportDesignerController(IReportDesignerMvcControllerService controllerService) : base(controllerService) {
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetDesignerModel([FromForm]string reportUrl, [FromForm] ReportDesignerSettingsBase designerModelSettings, [FromServices] IReportDesignerClientSideModelGenerator modelGenerator) {
            var dataSources = new Dictionary<string, object>();
            var ds = new SqlDataSource("NWindConnectionString");

            // Create a SQL query to access the Products data table.
            SelectQuery query = SelectQueryFluentBuilder.AddTable("Products").SelectAllColumnsFromTable().Build("Products");
            ds.Queries.Add(query);
            ds.RebuildResultSchema();
            dataSources.Add("Northwind", ds);

            var model = await modelGenerator.GetModelAsync(reportUrl, dataSources, ReportDesignerController.DefaultUri, WebDocumentViewerController.DefaultUri, QueryBuilderController.DefaultUri);
            model.Assign(designerModelSettings);
            return DesignerModel(model);
        }
    }

    public class CustomQueryBuilderController : QueryBuilderController {
        public CustomQueryBuilderController(IQueryBuilderMvcControllerService controllerService) : base(controllerService) {
        }
    }
}
