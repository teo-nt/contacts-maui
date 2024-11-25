using Contacts.Maui.ViewModels;

namespace Contacts.Maui.Views_Mvvm;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPageMVVM : ContentPage
{
    private readonly ContactViewModel contactViewModel;

    public EditContactPageMVVM(ContactViewModel contactViewModel)
	{
		InitializeComponent();
        this.contactViewModel = contactViewModel;
		BindingContext = contactViewModel;
    }

	public string ContactId 
	{ 
		set
		{
			if (!string.IsNullOrEmpty(value) && int.TryParse(value, out int contactId))
			{
				LoadContact(contactId);
			}
		}
	}

	private async void LoadContact(int contactId)
	{
        await contactViewModel.LoadContact(contactId);
    }
}