using ManjTables.DataModels.Models;
using ManjTables.DataModels;

namespace ManjTables
{
    public class ParentService
    {
        // This method now only checks if a parent exists in the context and returns it or null.
        public static Parent? FindExistingParent(ManjTablesContext context, Parent parent)
        {
            return context.Parents
                          .FirstOrDefault(p => p.FirstName == parent.FirstName &&
                                               p.LastName == parent.LastName &&
                                               p.Email == parent.Email);
        }

        // No longer removes parents, just finds existing ones or adds new ones.
        public static void AddOrUpdateParents(ManjTablesContext context, List<Parent> parents)
        {
            for (int i = 0; i < parents.Count; i++)
            {
                var parent = parents[i];
                var existingParent = FindExistingParent(context, parent);
                if (existingParent == null)
                {
                    context.Parents.Add(parent);
                }
                else
                {
                    // Update and use the existing parent
                    existingParent.PhoneNumber = parent.PhoneNumber ?? existingParent.PhoneNumber;
                    existingParent.Email = parent.Email ?? existingParent.Email;
                    parents[i] = existingParent; // Update the list with the tracked entity
                }
            }
            context.SaveChanges(); // Save changes after updating parents
        }
    }
}
