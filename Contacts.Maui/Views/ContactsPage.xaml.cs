using Contacts.Maui.Models;
using Contacts.UseCases.Interfaces;
using System.Collections.ObjectModel;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
    private readonly IViewContactsUseCase viewContactsUseCase;

    public ContactsPage(IViewContactsUseCase viewContactsUseCase)
	{
		InitializeComponent();
        this.viewContactsUseCase = viewContactsUseCase;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
		searchBar.Text = "";
		LoadContacts();
        
    }

    private async void listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (listContacts.SelectedItem != null)
		{
			await Shell.Current.GoToAsync($"{nameof(EditContactPage)}?Id={((Contact)listContacts.SelectedItem).ContactId}");
		}  

		//DisplayAlert("test", "Test", "OK");
	}
		

    private void listContacts_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listContacts.SelectedItem = null;
    }

    private void btnAdd_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync(nameof(AddContactPage));
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {
		var menuItem = sender as MenuItem;
		var contact = menuItem!.CommandParameter as Contact;

		ContactRepository.DeleteContact(contact!.ContactId);

		LoadContacts();
    }

	private async void LoadContacts()
	{
        var contacts = new ObservableCollection<Contact>(await viewContactsUseCase.ExecuteASync(string.Empty));
        listContacts.ItemsSource = contacts;
    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var contacts = new ObservableCollection<Contact>(await viewContactsUseCase.ExecuteASync(searchBar.Text));
        listContacts.ItemsSource = contacts;
    }

}