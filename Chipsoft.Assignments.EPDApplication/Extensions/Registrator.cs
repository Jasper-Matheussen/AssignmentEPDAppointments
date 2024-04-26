using System.Reflection;
using Chipsoft.Assignments.EPDApplication.Behaviours;
using Chipsoft.Assignments.EPDApplication.CQRS.Appointments.Commands;
using Chipsoft.Assignments.EPDApplication.CQRS.Appointments.Queries;
using Chipsoft.Assignments.EPDApplication.CQRS.Patients.Commands;
using Chipsoft.Assignments.EPDApplication.CQRS.Patients.Commands.DeletePatientCommand;
using Chipsoft.Assignments.EPDApplication.CQRS.Physicians.Commands.AddPhysician;
using Chipsoft.Assignments.EPDApplication.CQRS.Physicians.Commands.DeletePhysician;
using Chipsoft.Assignments.EPDContracts.Appointment;
using Chipsoft.Assignments.EPDContracts.Patients;
using Chipsoft.Assignments.EPDContracts.Physician;
using Chipsoft.Assignments.EPDDomain;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Chipsoft.Assignments.EPDApplication.Extensions;

public static class Registrator
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Registrator).Assembly);
        //services.AddSingleton<IMediator, Mediator>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient<IRequestHandler<AddPatientCommand, bool>, AddPatientCommandHandler>();
        services.AddTransient<IRequestHandler<DeletePatientCommand, bool>, DeletePatientCommandHandler>();
        services.AddTransient<IRequestHandler<AddPhysicianCommand, bool>, AddPhysicianCommandHandler>();
        services.AddTransient<IRequestHandler<DeletePhysicianCommand, bool>, DeletePhysicianCommandHandler>();
        services.AddTransient<IRequestHandler<AddAppointmentCommand, bool>, AddAppointmentCommandHandler>();
        services.AddTransient<IRequestHandler<GetAllAppointmentsQuery, IEnumerable<Appointment>>, GetAllAppointmentsQueryHandler>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        return services;
    }
    
}