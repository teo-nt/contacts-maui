using Contacts.Maui.Models;
using Microsoft.Maui.ApplicationModel.Communication;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId),"Id")]
public partial class EditContactPage : ContentPage
{
	private Contact? _contact;

	public EditContactPage()
	{
		InitializeComponent();
	}

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("..");
    }

	public string ContactId
	{
		set
		{
			_contact = ContactRepository.GetContactById(int.Parse(value));
			if (_contact != null)
			{
                contactCtrl.Name = _contact.Name;
                contactCtrl.Email = _contact.Email;
				contactCtrl.Phone = _contact.Phone;
				contactCtrl.Address = _contact.Address;
            }
			
		}
	}

    private void btnUpdate_Clicked(object sender, EventArgs e)
    {

		_contact!.Name = contactCtrl.Name;
		_contact.Email = contactCtrl.Email;
		_contact.Phone = contactCtrl.Phone;
		_contact.Address = contactCtrl.Address;

		ContactRepository.UpdateContact(_contact.ContactId, _contact);
        Shell.Current.GoToAsync("..");
    }

    private void contactCtrl_OnError(object sender, string e)
    {
		DisplayAlert("Error", e, "OK");
    }
}