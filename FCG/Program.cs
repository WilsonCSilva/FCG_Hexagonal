using Infra.Helpers;
using Infra.Infrastructure;
using Core.Interfaces;
using FCG.Middlewares;
using Infra.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.ListenAnyIP(80);
        });

        var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        builder.Services.AddControllers();

        #region [Swagger]

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Fiap Cloud Games (FCG)",
                Description = "Plataforma de venda de jogos digitais e gestão de servidores para partidas online. Grupo Tech Challenge 8NETT.",
                Contact = new OpenApiContact()
                {
                    Name = "Grupo 149, RMs: 364460, 364901, 362661, 363924, 363080",
                    Email = "wilson.carvalhais@gmail.com; rodrigomelos@outlook.com.br; felipe.lima1996@hotmail.com; rafaelhvasconcelos@hotmail.com; bruno_murata@hotmail.com",
                },

                License = new OpenApiLicense()
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                },
                Version = "Fase 1"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                In = ParameterLocation.Header,
                Description = "Insira o token JWT no campo abaixo:"
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
                new string[] {}
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        #endregion
         
        #region [JWT]

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        });

        #endregion

        #region [Policy]

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        });

        #endregion

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ConnectionStrings"));
            options.UseLazyLoadingProxies();
        }, ServiceLifetime.Scoped);

        #region [DI]

        builder.Services.AddTransient(typeof(BaseLogger<>));
        builder.Services.AddScoped<CriptografiaHelper>();
        builder.Services.AddScoped<TextoHelper>();
        builder.Services.AddScoped<LoginHelper>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        builder.Services.AddScoped<IGameRepository, GameRepository>();
        builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

        #endregion

        var app = builder.Build();

        //if (app.Environment.IsDevelopment())
        //{
        //app.UseSwagger();
        //app.UseSwaggerUI();
        //}
        
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fiap Cloud Games (FCG) v1"); 
            c.RoutePrefix = "swagger";  // garante que o Swagger fique em /swagger
        });

        // app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
