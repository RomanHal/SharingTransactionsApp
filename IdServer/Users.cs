using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace IdServer
{
    internal class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser> {
            new TestUser {
                SubjectId ="5BE86359-073C-434B-AD2D-A3932222DAAA",
                Username = "User1",
                Password = "pass",
                Claims = new List<Claim> {
                    new Claim(JwtClaimTypes.Email, "user1@test.com"),
                    new Claim(JwtClaimTypes.Role, "user"),
                    new Claim(JwtClaimTypes.Name, "Roman")
                }
            },
            new TestUser {
                SubjectId ="5BE86359-073C-434B-AD2D-A3932222DA",
                Username = "User2",
                Password = "pass",
                Claims = new List<Claim> {
                    new Claim(JwtClaimTypes.Email, "User2@test.com"),
                    new Claim(JwtClaimTypes.Role, "user"),
                    new Claim(JwtClaimTypes.Name, "User2")
                }
            }
        };




        }

    }
}
