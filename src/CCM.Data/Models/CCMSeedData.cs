using CCM.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CCM.Data.Models
{
    public class CCMSeedData
    {
        private CCMContext _context;
        private UserManager<CCMUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IFileProvider _fileProvider;

        public CCMSeedData(CCMContext context, UserManager<CCMUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IFileProvider fileProvider
            )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _fileProvider = fileProvider;
        }

        public async Task EnsureSeedData()
        {
            var speakerRoleExist = await _roleManager.RoleExistsAsync("speaker");
            if (!speakerRoleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole("speaker"));
            }
            var adminRoleExist = await _roleManager.RoleExistsAsync("admin");
            if (!adminRoleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
            }
            var attendeeRoleExist = await _roleManager.RoleExistsAsync("attendee");
            if (!attendeeRoleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole("attendee"));
            }
            var teamRoleExist = await _roleManager.RoleExistsAsync("team");
            if (!teamRoleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole("team"));
            }
            IDirectoryContents contents = _fileProvider.GetDirectoryContents("");
            if (await _userManager.FindByEmailAsync("pippo.franco@emaquest.net") == null)
            {
                var user = new CCMUser()
                {
                    UserName = "pippofranco",
                    Email = "pippo.franco@emaquest.net",
                    FirstName = "Pippo",
                    LastName = "Franco"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "admin");
                }
                IFileInfo fileInfo = _fileProvider.GetFileInfo("/img/TempImg/ema.png");
                using (var memoryStream = new MemoryStream())
                {
                    var fileStream = fileInfo.CreateReadStream();
                    await fileStream.CopyToAsync(memoryStream);
                    user.Avatar = memoryStream.ToArray();
                    user.AvatarContentType = "image/png";
                    user.AvatarFileName = fileInfo.Name;
                }
                await _context.SaveChangesAsync();
            }

            if (await _userManager.FindByEmailAsync("pippo.speaker@emaquest.net") == null)
            {
                var user = new CCMUser()
                {
                    UserName = "pippospeaker",
                    Email = "pippo.speaker@emaquest.net",
                    FirstName = "Giacomo",
                    LastName = "Speaker"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "speaker");
                }
                IFileInfo fileInfo = _fileProvider.GetFileInfo("/img/TempImg/ema.png");
                using (var memoryStream = new MemoryStream())
                {
                    var fileStream = fileInfo.CreateReadStream();
                    await fileStream.CopyToAsync(memoryStream);
                    user.Avatar = memoryStream.ToArray();
                    user.AvatarContentType = "image/png";
                    user.AvatarFileName = fileInfo.Name;
                }
                await _context.SaveChangesAsync();
            }

            if (await _userManager.FindByEmailAsync("pippo.attendee@emaquest.net") == null)
            {
                var user = new CCMUser()
                {
                    UserName = "pippoattendee",
                    Email = "pippo.attendee@emaquest.net"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "attendee");
                }
            }

            if (await _userManager.FindByNameAsync("Hattan") == null)
            {
                var user = new CCMUser()
                {
                    UserName = "Hattan",
                    FirstName = "Hattan",
                    LastName = "Shobokshi",
                    Email = "hatta@hattan.com",
                    Bio = "Hattan Shobokshi is a software architect, technical speaker & Microsoft MVP. Hattan is responsible for developing applications on a variety of platforms including web and mobile devices. He is passionate about delivering scalable applications that adhere to industry best practices. He is Vice President of the Los Angeles .NET user group as well as a member of the organizing committee for SoCal Code Camp. He regularly presents at local user groups and code camps.",
                    LinkedinUrl = "https://www.linkedin.com/pub/hattan-shobokshi/0/868/919",
                    TwitterUrl = "http://www.twitter.com/hattan"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "admin");
                }

                IFileInfo fileInfo = _fileProvider.GetFileInfo("/img/Team/hattan.jpg");
                using (var memoryStream = new MemoryStream())
                {
                    var fileStream = fileInfo.CreateReadStream();
                    await fileStream.CopyToAsync(memoryStream);
                    user.Avatar = memoryStream.ToArray();
                    user.AvatarContentType = "image/jpg";
                    user.AvatarFileName = fileInfo.Name;
                }
                await _context.SaveChangesAsync();
            }

            if (await _userManager.FindByNameAsync("Daisy") == null)
            {
                var user = new CCMUser()
                {
                    UserName = "Daisy",
                    FirstName = "Hattan",
                    LastName = "Shobokshi",
                    Email = "daisy@daisy.com",
                    LinkedinUrl = "https://www.linkedin.com/in/daisyshobokshi"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "attendee");
                }

                IFileInfo fileInfo = _fileProvider.GetFileInfo("/img/Team/daisy.jpg");
                using (var memoryStream = new MemoryStream())
                {
                    var fileStream = fileInfo.CreateReadStream();
                    await fileStream.CopyToAsync(memoryStream);
                    user.Avatar = memoryStream.ToArray();
                    user.AvatarContentType = "image/jpg";
                    user.AvatarFileName = fileInfo.Name;
                }
                await _context.SaveChangesAsync();
            }

            if (await _userManager.FindByNameAsync("Art") == null)
            {
                var user = new CCMUser()
                {
                    UserName = "Art",
                    FirstName = "Art",
                    LastName = "Villa",
                    Email = "art@art.com",
                    LinkedinUrl = "https://www.linkedin.com/pub/arthur-villa/3/631/b90"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "attendee");
                }

                IFileInfo fileInfo = _fileProvider.GetFileInfo("/img/Team/art.jpg");
                using (var memoryStream = new MemoryStream())
                {
                    var fileStream = fileInfo.CreateReadStream();
                    await fileStream.CopyToAsync(memoryStream);
                    user.Avatar = memoryStream.ToArray();
                    user.AvatarContentType = "image/jpg";
                    user.AvatarFileName = fileInfo.Name;
                }
                await _context.SaveChangesAsync();
            }

            if (await _userManager.FindByNameAsync("Janet") == null)
            {
                var user = new CCMUser()
                {
                    UserName = "Janet",
                    FirstName = "Janet",
                    LastName = "Chung",
                    Email = "janet@janet.com",
                    LinkedinUrl = "https://www.linkedin.com/in/janetchung"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "attendee");
                }

                IFileInfo fileInfo = _fileProvider.GetFileInfo("/img/Team/janet.jpg");
                using (var memoryStream = new MemoryStream())
                {
                    var fileStream = fileInfo.CreateReadStream();
                    await fileStream.CopyToAsync(memoryStream);
                    user.Avatar = memoryStream.ToArray();
                    user.AvatarContentType = "image/jpg";
                    user.AvatarFileName = fileInfo.Name;
                }
                await _context.SaveChangesAsync();
            }

            if (await _userManager.FindByNameAsync("Chuck") == null)
            {
                var user = new CCMUser()
                {
                    UserName = "Chuck",
                    FirstName = "Chuck",
                    LastName = "Bagby",
                    Email = "chuck@chuck.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "attendee");
                }

                IFileInfo fileInfo = _fileProvider.GetFileInfo("/img/Team/Charlie.jpg");
                using (var memoryStream = new MemoryStream())
                {
                    var fileStream = fileInfo.CreateReadStream();
                    await fileStream.CopyToAsync(memoryStream);
                    user.Avatar = memoryStream.ToArray();
                    user.AvatarContentType = "image/jpg";
                    user.AvatarFileName = fileInfo.Name;
                }
                await _context.SaveChangesAsync();
            }

            if (!_context.AppSettings.Any())
            {
                var appConfig = new AppSettings()
                {                    
                    CampName = "",
                    Id = true,
                    TagLine = ""
                };
                _context.AppSettings.Add(appConfig);
                await _context.SaveChangesAsync();
            }

            if (!_context.Tags.Any())
            {
                var tag1 = new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "asp.net Core"
                };

                var tag2 = new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Javascript"
                };
                _context.AddRange(tag1, tag2);
                await _context.SaveChangesAsync();
            }

            if (!_context.Camps.Any())
            {
                var tag1 = _context.Tags.FirstOrDefault(t => t.Name == "asp.net Core");
                var tag2 = _context.Tags.FirstOrDefault(t => t.Name == "Javascript");
                var tagSession1 = new TagSession() { Tag = tag1 };
                var tagSession2 = new TagSession() { Tag = tag2 };
                var tagSession3 = new TagSession() { Tag = tag1 };
                
                var newCamp = new Camp()
                {
                    DateFrom = new DateTime(2017, 6, 24),
                    DateTo = new DateTime(2017, 06, 25),
                    LocationName = "San Diego",
                    State = CampState.Active,
                    Sessions = new Session[]
                    {
                            new Session()
                            {
                                Description = @"Are your production releases big and scary? They shouldn’t be! In this talk I’ll show you how to make releases easier and repeatable. We’ll look at how CI can surface issues faster. Then we'll set up a CI server, hook up a process to build and deploy to Azure with every checkin. I’ll show you all the tools you need to get started with CI on the .NET stack." +
                                              @"This talk is by a Developer for Developers.If you're interested in the release process and making it easier, this talk is for you.",
                                Level = SessionLevel.L100,
                                Title = "Accidentally DevOps : Continuous Integration for the .NET Developer",
                                User = await _userManager.FindByEmailAsync("pippo.speaker@emaquest.net"),
                                TagSessions = new List<TagSession>() { tagSession1,tagSession2 }
                            },
                            new Session()
                            {
                                Description = @"In this talk, I will give you an overview of the Ionic Framework, show how to install it, and finally build a quick app and deploy it to both iOS and Android using some of the additional Ionic.io services.",
                                Level = SessionLevel.L100,
                                Title = "What is the Ionic Framework?",
                                TagSessions = new List<TagSession>(){ tagSession3 },
                                User = await _userManager.FindByEmailAsync("pippo.franco@emaquest.net"),
                            },

                    }


                };

                _context.Add(newCamp);
                await _context.SaveChangesAsync();
            }
            
        }
    }
}
