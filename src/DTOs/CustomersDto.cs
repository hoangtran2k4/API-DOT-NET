namespace MyApi.DTOs;

using System.ComponentModel.DataAnnotations;

public class CustomerDto
{
    [Required(ErrorMessage = "Họ tên là bắt buộc")]
    [MaxLength(100)]
    public string FullName { get; set; } = null!;

    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
    [MaxLength(20)]
    public string Phone { get; set; } = null!;

    public char? Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    [MaxLength(30)]
    public string? Channel { get; set; }

    public bool IsMember { get; set; }
}
