using le_kanal_sosial;
using le_kanal_sosial.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(
      policy =>
      {
        policy.WithOrigins("https://localhost:3000");
      });
});
var app = builder.Build();
app.UseCors();

var settings = builder.Configuration.GetSection("Settings").Get<Settings>();

var eventService = new EventService(settings);


app.MapGet("/", () => "Hello World!");

app.MapGet("/Events", () => eventService.GetTestEvents());

app.MapPost("/Events/{eventId}", (Guid eventId, Participant participant) =>
{
  if (participant is null)
  {
    throw new ArgumentNullException(nameof(Participant));
  }

  eventService.AddParticipant(eventId, participant);

  return Results.Created($"/Events/{eventId}", participant);
});

app.MapGet("/Events/{id}", (Guid id) => eventService.GetEvent(id));

app.MapGet("/People/{searchstring}", (string searchString) => eventService.PeopleSearch(searchString));

app.Run();
