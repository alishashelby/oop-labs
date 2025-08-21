using System;
using System.Security.Cryptography;
using System.Text;

namespace Itmo.ObjectOrientedProgramming.Lab5.Src.Application.Services;

public static class Hasher
{
    public static string Hash(string pin)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.ASCII.GetBytes(pin)));
    }

    public static bool Verify(string pin, string hash)
    {
        return Hash(pin) == hash;
    }
}