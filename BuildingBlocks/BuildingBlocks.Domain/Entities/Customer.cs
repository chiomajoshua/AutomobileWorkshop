using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Domain.Entities;

public class Customer : TrackedEntity
{
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    [EmailAddress]    
    public string Email {  get; set; } = null!;
    [Required]
    public string Address {  get; set; } = null!;
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset LastModifiedDate { get; set; } = DateTimeOffset.Now;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
