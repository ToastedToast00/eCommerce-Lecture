using System.ComponentModel.DataAnnotations;

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

    public required string Username { get; set; }
    
    /// <summary>
    /// Email for the member
    /// </summary>

    public required string Email { get; set; }

    /// <summary>
    /// The members password
    /// </summary>

    public required string Password { get; set; }
    
    /// <summary>
    /// the date of birth of the member
    /// </summary>
    
    public DateOnly DateOfBirth { get; set; }
}
