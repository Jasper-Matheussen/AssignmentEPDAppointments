#region

using MediatR;

#endregion

namespace Chipsoft.Assignments.EPDContracts.Physician;

public class DeletePhysicianCommand : IRequest<bool>
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateOnly Birthdate { get; set; }
}