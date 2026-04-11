using System.ComponentModel.Design;
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

            int option;

            while (true)
            {
                Console.Write("Choose option: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out option))
                {
                    if (option >= 1 && option <= 10)
                    {
                        return option;
                    }
                    else
                    {
                        Console.WriteLine("Please choose a number between 1 and 10.");
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
            string Input = searchInput.ToUpper();

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
                return admitted[FoundPatient] ; // return true if admitted, false if not
            }
            isFound = false; // patient not found
            return false;   // invalid index
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
                    string doctorName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(doctorName)) // Empty value is not allowed
                    {
                        return "Doctor name cannot be empty.";
                    }

                    assignedDoctors[index] = doctorName;
                    visitCount[index]++;
                    lastVisitDate[index] = DateTime.Now;
                    lastDischargeDate[index] = DateTime.MinValue;
                    admitted[index] = true;

                    string visitMessage = visitCount[index] > 1 ? "This patient has been admitted " + visitCount[index] + " times." : "This is the first visit.";


                    return "Patient admitted successfully and assigned to " + assignedDoctors[index] + "\nThe admission date: " + lastVisitDate[index].ToString("yyyy-MM-dd HH:mm") + "\nThis patient has been admitted " + visitCount[index] + " times.";
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
            bool Found;
            int index = FindPatient(InputSearch);
            bool AdmittedStatus = IsAdmitted(InputSearch, out Found);

            if (!Found)
            {
                return "patient not found";
            }

            string message = "";
            if (index != -1)
            {
                if(AdmittedStatus)
                {
                     message += "Assigned doctor: " + assignedDoctors[index];
                }

                string status = admitted[index] ? "Admitted" : "Not Admitted";
                message += "Patient name: " + patientNames[index] + ",\nPatient ID: " + patientIDs[index].ToUpper() + ",\nDiagnosis: " + diagnoses[index] +
                                 " ( " + diagnoses[index].Length + " characters)" + ",\nDepartment: " + departments[index] + ",\nAdmission status: " + status +
                                 ",\nVisit count: " + visitCount[index] + ",\ntotal billing amount: " + Convert.ToString(Math.Round(billingAmount[index], 2)) +
                                 ",\nBlood Type: " + bloodType[index] + ",\nTotal Days in Hospital: " + daysInHospital[index];

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

            }
            return message;
        }

        // case 5: list all admitted patients 
        static public void ListAdmittedPatients(string InputSearch)
        {
            string keyword = Console.ReadLine();

            Console.WriteLine("===========================");
            Console.WriteLine("Admitted Patients: ");

            if (string.IsNullOrEmpty(keyword))
            {

                bool Found;
                bool AdmittedStatus = IsAdmitted(keyword, out Found);
                int index = FindPatient(InputSearch);
                double maxBilling = 0;
                int Count = 0;

                for (int i = 0; i <= PatientIndex; i++)
                {
                    // تحقق إذا المريض منوّم
                    if (admitted[i])
                    {
                        // فلترة بالكلمة
                        if (!string.IsNullOrEmpty(keyword) &&
                            !patientNames[i].ToLower().Contains(keyword.ToLower()))
                        {
                            continue;
                        }

                        Count++;
                        maxBilling = Math.Max(maxBilling, billingAmount[i]);

                        Console.WriteLine("Patient name: " + patientNames[i] +
                                          ",\nPatient ID: " + patientIDs[i] +
                                          ",\nDiagnosis: " + diagnoses[i] +
                                          ",\nDepartment: " + departments[i] +
                                          ",\nAdmission status: " + admitted[i] +
                                          ",\nVisit count: " + visitCount[i] +
                                          ",\ntotal billing amount: " + billingAmount[i] +
                                          ",\nAssigned doctor: " + assignedDoctors[i] +
                                          ",\nAdmitted since: " + lastVisitDate[i]);
                    }
                }

                // 👇 الطباعة بعد اللوب
                if (Count > 0)
                {
                    Console.WriteLine("The total admitted count is: " + Count);
                    Console.WriteLine("The highest billing amount among admitted patients is: " + Math.Round(maxBilling, 2) + " OMR");
                }
                else
                {
                    Console.WriteLine("No patient admitted.");
                }

            }
        }

        // case 6: transfer patient to another doctor 
        static public void TransferPatient(string currentDoctor, string newDoctor)
        {
            currentDoctor = Console.ReadLine().Trim();
            newDoctor = Console.ReadLine().Trim();

            currentDoctor = currentDoctor.Replace("Dr ", "Dr. ");
            newDoctor = newDoctor.Replace("Dr ", "Dr. ");

            bool doctorFound = false;
            bool admittedPatientFound = false;

            for (int i = 0; i <= PatientIndex; i++)
            {
                if (assignedDoctors[i] == currentDoctor) // find current Doctor 
                {
                    doctorFound = true;

                    if (admitted[i] == true)
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
            for (int visit = 100; visit >= 0; visit--) //عداد تنازلي من الاكثر للاقل
            {
                for (int i = 0; i <= PatientIndex; i++)
                {
                    if (visitCount[i] == visit) //ترتيب المرضى على حسب عدد مرات الزيارة
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
            string dept = department.ToUpper();

            bool patFound = false;
            for (int i = 0; i <= PatientIndex; i++)
            {
                if (departments[i] != null && departments[i].ToLower().Contains(dept.ToLower()))
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
                string choice = Console.ReadLine();

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

                Console.WriteLine("Invalid choice.");
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

                for (int i = 0; i <= PatientIndex; i++)
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
                string patientInfo = Console.ReadLine();

                int index = FindPatient(patientInfo);

                if (index != -1)
                {
                    Console.WriteLine("Patient name: " + patientNames[index] + ",\nPatient ID: " + patientIDs[index] + ",\nTotal billing amount: " + Math.Round(billingAmount[index], 2) + " OMR");
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
            string confirmExit = Console.ReadLine();

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


        // main function to run the program
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

                        string AdmittedOutput = GetAdmitted(patientInfo);

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

                    case 5:

                        Console.WriteLine("Filter by name keyword (press Enter to skip): ");
                        string patientInfoList = Console.ReadLine();
                        ListAdmittedPatients(patientInfoList);

                        break;

                    case 6:

                        Console.WriteLine("Enter current doctor name: ");
                        string currentDoc = Console.ReadLine();

                        Console.WriteLine("Enter new doctor name: ");
                        string newDoc = Console.ReadLine();

                        TransferPatient(currentDoc, newDoc);

                        break;

                    case 7:

                        ViewMostVisitedPatients();

                        break;

                    case 8:

                        Console.WriteLine("Enter department name: ");
                        string dept = Console.ReadLine();

                        SearchPatientsByDepartment(dept);

                        break;

                     case 9:

                        BillingReport();

                        break;

                    case 10: 

                        exit = ExitProgram();

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
