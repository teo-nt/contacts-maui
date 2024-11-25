using CommunityToolkit.Maui;
using Contacts.Maui.ViewModels;
using Contacts.Maui.Views;
using Contacts.Maui.Views_Mvvm;
using Contacts.Plugins.DataStore.InMemory;
using Contacts.UseCases;
using Contacts.UseCases.Interfaces;
using Contacts.UseCases.PluginInterfaces;
using Microsoft.Extensions.Logging;

namespace Contacts.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiCommunityToolkit()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IContactRepository, ContactInMemoryRepository>();
            builder.Services.AddSingleton<IViewContactsUseCase, ViewContactsUseCase>();
            builder.Services.AddSingleton<IViewContactUseCase, ViewContactUseCase>();
            builder.Services.AddTransient<IEditContactUseCase, EditContactUseCase>();
            builder.Services.AddTransient<IAddContactUseCase, AddContactUseCase>();
            builder.Services.AddTransient<IDeleteContactUseCase, DeleteContactUseCase>();

            builder.Services.AddSingleton<ContactsViewModel>();
            

            builder.Services.AddSingleton<ContactsPage>();
            builder.Services.AddSingleton<EditContactPage>();
            builder.Services.AddSingleton<AddContactPage>();

            builder.Services.AddSingleton<ContactsMVVMPage>();
            return builder.Build();
        }
    }
}
