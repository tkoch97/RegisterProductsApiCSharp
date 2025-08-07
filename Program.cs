var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

app.UseEndpoints(e =>
{
  e.MapControllers();
});

app.Run();