using Contacts.UseCases.PluginInterfaces;
using SQLite;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.SQLLite
{
    public class ContactSQLiteRepository : IContactRepository
    {
        private SQLiteAsyncConnection _database;

        public ContactSQLiteRepository()
        {
            _database = new SQLiteAsyncConnection(Constants.DatabasePath);
            _database.CreateTableAsync<Contact>();
        }

        public async Task AddContactAsync(Contact contact)
        {
            await _database.InsertAsync(contact);
        }

        public async Task DeleteContactAsync(int id)
        {
            var contact = await GetContactByIdAsync(id)!;
            if (contact != null)
            {
                await _database.DeleteAsync(contact);
            }
        }

        public async Task<Contact?> GetContactByIdAsync(int contactId)
        {
            try
            {
                return await _database.GetAsync<Contact>(contactId);
            }
            catch (Exception)
            {

            }
            return null;
        }

        public async Task<List<Contact>> GetContactsAsync(string? filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
            {
                return await _database.Table<Contact>().ToListAsync();
            }
            return await _database.QueryAsync<Contact>(@"
                SELECT * 
                FROM Contact
                WHERE 
                    Name LIKE ? OR
                    Email LIKE ? OR
                    Phone LIKE ? OR
                    Address LIKE ?", 
                    $"{filterText}%",
                    $"{filterText}%",
                    $"{filterText}%",
                    $"{filterText}%");
        }

        public async Task UpdateContactAsync(int contactId, Contact contact)
        {
            if (contactId == contact.ContactId)
                await _database.UpdateAsync(contact);
        }
    }
}
