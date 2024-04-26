#region

using AutoMapper;
using Chipsoft.Assignments.EPDApplication.Interfaces;
using Chipsoft.Assignments.EPDContracts.Patients;
using Chipsoft.Assignments.EPDDomain;
using MediatR;

#endregion

namespace Chipsoft.Assignments.EPDApplication.CQRS.Patients.Commands;

public class AddPatientCommandHandler : IRequestHandler<AddPatientCommand, bool>
{
    private IEPDDbContext Context { get; }
    private IMapper Mapper { get; }
    
    public AddPatientCommandHandler(IEPDDbContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }
    
    
    public async Task<bool> Handle(AddPatientCommand request, CancellationToken cancellationToken)
    {
        var patient = Mapper.Map<Patient>(request);
        
        Context.Add(patient);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}