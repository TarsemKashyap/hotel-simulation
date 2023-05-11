public record ConnectionStrings
{
    public string DbConn { get; set; }
}



public class JwtSettings
{
    public string AccessTokenSecret { get; set; }
    public string RefreshTokenSecret { get; set; }
    public double AccessTokenExpirationMinutes { get; set; }
    public double RefreshTokenExpirationMinutes { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}

public class PaymentConfig
{
    public string authToken { get; set; }
    public string sandBoxUrl { get; set; }
    public string webUrl { get; set; }
}

public class Smtp
{
    public string SendGridKey { get; set; }
    public string WebAppUrl { get; set; }
    
}

public class AdminConfig
{
    public string UserId { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    
}