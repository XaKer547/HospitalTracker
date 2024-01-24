namespace HospitalTrackerApi.Data.Seeder
{
    public partial class HospitalTrackerDbSeeder
    {
        public async Task SeedRolesAsync(HospitalTrackerDbContext context)
        {
            if (context.Roles.Any())
                return;

            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "Клиент"
                },
                new Role()
                {
                    Name = "Соотрудник"
                }
            };

            context.Roles.AddRange(roles);

            await context.SaveChangesAsync();
        }
    }
}