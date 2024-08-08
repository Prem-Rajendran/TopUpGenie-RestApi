using System;
using Microsoft.EntityFrameworkCore;

namespace TopUpGenie.DataAccess.Extensions
{
	public static class ExceptionExtension
	{
		public static void HandleDbUpdateException(this DbContext dbContext, DbUpdateException ex)
		{
            foreach (var entry in ex.Entries)
            {
                // Determine the appropriate action based on the entity state
                switch (entry.State)
                {
                    case EntityState.Added:
                        // Remove the invalid entity from the DbContext
                        dbContext.Entry(entry.Entity).State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                        // Revert changes to the entity
                        dbContext.Entry(entry.Entity).State = EntityState.Unchanged;
                        break;
                    case EntityState.Deleted:
                        // Revert deletion of the entity
                        dbContext.Entry(entry.Entity).State = EntityState.Unchanged;
                        break;
                    default:
                        // Handle other states as needed
                        break;
                }
            }
        }
	}
}

