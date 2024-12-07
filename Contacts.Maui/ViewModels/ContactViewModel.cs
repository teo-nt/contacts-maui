using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Maui.Views_Mvvm;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.ViewModels
{
    public partial class ContactViewModel : ObservableObject
    {
        private readonly IViewContactUseCase viewContactUseCase;
        private readonly IEditContactUseCase editContactUseCase;
        private readonly IAddContactUseCase addContactUseCase;

        [ObservableProperty]
        private Contact _contact = new Contact();

        public ContactViewModel(IViewContactUseCase viewContactUseCase, 
            IEditContactUseCase editContactUseCase,
            IAddContactUseCase addContactUseCase)
        {
            this.viewContactUseCase = viewContactUseCase;
            this.editContactUseCase = editContactUseCase;
            this.addContactUseCase = addContactUseCase;
        }

        [ObservableProperty]
        private bool _isNameProvided;

        [ObservableProperty]
        private bool _isEmailProvided;

        [ObservableProperty]
        private bool _isEmailFormatValid; 

        public async Task LoadContact(int contactId)
        {
            Contact = await viewContactUseCase.ExecuteAsync(contactId) ?? new Contact();
        }

        [RelayCommand]
        public async Task EditContact()
        {
            if (await ValidateContact())
            {
                await editContactUseCase.ExecuteAsync(Contact.ContactId, Contact);
                await Shell.Current.GoToAsync($"//{nameof(ContactsMVVMPage)}");
            }      
        }

        [RelayCommand]
        public async Task AddContact()
        {
            if (await ValidateContact())
            {
                await addContactUseCase.ExecuteAsync(Contact);
                await Shell.Current.GoToAsync($"//{nameof(ContactsMVVMPage)}");
            }          
        }

        [RelayCommand]
        public async Task BackToContacts()
        {
            await Shell.Current.GoToAsync($"//{nameof(ContactsMVVMPage)}");
        }

        private async Task<bool> ValidateContact()
        {
            if (!IsNameProvided)
            {
                await Application.Current!.MainPage!.DisplayAlert("Error", "Name is required", "OK");
                return false;
            }
            //if (!IsEmailProvided)
            //{
            //    await Application.Current!.MainPage!.DisplayAlert("Error", "Email is required", "OK");
            //    return false;
            //}
            if (!IsEmailFormatValid)
            {
                await Application.Current!.MainPage!.DisplayAlert("Error", "Email is not valid", "OK");
                return false;
            }
            return true;
        }
    }
}
