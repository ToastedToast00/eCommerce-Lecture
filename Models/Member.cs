using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace eCommerce.Models;

/// <summary>
/// Represents an individual member in the eCommerce system.
/// </summary>
public class Member
{
    /// <summary>
    /// Unique identifier for the member.
    /// </summary>
    [Key]
    public int MemberId { get; set; }

    /// <summary>
    /// public facing username for the member.
    /// aplhanumeric only.
    /// </summary>
    [RegularExpression("^[a-Za-Z0-9]+$", 
        ErrorMessage = "Username must be alphanumeric only")]
    [StringLength(25, ErrorMessage = "Username must be less than 26 characters")]
    public required string Username { get; set; }
    
    /// <summary>
    /// Email for the member
    /// </summary>

    public required string Email { get; set; }

    /// <summary>
    /// The members password
    /// </summary>
    [StringLength(50, MinimumLength = 6, 
        ErrorMessage = "Your password must fit between 6 and 50 keys.")]
    public required string Password { get; set; }
    
    /// <summary>
    /// the date of birth of the member
    /// </summary>
    
    public DateOnly DateOfBirth { get; set; }
}

public class RegistrationViewModel
{
    /// <summary>
    /// public facing username for the member.
    /// aplhanumeric only.
    /// </summary>
    [RegularExpression("^[a-zA-Z0-9]+$",
        ErrorMessage = "Username must be alphanumeric only")]
    [StringLength(25, ErrorMessage = "Username must be less than 26 characters")]
    public required string Username { get; set; }

    /// <summary>
    /// Email for the member
    /// </summary>
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    /// <summary>
    /// The members password
    /// </summary>
    [StringLength(50, MinimumLength = 6,
        ErrorMessage = "Your password must fit between 6 and 50 keys.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Compare(nameof(Password))]
    [DataType(DataType.Password)]
    public required string ConfirmPassword { get; set; }

    /// <summary>
    /// the date of birth of the member
    /// </summary>
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }
}

public class LoginViewModel
{ 
    public required string UsernameOrEmail { get; set; }
    public required string Password { get; set; }

}