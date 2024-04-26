namespace Chipsoft.Assignments.EPDDomain;

public class Appointment : IBaseEntity
{
    public int Id { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime StartDate { get; set; }

    
    public Physician Physician { get; set; }
    public Patient Patient { get; set; }
}