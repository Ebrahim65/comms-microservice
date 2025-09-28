using EmailService.Config;
using EmailService.Services.Implementations;
using EmailService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Bind config
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

// Register services
builder.Services.AddScoped<IEmailService, SmtpEmailService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
