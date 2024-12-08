using Contacts.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            //app.UseHttpsRedirection();
            app.MapGet("/api/contacts", async (ApplicationDbContext db) =>
            {
                var contacts = await db.Contacts.ToListAsync();

                return Results.Ok(contacts);
            });

            app.MapPost("/api/contacts", async (Contact contact, ApplicationDbContext db) =>
            {
                db.Contacts.Add(contact);
                await db.SaveChangesAsync();
            });

            app.MapPut("/api/contacts/{id}", async (int id, Contact contact, ApplicationDbContext db) =>
            {
                var contactToUpdate = await db.Contacts.FindAsync(id);
                if (contactToUpdate is null) return Results.NotFound();

                contactToUpdate.Name = contact.Name;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Phone = contact.Phone;
                contactToUpdate.Address = contact.Address;
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("/api/contacts/{id}", async (int id, ApplicationDbContext db) =>
            {
                var contactToDelete = await db.Contacts.FindAsync(id);
                if (contactToDelete is null) return Results.NotFound();
                db.Contacts.Remove(contactToDelete);
                await db.SaveChangesAsync();
                return Results.Ok(contactToDelete);
            });

            app.Run();
        }
    }
}
