using System.Text.Json.Serialization;

namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Status { get; set; }

}