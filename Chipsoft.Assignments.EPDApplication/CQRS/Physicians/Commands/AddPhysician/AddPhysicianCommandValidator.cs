using Chipsoft.Assignments.EPDContracts.Physician;
using FluentValidation;

namespace Chipsoft.Assignments.EPDApplication.CQRS.Physicians.Commands.AddPhysician;

public class AddPhysicianCommandValidator : AbstractValidator<AddPhysicianCommand>
{
    public AddPhysicianCommandValidator()
    {
        RuleFor(x => x.Firstname).NotEmpty();
        RuleFor(x => x.Lastname).NotEmpty();
        RuleFor(x => x.Streetname).NotEmpty();
        RuleFor(x => x.Housenumber).NotEmpty();
        RuleFor(x => x.PostalCode).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.PhysicianType).NotEmpty();
    }
}