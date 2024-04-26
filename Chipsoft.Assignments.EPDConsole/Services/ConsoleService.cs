using Chipsoft.Assignments.EPDContracts.Appointment;
using Chipsoft.Assignments.EPDContracts.Patients;
using Chipsoft.Assignments.EPDContracts.Physician;
using Chipsoft.Assignments.EPDDomain;

namespace Chipsoft.Assignments.EPDConsole.Services;

public static class ConsoleService
{
    public static AddPatientCommand GetPatientDetails()
    {
        Console.WriteLine("Voer alstublieft de volgende gegevens van de patiënt in:");

        // Use helper methods to get user input
        var voornaam = GetUserInput("Voornaam: ");
        var achternaam = GetUserInput("Achternaam: ");
        DateOnly geboortedatum = GetDateInput("Geboortedatum (JJJJ-MM-DD): ");
        var straatnaam = GetUserInput("Straatnaam: ");
        var huisnummer = GetIntegerInput("Huisnummer: ");
        var postcode = GetUserInput("Postcode: ");
        var stad = GetUserInput("Stad: ");
        var email = GetUserInput("E-mail: ");
        var telefoonnummer = GetUserInput("Telefoonnummer: ");
        
        return new AddPatientCommand
        {
            Birthdate = geboortedatum,
            Firstname = voornaam,
            Lastname = achternaam,
            Streetname = straatnaam,
            Housenumber = huisnummer,
            PostalCode = postcode,
            City = stad,
            Email = email,
            PhoneNumber = telefoonnummer
        };
    }
    
    //Delete patient method
    public static DeletePatientCommand GetPatientInformationToDelete()
    {
        //first last name and birthdate
        Console.WriteLine("Voer alstublieft de volgende gegevens van de patiënt in voor het verwijderen:");
        var (voornaam, achternaam, geboortedatum) = GetUserIdentification();
        
        return new DeletePatientCommand
        {
            Firstname = voornaam,
            Lastname = achternaam,
            Birthdate = geboortedatum
        };
    }
    
    //Add physician method
    public static AddPhysicianCommand GetPhysicianDetails()
    {
        Console.WriteLine("Voer alstublieft de volgende gegevens van de arts in:");

        // Use helper methods to get user input
        var voornaam = GetUserInput("Voornaam: ");
        var achternaam = GetUserInput("Achternaam: ");
        var specialisatie = GetUserInput("Specialisatie: ");
        var stad = GetUserInput("Stad: ");
        var straatnaam = GetUserInput("Straatnaam: ");
        var huisnummer = GetIntegerInput("Huisnummer: ");
        var postcode = GetUserInput("Postcode: ");
        DateOnly geboortedatum = GetDateInput("Geboortedatum (JJJJ-MM-DD): ");
       
        
        return new AddPhysicianCommand
        {
            Firstname = voornaam,
            Lastname = achternaam,
            PhysicianType = specialisatie,
            City = stad,
            Streetname = straatnaam,
            Housenumber = huisnummer,
            PostalCode = postcode,
            Birthdate = geboortedatum
        };
    }
    
    //Delete physician method
    public static DeletePhysicianCommand GetPhysicianInformationToDelete()
    {
        Console.WriteLine("Voer alstublieft de volgende gegevens van de arts in voor het verwijderen:");
        var (voornaam, achternaam, geboortedatum) = GetUserIdentification();

        return new DeletePhysicianCommand
        {
            Firstname = voornaam,
            Lastname = achternaam,
            Birthdate = geboortedatum
        };
    }
    
    //Add appointment method
    public static AddAppointmentCommand GetAppointmentDetails()
    {
        Console.Clear();
        Console.WriteLine("Voer alstublieft de volgende gegevens van de afspraak in:");

        // Use helper methods to get user input
        Console.WriteLine("Gegevens patient:");
        var (voornaam, achternaam, geboortedatum) = GetUserIdentification();
        
        Console.WriteLine("Gegevens arts:");
        var (artsVoornaam, artsAchternaam, artsGeboortedatum) = GetUserIdentification();
        
        var datum = GetDateTimeInput("Datum afspraak: ");
        var beschrijving = GetUserInput("Beschrijving (optioneel): ", false);
        
        return new AddAppointmentCommand
        {
            PatientFirstname = voornaam,
            PatientLastname = achternaam,
            PatientBirthdate = geboortedatum,
            PhysicianFirstname = artsVoornaam,
            PhysicianLastname = artsAchternaam,
            PhysicianBirthdate = artsGeboortedatum,
            StartDate = datum,
            Description = beschrijving
        };
    }
    
    //Show all appointments method
    public static void ShowAllAppointments(IEnumerable<Appointment> appointments)
    {
        Console.Clear();
        Console.WriteLine("Alle afspraken:");
        var enumerable = appointments.ToList();
        if (!enumerable.Any())
        {
            Console.WriteLine("Er zijn geen afspraken gevonden.");
            Console.WriteLine("");
            Console.WriteLine("Druk op een toets om terug te gaan");
            Console.ReadKey();
            return;
        }
        
        LoopThruAppointmentsAndShow(enumerable);
        
        Console.WriteLine("");
        //Ask question for getting specific appointment for doctor or patient
        ShowSpecificAppointment(enumerable);

        Console.WriteLine("");
        Console.WriteLine("Druk op een toets om terug te gaan");
        Console.ReadKey();
        
    }

