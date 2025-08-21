using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Admin;
using System.Configuration;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Infrastructure.Services;

public class AdminService : IAdminService
{
    public LoginResult Login(string pin)
    {
        string? configPin = ConfigurationManager.AppSettings["AdminPin"];

        return configPin is null || !Hasher.Verify(pin, configPin)
            ? new LoginResult.Failure()
            : new LoginResult.Success();
    }
}