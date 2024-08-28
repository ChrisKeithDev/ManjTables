using ManjTables.DataModels;
using ManjTables.Reports.ReportModels;

namespace ManjTables.Reports.ReportServices
{
    public class ClassroomService
    {
        private readonly ManjTablesContext _dbContext;

        public ClassroomService(ManjTablesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ClassroomDto> GetClassrooms()
        {
            var classroomDtos = (from classroom in _dbContext.Classrooms
                                 join staffClassroom in _dbContext.StaffClassrooms on classroom.ClassroomId equals staffClassroom.ClassroomId
                                 join staff in _dbContext.StaffMembers on staffClassroom.StaffId equals staff.StaffId
                                 select new ClassroomDto
                                 {
                                     ClassroomId = classroom.ClassroomId ?? "Unknown",
                                     ClassroomName = classroom.ClassroomName ?? "Unknown",
                                     InstructorName = staff.FirstName + " " + staff.LastName
                                 }).ToList();

            return classroomDtos;
        }

        public string GetClassroomName(string classroomId)
        {
            var classroom = _dbContext.Classrooms.FirstOrDefault(c => c.ClassroomId == classroomId);
            return classroom?.ClassroomName ?? "Unknown";
        }

        public static ClassroomDto GetDtoById(List<ClassroomDto> dtos, string classroomId)
        {
            return dtos.FirstOrDefault(dto => dto.ClassroomId == classroomId) ?? new ClassroomDto();
        }

        public static ClassroomDto GetDtoByName(List<ClassroomDto> dtos, string classroomName)
        {
            return dtos.FirstOrDefault(dto => dto.ClassroomName == classroomName) ?? new ClassroomDto();
        }

        public static ClassroomDto GetDtoByInstructor(List<ClassroomDto> dtos, string instructorName)
        {
            return dtos.FirstOrDefault(dto => dto.InstructorName == instructorName) ?? new ClassroomDto();
        }
    }
}
