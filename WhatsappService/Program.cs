using Microsoft.Extensions.Options;
using WhatsAppService.Config;
using WhatsAppService.Services.Interfaces;
using WhatsAppService.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// ----- Configuration / Settings -----
builder.Services.Configure<WhatsAppSettings>(builder.Configuration.GetSection("WhatsApp"));

// OPTIONAL: if you want to inject WhatsAppSettings directly as a POCO:
// builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<WhatsAppSettings>>().Value);

// ----- MVC / Swagger -----
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----- HttpClient + Service registration -----
// Registers MetaWhatsAppService as the implementation of IWhatsAppService and provides a typed HttpClient.
builder.Services.AddHttpClient<IWhatsAppService, MetaWhatsAppService>((sp, client) =>
{
    // Optionally set a base address (we use full URL in the service, so not strictly required)
    client.BaseAddress = new Uri("https://graph.facebook.com/");
    // You can set default headers here if you want, but we set Authorization per-request inside the service.
});

var app = builder.Build();

// ----- Middleware pipeline -----
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "WhatsAppService API";
    });
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
