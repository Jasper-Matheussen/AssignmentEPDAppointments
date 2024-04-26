using Chipsoft.Assignments.EPDApplication.Exceptions;
using Chipsoft.Assignments.EPDApplication.Interfaces;
using Chipsoft.Assignments.EPDContracts.Physician;
using Chipsoft.Assignments.EPDDomain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chipsoft.Assignments.EPDApplication.CQRS.Physicians.Commands.DeletePhysician;

public class DeletePhysicianCommandHandler : IRequestHandler<DeletePhysicianCommand, bool>
{
    private IEPDDbContext Context { get; }
    
    public DeletePhysicianCommandHandler(IEPDDbContext context)
    {
        Context = context;
    }
    
    public async Task<bool> Handle(DeletePhysicianCommand request, CancellationToken cancellationToken)
    {
        var physician = await Context.GetQueryable<Physician>()
            .Where(physician => physician.Birthdate == request.Birthdate)
            .Where(physician => physician.Firstname.ToLower() == request.Firstname.ToLower())
            .Where(physician => physician.Lastname.ToLower() == request.Lastname.ToLower())
            .Where(physician => physician.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (physician == default)
        {
            throw new NotFoundException($"Physician met naam {request.Lastname} wat niet gevonden.");
        }
        
        //soft delete to keep the history of appointments
        physician.IsDeleted = true;
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}