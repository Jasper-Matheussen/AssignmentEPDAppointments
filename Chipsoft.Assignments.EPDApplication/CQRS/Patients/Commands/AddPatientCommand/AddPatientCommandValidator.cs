#region

using Chipsoft.Assignments.EPDContracts.Patients;
using FluentValidation;

#endregion

namespace Chipsoft.Assignments.EPDApplication.CQRS.Patients.Commands;

public class AddPatientCommandValidator : AbstractValidator<AddPatientCommand>
{
    public AddPatientCommandValidator()
    {
        RuleFor(x => x.Firstname).NotEmpty().MaximumLength(50)
            .WithMessage("Naam is verplicht en mag niet langer zijn dan 50 karakters.");
        RuleFor(x => x.Lastname).NotEmpty().MaximumLength(50)
            .WithMessage("Achternaam is verplicht en mag niet langer zijn dan 50 karakters.");
        RuleFor(x => x.Streetname).NotEmpty().MaximumLength(70)
            .WithMessage("Straatnaam is verplicht en mag niet langer zijn dan 70 karakters.");
        RuleFor(x => x.Housenumber).NotEmpty().GreaterThan(0)
            .WithMessage("Huisnummer is verplicht en moet groter zijn dan 0.");
        RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(10)
            .WithMessage("Postcode is verplicht en mag niet langer zijn dan 10 karakters.");
        RuleFor(x => x.City).NotEmpty().MaximumLength(60)
            .WithMessage("Stad is verplicht en mag niet langer zijn dan 60 karakters.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress()
            .WithMessage("E-mail is verplicht en moet een geldig e-mailadres zijn.");
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(15)
            .WithMessage("Telefoonnummer is verplicht en mag niet langer zijn dan 15 karakters.");
        RuleFor(x => x.Birthdate).NotEmpty()
            .WithMessage("Geboortedatum is verplicht.");
    }
}