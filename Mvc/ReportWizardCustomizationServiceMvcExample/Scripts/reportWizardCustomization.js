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
                Label1: this.label1Value,
                Label2: this.label2Value,
                Label3: this.label3Value,
                ReportName: this.label4Value,
            }).promise();
        }

        createViewModel() {
            return {
                ...super.createViewModel(),
                label1Editor: {
                    value: this.label1Value,
                    onValueChanged: (e) => {
                        this.label1Value = e.value;
                        this.notifyStateChanged();
                    }
                },
                label2Editor: {
                    value: this.label2Value,
                    onValueChanged: (e) => {
                        this.label2Value = e.value;
                        this.notifyStateChanged();
                    }
                },
                label3Editor: {
                    value: this.label3Value,
                    onValueChanged: (e) => {
                        this.label3Value = e.value;
                        this.notifyStateChanged();
                    }
                },
                label4Editor: {
                    value: this.label4Value,
                    onValueChanged: (e) => {
                        this.label4Value = e.value;
                        this.notifyStateChanged();
                    }
                }
            };
        }

        label1Value = 'Label1';
        label2Value = 'Label2';
        label3Value = 'Label3';
        label4Value = 'SomeReportName';
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
        DevExpress.Analytics.Widgets.Internal.addToBindingsCache('dxTextBox: label4Editor', function($context) { return { 'dxTextBox': function() { return $context.$data.label4Editor; } } });
        DevExpress.Analytics.Widgets.Internal.addToBindingsCache('dxTextBox: label1Editor', function($context) { return { 'dxTextBox': function() { return $context.$data.label1Editor; } } });
        DevExpress.Analytics.Widgets.Internal.addToBindingsCache('dxTextBox: label2Editor', function($context) { return { 'dxTextBox': function() { return $context.$data.label2Editor; } } });
        DevExpress.Analytics.Widgets.Internal.addToBindingsCache('dxTextBox: label3Editor', function($context) { return { 'dxTextBox': function() { return $context.$data.label3Editor; } } });
        e.Wizard.events.addHandler('beforeInitialize', beforeInitializeWizard);
        e.Wizard.events.addHandler('afterInitialize', afterInit);
        e.Wizard.events.addHandler('beforeFinish', (result) => {
            result.state.customData = result.state.customData || 'CustomizeEmptyReport';
        });
    }
}
