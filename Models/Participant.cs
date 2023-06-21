namespace le_kanal_sosial.Models;
public class Participant
{
  public string Name { get; set; }
  public string Email { get; set; }
  public Guid Id { get; set; }
  public bool IsGoing { get; set; }
  public string ProfilePicture { get; set; }
}
