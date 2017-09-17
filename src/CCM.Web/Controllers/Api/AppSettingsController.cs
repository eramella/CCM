using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CCM.ViewModels;
using CCM.Data.Enums;
using CCM.Business.Repositories;
using CCM.Data.Models;
using AutoMapper;
using CCM.Business.Utilities;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.IO;
using Microsoft.Extensions.FileProviders;
using System.Threading;
using Microsoft.AspNetCore.Authorization;

namespace CCM.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/AppSettings")]
    public class AppSettingsController : Controller
    {
        private CCMContext _context;
        private IMapper _mapper;
        private ILogger<AppSettingsController> _logger;
        private IFileProvider _fileProvider;

        public AppSettingsController(CCMContext context, IMapper mapper, ILogger<AppSettingsController> logger, IFileProvider fileProvider)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _fileProvider = fileProvider;

        }

        [HttpGet]
        public async Task<IActionResult> GetAppSettings()
        {
            UnitOfWork uow = new UnitOfWork(_context);
            var settings = await uow.AppSettingsRepository.GetAppSettings();
            if (settings == null)
            {
                return NotFound($"Could not find Settings");
            }
            return new ObjectResult(_mapper.Map<AppSettingsVm>(settings));
        }

        [HttpPost]
        public async Task<IActionResult> PostSettings(AppSettingsVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfWork uow = new UnitOfWork(_context);
                    var existingSettings = await uow.AppSettingsRepository.GetAppSettings();
                    if (existingSettings == null)
                    {
                        return NotFound($"Could not find Settings");
                    }

                    existingSettings.CampName = (model.CampName??"").Trim();
                    existingSettings.TagLine = (model.TagLine??"").Trim();
                    existingSettings.NextCampId = model.NextCamp;

                    if (model.pic1Upload != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.pic1Upload.CopyToAsync(memoryStream);
                            existingSettings.Pic1 = memoryStream.ToArray();
                            existingSettings.Pic1ContentType = model.pic1Upload.ContentType;
                            existingSettings.Pic1FileName = model.pic1Upload.FileName;
                        }
                    }
                    else
                    {
                        if (model.Image1Deleted.GetValueOrDefault())
                        {
                            existingSettings.Pic1 = null;
                            existingSettings.Pic1ContentType = null;
                            existingSettings.Pic1FileName = null;
                        }
                    }
                    if (model.pic2Upload != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.pic2Upload.CopyToAsync(memoryStream);
                            existingSettings.Pic2 = memoryStream.ToArray();
                            existingSettings.Pic2ContentType = model.pic2Upload.ContentType;
                            existingSettings.Pic2FileName = model.pic2Upload.FileName;
                        }
                    }
                    else
                    {
                        if (model.Image2Deleted.GetValueOrDefault())
                        {
                            existingSettings.Pic2 = null;
                            existingSettings.Pic2ContentType = null;
                            existingSettings.Pic2FileName = null;
                        }
                    }
                    if (model.pic3Upload != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.pic3Upload.CopyToAsync(memoryStream);
                            existingSettings.Pic3 = memoryStream.ToArray();
                            existingSettings.Pic3ContentType = model.pic3Upload.ContentType;
                            existingSettings.Pic3FileName = model.pic3Upload.FileName;
                        }
                    }
                    else
                    {
                        if (model.Image3Deleted.GetValueOrDefault())
                        {
                            existingSettings.Pic3 = null;
                            existingSettings.Pic3ContentType = null;
                            existingSettings.Pic3FileName = null;
                        }
                    }
                    if (model.pic4Upload != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.pic4Upload.CopyToAsync(memoryStream);
                            existingSettings.Pic4 = memoryStream.ToArray();
                            existingSettings.Pic4ContentType = model.pic4Upload.ContentType;
                            existingSettings.Pic4FileName = model.pic4Upload.FileName;
                        }
                    }
                    else
                    {
                        if (model.Image4Deleted.GetValueOrDefault())
                        {
                            existingSettings.Pic4 = null;
                            existingSettings.Pic4ContentType = null;
                            existingSettings.Pic4FileName = null;
                        }
                    }
                    if (model.pic5Upload != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await model.pic5Upload.CopyToAsync(memoryStream);
                            existingSettings.Pic5 = memoryStream.ToArray();
                            existingSettings.Pic5ContentType = model.pic5Upload.ContentType;
                            existingSettings.Pic5FileName = model.pic5Upload.FileName;
                        }
                    }
                    else
                    {
                        if (model.Image5Deleted.GetValueOrDefault())
                        {
                            existingSettings.Pic5 = null;
                            existingSettings.Pic5ContentType = null;
                            existingSettings.Pic5FileName = null;
                        }
                    }
                    if (await uow.SaveAsync())
                    {
                        return Ok(new ObjectResult(_mapper.Map<AppSettingsVm>(existingSettings)));
                    }
                    else
                    {
                        _logger.LogWarning("Could not update Settings");
                    }
                }
                catch (Exception x)
                {
                    _logger.LogError($"Throw exeption while updating AppSettings: {x}");
                }
            }
            return BadRequest("Could not update AppSettings");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/AppSettings/GetImage/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            IDirectoryContents contents = _fileProvider.GetDirectoryContents("");
            IFileInfo fileInfo = _fileProvider.GetFileInfo("/img/image-placeholder.png");
            
            var uow = new UnitOfWork(_context);
            var settings = await uow.AppSettingsRepository.GetAppSettings();

            switch (id)
            {
                case 1:
                    if (settings.Pic1 != null && settings.Pic1.Length > 0)
                    {
                        return File(settings.Pic1, settings.Pic1ContentType, settings.Pic1FileName);
                    }
                    break;
                case 2:
                    if (settings.Pic2 != null && settings.Pic2.Length > 0)
                    {
                        return File(settings.Pic2, settings.Pic2ContentType, settings.Pic2FileName);
                    }
                    break;
                case 3:
                    if (settings.Pic3 != null && settings.Pic3.Length > 0)
                    {
                        return File(settings.Pic3, settings.Pic3ContentType, settings.Pic3FileName);
                    }
                    break;
                case 4:
                    if (settings.Pic4 != null && settings.Pic4.Length > 0)
                    {
                        return File(settings.Pic4, settings.Pic4ContentType, settings.Pic4FileName);
                    }
                    break;
                case 5:
                    if (settings.Pic5 != null && settings.Pic5.Length > 0)
                    {
                        return File(settings.Pic5, settings.Pic5ContentType, settings.Pic5FileName);
                    }
                    break;
                default:
                    return BadRequest("No image available");
                    break;
            }
            return BadRequest("No image available"); // File(fileInfo.CreateReadStream(), "image/png", fileInfo.Name);                        
        }
    }
}