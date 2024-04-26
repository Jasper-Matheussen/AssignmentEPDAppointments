#region

using AutoMapper;
using Chipsoft.Assignments.EPDContracts.Appointment;
using Chipsoft.Assignments.EPDContracts.Patients;
using Chipsoft.Assignments.EPDContracts.Physician;
using Chipsoft.Assignments.EPDDomain;

#endregion

namespace Chipsoft.Assignments.EPDApplication.mappings;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<Patient, AddPatientCommand>();
        CreateMap<AddPatientCommand, Patient>();
        
        CreateMap<Physician, AddPhysicianCommand>();
        CreateMap<AddPhysicianCommand, Physician>();
        
        CreateMap<Appointment, AddAppointmentCommand>();
        CreateMap<AddAppointmentCommand, Appointment>();
    }
    
}