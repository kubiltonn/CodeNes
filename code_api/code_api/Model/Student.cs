using System;
using System.Collections.Generic;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime RegistrationDate { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; }
} 