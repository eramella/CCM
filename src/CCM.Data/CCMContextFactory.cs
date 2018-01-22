using CCM.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace CCM.Data
{
    public class CCMContextFactory : IDesignTimeDbContextFactory<CCMContext>
    {
        public CCMContext CreateDbContext(string[] args)
        {
            return CreateDbContext();
        }

        public CCMContext CreateDbContext()
        {         
            var basePath = Directory.GetCurrentDirectory();
            basePath = basePath.Replace("CCM.Data", "CCM.Web");
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(environmentName))
                environmentName = "Development";
            return CreateDbContext(basePath, environmentName);
        }        

        private CCMContext CreateDbContext(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            var connstr = config["ConnectionStrings:CCMContextConnection"];

            if (String.IsNullOrWhiteSpace(connstr) == true)
            {
                throw new InvalidOperationException(
                    "Could not find a connection string named 'ConnectionStrings:CCMContextConnection'.");
            }
            else
            {
                return CreateDbContext(connstr);
            }
        }

        private CCMContext CreateDbContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"{nameof(connectionString)} is null or empty.", nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<CCMContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new CCMContext(optionsBuilder.Options);
        }
    }
}
