using MediatR;

namespace Chipsoft.Assignments.EPDContracts.Patients;

public class AddPatientCommand : IRequest<bool>
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public string Streetname { get; set; }
    public int Housenumber { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly Birthdate { get; set; }
}