import { Routes } from '@angular/router';
import { Home } from './home/home';

export const routes: Routes = [
  { path: '', component: Home, pathMatch: 'full' },
  { path: 'ReportDesigner', loadComponent: () => import('./reportdesigner/report-designer').then(m => m.ReportDesigner) },
  { path: 'DocumentViewer', loadComponent: () => import('./reportviewer/report-viewer').then(m => m.ReportViewer) }
];