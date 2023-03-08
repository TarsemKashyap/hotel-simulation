public record LoginDto
{
    public string UserId { get; set; }
    public string Password { get; set; }
}

public record TokenApiRequest
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}