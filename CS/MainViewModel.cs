using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Reports.UserDesigner;
using DevExpress.XtraReports.UI;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Resources;

namespace ManipulateReportDesignerDocuments_MVVM {
    public class MainViewModel : ViewModelBase {
        public XtraReport DefaultReport { get; set; }

        IReportDesignerAPIService ReportDesignerAPI => ServiceContainer.GetService<IReportDesignerAPIService>();

        protected override void OnInitializeInRuntime() {
            base.OnInitializeInRuntime();
            DefaultReport = new XtraReport();
        }

        [Command]
        public void NewReport() {
            ReportDesignerAPI.NewReport();
        }
        public bool CanNewReport() => ReportDesignerAPI != null;

        [Command]
        public void OpenReport() {
            ReportDesignerAPI.Open();
        }
        public bool CanOpenReport() => ReportDesignerAPI != null;

        [Command]
        public void OpenReportFromStream() {
            var stream = Application.GetResourceStream(new Uri("Resources/Invoice.repx", UriKind.Relative))?.Stream;
            if(stream != null) {
                using(stream) {
                    ReportDesignerAPI.Open(stream);
                }
            }
        }
        public bool CanOpenReportFromStream() => ReportDesignerAPI != null;

        [Command]
        public void CloseActiveDocument() {
            ReportDesignerAPI.ActiveDocument.Close(true);
        }
        public bool CanCloseActiveDocument() => ReportDesignerAPI?.ActiveDocument != null;

        [Command]
        public void ActivateFirstDocumentTab() {
            ReportDesignerDocument firstTab = ReportDesignerAPI.Documents.First();
            firstTab.Activate();
        }
        public bool CanActivateFirstDocumentTab() => ReportDesignerAPI?.Documents.Any() ?? false;

        [Command]
        public void SwitchActiveDocumentView() {
            ReportDesignerDocument activeDocument = ReportDesignerAPI.ActiveDocument;
            activeDocument.ViewKind = activeDocument.ViewKind == ReportDesignerDocumentViewKind.Designer
                ? ReportDesignerDocumentViewKind.Preview
                : ReportDesignerDocumentViewKind.Designer;
        }
        public bool CanSwitchActiveDocumentView() => ReportDesignerAPI?.ActiveDocument != null;
    }
}
