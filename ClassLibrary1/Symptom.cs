namespace ClassLibrary1;
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

