public class LoginResultDto
{

    public string AccessToken { get;  set; }
    public string RefreshToken { get;  set; }
    public string[] Roles { get; set; }
}