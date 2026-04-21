import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { DxReportDesignerModule, DxReportViewerModule } from 'devexpress-reporting-angular';

@Component({
  selector: 'report-viewer',
  encapsulation: ViewEncapsulation.None,
  templateUrl: './report-viewer.html',
  styleUrls: [
    "../../../node_modules/devextreme/dist/css/dx.material.blue.light.css",
    "../../../node_modules/@devexpress/analytics-core/dist/css/dx-analytics.common.css",
    "../../../node_modules/@devexpress/analytics-core/dist/css/dx-analytics.material.blue.light.css",
    "../../../node_modules/devexpress-reporting/dist/css/dx-webdocumentviewer.css"
  ],
  imports: [DxReportViewerModule, DxReportDesignerModule]
})
export class ReportViewer {
  protected readonly reportUrl: string = "TestReport";
  protected readonly invokeAction: string = '/DXXRDV';
  protected readonly getLocalizationAction: string = `${this.invokeAction}/GetLocalization`

  constructor(@Inject('BASE_URL') protected readonly hostUrl: string) { }
}
