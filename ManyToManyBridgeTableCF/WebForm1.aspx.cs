using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManyToManyBridgeTableCF
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EmployeeDBContext employeeDbContext = new EmployeeDBContext();

            var query = from student in employeeDbContext.Students
                        from studentCourse in student.StudentCourses
                        select new
                        {
                            StudentName = student.StudentName,
                            CourseName = studentCourse.Course.CourseName,
                            EnrolledDate = studentCourse.EnrolledDate
                        };
            GridView1.DataSource = query.ToList();
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            EmployeeDBContext employeeDBContext = new EmployeeDBContext();

            StudentCourse WCFCourseForMike = new StudentCourse
            {
                StudentID = 1,
                CourseID = 4,
                EnrolledDate = DateTime.Now
            };

            employeeDBContext.StudentCourses.Add(WCFCourseForMike);
            employeeDBContext.SaveChanges();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            EmployeeDBContext employeeDBContext = new EmployeeDBContext();

            StudentCourse studentCourseToRemove = employeeDBContext.StudentCourses.FirstOrDefault(x => x.StudentID == 2 && x.CourseID == 3);
            employeeDBContext.StudentCourses.Remove(studentCourseToRemove);
            employeeDBContext.SaveChanges();
        }
    }
}