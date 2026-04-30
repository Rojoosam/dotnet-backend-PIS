using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SIADAL.Services;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// JWT Authentication configuration
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddScoped<JwtService>();

builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Register repositories
builder.Services.AddScoped<SIADAL.Interfaces.IUser, SIADAL.Repository.UserRepository>();
builder.Services.AddScoped<SIADAL.Interfaces.IStudent, SIADAL.Repository.StudentRepository>();
builder.Services.AddScoped<SIADAL.Interfaces.ITeacher, SIADAL.Repository.TeacherRepository>();
builder.Services.AddScoped<SIADAL.Interfaces.IClass, SIADAL.Repository.ClassRepository>();
builder.Services.AddScoped<SIADAL.Interfaces.IEducationalLevel, SIADAL.Repository.EducationalLevelRepository>();
builder.Services.AddScoped<SIADAL.Interfaces.IProgram, SIADAL.Repository.ProgramRepository>();
builder.Services.AddScoped<SIADAL.Interfaces.IAcademicPeriod, SIADAL.Repository.AcademicPeriodRepository>();
builder.Services.AddScoped<SIADAL.Interfaces.IEnrollment, SIADAL.Repository.EnrollmentRepository>();
builder.Services.AddScoped<SIADAL.Interfaces.IAssignment, SIADAL.Repository.AssignmentRepository>();
builder.Services.AddScoped<SIADAL.Interfaces.ISubmission, SIADAL.Repository.SubmissionRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SIADAL API",
        Version = "v1",
        Description = "user@example.com | userStudent@example.com | user@example.com : password123"
    });

    // 🔐 Configuración de JWT en Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT así: Bearer {tu token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    )
);

// Allows API calls from frontend - browser policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173", // Local frontend dev server
            "http://0.0.0.0" // Deployed Frontend on VPS
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
