using Chipsoft.Assignments.EPDContracts.Appointment;
using FluentValidation;

namespace Chipsoft.Assignments.EPDApplication.CQRS.Appointments.Commands;

public class AddAppointmentCommandValidator : AbstractValidator<AddAppointmentCommand>
{
    public AddAppointmentCommandValidator()
    {
        RuleFor(x => x.PhysicianFirstname).NotEmpty();
        RuleFor(x => x.PhysicianLastname).NotEmpty();
        RuleFor(x => x.PhysicianBirthdate).NotEmpty();
        RuleFor(x => x.PatientFirstname).NotEmpty();
        RuleFor(x => x.PatientLastname).NotEmpty();
        RuleFor(x => x.PatientBirthdate).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty();
    }
    
}