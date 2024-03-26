using AgendaApp.API.Configurations;
using AgendaApp.API.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

SwaggerConfiguration.Configure(builder.Services);
CorsConfiguration.Configure(builder.Services);
DependencyInjectionConfiguration.Configure(builder.Services);
JwtBearerConfiguration.Configure(builder.Services);

builder.Services.AddAutoMapper(typeof(ProfileMapping));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(CorsConfiguration.PolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();



