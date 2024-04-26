#region

using MediatR;

#endregion

namespace Chipsoft.Assignments.EPDContracts.Patients;

public class DeletePatientCommand : IRequest<bool>
{
    // Naam en geboortedatum om patient te identificeren
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateOnly Birthdate { get; set; }
}