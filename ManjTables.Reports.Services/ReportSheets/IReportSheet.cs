namespace ManjTables.Reports.ReportSheets
{
    public interface IReportSheet
    {
        string Filename { get; set; }
        string SheetTitle { get; set; }
        DateTime Date { get; set; }
        List<string> ColumnHeaders { get; }
        int ColumnCount { get; }
    }

}
