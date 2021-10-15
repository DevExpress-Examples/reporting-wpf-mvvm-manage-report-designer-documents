Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Reports.UserDesigner
Imports DevExpress.XtraReports.UI
Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Resources

Namespace ManipulateReportDesignerDocuments_MVVM
	Public Class MainViewModel
		Inherits ViewModelBase

		Public Property DefaultReport() As XtraReport

		Private ReadOnly Property ReportDesignerAPI() As IReportDesignerAPIService
			Get
				Return ServiceContainer.GetService(Of IReportDesignerAPIService)()
			End Get
		End Property

		Protected Overrides Sub OnInitializeInRuntime()
			MyBase.OnInitializeInRuntime()
			DefaultReport = New XtraReport()
		End Sub

		<Command>
		Public Sub NewReport()
			ReportDesignerAPI.NewReport()
		End Sub
		Public Function CanNewReport() As Boolean
			Return ReportDesignerAPI IsNot Nothing
		End Function

		<Command>
		Public Sub OpenReport()
			ReportDesignerAPI.Open()
		End Sub
		Public Function CanOpenReport() As Boolean
			Return ReportDesignerAPI IsNot Nothing
		End Function

		<Command>
		Public Sub OpenReportFromStream()
			Dim stream = Application.GetResourceStream(New Uri("Resources/Invoice.repx", UriKind.Relative))?.Stream
			If stream IsNot Nothing Then
				Using stream
					ReportDesignerAPI.Open(stream)
				End Using
			End If
		End Sub
		Public Function CanOpenReportFromStream() As Boolean
			Return ReportDesignerAPI IsNot Nothing
		End Function

		<Command>
		Public Sub CloseActiveDocument()
			ReportDesignerAPI.ActiveDocument.Close(True)
		End Sub
		Public Function CanCloseActiveDocument() As Boolean
			Return ReportDesignerAPI?.ActiveDocument IsNot Nothing
		End Function

		<Command>
		Public Sub ActivateFirstDocumentTab()
			Dim firstTab As ReportDesignerDocument = ReportDesignerAPI.Documents.First()
			firstTab.Activate()
		End Sub
		Public Function CanActivateFirstDocumentTab() As Boolean
			Return If(ReportDesignerAPI?.Documents.Any(), False)
		End Function

		<Command>
		Public Sub SwitchActiveDocumentView()
			Dim activeDocument As ReportDesignerDocument = ReportDesignerAPI.ActiveDocument
			activeDocument.ViewKind = If(activeDocument.ViewKind = ReportDesignerDocumentViewKind.Designer, ReportDesignerDocumentViewKind.Preview, ReportDesignerDocumentViewKind.Designer)
		End Sub
		Public Function CanSwitchActiveDocumentView() As Boolean
			Return ReportDesignerAPI?.ActiveDocument IsNot Nothing
		End Function
	End Class
End Namespace
