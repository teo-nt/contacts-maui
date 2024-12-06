using Contacts.Maui.ViewModels;

namespace Contacts.Maui.Views_Mvvm;

public partial class AddContactPageMVVM : ContentPage
{
    private readonly ContactViewModel contactViewModel;

    public AddContactPageMVVM(ContactViewModel contactViewModel)
	{
		InitializeComponent();
        this.contactViewModel = contactViewModel;

        BindingContext = contactViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        contactViewModel.Contact = new CoreBusiness.Contact();
    }
}