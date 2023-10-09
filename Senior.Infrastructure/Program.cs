using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
Senior.Infrastructure.DependencyInjection.AddServices(builder.Services, builder.Configuration);

builder.Services.AddCors(p => p.AddPolicy("corsApp", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mi API",
        Version = "v1"
    });

    // Agrega el soporte para la autenticación con Bearer Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Ejemplo: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    // Configura el requerimiento de seguridad para Swagger
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
string imagesDirectory = Path.Combine(builder.Environment.ContentRootPath, "public", "images");

if (!Directory.Exists(imagesDirectory))
{
    Directory.CreateDirectory(imagesDirectory);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "public", "images")),
    RequestPath = "/public/images"
});

app.UseCors("corsApp");

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<Senior.Infrastructure.Middleware.ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
