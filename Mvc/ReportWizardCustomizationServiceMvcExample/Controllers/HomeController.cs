using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using DevExpress.DataAccess.Sql;

namespace ReportWizardCustomizationServiceMvcExample.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult Designer() {
            Models.ReportDesignerModel model = new Models.ReportDesignerModel();
            // Create a SQL data source with the specified connection string.
            SqlDataSource ds = new SqlDataSource("NWindConnectionString");
            // Create a SQL query to access the Products data table.
            SelectQuery query = SelectQueryFluentBuilder.AddTable("Products").SelectAllColumnsFromTable().Build("Products");
            ds.Queries.Add(query);
            ds.RebuildResultSchema();
            model.DataSources = new Dictionary<string, object>();
            model.DataSources.Add("Northwind", ds);
            return View(model);
        }

        public ActionResult Viewer() {
            return View();
        }
    }
}
