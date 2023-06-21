namespace le_kanal_sosial.Models;

public class User
{
  public Guid Id { get; set; }

  public string DisplayName { get; set; }

}

public record GraphResponse<T>(List<T> Value);