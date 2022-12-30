using StudentManagementWithJWT.Data;
using StudentManagementWithJWT.Models;

namespace StudentManagementWithJWT.Services
{
    public class StudentService
    {
        private readonly AppDbContext _appDbContext;

        public StudentService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddStudent(StudentsVM studentsVM)
        {

            var _student = new Student()
            {
                FirstName = studentsVM.FirstName,
                LastName = studentsVM.LastName,
                Percentage = studentsVM.Percentage
            };

            _appDbContext.tbl_Students.Add(_student);
            _appDbContext.SaveChanges();

        }

        public Student UpdateStudent(int? studentId, StudentsVM studentsVM)
        {


            var _student = _appDbContext.tbl_Students.FirstOrDefault(n => n.Id == studentId);

            if(_student!=null)
            {
                _student.FirstName = studentsVM.FirstName;
                _student.LastName = studentsVM.LastName;
                _student.Percentage = studentsVM.Percentage;
                _appDbContext.tbl_Students.Update(_student);
                _appDbContext.SaveChanges();
            }

            return (_student);

            

        }

        public List<Student> GetAllStudents()
        {
            return _appDbContext.tbl_Students.ToList();
        }

        public Student GetStudentById(int? studentId)
        {

            var _student =_appDbContext.tbl_Students.FirstOrDefault(n=> n.Id==studentId);
            return _student;

        }

        public void DeleteStudent(int? studentId)
        {
            var _student = _appDbContext.tbl_Students.FirstOrDefault(n => n.Id == studentId);
            if (_student != null)
            {
                _appDbContext.tbl_Students.Remove(_student);
                _appDbContext.SaveChanges();
            }

        }

    }
}
