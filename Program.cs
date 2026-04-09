using System.Xml.Linq;

namespace Healthcare_Management_System_with_Function
{
    internal class Program
    {
        // global scope array of data storage

        static string[] patientNames = new string[100];
        static string[] patientIDs = new string[100];
        static string[] diagnoses = new string[100];
        static bool[] admitted = new bool[100];       // true = currently admitted
        static string[] assignedDoctors = new string[100];
        static string[] departments = new string[100];     // e.g. "Cardiology", "Orthopedics"
        static int[] visitCount = new int[100];        // how many times admitted
        static double[] billingAmount = new double[100];     // total fees owed
        static DateTime[] lastVisitDate = new DateTime[100];
        static DateTime[] lastDischargeDate = new DateTime[100];
        static int[] daysInHospital = new int[100];
        static string[] bloodType = new string[100];
        static int PatientIndex = -1;


        //////////////////////////////////////////////////////////////////////////////////////////

        // seed of  data for testing

        static public void SeedData()
        {
            //Patient 1

            PatientIndex++;

            patientNames[PatientIndex] = "Ali Hassan";
            patientIDs[PatientIndex] = "P001";
            diagnoses[PatientIndex] = "Flu";
            admitted[PatientIndex] = false;
            assignedDoctors[PatientIndex] = "";
            departments[PatientIndex] = "General";
            visitCount[PatientIndex] = 2;
            billingAmount[PatientIndex] = 0;
            lastVisitDate[PatientIndex] = DateTime.Parse("2025-01-10");
            lastDischargeDate[PatientIndex] = DateTime.Parse("2025-01-15");
            daysInHospital[PatientIndex] = 12;
            bloodType[PatientIndex]  = "A+";

            //Patient 2

            PatientIndex++;

            patientNames[PatientIndex] = "Sara Ahmed";
            patientIDs[PatientIndex] = "P002";
            diagnoses[PatientIndex] = "Fracture";
            admitted[PatientIndex] = true;
            assignedDoctors[PatientIndex] = "Dr. Noor";
            departments[PatientIndex] = "Orthopedics";
            visitCount[PatientIndex] = 4;
            billingAmount[PatientIndex] = 0;
            lastVisitDate[PatientIndex] = DateTime.Parse("2025-03-02");
            lastDischargeDate[PatientIndex] = DateTime.MinValue;
            daysInHospital[PatientIndex] = 8;
            bloodType[PatientIndex] = "O-";

            //Patient 3

            PatientIndex++;

            patientNames[PatientIndex] = "Omar Khalid";
            patientIDs[PatientIndex] = "P003";
            diagnoses[PatientIndex] = "Diabetes";
            admitted[PatientIndex] = false;
            assignedDoctors[PatientIndex] = "";
            departments[PatientIndex] = "Cardiology";
            visitCount[PatientIndex] = 1;
            billingAmount[PatientIndex] = 0;
            lastVisitDate[PatientIndex] = DateTime.Parse("2024-12-20");
            lastDischargeDate[PatientIndex] = DateTime.Parse("2024-12-28");
            daysInHospital[PatientIndex] = 5;
            bloodType[PatientIndex] = "B+";

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
            Console.WriteLine("==========================================");
        }

        // select choice from menu and call 
        static public int SelectMenuChoice()
        {
            Console.Write("Choose option: ");

            int option = 0;
            try
            {
                option = int.Parse(Console.ReadLine());
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Invalid input. Please choose a number from 1 to 10.");
            }
            return option;
        }

        // case 1: register new patient
        static public string RegisterNewPatient()
        {
            PatientIndex++;

            Console.WriteLine("Enter patient name: ");
            patientNames[PatientIndex] = Console.ReadLine().Trim();

            Console.WriteLine("Enter the diagnose: ");
            diagnoses[PatientIndex] = Console.ReadLine().Trim();
                        
            Console.WriteLine("Enter the department: ");
            departments[PatientIndex] = Console.ReadLine().Trim();

            Console.WriteLine("Enter the blood type: ");
            bloodType[PatientIndex] = Console.ReadLine().ToUpper();

            patientIDs[PatientIndex] = "P" + (PatientIndex + 1).ToString("D3");

            admitted[PatientIndex] = false;
            assignedDoctors[PatientIndex] = "";
            visitCount[PatientIndex] = 0;
            billingAmount[PatientIndex] = 0;
            lastDischargeDate[PatientIndex] = DateTime.MinValue;
            lastVisitDate[PatientIndex] = DateTime.MinValue;
            daysInHospital[PatientIndex] = 0;
            return patientIDs[PatientIndex];
        }

        // helper function to find patient 
        static public int FindPatient(string searchInput)
        {
            string Input = Console.ReadLine().ToUpper();

            int PatientFound = -1;
            for (int i = 0; i <= PatientIndex; i++)
            {
                if (Input == patientIDs[i] || Input == patientNames[i])
                {
                    PatientFound = i;
                    break;
                }
            }
            return PatientFound;  // not found
        }

        // helper function to check if patient is currently admitted
        static public bool IsAdmitted(string InputSearch, out bool isFound)
        {
            int FoundPatient = FindPatient(InputSearch);
            
            if (FoundPatient != -1)
            {
                isFound = true; // patient found
                return admitted[FoundPatient]; // return true if admitted, false if not
            }
            isFound = false; // patient not found
            return false;   // invalid index
        }

        // case 2: admit patient
        static public string GetAdmitted(string InputSearch, string doctorName)
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


