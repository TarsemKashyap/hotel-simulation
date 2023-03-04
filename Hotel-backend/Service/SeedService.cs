using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Identity;

namespace Service;

public class SeedService
{
    private readonly HotelDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppUserRole> roleManager;

    public SeedService(HotelDbContext context, UserManager<AppUser> userManager, RoleManager<AppUserRole> roleManager)
    {
        this._context = context;
        this._userManager = userManager;
        this.roleManager = roleManager;
    }

    public async Task CreateAdminDefault()
    {
        var script = new MigrationScript("56974EE9-E489-40A9-9A56-08C7AC553364", "Admin account");
        var alreadyExecuted = _context.MigrationScripts.Find(script.ScriptId);
        if (script == null)
        {
            var adminUser = new AppUser() { FirstName = "Super", LastName = "Admin", UserName = "admin@hotelsimulation.com", PasswordHash = "123456" };
            await _userManager.CreateAsync(adminUser);
            await _userManager.AddToRoleAsync(adminUser, RoleType.Admin.ToString());
        }


    }


}
