namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services.Admin;

public interface IAdminService
{
    LoginResult Login(string pin);
}