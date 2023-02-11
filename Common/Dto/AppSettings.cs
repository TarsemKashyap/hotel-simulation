public record ConnectionStrings
{
    public string DbConn { get; set; }
}

public record Jwt {
    public string Issuer {get;set;}
    public string Audience {get;set;}
    public string Key {get;set;}
}