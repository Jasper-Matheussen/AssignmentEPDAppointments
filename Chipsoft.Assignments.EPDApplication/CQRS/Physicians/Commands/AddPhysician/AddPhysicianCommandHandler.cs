using AutoMapper;
using Chipsoft.Assignments.EPDApplication.Interfaces;
using Chipsoft.Assignments.EPDContracts.Physician;
using Chipsoft.Assignments.EPDDomain;
using MediatR;

namespace Chipsoft.Assignments.EPDApplication.CQRS.Physicians.Commands.AddPhysician;

public class AddPhysicianCommandHandler : IRequestHandler<AddPhysicianCommand, bool>
{
    private IEPDDbContext Context { get; }
    private IMapper Mapper { get; }
    
    public AddPhysicianCommandHandler(IEPDDbContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }
    
    
    public async Task<bool> Handle(AddPhysicianCommand request, CancellationToken cancellationToken)
    {
        var physician = Mapper.Map<Physician>(request);
        
        Context.Add(physician);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
    
}