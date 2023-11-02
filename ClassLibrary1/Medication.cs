namespace ClassLibrary1;
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

