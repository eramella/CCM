using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CCM.Data.Models;
using Microsoft.AspNetCore.Identity;
using CCM.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authorization;

namespace CCM.Controllers
{
    public class ProfileController : Controller
    {
        private IMapper _mapper;
        private CCMContext _context;
        private UserManager<CCMUser> _userManager;
        private ILogger _logger;
        private IHostingEnvironment _env;
        private IFileProvider _fileProvider;

        public ProfileController(IMapper mapper,
            CCMContext context,
            UserManager<CCMUser> userManager,
            ILogger<ProfileController> logger,
            IHostingEnvironment env,
            IFileProvider fileProvider)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _env = env;
            _fileProvider = fileProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            CCMUser user = await _userManager.GetUserAsync(User);
            var outUser = _mapper.Map<CCMUser, UserVm>(user);
            return View(outUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveProfile(UserVm user)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var avatar = currentUser.Avatar;
                var avatarContentType = currentUser.AvatarContentType;
                var avatarFileName = currentUser.AvatarFileName;

                CCMUser updatedUser = _mapper.Map(user, currentUser);

                if (user.UploadedImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await user.UploadedImage.CopyToAsync(memoryStream);
                        updatedUser.Avatar = memoryStream.ToArray();
                        updatedUser.AvatarContentType = user.UploadedImage.ContentType;
                        updatedUser.AvatarFileName = user.UploadedImage.FileName;
                    }
                }
                else
                {
                    updatedUser.Avatar = avatar;
                    updatedUser.AvatarContentType = avatarContentType;
                    updatedUser.AvatarFileName = avatarFileName;
                }
                
                var result = await _userManager.UpdateAsync(updatedUser);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Something went wrong. Try again!");
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError($"{error.Code}: {error.Description}");

                        if (_env.IsDevelopment())
                        {
                            ModelState.AddModelError("", $"{error.Code}: {error.Description}");
                        }
                    }
                }
                user = _mapper.Map(updatedUser, user);
            }

            return View("Profile", user);
        }

        [HttpGet]
        [Route("/Profile/GetImage/{id?}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetImage(string id)
        {
            CCMUser requestedUser;

            if (String.IsNullOrEmpty(id))
            {
                requestedUser = await _userManager.GetUserAsync(User);
            }
            else
            {
                requestedUser = await _userManager.FindByIdAsync(id);
            }

            IDirectoryContents contents = _fileProvider.GetDirectoryContents("");
            IFileInfo fileInfo = _fileProvider.GetFileInfo("/img/anonimus.png");

            if (requestedUser.Avatar == null)
            {
                return File(fileInfo.CreateReadStream(), "image/png", fileInfo.Name);
            }

            return File(requestedUser.Avatar, requestedUser.AvatarContentType, requestedUser.AvatarFileName);
        }

    }
}