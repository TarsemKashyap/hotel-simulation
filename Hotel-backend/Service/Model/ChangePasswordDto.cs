namespace Service;
public class ChangePasswordDto
{
    public string UserId { get; set; }
    public string currentPassword { get; set; }
    public string newPassword { get; set; }
}

public abstract class AccountDto
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public AppRoleType Role { get; set; }

}
public class InstructorAccountDto : AccountDto
{
    public string Institue { get; set; }
}