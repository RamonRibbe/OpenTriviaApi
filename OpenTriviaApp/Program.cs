using NLog.Extensions.Logging;
using OpenTriviaApp;
using OpenTriviaApp.Components;
using OpenTriviaApp.Presenters;
using OpenTriviaAppClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTransient<IApiCaller, ApiCaller>();
builder.Services.AddTransient<IOpenTriviaPresenter, OpenTriviaPresenter>();

builder.Services.AddHttpClient<swaggerClient>(
    x => x.BaseAddress = new Uri(builder.Configuration.GetValue<string>("OpenTriviaApiUrl")));

builder.Host.ConfigureLogging((hostingContext, logging) =>
{
    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    logging.AddDebug();
    logging.AddNLog();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
