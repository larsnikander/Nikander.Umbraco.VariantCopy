namespace Nikander.Umbraco.VariantCopy.Models;
public class VariantResult
{
    public bool IsSucces { get; set; }

    public Error? Error { get; set; }

    public static VariantResult ErrorResult(string message)
    {
        return new VariantResult
        {
            Error = new Error
            {
                Message = message
            }
        };
    }

    public static VariantResult OkResult()
        => new VariantResult { IsSucces = true };
}
