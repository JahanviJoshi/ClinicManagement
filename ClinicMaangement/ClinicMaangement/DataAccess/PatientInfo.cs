using ClinicMaangement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClinicMaangement.DataAccess
{
    internal class PatientInfo
    {
        ClinicContext ctx;

        public PatientInfo()
        {
            ctx = new ClinicContext();
        }


        public void AddPatient()
        {
            try
            {
                string PatientName, Address, medSub, Gender, Email;
                long PhoneNumber;
                double bp;
                double cholestrol_HDL;
                double cholestrol_LDC;
                double Sugar_fast;
                double Sugar_post;

                DateTime AppointDate;
                int PatientAge;
                int weight;
             
                bool isGender = false, isName = false, isNumber = false, isAge = false, isWeight = false, isBP = false, isChoslestrol1 = false, isChoslestrol2 = false,isSugar1 = false, isSugar2 = false, isEmail=false;
                do
                {

                    Console.WriteLine("Enter Patient's Name");
                    PatientName = Console.ReadLine();
                    string regex = @"^[A-Z][a-z]";
                    Regex rx = new Regex(regex);
                    if (rx.IsMatch(PatientName))
                    {
                        isName = true;
                    }
                    else
                    {
                        Console.WriteLine("Enter Valid Name");
                    }
                } while (isName == false);

                do
                {
                    Console.WriteLine("Enter Phone Number");
                    PhoneNumber = Convert.ToInt64(Console.ReadLine());
                    string regex = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
                    string PNO = PhoneNumber.ToString();
                    Regex rx = new Regex(regex);
                    if (rx.IsMatch(PNO))
                    {
                        isNumber = true;
                    }
                    else
                    {
                        Console.WriteLine("Enter Valid Name");
                    }
                }
                while (isNumber == false);

                do
                {
                    Console.WriteLine("Enter Gender");
                    Gender = Console.ReadLine().ToLower();
                    if (Gender == "female" || Gender == "male" || Gender == "other")
                    {
                        isGender = true;
                    }
                    else
                    {
                        Console.WriteLine("Enter Gender as female or male or other\n");
                    }
                } while (isGender == false);

                do
                {
                    Console.WriteLine("Enter Email ID");
                Email = Console.ReadLine();
                string regex = @"(@)(.+)$";

                Regex rx1 = new Regex(regex);
                if (rx1.IsMatch(Email))
                {
                    isEmail = true;
                }
                else
                {
                    Console.WriteLine("Enter Valid Email");
                }
            } while (isEmail == false) ;

            do
                {
                    Console.WriteLine("Enter Patient's Age");
                    PatientAge = Convert.ToInt32(Console.ReadLine());
                    if (PatientAge > 0)
                    {
                        isAge = true;
                    }
                    else
                    {
                        Console.WriteLine("Age can't be negative\n");
                    }
                } while (isAge == false);



                Console.WriteLine("Enter Address");
                Address = Console.ReadLine();


                do
                {
                    Console.WriteLine("Enter weight ");
                    weight = Convert.ToInt32(Console.ReadLine());
                    if (weight > 0)
                    {
                        isWeight = true;
                    }
                    else
                    {
                        Console.WriteLine("Weight can't be negative\n");
                    }

                } while (isWeight == false);

                do
                {
                    Console.WriteLine("Enter Patients BP");
                    bp = Double.Parse(Console.ReadLine());
                    if (bp > 0)
                    {
                        isBP = true;
                    }
                    else
                    {
                        Console.WriteLine("BP can't be negative\n");
                    }


                } while (isBP == false);


                do
                {
                    Console.WriteLine("Patients Cholestrol HDL");
                    cholestrol_HDL = Double.Parse(Console.ReadLine());
                    if (cholestrol_HDL > 0)
                    {
                        isChoslestrol1 = true;
                    }
                    else
                    {
                        Console.WriteLine("Cholestrol can't be negative\n");
                    }

                } while (isChoslestrol1 == false);

                do
                {
                    Console.WriteLine("Patients Cholestrol LDC");
                    cholestrol_LDC = Double.Parse(Console.ReadLine());
                    if (cholestrol_LDC > 0)
                    {
                        isChoslestrol2 = true;
                    }
                    else
                    {
                        Console.WriteLine("Cholestrol can't be negative\n");
                    }

                } while (isChoslestrol2 == false);

                do
                {
                    Console.WriteLine("Enter Sugar Fast Data ");
                    Sugar_fast = Double.Parse(Console.ReadLine());
                    if (Sugar_fast > 0)
                    {
                        isSugar1 = true;
                    }
                    else
                    {
                        Console.WriteLine("Sugar can't be negative\n");
                    }
                } while (isSugar1 == false);

                do
                {
                    Console.WriteLine("Enter Sugar Post Data ");
                    Sugar_post = Double.Parse(Console.ReadLine());
                    if (Sugar_post > 0)
                    {
                        isSugar2 = true;
                    }
                    else
                    {
                        Console.WriteLine("Sugar can't be negative\n");
                    }

                } while (isSugar2 == false);


                Console.WriteLine("Medicine Subscribed");
                medSub = Console.ReadLine();


                Console.WriteLine(" Enter next Appointment Date (format yyyy-mm-dd)");
                AppointDate = Convert.ToDateTime(Console.ReadLine());


                RegisterPatient registerPatient = new RegisterPatient()
                {
                    Name = PatientName,
                    Gender = Gender,
                    Age = PatientAge,
                    Email = Email,
                    Address = Address,
                    Phoneno = PhoneNumber

                };
               

                ctx.RegisterPatients.AddAsync(registerPatient);
                ctx.SaveChangesAsync();



                PatientMedicalInfo PMI = new PatientMedicalInfo()
                {


                    Name = PatientName,
                    Weight = weight,
                    Bp = bp,
                    CholestrolHdl = cholestrol_HDL,
                    CholestrolLdc = cholestrol_LDC,
                    SugarFast = Sugar_fast,
                    SugarPost = Sugar_post,
                    MedicineSubscription = medSub,
                    AppointmentDate = AppointDate


                };

                ctx.PatientMedicalInfos.AddAsync(PMI);

                ctx.SaveChangesAsync();

                Console.WriteLine("Patient Added successfully\n");
                Payment(PMI);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured {ex.Message}");
                //throw;
            }

        }
        public void ReadPatient()
        {
            var PatientList = ctx.RegisterPatients.ToListAsync();
            foreach (var patient in PatientList.Result)
            {
                Console.WriteLine($"{patient.No} {patient.Name}  {patient.Gender} {patient.Age} {patient.Email} {patient.Address}");
            }
            Console.WriteLine("Enter patients name whose records you want to find");
            string record = Console.ReadLine();
            //var view = ctx.PatientMedicalInfos.Where(x => x.Name == record).Where(y => y.Name == record);
            var view = ctx.PatientMedicalInfos.Where(x => x.Name == record);
            if (view.Count() == 0)
            {
                Console.WriteLine("Patient does not exist");
            }
            else
            {
                foreach (var record1 in view)
                {
                    Console.WriteLine($"{record1.Id} {record1.Name} {record1.Weight}  {record1.Bp} {record1.CholestrolHdl} {record1.CholestrolLdc} {record1.SugarFast} {record1.SugarPost} {record1.MedicineSubscription} {record1.AppointmentDate} ");
                }
            }
        }



        public void Payment(PatientMedicalInfo patient)
        {
            Console.WriteLine(" Patient's first time visiting? (Yes/No)\n");
            string visiting = Console.ReadLine().ToLower();
            int VisitingCharge = 0;
            if (visiting == "yes")
            {
                Console.WriteLine("First Visit charges 1000Rs\n");
                VisitingCharge = 1000;
            }
            else
            {
                Console.WriteLine("850/- will be charged");
                VisitingCharge = 850;
            }
            Bill billing = new Bill()
            {
                No = patient.Id,
                Fees = VisitingCharge,
                Name = patient.Name,
                Date = DateTime.Now


            };
            var fee = ctx.Bills.AddAsync(billing);
            ctx.SaveChangesAsync();
        }

        public void DailyCollection()
        {

            DateTime dateTime = Convert.ToDateTime(DateTime.Now);
            var TotalCollection = ctx.Bills.Where(x => x.Date == dateTime).Sum(x => x.Fees);
            Console.WriteLine($"Total Todays Collection was Rs{TotalCollection}/-");
        }


        public void UpdatePatient()
        {
            try
            {
                var PatientList = ctx.RegisterPatients.ToListAsync();
                foreach (var patient in PatientList.Result)
                {
                    Console.WriteLine($"{patient.No} {patient.Name} {patient.Age}");
                }







                Console.WriteLine("Enter the id of the patient you want to operate");
                int pId = Convert.ToInt32(Console.ReadLine());
                var patientFind = ctx.PatientMedicalInfos.Where(x => x.Id == pId).FirstOrDefault();


                if (patientFind.Id == pId)
                {
                    Console.WriteLine("Patient found\n");
                    Console.WriteLine($"Patient Info {patientFind.Id} {patientFind.Name}\n");
                    bool isOperating = true;
                    PatientMedicalInfo pmi = new PatientMedicalInfo()
                    {
                        Id = patientFind.Id,
                        Name = patientFind.Name,
                        Weight = patientFind.Weight,

                        Bp = patientFind.Bp,
                        CholestrolHdl = patientFind.CholestrolHdl,
                        CholestrolLdc = patientFind.CholestrolLdc,
                        SugarFast = patientFind.SugarFast,
                        SugarPost = patientFind.SugarPost,
                        MedicineSubscription = patientFind.MedicineSubscription,
                        AppointmentDate = patientFind.AppointmentDate
                    };
                    do
                    {

                        Console.WriteLine($"what you want to update in records\n" +
                                      $"1. Weight\n" +

                                      $"2. BP\n" +
                                      $"3. Sugar Fast\n" +
                                      $"4. Sugar Post\n" +
                                      $"5. Cholestrol HDL\n" +
                                      $"6. Cholestrol DLC\n" +

                                      $"7. Medicine Subscribed\n" +
                                      $"8. Appointment Date\n" +
                                      $"9. Done\n");
                        int updateInput = Convert.ToInt32(Console.ReadLine());
                        switch (updateInput)
                        {
                            case 1:
                                bool isUpdatedWeight = true;
                                do
                                {
                                    Console.WriteLine("Enter updated weight\n");
                                    double updatedWeight = Convert.ToDouble(Console.ReadLine());
                                    if (updatedWeight > 0)
                                    {
                                        pmi.Weight = updatedWeight;
                                        isUpdatedWeight = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Weight Cannot be negative");
                                    }

                                } while (isUpdatedWeight == true);
                                break;



                            case 2:
                                bool isBloodPressur = true;
                                do
                                {
                                    Console.WriteLine("Enter updated BloodPressure\n");
                                    double updatedBp = Double.Parse(Console.ReadLine());
                                    if (updatedBp > 0)
                                    {
                                        pmi.Bp = updatedBp;
                                        isBloodPressur = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("blood pressure can't be negative\n");
                                    }
                                } while (isBloodPressur == true);
                                break;

                            case 3:

                                bool isUpdatedSugar = true;

                                do
                                {
                                    Console.WriteLine("Enter  Sugar Fast to update\n");
                                    double UpdatedSugar = Double.Parse(Console.ReadLine());
                                    if (UpdatedSugar > 0)
                                    {
                                        isUpdatedSugar = false;
                                        pmi.SugarFast = UpdatedSugar;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Sugar cant be negative");
                                    }
                                } while (isUpdatedSugar == true);
                                break;

                            case 4:

                                bool isUpdatedSugar1 = true;

                                do
                                {
                                    Console.WriteLine("Enter  Sugar Fast to update\n");
                                    double UpdatedSugar1 = Double.Parse(Console.ReadLine());
                                    if (UpdatedSugar1 > 0)
                                    {
                                        isUpdatedSugar = false;
                                        pmi.SugarPost = UpdatedSugar1;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Sugar cant be negative");
                                    }
                                } while (isUpdatedSugar1 == true);
                                break;

                            case 5:
                                bool isUpdatedChoslestrol = true;
                                do
                                {
                                    Console.WriteLine("Patients Cholestrol HDL");
                                    double Updatedcholestrol_HDL = Double.Parse(Console.ReadLine());
                                    if (Updatedcholestrol_HDL > 0)
                                    {
                                        isUpdatedChoslestrol = false;
                                        pmi.CholestrolHdl = Updatedcholestrol_HDL;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Cholestrol can't be negative\n");
                                    }

                                } while (isUpdatedChoslestrol == true);
                                break;
                            case 6:
                                bool isUpdatedChoslestrol1 = true;
                                do
                                {
                                    Console.WriteLine("Patients Cholestrol LDC");
                                    double Updatedcholestrol_LDC = Double.Parse(Console.ReadLine());
                                    if (Updatedcholestrol_LDC > 0)
                                    {
                                        isUpdatedChoslestrol1 = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Cholestrol can't be negative\n");
                                    }

                                } while (isUpdatedChoslestrol1 == true);
                                break;


                            case 7:

                                Console.WriteLine("Enter new Medicines given\n");
                                string UpdatedMed = Console.ReadLine();
                                pmi.MedicineSubscription = UpdatedMed;
                                break;

                            case 8:

                                Console.WriteLine("Enter new appointment given\n");
                                string newAppointment = Console.ReadLine();
                                pmi.AppointmentDate = Convert.ToDateTime(newAppointment);
                                break;

                            case 9:

                                System.Environment.Exit(0);
                                break;
                            default:

                                Console.WriteLine(" InValid choice");
                                break;

                        }

                    } while (isOperating == true);

                    ctx.PatientMedicalInfos.AddAsync(pmi);

                    ctx.SaveChangesAsync();
                    Payment(pmi);

                }
                else
                {
                    Console.WriteLine("Patient Not Found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured {ex.Message}");
               // throw;
            }
        }


    }
}
