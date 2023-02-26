using DevExpress.XtraReports.UI;
using System.Text.Json;

namespace ReportWizardCustomizationServiceAspNetCoreExample.PredefinedReports {
    public partial class LabelReport : XtraReport {
        public LabelReport() {
            InitializeComponent();
        }

        public void ApplyCustomData(string customDataJson) {
            Labels labels = JsonSerializer.Deserialize<Labels>(customDataJson);
            xrLabel1.Text = labels.Label1;
            xrLabel2.Text = labels.Label2;
            xrLabel3.Text = labels.Label3;
            Name = labels.ReportName;
            DisplayName = labels.ReportName;
        }
    }

    public class Labels {

        public string Label1 { get; set; }

        public string Label2 { get; set; }

        public string Label3 { get; set; }

        public string ReportName { get; set; }
    }
}
