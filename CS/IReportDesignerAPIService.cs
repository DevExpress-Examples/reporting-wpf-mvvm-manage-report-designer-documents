using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Reports.UserDesigner;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.IO;

namespace ManipulateReportDesignerDocuments_MVVM {
    public interface IReportDesignerAPIService {
        IEnumerable<ReportDesignerDocument> Documents { get; }
        ReportDesignerDocument ActiveDocument { get; }
        ReportDesignerDocument NewReport(XtraReport report = null);
        ReportDesignerDocument Open();
        ReportDesignerDocument Open(Stream stream);
    }

    public class ReportDesignerAPIService : ServiceBase, IReportDesignerAPIService {
        ReportDesigner Designer => (ReportDesigner)AssociatedObject;

        public IEnumerable<ReportDesignerDocument> Documents => Designer.Documents;

        public ReportDesignerDocument ActiveDocument => Designer.ActiveDocument;

        public ReportDesignerDocument NewReport(XtraReport report = null) {
            Func<XtraReport> reportFactory = null;
            if(report != null)
                reportFactory = () => report;
            return Designer.NewDocument(reportFactory);
        }

        public ReportDesignerDocument Open() {
            return Designer.OpenDocument();
        }

        public ReportDesignerDocument Open(Stream stream) {
            return Designer.OpenDocument(stream);
        }
    }
}
