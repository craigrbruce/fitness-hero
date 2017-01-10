using System;

namespace Server.Models
{
  public class Model
  {
    public int Id { get; set; }

    public string UserId { get; set; }

    public DateTime Created { get;set; }
    public DateTime Modified { get;set; }
  }
}
