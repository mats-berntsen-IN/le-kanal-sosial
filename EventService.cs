using le_kanal_sosial.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace le_kanal_sosial;

public class EventService
{

  private HttpClient HttpClient { get; set; }

  public List<Event> Events { get; set; }
  public EventService(Settings settings)
  {
    HttpClient = new HttpClient()
    {
      BaseAddress = new Uri("https://graph.microsoft.com/v1.0/")
    };
    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", settings.MsGraphToken);
    HttpClient.DefaultRequestHeaders.Add("ConsistencyLevel", "eventual");
  }

  public Event AddEvent(Event @event)
  {
    @event.Id = Guid.NewGuid();
    Events.Add(@event);

    return @event;
  }

  public Event AddParticipant(Guid eventId, Participant participant)
  {
    var @event = Events.FirstOrDefault(x => x.Id == eventId);

    if (@event == null)
    {
      return null;
    }

    @event.Participants.Add(participant);

    return @event;
  }

  public Event GetEvent(Guid id)
  {
    return Events.FirstOrDefault(x => x.Id == id);
  }

  public async Task<List<User>> PeopleSearch(string searchString)
  {
    var res = await HttpClient.GetAsync($"users?$search=\"displayName: {searchString}\"");
    try
    {
      var userRes = JsonConvert.DeserializeObject<Models.GraphResponse<User>>(await res.Content.ReadAsStringAsync());
      return userRes.Value;

    }
    catch (Exception e)
    {

      throw;
    }
    return null;
  }

  #region Tests

  //Creates a list of testdata longer than 5. With a descriptive name and description.
  public List<Event> GetTestEvents()
  {
    var events = new List<Event>();
    for (int i = 0; i < 10; i++)
    {
      events.Add(new Event
      {
        Name = $"Event {i}",
        Description = $"This is a description of event {i}",
        Date = DateTime.Now.AddDays(i),
        Location = $"Location {i}",
        Image = $"Image {i}",
        Link = $"Link {i}",
        Category = $"Category {i}",
        Tags = $"Tags {i}",
        Participants = GetTestParticipants()
      });
    }
    return events;

  }

  public List<Participant> GetTestParticipants()
  {
    var participants = new List<Participant>();
    for (int i = 0; i < 5; i++)
    {
      participants.Add(new Participant
      {
        Name = $"Participant {i}",
        Email = $"Email {i}",
      });
    }
    return participants;
  }
  #endregion

}
