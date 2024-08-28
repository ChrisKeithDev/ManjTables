using ManjTables.DataModels.Models;
using ManjTables.DataModels;
using Microsoft.EntityFrameworkCore;

namespace ManjTables
{
    public class AddressService
    {
        public static async Task<Address> HandleAddressAsync(ManjTablesContext context, Address address)
        {
            address.StreetAddress = WorkerUtility.RemoveExtraCharacters(address.StreetAddress ?? "");
            address.City = WorkerUtility.RemoveExtraCharacters(address.City ?? "");
            address.State = WorkerUtility.RemoveExtraCharacters(address.State ?? "");
            address.ZipCode = WorkerUtility.RemoveExtraCharacters(address.ZipCode ?? "");

            address.StreetAddress = WorkerUtility.HandleCommonAbbreviations(WorkerUtility.ToTitleCaseAndTrim(address.StreetAddress));
            address.City = WorkerUtility.ToTitleCaseAndTrim(address.City);
            address.State = WorkerUtility.ToUpperCaseAndTrim(address.State);
            address.ZipCode = WorkerUtility.ToTitleCaseAndTrim(address.ZipCode);

            var existingAddress = context.Addresses.FirstOrDefault(a => a.StreetAddress == address.StreetAddress && a.City == address.City && a.State == address.State && a.ZipCode == address.ZipCode);

            if (existingAddress != null)
            {
                return existingAddress;
            }
            else
            {
                context.Addresses.Add(address);
                context.Entry(address).State = EntityState.Added;
                await context.SaveChangesAsync();
                return address;
            }
        }
    }
}
