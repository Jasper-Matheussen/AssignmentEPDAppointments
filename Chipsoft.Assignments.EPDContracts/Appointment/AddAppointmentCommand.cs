#region

using MediatR;

#endregion

namespace Chipsoft.Assignments.EPDContracts.Appointment;

public class AddAppointmentCommand : IRequest<bool>
{
    public string PhysicianFirstname { get; set; }
    public string PhysicianLastname { get; set; }
    public DateOnly PhysicianBirthdate { get; set; }
    
    public string PatientFirstname { get; set; }
    public string PatientLastname { get; set; }
    public DateOnly PatientBirthdate { get; set; }
    
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
}