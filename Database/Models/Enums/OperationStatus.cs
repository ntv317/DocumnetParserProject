using System.ComponentModel.DataAnnotations;
namespace Database.Models.Enums;

public enum OperationStatus
{
    /// <summary>
    /// Approved
    /// </summary>
    [Display(Name = "A")]
    Approved,
    /// <summary>
    /// Rejected
    /// </summary>
    [Display(Name = "R")]
    Rejected,
    /// <summary>
    /// Done
    /// </summary>
    [Display(Name = "D")]
    Done
}