import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { WizardPageBase } from '@devexpress/analytics-core/analytics-wizard';
import { addToBindingsCache } from '@devexpress/analytics-core/analytics-utils'
import * as ko from 'knockout';
import * as $ from 'jquery';

@Component({
    selector: 'report-designer',
    encapsulation: ViewEncapsulation.None,
    templateUrl: './report-designer.html',
    styleUrls: [
        '../../../node_modules/ace-builds/css/ace.css',
        '../../../node_modules/ace-builds/css/theme/dreamweaver.css',
        '../../../node_modules/ace-builds/css/theme/ambiance.css',
        "../../../node_modules/devextreme/dist/css/dx.material.blue.light.css",
        "../../../node_modules/@devexpress/analytics-core/dist/css/dx-analytics.common.css",
        "../../../node_modules/@devexpress/analytics-core/dist/css/dx-analytics.material.blue.light.css",
        "../../../node_modules/@devexpress/analytics-core/dist/css/dx-querybuilder.css",
        "../../../node_modules/devexpress-reporting/dist/css/dx-webdocumentviewer.css",
        "../../../node_modules/devexpress-reporting/dist/css/dx-reportdesigner.css"
    ]
})


export class ReportDesignerComponent {
    getDesignerModelAction = "DXXRD/GetDesignerModel";
    reportUrl = "TestReport";

    OnCustomizeReportWizard(event) {
        this.CustomizeReportWizard(event.args);
    }

    beforeInitializeWizard(args) {
        class LabelPage extends WizardPageBase {
            canNext() {
                return false;
            }

            canFinish() {
                return true;
            }

            commit() {
                return $.Deferred().resolve({
                    Label1: this.label1Value(),
                    Label2: this.label2Value(),
                    Label3: this.label3Value(),
                    ReportName: this.label4Value(),
                }).promise();
            }

            label1Value = ko.observable('Label1');
            label2Value = ko.observable('Label2');
            label3Value = ko.observable('Label3');
            label4Value = ko.observable('SomeReportName');
        }
        args.wizard.pageFactory.registerMetadata('CustomLabelPage', {
            create: () => new LabelPage(),
            getState: (state) => state,
            setState: (data, state) => {
                state.customData = JSON.stringify(data);
            },
            resetState: (state, defaultState) => {
                state.customData = undefined;
            },
            template: 'wizard-labels-page',
            navigationPanelText: 'Specify label values'
        });
    }

    afterInit(args) {
        const defaultGetNextPageId = args.wizard.iterator.getNextPageId;
        args.wizard.iterator.getNextPageId = function (pageId) {
            if (pageId === 'selectReportTypePage' && args.wizard.iterator._getCurrentState().reportTemplateID === 'CustomLabelReport') {
                return 'CustomLabelPage';
            } else {
                return defaultGetNextPageId.apply(this, [pageId]);
            }
        };
    }

    beforeFinishWizard(args) {
        args.state.customData = args.state.customData || 'CustomizeEmptyReport';
    }

    CustomizeReportWizard(event) {
        if (event.Type === 'ReportWizard') {
           addToBindingsCache('dxTextBox: { value: label4Value }', function ($context, $element) { return { 'dxTextBox': function () { return { 'value': $context.$data.label4Value } } } });
           addToBindingsCache('dxTextBox: { value: label1Value }', function ($context, $element) { return { 'dxTextBox': function () { return { 'value': $context.$data.label1Value } } } });
           addToBindingsCache('dxTextBox: { value: label2Value }', function ($context, $element) { return { 'dxTextBox': function () { return { 'value': $context.$data.label2Value } } } });
           addToBindingsCache('dxTextBox: { value: label3Value }', function ($context, $element) { return { 'dxTextBox': function () { return { 'value': $context.$data.label3Value } } } });
            event.Wizard.events.addHandler('beforeInitialize', this.beforeInitializeWizard);
            event.Wizard.events.addHandler('afterInitialize', this.afterInit);
            event.Wizard.events.addHandler('beforeFinish', this.beforeFinishWizard);
        }
    }

    constructor(@Inject('BASE_URL') public hostUrl: string) { }
}






