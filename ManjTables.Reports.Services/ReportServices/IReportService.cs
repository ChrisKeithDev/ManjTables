
using ManjTables.SQLiteReaderLib.Connection;

namespace ManjTables.Reports.ReportServices
{
    public interface IReportService<TReportDto>
    {
        //Task<IEnumerable<TReportDto>> GenerateReportAsync(int classroomIds);
        List<TReportDto> GetReportDtos(string classroomId);
        void GenerateReportExcel(List<TReportDto> reportDtos, string filename, string classroomId);
        void RunReport(string classroomId, string filePath);

        // Overload for reports that require a date
        void RunReport(string classroomId, string filePath, DateTime selectedDate);

    }

}
