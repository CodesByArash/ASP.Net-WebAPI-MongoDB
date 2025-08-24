using System.ComponentModel.DataAnnotations;

namespace AuthService.Dtos.UserDtos;

public class UpdateUserDto
{
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;
}