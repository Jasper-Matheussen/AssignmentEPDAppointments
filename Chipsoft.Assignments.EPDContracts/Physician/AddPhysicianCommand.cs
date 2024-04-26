#region

using MediatR;

#endregion

namespace Chipsoft.Assignments.EPDContracts.Physician;

public class AddPhysicianCommand : IRequest<bool>
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public string Streetname { get; set; }
    public int Housenumber { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    
    public string PhysicianType { get; set; }
    
    public DateOnly Birthdate { get; set; }
}