Imports DevExpress.Mvvm.UI
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Reports.UserDesigner
Imports DevExpress.XtraReports.UI
Imports System
Imports System.Collections.Generic
Imports System.IO

Namespace ManipulateReportDesignerDocuments_MVVM
	Public Interface IReportDesignerAPIService
		ReadOnly Property Documents() As IEnumerable(Of ReportDesignerDocument)
		ReadOnly Property ActiveDocument() As ReportDesignerDocument
		Function NewReport(Optional ByVal report As XtraReport = Nothing) As ReportDesignerDocument
		Function Open() As ReportDesignerDocument
		Function Open(ByVal stream As Stream) As ReportDesignerDocument
	End Interface

	Public Class ReportDesignerAPIService
		Inherits ServiceBase
		Implements IReportDesignerAPIService

		Private ReadOnly Property Designer() As ReportDesigner
			Get
				Return CType(AssociatedObject, ReportDesigner)
			End Get
		End Property

		Public ReadOnly Property Documents() As IEnumerable(Of ReportDesignerDocument) Implements IReportDesignerAPIService.Documents
			Get
				Return Designer.Documents
			End Get
		End Property

		Public ReadOnly Property ActiveDocument() As ReportDesignerDocument Implements IReportDesignerAPIService.ActiveDocument
			Get
				Return Designer.ActiveDocument
			End Get
		End Property

		Public Function NewReport(Optional ByVal report As XtraReport = Nothing) As ReportDesignerDocument Implements IReportDesignerAPIService.NewReport
			Dim reportFactory As Func(Of XtraReport) = Nothing
			If report IsNot Nothing Then
				reportFactory = Function() report
			End If
			Return Designer.NewDocument(reportFactory)
		End Function

		Public Function Open() As ReportDesignerDocument Implements IReportDesignerAPIService.Open
			Return Designer.OpenDocument()
		End Function

		Public Function Open(ByVal stream As Stream) As ReportDesignerDocument Implements IReportDesignerAPIService.Open
			Return Designer.OpenDocument(stream)
		End Function
	End Class
End Namespace
