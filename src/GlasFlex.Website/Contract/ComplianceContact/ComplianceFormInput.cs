using System;
using System.ComponentModel.DataAnnotations;

namespace GlasFlex.Website.Contract.ComplianceContact;

public class ComplianceFormInput
{
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [StringLength(400, MinimumLength = 10, ErrorMessage = "Message must be between 10 and 400 characters.")]
    public string Message { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty; // This is a honeypot field. It should be left empty
    public string ReplyTo { get; set; } = string.Empty; // This is a honeypot field. It should be left empty
    public string UserAgent { get; set; } = string.Empty; // This is a honeypot field. It should be left empty
    public string Referrer { get; set; } = string.Empty; // This is a honeypot field. It should be left empty
}
