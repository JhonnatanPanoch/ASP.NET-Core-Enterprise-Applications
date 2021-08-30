Montar estrutura
	- Criar esquemas de pastas e servicos
	- Criar dbcontext do entity
	- Mapear no startup
	- Adicionar UseAuthentication();
	- Para adicionar as migrations instalar o pacote Microsoft.EntityFrameworkCore.Tools
	- Criar migrations: add-migration Initil
	- Ciar banco de dados: update-database
	- [ApiController] define um controler como controller de api

Autenticação:
	- Uma claim é um papel. 
	- Uma role é um dado aberto. Permissao, Dados do usuario, etc.

Ambientes:
	- Para pegar o appsettings conforme o ambiente, usar o ConfigurationBuilder() no construtor do startup.
















	    public static IServiceCollection AddConfiguration(this IServiceCollection services)
        {
            
            return services;
        }

        public static IApplicationBuilder UseConfiguration(this IApplicationBuilder app)
        {

            return app;
        }
	

