using Contacts.Maui.Views;
using Contacts.Maui.Views_Mvvm;

namespace Contacts.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ContactsPage), typeof(ContactsPage));
            Routing.RegisterRoute(nameof(AddContactPage), typeof(AddContactPage));
            Routing.RegisterRoute(nameof(EditContactPage), typeof(EditContactPage));
            Routing.RegisterRoute(nameof(ContactsMVVMPage), typeof(ContactsMVVMPage));
            Routing.RegisterRoute(nameof(AddContactPageMVVM), typeof(AddContactPageMVVM));
            Routing.RegisterRoute(nameof(EditContactPageMVVM), typeof(EditContactPageMVVM));
        }
    }
}
