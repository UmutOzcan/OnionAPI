using Newtonsoft.Json;

namespace OnionAPI.Application.Exceptions;

// toString override ile json serialize yapar
public class ExceptionModel : ErrorStatusCode
{
    public IEnumerable<string> Errors { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

}

public class ErrorStatusCode
{
    public int StatusCode { get; set; }
}