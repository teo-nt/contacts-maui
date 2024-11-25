using Contacts.Maui.ViewModels;

namespace Contacts.Maui.Views_Mvvm;

public partial class ContactsMVVMPage : ContentPage
{
    private readonly ContactsViewModel _contactsViewModel;

    public ContactsMVVMPage(ContactsViewModel contactsViewModel)
	{
		InitializeComponent();
        _contactsViewModel = contactsViewModel;
        BindingContext = _contactsViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _contactsViewModel.LoadContactsAsync();
    }
}