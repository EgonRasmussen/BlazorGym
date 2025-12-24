using System.ComponentModel.DataAnnotations;

namespace BlazorGym.Components;
public static class Countries
{
    public static Dictionary<string, string> All { get; } = new()
    {
        ["us"] = "United States",
        ["ca"] = "Canada",
        ["uk"] = "United Kingdom",
        ["be"] = "Belgium"
    };
}
public enum Gender
{
    Female, Male
}

public class Member
{
    public required string Name { get; set; } 
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string Message { get; set; } = string.Empty;
    public required string Country { get; set; }
    public bool Subscriber { get; set; }
    public Gender Gender { get; set; }
}

//public class Member
//{
//    public const string EmailRegEx = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";

//    [Required(ErrorMessage = "Name is mandatory")]
//    [StringLength(100, ErrorMessage = "Name cannot be longer that 100 characters")]
//    public string Name { get; set; } = string.Empty;

//    [Required(ErrorMessage = "Email is mandatory")]
//    [RegularExpression(EmailRegEx, ErrorMessage =
//      "This is not a valid e-mail address")]
//    public string Email { get; set; } = string.Empty;

//    [Required(ErrorMessage = "Password is mandatory")]
//    [StringLength(maximumLength: 100, MinimumLength = 14, ErrorMessage = "Passwords should be at least 14 long, an no more that 100")]
//    public string Password { get; set; } = string.Empty;
//    public string Message { get; set; } = string.Empty;
//    public string Country { get; set; } = string.Empty;
//    public bool Subscriber { get; set; }
//    public Gender Gender { get; set; }
//}