using AuthenticationAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
namespace AuthenticationAPI.Data.Data
{
    public class AuthenticationDbContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<QrCode> QrCodes { get; set; }
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options) { }
       
    }
}
