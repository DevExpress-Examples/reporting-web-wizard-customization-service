using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.ReportDesigner.DataContracts;
using DevExpress.XtraReports.Web.ReportDesigner.Services;
using DevExpress.XtraReports.Wizards;
using ReportWizardCustomizationServiceAspNetCoreExample.PredefinedReports;
using System;
using System.Linq;
using System.Threading.Tasks;

public class InstantReportWizardCustomizationService : ReportWizardCustomizationService {
    public enum CustomReportType {
        CustomLabelReport,
        InstantReport
    }

    public override XtraReport TryCreateCustomReport(XtraReportModel model, object dataSource, string dataMember, CustomWizardData customWizardData, XtraReport report) {
        if(Enum.TryParse(customWizardData.ReportTemplateID, out CustomReportType customReportType)) {
            if(customReportType == CustomReportType.InstantReport) {
                return new InstantReport() {
                    Name = customWizardData.ReportTemplateID,
                    DisplayName = customWizardData.ReportTemplateID
                };
            } else if(customReportType == CustomReportType.CustomLabelReport) {
                var labelReport = new LabelReport();
                labelReport.ApplyCustomData(customWizardData.Data);
                return labelReport;
            }
        }
        return base.TryCreateCustomReport(model, dataSource, dataMember, customWizardData, report);
    }
    public override Task<XtraReport> TryCreateCustomReportAsync(XtraReportModel model, object dataSource, string dataMember, CustomWizardData customWizardData, XtraReport report) {
        return Task.FromResult(TryCreateCustomReport(model, dataSource, dataMember, customWizardData, report));
    }
    public override void CustomizeReportTypeList(ReportWizardTemplateCollection predefinedTypes) {
        predefinedTypes.Remove(predefinedTypes.Where(x => x.ID == nameof(ReportType.CrossTab)).First());
        predefinedTypes.Add(new DevExpress.XtraReports.Web.ReportDesigner.DataContracts.ReportWizardTemplate() {
            CanInstantlyFinish = true,
            ID = nameof(CustomReportType.InstantReport),
            Text = "Instant Report",
            ImageTemplateName = "instant-report"
        });
        predefinedTypes.Add(new DevExpress.XtraReports.Web.ReportDesigner.DataContracts.ReportWizardTemplate() {
            CanInstantlyFinish = true,
            ID = nameof(CustomReportType.InstantReport),
            Text = "Instant Report",
            ImageClassName = "instant-report-image"
        });
        predefinedTypes.Add(new DevExpress.XtraReports.Web.ReportDesigner.DataContracts.ReportWizardTemplate() {
            ID = nameof(CustomReportType.CustomLabelReport),
            Text = "Custom Label Report",
            ImageTemplateName = "dxrd-svg-wizard-LabelReport"
        });
    }

    public override Task CustomizeReportTypeListAsync(ReportWizardTemplateCollection predefinedTypes) {
        CustomizeReportTypeList(predefinedTypes);
        return Task.CompletedTask;
    }

    public override void CustomizeReportOnFinish(XtraReport report) {
        if (report.Bands.GetBandByType(typeof(ReportHeaderBand)) == null)
            report.Bands.Add(new ReportHeaderBand() {
                Controls = {
                    new XRLabel() {
                        Text = string.Format("Instant Report {0:MMM dd}", System.DateTime.Today),
                        SizeF= new System.Drawing.SizeF(650F, 100F),
                        Font = new DevExpress.Drawing.DXFont("Arial", 24)

    } }
            }); ; ;
    }

    public override Task CustomizeReportOnFinishAsync(XtraReport report) {
        CustomizeReportOnFinish(report);
        return Task.CompletedTask;
    }
}
