using System;
using Server.Models.Core;

namespace Server.Models.Client
{
  public class Client : Model
  {
    public Gender Gender { get; set; } = Gender.Unspecified;
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public string Email { get; set; }
    public float StartingWeight { get; set; }
    public float Height { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string TelephoneNumber { get; set; }
    public string EmergencyContactTelephoneNumber { get; set; }
    public string EmergencyContactName { get; set; }
    public Image Avatar { get; set; }

  }
}
