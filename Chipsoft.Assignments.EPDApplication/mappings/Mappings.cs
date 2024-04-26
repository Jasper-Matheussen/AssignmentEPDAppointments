using AutoMapper;
using Chipsoft.Assignments.EPDContracts.Appointment;
using Chipsoft.Assignments.EPDContracts.Physician;

namespace Chipsoft.Assignments.EPDApplication.mappings;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<EPDDomain.Patient, EPDContracts.Patients.AddPatientCommand>();
        CreateMap<EPDContracts.Patients.AddPatientCommand, EPDDomain.Patient>();
        
        CreateMap<EPDDomain.Physician, AddPhysicianCommand>();
        CreateMap<AddPhysicianCommand, EPDDomain.Physician>();
        
        CreateMap<EPDDomain.Appointment, AddAppointmentCommand>();
        CreateMap<AddAppointmentCommand, EPDDomain.Appointment>();
    }
    
}