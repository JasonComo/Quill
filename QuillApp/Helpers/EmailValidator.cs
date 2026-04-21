namespace QuillApp.Helpers;

public class EmailValidator
{

    private static readonly string[] AllowedDomains =
    {
        ".com", ".net", ".edu"
    };

    public static bool IsValid(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        email = email.Trim().ToLowerInvariant();

        if (!email.Contains('@'))
            return false;

        return AllowedDomains.Any(d => email.EndsWith(d));
    }

}
