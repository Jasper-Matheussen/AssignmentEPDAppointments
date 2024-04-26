using Chipsoft.Assignments.EPDContracts.Physician;
using FluentValidation;

namespace Chipsoft.Assignments.EPDApplication.CQRS.Physicians.Commands.DeletePhysician;

public class DeletePhysicianCommandValidator : AbstractValidator<DeletePhysicianCommand>
{
    public DeletePhysicianCommandValidator()
    {
        RuleFor(x => x.Firstname).NotEmpty();
        RuleFor(x => x.Lastname).NotEmpty();
        RuleFor(x => x.Birthdate).NotEmpty();
    }
}