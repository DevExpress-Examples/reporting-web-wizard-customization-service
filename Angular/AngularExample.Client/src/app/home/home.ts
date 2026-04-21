import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.html',
})
export class Home {
  protected readonly links = [
    { url: 'https://docs.devexpress.com/XtraReports/2162/index', text: 'Reporting' },
    { url: 'https://docs.devexpress.com/XtraReports/9814/web-reporting', text: 'Web Reporting' },
    { url: 'https://docs.devexpress.com/XtraReports/401914/web-reporting/javascript-reporting/angular', text: 'Reporting for Angular' },
    { url: 'https://docs.devexpress.com/XtraReports/400292/web-reporting/javascript-reporting/angular/document-viewer/quick-start/document-viewer-client-side-configuration-angular', text: 'Web Document Viewer Client-Side Configuration' },
    { url: 'https://docs.devexpress.com/XtraReports/400295/web-reporting/javascript-reporting/angular/report-designer/quick-start/report-designer-client-side-configuration-angular', text: 'Report Designer Client-Side Configuration' },
    { url: 'https://docs.devexpress.com/XtraReports/400196/web-reporting/javascript-reporting/server-side-configuration/report-designer/report-designer-server-side-configuration-asp-net-core', text: 'Report Designer Server-Side Configuration (ASP.NET Core)' },
    { url: 'https://docs.devexpress.com/XtraReports/401726/web-reporting/general-information-on-web-reporting/troubleshooting', text: 'Troubleshooting' }
  ] as const;
}
