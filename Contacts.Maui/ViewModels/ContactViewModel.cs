using CommunityToolkit.Mvvm.ComponentModel;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.ViewModels
{
    public partial class ContactViewModel : ObservableObject
    {
        private readonly IViewContactUseCase viewContactUseCase;

        [ObservableProperty]
        private Contact? _contact;

        public ContactViewModel(IViewContactUseCase viewContactUseCase)
        {
            this.viewContactUseCase = viewContactUseCase;
        }

        public async Task LoadContact(int contactId)
        {
            Contact = await viewContactUseCase.ExecuteAsync(contactId);
        }
    }
}
