var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilog();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddContext(builder.Configuration);

builder.Services.AddJwtToken(builder.Configuration);

builder.Services.AddServices();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwagger();

app.UseSwagger();

app.UseHttpsRedirection();

app.AddEndpoints();

app.AddFilterException();

app.Run();