    private static void ShowSpecificAppointment(List<Appointment> enumerable)
    {
        Console.WriteLine("Wilt u de afspraken opvragen voor een specifieke arts of patient? (ja/nee)");
        string? input = Console.ReadLine();
        if (input?.ToLower() != "ja") return;
        Console.Clear();
        Console.WriteLine("Voer de voornaam van de arts of patient in:");
        string? voornaam = Console.ReadLine();
        Console.WriteLine("Voer de achternaam van de arts of patient in:");
        string? achternaam = Console.ReadLine();
        var filteredAppointments = enumerable.Where(appointment =>
            appointment.Patient.Firstname.ToLower() == voornaam.ToLower() &&
            appointment.Patient.Lastname.ToLower() == achternaam.ToLower() ||
            appointment.Physician.Firstname.ToLower() == voornaam.ToLower() &&
            appointment.Physician.Lastname.ToLower() == achternaam.ToLower());
        var appointments = filteredAppointments.ToList();
        if (!appointments.Any())
        {
            Console.WriteLine("Er zijn geen afspraken gevonden voor deze arts of patient.");
        }
        else
        {
            LoopThruAppointmentsAndShow(appointments);
        }
    }
    
    public static void ShowSuccessOrFailedMessage(bool isSuccess, string successMessage, string failedMessage)
    {
        if (isSuccess)
        {
            Console.Clear();
        }
        Console.WriteLine(isSuccess ? successMessage : failedMessage);

        Console.WriteLine("");
        Console.WriteLine("Druk op een toets om terug te gaan");
        Console.ReadKey();
    }

    private static void LoopThruAppointmentsAndShow(List<Appointment> appointments)
    {
        foreach (var appointment in appointments)
        {
            Console.WriteLine($"Datum: {appointment.StartDate},");
            Console.WriteLine($"Beschrijving: {appointment.Description},");
            Console.WriteLine($"Patient: {appointment.Patient.Firstname} {appointment.Patient.Lastname},");
            Console.WriteLine($"Arts: {appointment.Physician.Firstname} {appointment.Physician.Lastname}");
            Console.WriteLine(new string('-', 30)); // Separator
        }
    }

    private static (string voornaam, string achternaam, DateOnly geboortedatum) GetUserIdentification()
    {
        var voornaam = GetUserInput("Voornaam: ");
        var achternaam = GetUserInput("Achternaam: ");
        DateOnly geboortedatum = GetDateInput("Geboortedatum (JJJJ-MM-DD): ");
        return (voornaam, achternaam, geboortedatum);
    }

    private static string GetUserInput(string prompt, bool isRequired = true)
    {
        Console.Write(prompt);
        
        //check if the input is not empty
        string input = Console.ReadLine() ?? string.Empty;

        if (!isRequired) return input;
        //Initial check if the input is empty (better user experience then checking validation at the end)
        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Dit veld mag niet leeg zijn.");
            Console.Write(prompt);
            input = Console.ReadLine() ?? string.Empty;
        }


        return input;
    }
    
    private static int GetIntegerInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            //Initial check if the input is empty (better user experience then checking validation at the end)
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                return result;
            }
            Console.WriteLine("Ongeldige invoer. Voer alstublieft een geheel getal in.");
        }
    }

    private static DateOnly GetDateInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            //Initial check if the input is empty (better user experience then checking validation at the end)
            if (DateOnly.TryParse(input, out DateOnly date))
            {
                return date;
            }
            Console.WriteLine("Ongeldige datumnotatie. Voer alstublieft in het formaat JJJJ-MM-DD.");
        }
    }
    
    //get datetime input
    private static DateTime GetDateTimeInput(string prompt)
    {
        DateTime finalDateTime = DateTime.MinValue;
        bool dateParsed = false;
        bool timeParsed = false;

        while (true)
        {
            // Get the date input
            if (!dateParsed)
            {
                Console.Write(prompt + " (YYYY-MM-DD): ");
                string dateInput = Console.ReadLine();
                if (DateTime.TryParse(dateInput, out DateTime date))
                {
                    finalDateTime = date.Date;
                    dateParsed = true;
                }
                else
                {
                    Console.WriteLine("Ongeldige datumnotatie. Voer de datum in het formaat JJJJ-MM-DD.");
                    continue;
                }
            }

            // Get the time input
            if (dateParsed && !timeParsed)
            {
                Console.Write(prompt + " (HH:MM): ");
                string timeInput = Console.ReadLine();
                if (TimeSpan.TryParse(timeInput, out TimeSpan time))
                {
                    finalDateTime = finalDateTime.Add(time);
                    timeParsed = true;
                }
                else
                {
                    Console.WriteLine("Ongeldige tijdnotatie. Voer de tijd in het formaat HH:MM.");
                    continue;
                }
            }

            // If both date and time have been parsed, break the loop
            if (dateParsed && timeParsed)
            {
                break;
            }
        }

        return finalDateTime;
    }
}