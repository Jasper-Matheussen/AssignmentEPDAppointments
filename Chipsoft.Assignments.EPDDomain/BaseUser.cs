namespace Chipsoft.Assignments.EPDDomain;

public class BaseUser : IBaseEntity
{
    public int Id { get; set; }

    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public string Streetname { get; set; }
    public int Housenumber { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public DateOnly Birthdate { get; set; }

    //Isdeleted for soft delete so that appointments can still be linked to the patient
    public bool IsDeleted { get; set; }
    
    
    public List<Appointment> Appointments { get; set; } = new();
}