                    if (string.IsNullOrWhiteSpace(doctorName)) // Empty value is not allowed
                    {
                        return "Doctor name cannot be empty.";
                    }

                    assignedDoctors[index] = doctorName;
                    visitCount[index]++;
                    lastVisitDate[index] = DateTime.Now;
                    lastDischargeDate[index] = DateTime.MinValue;
                    admitted[index] = true;


                    if (visitCount[index] > 1)
                    {
                        Console.WriteLine("This patient has been admitted " + visitCount[index] + " times.");
                    }

                    else
                    {
                        Console.WriteLine("This is the frist visit.");
                    }
            }

            return "Patient admitted successfully and assigned to " + assignedDoctors[index] + "\nThe admission date: " + lastVisitDate[index].ToString("yyyy-MM-dd HH:mm") + "\nThis patient has been admitted " + visitCount[index] + " times.";

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
            if (index != -1)
            {
                if (!AdmittedStatus)
                {
                    return "Patient is not currently admitted.";
                }

                double visitCharge = 0; // fee for this Discharge
                string message = "";

                Console.WriteLine("Was there a consultation fee? (yes/no)");
                string fee = Console.ReadLine();

                if (fee.ToLower() == "yes")
                {
                    double amount = 0;
                    bool amountValid = false;

                    while (!amountValid)
                    {
                        Console.WriteLine("Enter consultation fee amount: ");
                        string feeAmount = Console.ReadLine();


                        if (double.TryParse(feeAmount, out amount))
                        {

                            if (amount > 0)
                            {
                                billingAmount[index] += amount;
                                visitCharge += amount;
                                amountValid = true;
                                message += "Consultation fee added.\n";
                            }
                            else
                            {
                                message += "fee amount must be positive\n";
                            }
                        }

                        else
                        {
                            message += "Invalid amount entered. No charge added.\n";
                        }
                    }
                }

                Console.WriteLine("Any medication charges? (yes/no)");
                string medication = Console.ReadLine();

                if (medication.ToLower() == "yes")
                {

                    double price = 0;
                    bool priceValid = false;

                    while (!priceValid)
                    {
                        Console.WriteLine("Enter medication charges: ");
                        string medCharge = Console.ReadLine();

                        if (double.TryParse(medCharge, out price))
                        {
                            if (price > 0)
                            {
                                billingAmount[index] += price;
                                visitCharge += price;
                                priceValid = true;
                                message += "Medication charges added.\n";
                            }
                            else
                            {
                                message += "medication price must be positive\n";
                            }
                        }
                        else
                        {
                            message += "Invalid amount entered. No charge added.\n";
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

                admitted[index] = false;


                lastDischargeDate[index] = DateTime.Now;
                TimeSpan days = DateTime.Now - lastVisitDate[index];
                daysInHospital[index] += (int)days.TotalDays;

                assignedDoctors[index] = "";
                message += "Patient discharged successfully, with total days in hospital: " + daysInHospital[index];

                return message;
            }
            return "patient not found";
        }

        // case 4: search patient 
        static public string SearchPatient(string InputSearch)
        {
            int index = FindPatient(InputSearch);
            if (index != -1)
            {
                string status = admitted[index] ? "Admitted" : "Not Admitted";
                return $"Patient ID: {patientIDs[index]}\nName: {patientNames[index]}\nDiagnose: {diagnoses[index]}\nDepartment: {departments[index]}\nBlood Type: {bloodType[index]}\nAssigned Doctor: {assignedDoctors[index]}\nVisit Count: {visitCount[index]}\nBilling Amount: {billingAmount[index]} OMR\nLast Visit Date: {(lastVisitDate[index] == DateTime.MinValue ? "N/A" : lastVisitDate[index].ToString("yyyy-MM-dd HH:mm"))}\nLast Discharge Date: {(lastDischargeDate[index] == DateTime.MinValue ? "N/A" : lastDischargeDate[index].ToString("yyyy-MM-dd HH:mm"))}\nDays in Hospital: {daysInHospital[index]}\nStatus: {status}";
            }
            return "patient not found";
        }

        static void Main(string[] args)
        {
            bool exit = false;
            while (exit == false)
            {
                SeedData();

                DisplayMenu();

                int choice = SelectMenuChoice();

                switch (choice)
                {
                    case 1:

                       string PID = RegisterNewPatient();

                       Console.WriteLine($"Patient registered successfully with ID: {PID}");
                          
                        break;

                    case 2:

                        Console.WriteLine("Enter patient ID or patient name: ");
                        string patientInfo = Console.ReadLine();

                        Console.WriteLine("Enter doctor name: ");
                        string doc = Console.ReadLine();

                        string AdmittedOutput = GetAdmitted(patientInfo, doc);

                        Console.WriteLine(AdmittedOutput);

                        break;

                    case 3:

                        Console.WriteLine("Enter patient ID or patient name: ");
                        string patientInfoDischarge = Console.ReadLine();

                        // call discharge function here and print output
                        string DischargeOutput = DischargePatient(patientInfoDischarge);
                        Console.WriteLine(DischargeOutput);

                        break;

                    case 4:

                        Console.WriteLine("Enter patient ID or patient name: ");
                        string patientInfoSearch = Console.ReadLine();

                        // call search function here and print output
                        string searchOutput = SearchPatient(patientInfoSearch);
                        Console.WriteLine(searchOutput);

                        break;

                }
            }

            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
