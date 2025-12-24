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