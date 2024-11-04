using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SecureApi.dbContext
{
    public class ApplicationUser : IdentityUser<Guid>
    {
    }

    public class ApplicationRole : IdentityRole<Guid>
    {

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
    }
}
