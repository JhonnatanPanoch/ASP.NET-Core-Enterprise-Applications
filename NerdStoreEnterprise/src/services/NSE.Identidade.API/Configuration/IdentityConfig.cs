using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSE.Identidade.API.Data;
using NSE.Identidade.API.Extensions;
using System.Text;

namespace NSE.Identidade.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            /// Adiciona o identity.
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            /// Configura sessao do appsettings
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            /// Obtém e tipa informações do appsettings
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            // JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = true;
                bearer.SaveToken = true; // Token vai ser guardado na instancia;
                bearer.TokenValidationParameters = new TokenValidationParameters() // Parametros de validação do token
                {
                    ValidateIssuerSigningKey = true, // Valida o emissor com base na assinatura.
                    IssuerSigningKey = new SymmetricSecurityKey(key), // Assinatura do emissor criada a partir do SymmetricSecurityKey, sendo x o segredo
                    ValidateIssuer = true, // Token valida o emissor
                    ValidateAudience = true, // Define quais dominios validos
                    ValidAudience = appSettings.ValidoEm,  // Dominios
                    ValidIssuer = appSettings.Emissor, // Chave emissor
                };
            });

            return services;
        }

        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
