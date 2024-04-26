using Chipsoft.Assignments.EPDDomain;
using MediatR;

namespace Chipsoft.Assignments.EPDApplication.CQRS.Appointments.Queries;

public class GetAllAppointmentsQuery : IRequest<IEnumerable<Appointment>>
{
    
}