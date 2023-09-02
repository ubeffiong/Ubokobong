namespace IHVNMedix.Models
{
    public class SymptomDiagnosis
    {
        public int SymptomsId { get; set; }
        public Symptoms Symptoms { get; set; }

        public int DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }
    }
}
