<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/602963716/2022.2)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1148046)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Reporting for Web (ASP.NET MVC, ASP.NET Core and Angular) - How to Use the Report Wizard Customization API and Hide Data Source Actions in Report Designer

This example shows how to add a custom report template to the Report Wizard and make final adjustments to the report you are creating. The Report designer is configured to hide data source actions so that the user cannot add, modify, or delete the report data source.

The example includes separate projects for ASP.NET MVC, ASP.NET Core and Angular client application with ASP.NET Core backend.

The implementation is based on the following classes:
 
- The [ReportWizardCustomizationService](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.ReportDesigner.Services.ReportWizardCustomizationService) class descendant is implemented and registered as a service to customize the Report Wizard.

The Report Designer `CustomizeWizard` event is handled to register a custom wizard page. The `reportWizardCustomization.js` file contains JavaScript code required for registration.

- The [ReportDesignerDataSourceSettings](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.ReportDesigner.ReportDesignerDataSourceSettings) class contains settings that allow you to hide data source actions from the Field List panel.
 
After you run the application, invoke the **Report Wizard** to see new `Instant Report` and `Custom Label Report` templates:

![Report Wizard with Custom Template](Images/template.png)

In the Report Designer, switch to the **Field List** panel to make sure that data source actions are hidden:

![Report Designer Field List with Hidden Actions](Images/field-list-actions.png)


## Files to Review

### Service that Customizes the Report Wizard

- [InstantReportWizardCustomizationService.cs](Mvc/ReportWizardCustomizationServiceMvcExample/Services/InstantReportWizardCustomizationService.cs)

### Service Registration

- ASP.NET MVC: [Global.asax.cs](Mvc/ReportWizardCustomizationServiceMvcExample/Global.asax.cs)
- ASP.NET Core: [Startup.cs](AspNetCore/RWCSAspNetCoreExample/Startup.cs)

### Custom Wizard Page for the Custom Label Report

- ASP.NET MVC: [Designer.cshtml](Mvc/ReportWizardCustomizationServiceMvcExample/Views/Home/Designer.cshtml)
- ASP.NET Core: [Designer.cshtml](AspNetCore/RWCSAspNetCoreExample/Views/Home/Designer.cshtml)
- Angular: [report-designer.html](Angular/RWCSAngularExample/ClientApp/src/app/reportdesigner/report-designer.html) and[report-designer.ts](Angular/RWCSAngularExample/ClientApp/src/app/reportdesigner/report-designer.ts)
- [reportWizardCustomization.js](Mvc/ReportWizardCustomizationServiceMvcExample/Scripts/reportWizardCustomization.js)
- [LabelReport.cs](Mvc/ReportWizardCustomizationServiceMvcExample/PredefinedReports/LabelReport.cs)

### Report Designer Data Source Settings

- ASP.NET MVC: [Designer.cshtml](Mvc/ReportWizardCustomizationServiceMvcExample/Views/Home/Designer.cshtml)
- ASP.NET Core: [HomeController.cs](AspNetCore/RWCSAspNetCoreExample/Controllers/HomeController.cs)
- Angular: [report-designer.html](Angular/RWCSAngularExample/ClientApp/src/app/reportdesigner/report-designer.html)

## Documentation

- [ReportWizardCustomizationService](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.ReportDesigner.Services.ReportWizardCustomizationService)
- [ReportDesignerDataSourceSettings](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.ReportDesigner.ReportDesignerDataSourceSettings)
- [Customize the Report/Data Source Wizard (ASP.NET MVC)](https://docs.devexpress.com/XtraReports/401087/web-reporting/asp-net-mvc-reporting/end-user-report-designer-in-asp-net-mvc-applications/customization/customize-the-report-data-source-wizard)
- [Customize the Report Wizard and Data Source Wizard (ASP.NET Core)](https://docs.devexpress.com/XtraReports/401088/web-reporting/asp-net-core-reporting/end-user-report-designer-in-asp-net-applications/customize-the-report-designer/customize-the-report-wizard-and-data-source-wizard)

## More Examples

- [Reporting for Web - How to customize the Web Report Wizard](https://github.com/DevExpress-Examples/Reporting-Customize-Web-Report-Wizard)

