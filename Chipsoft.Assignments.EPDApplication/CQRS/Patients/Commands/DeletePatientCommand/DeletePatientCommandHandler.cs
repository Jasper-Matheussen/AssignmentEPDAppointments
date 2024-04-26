using Chipsoft.Assignments.EPDApplication.Exceptions;
using Chipsoft.Assignments.EPDApplication.Interfaces;
using Chipsoft.Assignments.EPDDomain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chipsoft.Assignments.EPDApplication.CQRS.Patients.Commands.DeletePatientCommand;

public class DeletePatientCommandHandler : IRequestHandler<EPDContracts.Patients.DeletePatientCommand, bool>
{
    private IEPDDbContext Context { get; }
    
    public DeletePatientCommandHandler(IEPDDbContext context)
    {
        Context = context;
    }
    
    public async Task<bool> Handle(EPDContracts.Patients.DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await Context.GetQueryable<Patient>()
            .Where(patient => patient.Firstname.ToLower() == request.Firstname.ToLower())
            .Where(patient => patient.Lastname.ToLower() == request.Lastname.ToLower())
            .Where(patient => patient.Birthdate == request.Birthdate)
            .Where(patient => patient.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (patient == default)
        {
            throw new NotFoundException($"Patient met naam {request.Lastname} niet gevonden.");
        }
        
        //soft delete to keep the history of appointments
        patient.IsDeleted = true;
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}