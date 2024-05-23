namespace Washateria.Models;

public class Washer
{
    public int Id { get; set; }
    public string? Designation { get; set; }
    public string? Manufacturer { get; set; }
    public string? ModelNumber { get; set; }
    public DateTime LastMaintenanceTs { get; set; }
    public DateTime CreatedTs { get; set; }
    public DateTime UpdatedTs { get; set; }
    public bool IsActive { get; set; } 
}