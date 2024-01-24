namespace HospitalTrackerApi.Data.Seeder
{
    public partial class HospitalTrackerDbSeeder
    {
        private const int UsersPerRole = 10;
        public async Task SeedPersonsAsync(HospitalTrackerDbContext context)
        {
            if (context.Persons.Any())
                return;

            var roles = context.Roles.ToArray();

            foreach (var role in roles)
            {
                var users = CreatePersonsWithRole(role);

                context.Persons.AddRange(users);
            }

            await context.SaveChangesAsync();
        }

        private IEnumerable<Person> CreatePersonsWithRole(Role role)
        {
            for (int i = 0; i < UsersPerRole; i++)
            {
                yield return new Person
                {
                    Role = role
                };
            }
        }
    }
}
