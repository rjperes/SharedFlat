namespace SharedFlat.EntityFrameworkCore
{
    public static class DbContextExtensions
    {
        public static T SetTenant<T>(this T context, string name) where T : TenantDbContext
        {

            return context;
        }
    }
}
