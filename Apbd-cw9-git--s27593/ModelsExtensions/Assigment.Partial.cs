namespace Apbd_cw9_git__s27593.ModelsAndEntities;

public partial class Assignment
{
    public bool IsOverdue(DateTime now)
    {
        return DueDate < now;
    }
}