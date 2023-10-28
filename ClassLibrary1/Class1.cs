using System.Security.Cryptography;
using System.Text;

namespace ClassLibrary1;
public class MedicalBot
{
  static string BotName { get; set; } = string.Empty;
  static string GetBotName() => BotName;
  void GetPrescribeMedication(Patiend patiend, Symptom[] symptoms)
  {
    var symptom = symptoms.FirstOrDefault(s => s.Code == patiend.GetSymptomCode());
    if (symptom != null)
    {
      patiend.SetPrescription(symptom.GetPrescribeMedication(patiend.GetAge(), patiend.GetMedicalHistory().Split(" ")));
    }
  }
}
public class Symptom
{
  public required string Code { get; set; }
  public required string Description { get; set; }
  public List<Medication> PrescribeMedication { get; set; } = new List<Medication>();
  public string GetPrescribeMedication(int age, string[] contraindication)
  {
    string[] contraindicationToLower = contraindication.ToList().ConvertAll(c => c.ToLower()).ToArray();
    var medications = PrescribeMedication
    .FindAll(m => m.SymptomCode == Code && (contraindication == null || !m.Contraindication.Any(c => contraindicationToLower.Contains(c))))
    .ConvertAll<string>(m => string.Format("{0} {1}", m.Name, m.GetDosage(age)));
    return string.Join(" or ", medications);
  }
}
public class Medication
{
  public required string SymptomCode { get; set; }
  public required string Name { get; set; }
  public string[] Contraindication { get; set; } = Array.Empty<string>();
  public List<MedicationDosage> Dosages { get; set; } = new List<MedicationDosage>();

  public string GetDosage(int age)
  {
    var dosage = Dosages.OrderByDescending(d => d.Age).ToList().Find(d => age >= d.Age);
    return dosage == null ? string.Empty : dosage.Dosage;
  }
  public class MedicationDosage
  {
    public int? Age { get; set; } = -1;
    public required string Dosage { get; set; } = string.Empty;

  }
}


public class Patiend
{
  private string _name = string.Empty;
  public string GetName() => _name;
  public bool SetName(string value, out string errorMessage)
  {
    errorMessage = string.Empty;
    if (value == null)
    {
      errorMessage = "The name should not be empty";
    }
    else if (value.Length < 2)
    {
      errorMessage = "Patient name should contain at least two or more characters";
    }
    else
    {
      _name = value;
    }
    return string.IsNullOrEmpty(errorMessage);
  }

  private int _age = 0;
  public int GetAge() => _age;
  public bool SetAge(string value, out string errorMessage)
  {
    errorMessage = string.Empty;
    int intValue;
    if (int.TryParse(value, out intValue))
    {
      SetAge(intValue, out errorMessage);
    }
    else
    {
      errorMessage = "Patient age can be only number";
    }
    return string.IsNullOrEmpty(errorMessage);

  }
  public bool SetAge(int value, out string errorMessage)
  {
    errorMessage = string.Empty;
    if (value < 0)
    {
      errorMessage = "Patient age can't be negative";
    }
    else if (value > 100)
    {
      errorMessage = "Patient age can't be greater than 100";
    }
    else
    {
      _age = value;
    }
    return string.IsNullOrEmpty(errorMessage);
  }

  private Gender _gender;
  public Gender GetGender() => _gender;
  public bool SetGender(string value, out string errorMessage)
  {
    StringBuilder sb = new StringBuilder();
    Gender gender;
    if (string.IsNullOrEmpty(value))
    {
      sb.Append("The gender should not be empty");
    }
    else if (!Enum.TryParse(value.ToUpper(), out gender))
    {

      sb.Append("Patient gender should be either ");
      Array enumValues = Enum.GetValues(typeof(Gender));
      foreach (Gender item in enumValues)
      {
        // Console.WriteLine(item);
        sb.Append(item);
        if (Array.IndexOf(enumValues, item) == enumValues.Length - 2)
        {
          sb.Append(" or ");
        }
        else if (Array.IndexOf(enumValues, item) != enumValues.Length - 1)
        {
          sb.Append(", ");
        }
      };
    }
    else
    {
      _gender = gender;
    }
    errorMessage = sb.ToString();
    return string.IsNullOrEmpty(errorMessage);
  }
  private string _medicalHistory = string.Empty;
  public string GetMedicalHistory() => _medicalHistory;
  public void SetMedicalHistory(string value) => _medicalHistory = value;
  private string _symptomCode = string.Empty;
  public string GetSymptomCode() => _symptomCode;
  public void SetSymptomCode(string value) => _symptomCode = value;

  private string _prescription = string.Empty;
  public string GetPrescription() => _prescription;
  public void SetPrescription(string value) => _prescription = value;
}

public enum Gender
{
  MALE,
  FEMALE,
  OTHER
}

