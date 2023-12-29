
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using EstadisticaApp.DataAcces;

using EstadisticaApp.Utilities;

using Microsoft.Extensions.Logging;
using Radzen;

namespace EstadisticaApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            //Base de Datos
            var dbContext = new DBContext();
            dbContext.Database.EnsureCreatedAsync().Wait();
            dbContext.Dispose();


            builder.Services.AddDbContext<DBContext>();
            builder.Services.AddScoped<DBContext>();
            //Service title
            builder.Services.AddScoped<ComponentEvents>();


            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services
                    .AddBlazorise()
                    .AddBootstrap5Providers()
                    .AddFontAwesomeIcons();
            //View Model
            builder.Services.AddViewModel();
            builder.Services.AddMauiBlazorWebView();
            //Radzen
            builder.Services.AddRadzenComponents();
            

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            //builder.Services.AddSingleton<ApiRes<Class>>();
            return builder.Build();
        }
    }
}
