using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Student
/// </summary>
public class Student
{
    public Student()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string srno { get; set; }
    public string StudentName { get; set; }
    public string FatherName { get; set; }
    public string MobileNo { get; set; } 

}

public class StudentAttendance:Student
{
    public string AttendanceValue { get; set; }
    public string AttendanceDate { get; set; }
    public string AttendanceName { get; set; }   
    public string In { get; set; }
    public string Out { get; set; }
    public StudentAttendance(string _srno, string _StudentName, string _FatherName, string _MobileNo, string _AttendanceValue, string _AttendanceDate, string _In, string _Out,string _AttendanceName)
    {
        srno = _srno;
        StudentName = _StudentName;
        FatherName = _FatherName;
        MobileNo = _MobileNo;
        AttendanceValue = _AttendanceValue;
        AttendanceDate = _AttendanceDate;
        In = _In;
        Out = _Out;
        AttendanceName = _AttendanceName;
    }
}