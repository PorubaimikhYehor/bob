// See https://aka.ms/new-console-template for more information
using ClassLibrary1;

// Console.WriteLine("s===== set Symptoms =====");


List<Symptom> symptoms = new List<Symptom>();
Symptom symptom = new Symptom() { Code = "S1", Description = "Headache", };
Medication medication = new Medication() { SymptomCode = symptom.Code, Name = "ibuprofen" };
medication.Dosages.Add(new Medication.MedicationDosage() { Age = 0, Dosage = "400 mg" });
medication.Dosages.Add(new Medication.MedicationDosage() { Age = 18, Dosage = "800 mg" });
symptom.PrescribeMedication.Add(medication);
symptoms.Add(symptom);

symptom = new Symptom() { Code = "S2", Description = "Skin rashes", };
medication = new Medication() { SymptomCode = symptom.Code, Name = "diphenhydramine" };
medication.Dosages.Add(new Medication.MedicationDosage() { Age = 0, Dosage = "50 mg" });
medication.Dosages.Add(new Medication.MedicationDosage() { Age = 18, Dosage = "300 mg" });
symptom.PrescribeMedication.Add(medication);
symptoms.Add(symptom);

symptom = new Symptom() { Code = "S3", Description = "Dizziness", };
medication = new Medication() { SymptomCode = symptom.Code, Name = "metformin", Contraindication = new string[] { "diabetes" } };
medication.Dosages.Add(new Medication.MedicationDosage() { Age = 0, Dosage = "500 mg" });
symptom.PrescribeMedication.Add(medication);
medication = new Medication() { SymptomCode = symptom.Code, Name = "dimenhydrinate" };
medication.Dosages.Add(new Medication.MedicationDosage() { Age = 0, Dosage = "50 mg" });
medication.Dosages.Add(new Medication.MedicationDosage() { Age = 18, Dosage = "400 mg" });
symptom.PrescribeMedication.Add(medication);
symptoms.Add(symptom);

// Console.WriteLine("s===== set Symptoms =====");
/* testing */
/* foreach (Symptom symp in symptoms)
{
  Console.WriteLine("10 => ");
  Console.WriteLine(symp.GetPrescribeMedication(10, Array.Empty<string>()));
  Console.WriteLine(symp.GetPrescribeMedication(10, new string[] { "Diabetes" }));
  Console.WriteLine("20 => ");
  Console.WriteLine(symp.GetPrescribeMedication(20, Array.Empty<string>()));
  Console.WriteLine(symp.GetPrescribeMedication(20, new string[] { "Diabetes" }));
  Console.WriteLine("18 => ");
  Console.WriteLine(symp.GetPrescribeMedication(18, Array.Empty<string>()));
  Console.WriteLine(symp.GetPrescribeMedication(18, new string[] { "Diabetes" }));
} */
/* testing end */

Patiend patined = new Patiend();
Console.WriteLine("Enter your (patient) details:");
string errorMessage = string.Empty;

Console.Write("Enter Patient Name: ");
while (!patined.SetName(Console.ReadLine() ?? string.Empty, out errorMessage))
{
  Console.WriteLine(errorMessage);
}

Console.Write("Enter Patient Age: ");
while (!patined.SetAge(Console.ReadLine() ?? string.Empty, out errorMessage))
{
  Console.WriteLine(errorMessage);
}

List<string> items = new List<string>();
foreach (Gender item in Enum.GetValues(typeof(Gender))) items.Add(item.ToString());
createHorisontalSelector(items, "Select the gender: ");

Console.Write("Enter Medical History. Eg: Diabetes. Press Enter for None: ");
patined.SetMedicalHistory(Console.ReadLine() ?? string.Empty);

Console.WriteLine();
Console.WriteLine($"Welcome, {patined.GetName()}, {patined.GetAge()}.");
Console.WriteLine("Which of the following symptoms do you have:");
createHorisontalSelector(new List<string> { "S1", "S2", "S3" }, string.Empty);

static int createHorisontalSelector(List<string> items, string? title)
{
  if (title == null)
  {
    title = string.Empty;
  }
  Console.Write(title);
  Console.CursorVisible = false;
  int cursorPosLeft = title.Length;
  int selectedIndex = 0;
  for (; ; )
  {
    for (int i = 0; i < items.Count; i++)
    {
      if (i == selectedIndex)
      {
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.DarkBlue;
      }
      else
      {
        Console.ResetColor();
      }
      Console.Write(string.Format(" {0} ", items[i]));
    }

    Console.WriteLine();
    var keyInfo = Console.ReadKey(intercept: true);
    var cursorPos = Console.GetCursorPosition();
    Console.SetCursorPosition(cursorPosLeft, cursorPos.Top - 1);
    if (keyInfo.Key == ConsoleKey.LeftArrow && selectedIndex > 0)
    {
      --selectedIndex;
    }
    else if (keyInfo.Key == ConsoleKey.RightArrow && selectedIndex < items.Count - 1)
    {
      ++selectedIndex;
    }
    else if (keyInfo.Key == ConsoleKey.Enter)
    {
      Console.ResetColor();
      Console.SetCursorPosition(cursorPosLeft, cursorPos.Top - 1);
      Console.WriteLine(new string(' ', items.ConvertAll(i => i.Length).Sum() + items.Count * 2));
      Console.SetCursorPosition(cursorPosLeft, cursorPos.Top - 1);
      Console.WriteLine(items[selectedIndex]);
      Console.CursorVisible = true;
      return selectedIndex;
    }
  }
}

