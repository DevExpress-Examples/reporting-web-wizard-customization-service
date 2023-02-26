function beforeInitializeWizard(args) {
    class LabelPage extends DevExpress.Analytics.Wizard.WizardPageBase {
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

function afterInit(args) {
    const defaultGetNextPageId = args.wizard.iterator.getNextPageId;
    args.wizard.iterator.getNextPageId = function (pageId) {
        if(pageId === 'selectReportTypePage' && args.wizard.iterator._getCurrentState().reportTemplateID === 'CustomLabelReport') {
            return 'CustomLabelPage';
        } else {
            return defaultGetNextPageId.apply(this, [pageId]);
        }
    };
}

function CustomizeReportWizard(s, e) {
    if(e.Type === 'ReportWizard') {
        DevExpress.Analytics.Widgets.Internal.addToBindingsCache('dxTextBox: { value: label4Value }', function($context, $element) { return { 'dxTextBox': function() { return { 'value': $context.$data.label4Value } } } });
        DevExpress.Analytics.Widgets.Internal.addToBindingsCache('dxTextBox: { value: label1Value }', function($context, $element) { return { 'dxTextBox': function() { return { 'value': $context.$data.label1Value } } } });
        DevExpress.Analytics.Widgets.Internal.addToBindingsCache('dxTextBox: { value: label2Value }', function($context, $element) { return { 'dxTextBox': function() { return { 'value': $context.$data.label2Value } } } });
        DevExpress.Analytics.Widgets.Internal.addToBindingsCache('dxTextBox: { value: label3Value }', function($context, $element) { return { 'dxTextBox': function() { return { 'value': $context.$data.label3Value } } } });
        e.Wizard.events.addHandler('beforeInitialize', beforeInitializeWizard);
        e.Wizard.events.addHandler('afterInitialize', afterInit);
        e.Wizard.events.addHandler('beforeFinish', (result) => {
            result.state.customData = result.state.customData || 'CustomizeEmptyReport';
        });
    }
}

