namespace Healthcare_Management_System_with_Functions
{
    internal class Program
    {
        // global scope array of data storage

        static List<string> patientNames = new List<string>(); // to store patient names, using List for dynamic sizing
        static List<string> patientIDs = new List<string>(); // to store patient IDs, using List for dynamic sizing
        static List<string> diagnoses = new List<string>(); // to store patient diagnoses, using List for dynamic sizing
        static List<bool> admitted = new List<bool>(); // to store admission status, using List for dynamic sizing
        static List<string> assignedDoctors = new List<string>(); // to store assigned doctor names, using List for dynamic sizing
        static List<string> departments = new List<string>(); // to store patient departments, using List for dynamic sizing
        static List<int> visitCount = new List<int>(); // to store patient visit counts, using List for dynamic sizing
        static List<double> billingAmount = new List<double>(); // to store patient billing amounts, using List for dynamic sizing
        static List<DateTime> lastVisitDate = new List<DateTime>(); // to store patient last visit dates, using List for dynamic sizing
        static List<DateTime> lastDischargeDate = new List<DateTime>(); // to store patient last discharge dates, using List for dynamic sizing
        static List<int> daysInHospital = new List<int>(); // to store total days in hospital for each patient, using List for dynamic sizing
        static List<string> bloodType = new List<string>(); // to store patient blood types, using List for dynamic sizing
        static List<string> doctorNames = new List<string>(); // to store doctor names, using List for dynamic sizing
        static List<int> doctorAvailableSlots = new List<int>(); // to store doctor available slots, using List for dynamic sizing
        static List<int> doctorVisitCount = new List<int>(); // to store doctor visit counts, using List for dynamic sizing



        //////////////////////////////////////////////////////////////////////////////////////////

        // seed of  data for testing

        static public void SeedData()
        {
            //Patient 1

            patientNames.Add("Ali Hassan");
            patientIDs.Add("P001");
            diagnoses.Add("Flu");
            admitted.Add(false);
            assignedDoctors.Add("");
            departments.Add("General");
            visitCount.Add(2);
            billingAmount.Add(0);
            lastVisitDate.Add(DateTime.Parse("2025-01-10"));
            lastDischargeDate.Add(DateTime.Parse("2025-01-15"));
            daysInHospital.Add(12);
            bloodType.Add("A+");

            //Patient 2

            patientNames.Add("Sara Ahmed");
            patientIDs.Add("P002");
            diagnoses.Add("Fracture");
            admitted.Add(true);
            assignedDoctors.Add("Noor");
            departments.Add("Orthopedics");
            visitCount.Add(4);
            billingAmount.Add(0);
            lastVisitDate.Add(DateTime.Parse("2025-03-02"));
            lastDischargeDate.Add(DateTime.MinValue);
            daysInHospital.Add(8);
            bloodType.Add("O-");

            //Patient 3

            patientNames.Add("Omar Khalid");
            patientIDs.Add("P003");
            diagnoses.Add("Diabetes");
            admitted.Add(false);
            assignedDoctors.Add("");
            departments.Add("Cardiology");
            visitCount.Add(1);
            billingAmount.Add(0);
            lastVisitDate.Add(DateTime.Parse("2024-12-20"));
            lastDischargeDate.Add(DateTime.Parse("2024-12-28"));
            daysInHospital.Add(5);
            bloodType.Add("B+");

            //Doctor 1

            doctorNames.Add("Noor");
            doctorAvailableSlots.Add(5);
            doctorVisitCount.Add(0);

            //Doctor 2

            doctorNames.Add("Salem");
            doctorAvailableSlots.Add(3);
            doctorVisitCount.Add(0);

            //Doctor 3

            doctorNames.Add("Hana");
            doctorAvailableSlots.Add(8);
            doctorVisitCount.Add(0);

        }

        // display menu
        static public void DisplayMenu()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("===== Healthcare Management System =====");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1. Register New Patient.");
            Console.WriteLine("2. Admit Patient.");
            Console.WriteLine("3. Discharge Patient.");
            Console.WriteLine("4. Search Patient.");
            Console.WriteLine("5. List All dmitted Patients.");
            Console.WriteLine("6. Transfer Patient to Another Doctor.");
            Console.WriteLine("7. View Most Visited Patients.");
            Console.WriteLine("8. Search Patients by Department.");
            Console.WriteLine("9. Billing Report.");
            Console.WriteLine("10. Exit.");
            Console.WriteLine("11. Add Doctor");
            Console.WriteLine("12. Doctor Salary Report");
            Console.WriteLine("==========================================");
        }

