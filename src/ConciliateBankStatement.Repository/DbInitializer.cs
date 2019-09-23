using System;
using System.Collections.Generic;
using System.Text;

namespace ConciliateBankStatement.Repository
{
    public static class DbInitializer
    {
        public static void Initialize(StatementContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
