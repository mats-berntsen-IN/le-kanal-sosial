namespace le_kanal_sosial.Models;

public class Event
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public DateTime Date { get; set; }
  public string Location { get; set; }
  public string Image { get; set; }
  public string Link { get; set; }
  public string Category { get; set; }
  public string Tags { get; set; }
  public List<Participant> Participants { get; set; } = new();
}