        // select choice from menu and call 
        static public int SelectMenuChoice()
        {

            int option;

            while (true)
            {
                Console.Write("Choose option: ");
                string input = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(input, out option))
                {
                    if (option >= 1 && option <= 12)
                    {
                        return option;
                    }
                    else
                    {
                        Console.WriteLine("Please choose a number between 1 and 12.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

        }

        // case 1: register new patient
        static public string RegisterNewPatient()
        {
            Console.WriteLine("Enter patient name: ");
            string name = Console.ReadLine().ToLower();

            Console.WriteLine("Enter the diagnose: ");
            string diagnose = Console.ReadLine().ToLower();

            Console.WriteLine("Enter the department: ");
            string department = Console.ReadLine().ToLower();

            Console.WriteLine("Enter the blood type: ");
            string blood = Console.ReadLine().ToUpper();

            // check if patient already exists in the system by name or ID to prevent duplicates
            patientNames.Add(name);
            diagnoses.Add(diagnose);
            departments.Add(department);
            bloodType.Add(blood);
            admitted.Add(false);
            assignedDoctors.Add("");
            visitCount.Add(0);
            billingAmount.Add(0);
            lastDischargeDate.Add(DateTime.MinValue);
            lastVisitDate.Add(DateTime.MinValue);
            daysInHospital.Add(0);
            // generate patient ID based on the count of existing patients, formatted with leading zeros for consistency
            string newID = "P" + (patientIDs.Count + 1).ToString("D3");
            patientIDs.Add(newID);
            Console.WriteLine("Patient registered successfully. ID: " + newID);
            return newID;
        }

        // helper function to find patient 
        static public int FindPatient(string searchInput)
        {
            for (int i = 0; i < patientNames.Count; i++)
            {
                if (patientNames[i].Equals(searchInput, StringComparison.OrdinalIgnoreCase) || patientIDs[i].Equals(searchInput, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }

        // helper function to check if patient is currently admitted
        static public bool IsAdmitted(string InputSearch, out bool isFound)
        {
            int index = FindPatient(InputSearch);

            if (index != -1)
            {
                isFound = true;
                return admitted[index];
            }
            else
            {
                isFound = false;
                return false;
            }
        }

        // case 2: admit patient
        static public string GetAdmitted(string InputSearch)
        {
            bool Found;
            bool AdmittedStatus = IsAdmitted(InputSearch, out Found);
            int index = FindPatient(InputSearch);

            if (!Found)
            {
                return "patient not found";
            }

            if (index != -1)
            {
                if (AdmittedStatus)
                {
                    return "Patient is already admitted under " + assignedDoctors[index];
                }

                else
                {

                    Console.Write("Enter doctor name: ");
                    string doctorName = (Console.ReadLine() ?? string.Empty).ToLower().Trim();

                    string assignedDoctor = "";
                    int SlotsAvailable = 0;

                    int doctIndex = doctorNames.FindIndex(d => d.Equals(doctorName, StringComparison.OrdinalIgnoreCase));

                    if (doctIndex == -1)
                    {
                        return "Doctor not found in the system. Please register the doctor first.";
                    }

                    if (doctorAvailableSlots[doctIndex] <= 0)
                    {
                        return doctorNames[doctIndex] + " has no available slots at this time.";

                    }

                    // if doctor is found and has available slots, assign doctor to patient and update slots and visit count
                    doctorAvailableSlots[doctIndex]--;
                    doctorVisitCount[doctIndex]++;

                    SlotsAvailable = doctorAvailableSlots[doctIndex]; // to display remaining slots after assignment
                    assignedDoctor = doctorNames[doctIndex];

                    assignedDoctors[index] = assignedDoctor; // assign doctor to patient only if doctor is found and has available slots

                    // update patient admission status, visit count, and last visit date
                    visitCount[index]++;
                    lastVisitDate[index] = DateTime.Now;
                    lastDischargeDate[index] = DateTime.MinValue;
                    admitted[index] = true;

                    string visitMessage = visitCount[index] > 1 ? "This patient has been admitted " + visitCount[index] + " times." : "This is the first visit.";

                    Console.WriteLine("==============================================================================");
                    return "Patient admitted successfully and assigned to Dr. " + assignedDoctor + "\nThe admission date: " + lastVisitDate[index].ToString("yyyy-MM-dd HH:mm") + "\nThis patient has been admitted " + visitCount[index] + " times." +
                            "\n" + assignedDoctor + " now has: " + SlotsAvailable + " slot(s) remaining.";
                }
            }
            return "patient not found";
        }

        // case 3: discharge patient
        static public string DischargePatient(string InputSearch)
        {
            bool Found;
            bool AdmittedStatus = IsAdmitted(InputSearch, out Found);
            int index = FindPatient(InputSearch);

            if (!Found)
            {
                return "patient not found";
            }

            if (!AdmittedStatus)
            {
                return "Patient is not currently admitted.";
            }

            double visitCharge = 0; // fee for this Discharge
            string message = "";

            Console.WriteLine("Was there a consultation fee? (yes/no)");
            string fee = Console.ReadLine() ?? string.Empty;

            if (fee.Trim().Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                while (true)
                {
                    Console.WriteLine("Enter consultation fee amount: ");
                    string feeAmount = Console.ReadLine() ?? string.Empty;

                    if (double.TryParse(feeAmount, out double amount) && amount > 0)
                    {
                        billingAmount[index] += amount;
                        visitCharge += amount;
                        break;
                    }

                    else
                    {
                        message += "Invalid amount entered. Fee amount must be positive.\n";

                    }
                }
            }

            Console.WriteLine("Any medication charges? (yes/no)");
            string medication = Console.ReadLine() ?? string.Empty;

            if (medication.Trim().Equals("yes", StringComparison.OrdinalIgnoreCase))
            {

                while (true)
                {
                    Console.WriteLine("Enter medication charges: ");
                    string medCharge = Console.ReadLine() ?? string.Empty;

                    if (double.TryParse(medCharge, out double price) && price > 0)
                    {

                        billingAmount[index] += price;
                        visitCharge += price;
                        break;
                    }
                    else
                    {
                        message += "Invalid amount entered. Medication price must be positive\n";
                    }
                }
            }

            if (billingAmount[index] > 0)
            {
                message += "Total charges added this visit: " + Math.Round(visitCharge, 2) + " OMR\n" + "Total billing amount for this patient: " + Math.Round(billingAmount[index], 2) + " OMR\n";
            }

            else
            {
                message += "No charges recorded\n";
            }

            // find the assigned doctor index to update available slots and visit count
            int doctorIndex = doctorNames.FindIndex(d => d.ToLower() == assignedDoctors[index].ToLower());

            if (doctorIndex != -1)
            {
                // update doctor available slots and visit count when patient is discharged
                doctorAvailableSlots[doctorIndex]++;
                int AvailableSlots = doctorAvailableSlots[doctorIndex];
                string doctorDisplay = doctorNames[doctorIndex].Replace("Dr ", "Dr. ");
                message += "Dr. " + doctorDisplay + " now has: " + AvailableSlots + " slot(s) available.";
            }

            if (doctorIndex == -1 && !string.IsNullOrEmpty(assignedDoctors[index]))
            {
                message += "Warning: assigned doctor not found in registry.Slots not updated."; // to handle edge case where assigned doctor name was manually entered but does not match any registered doctor, preventing system from updating slots and avoiding potential errors
            }

            // update patient to discharge
            admitted[index] = false;
            lastDischargeDate[index] = DateTime.Now;
            daysInHospital[index] += (int)Math.Floor((DateTime.Now - lastVisitDate[index]).TotalDays);

            message += "\nPatient discharged successfully, with total days in hospital: " + daysInHospital[index];

            assignedDoctors[index] = "";

            Console.WriteLine("==============================================================================");
            return message;
        }

        // case 4: search patient 
        static public string SearchPatient(string InputSearch)
        {
            bool Found;
            int index = FindPatient(InputSearch);
            bool AdmittedStatus = IsAdmitted(InputSearch, out Found);

            if (!Found)
            {
                return "patient not found";
            }

            string status = admitted[index] ? "Admitted" : "Not Admitted";
            Console.WriteLine("==============================================================================");
            string message = "Patient name: " + patientNames[index] + ",\nPatient ID: " + patientIDs[index].ToUpper() + ",\nDiagnosis: " + diagnoses[index] +
                                " ( " + diagnoses[index].Length + " characters)" + ",\nDepartment: " + departments[index] + ",\nAdmission status: " + status +
                                ",\nVisit count: " + visitCount[index] + ",\ntotal billing amount: " + Math.Round(billingAmount[index], 2).ToString() +
                                ",\nBlood Type: " + bloodType[index] + ",\nTotal Days in Hospital: " + daysInHospital[index];

            if (AdmittedStatus)
            {
                message += "\n" + "Assigned doctor: " + assignedDoctors[index];
            }

            if (lastVisitDate[index] != DateTime.MinValue)
            {
                message += "\nLast visit date: " + lastVisitDate[index].ToString("yyyy-MM-dd");
            }
            else
            {
                message += "\nNo admission recorded.";
            }

            if (lastDischargeDate[index] != DateTime.MinValue)
            {
                message += "\nLast discharge date: " + lastDischargeDate[index].ToString("yyyy-MM-dd");
            }

            else
            {
                message += "\nThe patient still admitted.";
            }

            return message;
        }

        // case 5: list all admitted patients 
        static public void ListAdmittedPatients()
        {
            Console.WriteLine("Filter by name keyword (press Enter to skip): ");
            string keyword = (Console.ReadLine() ?? string.Empty).ToLower();

            int Count = 0;
            double maxBilling = 0;
            bool HasAdmitted = false;


            for (int i = 0; i < patientNames.Count; i++)
            {
                if (!admitted[i])
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(keyword) && !patientNames[i].ToLower().Contains(keyword) && patientIDs[i].ToLower().Contains(keyword))
                    continue;

                HasAdmitted = true;
                Count++;
                maxBilling = Math.Max(maxBilling, billingAmount[i]); // to track the running maximum
                Console.WriteLine("Patient name: " + patientNames[i] + ",\nPatient ID: " + patientIDs[i] + ",\nDiagnosis: " + diagnoses[i] + ",\nDepartment: " + departments[i] + ",\nAdmission status: " + admitted[i] + ",\nVisit count: " + visitCount[i] + ",\ntotal billing amount: " + billingAmount[i] + ",\nAssigned doctor: " + assignedDoctors[i] + ",\nAdmitted since: " + lastVisitDate[i].ToString("yyyy-MM-dd HH:mm"));
            }

            Console.WriteLine("==================================================");

            if (HasAdmitted)
            {
                Console.WriteLine("The  total admitted count is: " + Count);
                Console.WriteLine("The highest billing amount among admitted patients is: " + Math.Round(maxBilling, 2) + " OMR");
            }

            else
            {
                Console.WriteLine("No patient admitted.");
            }

        }

        // case 6: transfer patient to another doctor 
        static public void TransferPatient(string currentDoctor, string newDoctor)
        {
            currentDoctor = currentDoctor.Trim();
            newDoctor = newDoctor.Trim();

            // check if new doctor exists in the system
            int newDocIndex = doctorNames.FindIndex(d => d.Trim().Equals(newDoctor.Trim(), StringComparison.OrdinalIgnoreCase));

            if (newDocIndex == -1)
            {
                Console.WriteLine("New doctor not found!");
                return;
            }

            bool doctorFound = false;
            bool admittedPatientFound = false;

            for (int i = 0; i < patientNames.Count; i++)
            {
                if (assignedDoctors[i].Trim().Equals(currentDoctor.Trim(), StringComparison.OrdinalIgnoreCase)) // find current Doctor 
                {
                    doctorFound = true;

                    if (admitted[i])
                    {
                        admittedPatientFound = true;

                        if (newDoctor != currentDoctor)
                        {
                            assignedDoctors[i] = newDoctor;
                            Console.WriteLine("Patient name: " + patientNames[i] + " has been transferred to " + newDoctor);
                            Console.WriteLine("Patient last admitted on: " + lastVisitDate[i]);
                        }

                        else
                        {
                            Console.WriteLine("Doctor names must be different!");
                            break;
                        }

                    }

                }
            }

            if (doctorFound == false)
            {
                Console.WriteLine("Doctor not found!.");
            }

            else if (!admittedPatientFound)
            {
                Console.WriteLine("No admitted patient found under this doctor");
            }

        }

        // case 7: view most visited patients 
        static public void ViewMostVisitedPatients()
        {
            for (int visit = 100; visit >= 0; visit--) // to display patients in descending order of visit count
            {
                for (int i = 0; i < patientNames.Count; i++)
                {
                    if (visitCount[i] == visit) // to display all patients with the same visit count together
                    {
                        Console.WriteLine("Patient name: " + patientNames[i] + ",\nPatient ID: " + patientIDs[i] + ",\nDiagnosis: " + diagnoses[i] + ",\nDepartment: " + departments[i] + ",\nVisit count: " + visitCount[i]);
                        Console.WriteLine("----------------------------");
                    }
                }
            }
        }

        // case 8: search patients by department
        static public void SearchPatientsByDepartment(string department)
        {
            bool patFound = false;
            string dep = department.Trim().ToLower();
            for (int i = 0; i < patientNames.Count; i++)
            {
                if (!string.IsNullOrEmpty(departments[i]) && departments[i].ToLower().Contains(dep))
                {
                    patFound = true;

                    string diagnosisDisplay;

                    if (diagnoses[i].Length > 15)
                    {
                        diagnosisDisplay = diagnoses[i].Substring(0, 15) + "...";
                    }
                    else
                    {
                        diagnosisDisplay = diagnoses[i];
                    }

                    string AdmissionStatus = admitted[i] ? "Admitted" : "Not Admitted";
                    Console.WriteLine("Patient name: " + patientNames[i] + ",\nPatient ID: " + patientIDs[i] + ",\nDiagnosis: " + diagnosisDisplay + ",\nStatus: " + AdmissionStatus + ",\nBlood type: " + bloodType[i]);
                    Console.WriteLine("============================================================");
                }
            }

            if (!patFound)
            {
                Console.WriteLine("No patients found in this department");
            }


        }

        // select choice from billing menu
        static public int SelectChoice()
        {
            int option;

            while (true)
            {
                Console.WriteLine("Enter your choice: ");
                string choice = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(choice, out option))
                {

                    if (option == 1 || option == 2)
                    {
                        return option;
                    }

                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                    }
                }
            }
        }

        // case 9: billing report
        static public void BillingReport()
        {
            Console.WriteLine("===== Sub Menu =====");
            Console.WriteLine("1. System billing amount.");
            Console.WriteLine("2. Individual patient billing.");

            int choice = SelectChoice();
            if (choice == 1)
            {
                double TotalAmount = 0;
                double maxIndividualBilling = 0;
                double minIndividualBilling = double.MaxValue;

                for (int i = 0; i < patientNames.Count; i++)
                {

                    TotalAmount += billingAmount[i];

                    if (billingAmount[i] > 0)
                    {
                        maxIndividualBilling = Math.Max(maxIndividualBilling, billingAmount[i]);
                        minIndividualBilling = Math.Min(minIndividualBilling, billingAmount[i]);
                    }
                }

                Console.WriteLine("Total amount = " + Math.Round(TotalAmount, 2) + " OMR");

                if (minIndividualBilling != double.MaxValue)
                {
                    Console.WriteLine("Highest individual billing: " + Math.Round(maxIndividualBilling, 2) + "OMR");
                    Console.WriteLine("Lowest individual billing: " + Math.Round(minIndividualBilling, 2) + "OMR");
                }

            }


            else if (choice == 2)
            {

                Console.WriteLine("Enter patient ID or patient name: ");
                string patientInfo = Console.ReadLine() ?? string.Empty;

                int index = FindPatient(patientInfo);

                if (index != -1)
                {
                    Console.WriteLine("Patient name: " + patientNames[index] + ",\nPatient ID: " + patientIDs[index] + ",\nTotal billing amount: " + Math.Round(billingAmount[index], 2) + " OMR");

                    Random rand = new Random(); // to generate random invoice number for demonstration purposes
                    int randomDiscount = rand.Next(5, 21); // Generate a random invoice number between 5 and 20

                    double discountAmount = billingAmount[index] * (randomDiscount / 100); // Calculate the discount amount based on the random percentage

                    double finalAmount = billingAmount[index] - discountAmount; // Calculate the final amount after applying the discount
                    finalAmount = Math.Round(finalAmount, 2); // Round the final amount to 2 decimal places

                    Console.WriteLine("Discount applied: " + randomDiscount + "% - Amount after discount: " + finalAmount + " OMR"); // Display the random invoice number
                }

                else

                {
                    Console.WriteLine("Patient not found.");
                }
            }

        }

        // case 10: exit program
        static public bool ExitProgram()
        {
            Console.WriteLine("Are you sure you want to exit? (yes/no): ");
            string confirmExit = Console.ReadLine() ?? string.Empty;

            if (confirmExit == "yes")
            {
                Console.WriteLine("Exiting system...");
                Console.WriteLine("Thank you for using the Healthcare Management System!");
                return true;
            }

            else
            {
                Console.WriteLine("Returning to menu...");
                return false;
            }
        }

        // case 11: add doctor
        static public void AddDoctor()
        {
            Console.WriteLine("Enter doctor full name: ");
            string name = (Console.ReadLine() ?? string.Empty).Trim();

            // check if doctor already exists in the system 
            for (int i = 0; i < doctorNames.Count; i++)
            {
                if (doctorNames[i].ToLower() == name.ToLower())
                {
                    Console.WriteLine("Doctor already exists. Registration cancelled.");
                    return;
                }
            }

            // format doctor name to have first letter capitalized and the rest in lowercase for consistency
            name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
            doctorNames.Add(name);

            Console.WriteLine("Enter available slots for this doctor: ");
            int slots;

            while (!int.TryParse(Console.ReadLine(), out slots) || slots < 1 || slots > 50)
            {
                Console.WriteLine("Invalid input. Enter slots between 1 and 50: ");
            }

            doctorAvailableSlots.Add(slots);
            doctorVisitCount.Add(0);
            Console.WriteLine("Doctor: " + name + ", added successfully with available slots : " + slots);
        }

        // case 12: doctor salary report
        static void DoctorSalaryReport()
        {
            Console.WriteLine("===== Doctor Salary Report =====");
            Console.WriteLine("================================");

            double maxSalary = 0;
            string DoctorName = "";
            const double BASE_SALARY = 300; // base salary for all doctors
            const double BONUS_PER_VISIT = 15; // bonus amount added to salary for each patient assigned

            for (int i = 0; i < doctorNames.Count; i++)
            {
                double salary = Math.Round(BASE_SALARY + (doctorVisitCount[i] * BONUS_PER_VISIT), 2);

                double newMax = Math.Max(maxSalary, salary);

                if (newMax > maxSalary)
                {
                    maxSalary = newMax;
                    DoctorName = doctorNames[i];
                }

                Console.WriteLine("Dr.: " + doctorNames[i] + "| Total Patients Assigned: " + doctorVisitCount[i] + "| Salary: " + salary.ToString() + " OMR");

            }

            Console.WriteLine("=============================================================");
            Console.WriteLine("Highest earning doctor: " + DoctorName + "-" + maxSalary + " OMR");

        }

        // main function to run the program
        static void Main(string[] args)
        {
            SeedData();

            bool exit = false;
            while (exit == false)
            {

                DisplayMenu();

                int choice = SelectMenuChoice();

                switch (choice)
                {
                    case 1:

                        string PID = RegisterNewPatient();

                        Console.WriteLine("Patient registered successfully with ID: " + PID);

                        break;

                    case 2:

                        Console.WriteLine("Enter patient ID or patient name: ");
                        string patientInfo = Console.ReadLine() ?? string.Empty;

                        string AdmittedOutput = GetAdmitted(patientInfo);

                        Console.WriteLine(AdmittedOutput);

                        break;

                    case 3:

                        Console.WriteLine("Enter patient ID or patient name: ");
                        string patientInfoDischarge = Console.ReadLine() ?? string.Empty;

                        // call discharge function here and print output
                        string DischargeOutput = DischargePatient(patientInfoDischarge);
                        Console.WriteLine(DischargeOutput);

                        break;

                    case 4:

                        Console.WriteLine("Enter patient ID or patient name: ");
                        string patientInfoSearch = Console.ReadLine() ?? string.Empty;

                        // call search function here and print output
                        string searchOutput = SearchPatient(patientInfoSearch);
                        Console.WriteLine(searchOutput);

                        break;

                    case 5:

                        ListAdmittedPatients();

                        break;

                    case 6:

                        Console.WriteLine("Enter current doctor name: ");
                        string currentDoc = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Enter new doctor name: ");
                        string newDoc = Console.ReadLine() ?? string.Empty;

                        TransferPatient(currentDoc, newDoc);

                        break;

                    case 7:

                        ViewMostVisitedPatients();

                        break;

                    case 8:

                        Console.WriteLine("Enter department name: ");
                        string dept = Console.ReadLine() ?? string.Empty.ToUpper();

                        SearchPatientsByDepartment(dept);

                        break;

                    case 9:

                        BillingReport();

                        break;

                    case 10:

                        exit = ExitProgram();

                        break;

                    case 11:

                        AddDoctor();

                        break;

                    case 12:

                        DoctorSalaryReport();

                        break;

                    default:

                        Console.WriteLine("Invalid choice. Please select a valid option from the menu.");

                        break;


                }

                Console.WriteLine("Press any key to continue....");
                Console.ReadKey();
                Console.Clear();
            }

        }
    }
}


