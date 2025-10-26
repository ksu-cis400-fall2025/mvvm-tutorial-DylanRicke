using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmExample
{
    public class ComputerScienceStudentViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Notifies when a property of this class changes
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// the student this class wraps
        /// </summary>
        private Student _student;
        /// <summary>
        /// the students first name
        /// </summary>
        public string FirstName => _student.FirstName;
        /// <summary>
        /// the students last name
        /// </summary>
        public string LastName => _student.LastName;

        /// <summary>
        /// The students course history
        /// </summary>
        public IEnumerable<CourseRecord> CourseRecords => _student.CourseRecords;

        /// <summary>
        /// The students gpa
        /// </summary>
        public double GPA => _student. GPA;

        public double ComputerScienceGPA
        {
            get
            {
                var points = 0.0;
                var hours = 0.0;
                foreach (var cr in _student.CourseRecords)
                {
                    if (cr.CourseName.Contains("CIS"))
                    {
                        points += (double)cr.Grade * cr.CreditHours;
                        hours += cr.CreditHours;
                    }
                }
                return points / hours;
            }
        }
        /// <summary>
        /// custome handler for handling pass-foward from the student
        /// </summary>
        /// <param name="sender">the studnet that is changing</param>
        /// <param name="e">the event args describing the changing property</param>
        private void HandleStudentPropertyChanged(object sender, PropertyChangedEventArgs e) 
        {
            if (e.PropertyName == nameof(Student.GPA)) 
            {
                PropertyChanged?.Invoke(this, e);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ComputerScienceGPA)));
            }
        }
        /// <summary>
        /// Constructs a new ComputerScienceStudentViewModel 
        /// </summary>
        /// <param name="student">the student wrapped in this view model</param>
        public ComputerScienceStudentViewModel(Student student) 
        {
            _student = student; 
            student.PropertyChanged += HandleStudentPropertyChanged;
        }
    }
}
