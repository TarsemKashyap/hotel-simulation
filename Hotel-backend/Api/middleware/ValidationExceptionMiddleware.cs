
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service;
using System.Globalization;
using System.Net;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var dict = new Dictionary<string, string> { { "", ex.Message } };
            await context.Response.WriteAsJsonAsync(dict);
        }
        catch (FluentValidation.ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(Message(ex));
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var dict = new Dictionary<string, string> { { "", ex.Message } };
            await context.Response.WriteAsJsonAsync(dict);
        }
    }


    private Dictionary<string, string> Message(FluentValidation.ValidationException ex)
    {
        Dictionary<string, string> list = new Dictionary<string, string>();
        foreach (var item in ex.Errors)
        {
            string prop = ToCamelCase(item.PropertyName);
            list.Add(prop, item.ErrorMessage);
        }
        return list;
    }

    private string ToCamelCase(string s)
    {
        if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
        {
            return s;
        }

        var chars = s.ToCharArray();

        for (var i = 0; i < chars.Length; i++)
        {
            if (i == 1 && !char.IsUpper(chars[i]))
            {
                break;
            }

            var hasNext = (i + 1 < chars.Length);
            if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
            {
                break;
            }

            chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
        }

        return new string(chars);
    }
}
