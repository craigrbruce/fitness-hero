using Server.Models.Core;

namespace Server.Models.Client
{
  public class Injury : Model
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public string Cause { get; set; }
    public InjuryStatus InjuryStatus { get; set; } = InjuryStatus.Undetermined;
    public string SignOrSymptom { get; set; }
    public string Medication { get; set; }
    public string PotentialAction { get; set; }
  }

}
