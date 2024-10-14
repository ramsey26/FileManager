using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API
{
    public class DBInitializer
    {
        public static void InitDb(WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                Scope(services.GetRequiredService<DataContext>(),
                services.GetRequiredService<UserManager<AppUser>>());
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<DBInitializer>>();
                logger.LogError(ex, "An error occured during migration");
            }
        }

        private static async void Scope(DataContext context, UserManager<AppUser> userManager)
        {
            context.Database.Migrate();

            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>{
                    new AppUser{DisplayName = "Bob",UserName = "bob" , Email = "bob@test.com"},
                    new AppUser{DisplayName = "Tom",UserName = "tom" , Email = "tom@test.com"},
                    new AppUser{DisplayName = "Jane",UserName = "jane" , Email = "jane@test.com"}
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            // Get users from the UserManager to retrieve their Ids
            var bob = await userManager.FindByNameAsync("bob");
            var tom = await userManager.FindByNameAsync("tom");
            var jane = await userManager.FindByNameAsync("jane");

            if (context.Files.Any())
            {
                return;
            }

            var files = new List<FileMetaData>
    {
        new FileMetaData
        {
            Id = Guid.NewGuid(),
            FileName = "Document1.pdf",
            Size = 204800,
            Format = "pdf",
            UploadDate = new DateTime(2023, 10, 01),
            FilePath = "/uploads/Document1.pdf",
            AppUserId = bob.Id // Assign AppUserId to Bob
        },
        new FileMetaData
        {
            Id = Guid.NewGuid(),
            FileName = "Image1.png",
            Size = 512000,
            Format = "png",
            UploadDate = new DateTime(2023, 10, 02),
            FilePath = "/uploads/Image1.png",
            AppUserId = tom.Id // Assign AppUserId to Tom
        },
        new FileMetaData
        {
            Id = Guid.NewGuid(),
            FileName = "Presentation.pptx",
            Size = 1048576,
            Format = "pptx",
            UploadDate = new DateTime(2023, 10, 03),
            FilePath = "/uploads/Presentation.pptx",
            AppUserId = jane.Id // Assign AppUserId to Jane
        },
        new FileMetaData
        {
            Id = Guid.NewGuid(),
            FileName = "Report.pdf",
            Size = 307200,
            Format = "pdf",
            UploadDate = new DateTime(2023, 10, 04),
            FilePath = "/uploads/Report.pdf",
            AppUserId = bob.Id // Assign AppUserId to Bob
        },
        new FileMetaData
        {
            Id = Guid.NewGuid(),
            FileName = "Design.png",
            Size = 716800,
            Format = "png",
            UploadDate = new DateTime(2023, 10, 05),
            FilePath = "/uploads/Design.png",
            AppUserId = tom.Id // Assign AppUserId to Tom
        },
        new FileMetaData
        {
            Id = Guid.NewGuid(),
            FileName = "Manual.docx",
            Size = 204800,
            Format = "docx",
            UploadDate = new DateTime(2023, 10, 06),
            FilePath = "/uploads/Manual.docx",
            AppUserId = jane.Id // Assign AppUserId to Jane
        }
    };

            await context.Files.AddRangeAsync(files);
            await context.SaveChangesAsync();

        }
    }
}