using AutoMapper;
using Chipsoft.Assignments.EPDApplication.Interfaces;
using Chipsoft.Assignments.EPDContracts.Appointment;
using Chipsoft.Assignments.EPDDomain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chipsoft.Assignments.EPDApplication.CQRS.Appointments.Commands;

public class AddAppointmentCommandHandler : IRequestHandler<AddAppointmentCommand, bool>
{
    private IEPDDbContext Context { get; }
    private IMapper Mapper { get; }
    
    public AddAppointmentCommandHandler(IEPDDbContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }
    
    
    public async Task<bool> Handle(AddAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = Mapper.Map<Appointment>(request);
        
        //get physician
        var physician = await Context.GetQueryable<Physician>()
            .Where(physician => physician.Firstname.ToLower() == request.PhysicianFirstname.ToLower())
            .Where(physician => physician.Lastname.ToLower() == request.PhysicianLastname.ToLower())
            .Where(physician => physician.Birthdate == request.PhysicianBirthdate)
            .Where(physician => physician.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);
        
        //get patient
        var patient = await Context.GetQueryable<Patient>()
            .Where(patient => patient.Firstname.ToLower() == request.PatientFirstname.ToLower())
            .Where(patient => patient.Lastname.ToLower() == request.PatientLastname.ToLower())
            .Where(patient => patient.Birthdate == request.PatientBirthdate)
            .Where(patient => patient.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (physician == default)
        {
            throw new Exception("Physician not found");
        }
        
        if (patient == default)
        {
            throw new Exception("Patient not found");
        }
        
        appointment.Physician = physician;
        appointment.Patient = patient;
        
        Context.Add(appointment);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
    
}