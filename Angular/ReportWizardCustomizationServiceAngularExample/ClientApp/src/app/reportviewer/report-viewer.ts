import { Component, Inject, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'report-viewer',
  encapsulation: ViewEncapsulation.None,
  templateUrl: './report-viewer.html',
  styleUrls: [
    "../../../node_modules/devextreme/dist/css/dx.material.blue.light.css",
    "../../../node_modules/@devexpress/analytics-core/dist/css/dx-analytics.common.css",
    "../../../node_modules/@devexpress/analytics-core/dist/css/dx-analytics.material.blue.light.css",
    "../../../node_modules/devexpress-reporting/dist/css/dx-webdocumentviewer.css"
  ]
})
export class ReportViewerComponent {
  reportUrl: string = "TestReport";
  invokeAction: string = '/DXXRDV';

  constructor(@Inject('BASE_URL') public hostUrl: string) { }
}