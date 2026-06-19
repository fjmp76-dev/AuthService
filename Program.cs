using AuthService.Extensions;
using AuthService.Helpers;
using AuthService.Services;

var builder = WebApplication.CreateBuilder(args);

// ── Servicios ──────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar nuestros servicios en DI
builder.Services.AddSingleton<JwtHelper>();
builder.Services.AddScoped<IAuthService, AuthService.Services.AuthService>();
builder.Services.AddScoped<IPermissionsService, PermissionsService>();

// ── Middlewares externos ───────────────────────────────────
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAngularCors(builder.Configuration);

// ── Pipeline ───────────────────────────────────────────────
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CorsExtensions.AngularPolicy);      // CORS antes de auth
app.UseAuthentication();                        // primero authentication
app.UseAuthorization();                         // luego authorization
app.MapControllers();

app.Run();