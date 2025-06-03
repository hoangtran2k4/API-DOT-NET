namespace MyApi.DTOs;

public class CustomerDto
{
    public string FullName { get; set; } = null!;
    public string? Email { get; set; }
    public string Phone { get; set; } = null!;
    public char? Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Channel { get; set; }
    public bool IsMember { get; set; } = false;
}
