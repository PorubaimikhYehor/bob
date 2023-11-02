using System.Text;
namespace ClassLibrary1;
public class MedicalBot
{
  public static string BotName { get; set; } = string.Empty;
  public void GetPrescribeMedication(Patient patiend, List<Symptom> symptoms)
  {
    var symptom = symptoms.FirstOrDefault(s => s.Code == patiend.GetSymptomCode());
    if (symptom != null)
    {
      patiend.SetPrescription(symptom.GetPrescribeMedication(patiend.GetAge(), patiend.GetMedicalHistory().Split(" ")));
    } 
  }
  public void Greeting()
  {
    Console.WriteLine(string.Format("Hi, I'm {0}. I'm here to help you in your medication.", BotName));
  }

}

