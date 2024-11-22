namespace Contacts.CoreBusiness
{
    // All the code in this file is included in all platforms.
    public class Contact
    {
        public int ContactId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
