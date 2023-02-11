namespace Common;
using System.Runtime.Serialization;
public enum Roles
{
    [EnumMember(Value = "Admin")]
    Admin = 0,
    [EnumMember(Value = "Instructor")]
    Instructor = 1,
    [EnumMember(Value = "Student")]
    Student = 3

}
