using System.Reflection;
using AutoMapper;
using Chipsoft.Assignments.EPDApplication.CQRS.Appointments.Queries;
using Chipsoft.Assignments.EPDApplication.CQRS.Patients.Commands;
using Chipsoft.Assignments.EPDApplication.Exceptions;
using Chipsoft.Assignments.EPDApplication.Extensions;
using Chipsoft.Assignments.EPDApplication.Interfaces;
using Chipsoft.Assignments.EPDApplication.mappings;
using Chipsoft.Assignments.EPDCInfrastructure;
using Chipsoft.Assignments.EPDCInfrastructure.Contexts;
using Chipsoft.Assignments.EPDCInfrastructure.Extensions;
using Chipsoft.Assignments.EPDConsole.Services;
using Chipsoft.Assignments.EPDContracts.Patients;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Chipsoft.Assignments.EPDConsole
{
    public class Program
    {
        //Don't create EF migrations, use the reset db option
        //This deletes and recreates the db, this makes sure all tables exist
        private static IMediator? _mediator;
        private static async Task AddPatient()
        {
            bool succes;
            var patient = ConsoleService.GetPatientDetails();
            //TODO: Exception handling middleware toevoegen (geen tijd meer)
            try
            {
                succes = await _mediator?.Send(patient);
            }
            catch (ValidationException e)
            {
                Console.WriteLine(e.Message);
                succes = false;
            }
            
            ConsoleService.ShowSuccessOrFailedMessage(succes, "Patient is toegevoegd", "Patient is niet toegevoegd probeer opnieuw");
        }
        
        private static async Task ShowAppointment()
        {
            var appointments = await _mediator.Send(new GetAllAppointmentsQuery());
            ConsoleService.ShowAllAppointments(appointments);
        }

        private static async Task AddAppointment()
        {
            bool succes;
            var appointment = ConsoleService.GetAppointmentDetails();
            try
            {
                succes = await _mediator?.Send(appointment);
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                succes = false;
            }
            
            ConsoleService.ShowSuccessOrFailedMessage(succes, "Afspraak is toegevoegd", "Afspraak is niet toegevoegd probeer opnieuw");
        }

        private static async Task DeletePhysician()
        {
            bool succes;
            var physician = ConsoleService.GetPhysicianInformationToDelete();
            try
            {
                succes = await _mediator?.Send(physician);
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                succes = false;
            }
            
            ConsoleService.ShowSuccessOrFailedMessage(succes, "Arts is verwijderd", "Arts is niet verwijderd probeer opnieuw");
        }

        private static async Task AddPhysician()
        {
            bool succes;
            var physician = ConsoleService.GetPhysicianDetails();
            try
            {
                succes = await _mediator?.Send(physician);
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                succes = false;
            }

            ConsoleService.ShowSuccessOrFailedMessage(succes, "Arts is toegevoegd", "Arts is niet toegevoegd probeer opnieuw");
        }

        private static async Task DeletePatient()
        {
            bool succes;
            var patient = ConsoleService.GetPatientInformationToDelete();
            
            try
            {
                succes = await _mediator?.Send(patient)!;
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                succes = false;
            }
            
            ConsoleService.ShowSuccessOrFailedMessage(succes, "Patient is verwijderd", "Patient is niet verwijderd probeer opnieuw");
        }


        #region FreeCodeForAssignment
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .RegisterApplication()
                .RegisterInfrastructure()
                .BuildServiceProvider();
             
            _mediator = serviceProvider.GetService<IMediator>();
            
            while (ShowMenu())
            {
                //Continue
            }
        }

        public static bool ShowMenu()
        {
            Console.Clear();
            foreach (var line in File.ReadAllLines("logo.txt"))
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("");
            Console.WriteLine("1 - Patient toevoegen");
            Console.WriteLine("2 - Patienten verwijderen");
            Console.WriteLine("3 - Arts toevoegen");
            Console.WriteLine("4 - Arts verwijderen");
            Console.WriteLine("5 - Afspraak toevoegen");
            Console.WriteLine("6 - Afspraken inzien");
            Console.WriteLine("7 - Sluiten");
            Console.WriteLine("8 - Reset db");

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                switch (option)
                {
                    case 1:
                        AddPatient();
                        return true;
                    case 2:
                        DeletePatient();
                        return true;
                    case 3:
                        AddPhysician();
                        return true;
                    case 4:
                        DeletePhysician();
                        return true;
                    case 5:
                        AddAppointment();
                        return true;
                    case 6:
                        ShowAppointment();
                        return true;
                    case 7:
                        return false;
                    case 8:
                        EPDDbContext dbContext = new EPDDbContext();
                        dbContext.Database.EnsureDeleted();
                        dbContext.Database.EnsureCreated();
                        return true;
                    default:
                        return true;
                }
            }
            return true;
        }

        #endregion
    }
}