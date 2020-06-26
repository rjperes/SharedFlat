using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedFlat
{
    public static class DbContextExtensions
    {
        public static T SetTenant<T>(this T context, string name) where T : TenantDbContext
        {

            return context;
        }
    }
}
