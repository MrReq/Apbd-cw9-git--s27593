namespace Apbd_cw9_git__s27593.ModelsAndEntities;

public partial class Student
{
    public string FullName => $"{FirstName} {LastName}";

    public bool HasAcademicEmail()
    {
        return Email.EndsWith(
            "@students.example.edu",
            StringComparison.OrdinalIgnoreCase);
    }
}