using Chipsoft.Assignments.EPDApplication.Interfaces;
using Chipsoft.Assignments.EPDDomain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chipsoft.Assignments.EPDApplication.CQRS.Appointments.Queries;

public class GetAllAppointmentsQueryHandler : IRequestHandler<GetAllAppointmentsQuery, IEnumerable<Appointment>>
{
    private IEPDDbContext Context { get; }
    
    public GetAllAppointmentsQueryHandler(IEPDDbContext context)
    {
        Context = context;
    }
    
    public async Task<IEnumerable<Appointment>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
    {
        return await Context.GetNoTrackingQueryable<Appointment>()
            .Include(patient => patient.Patient)
            .Include(physician => physician.Physician)
            .ToListAsync(cancellationToken);
    }
    
}