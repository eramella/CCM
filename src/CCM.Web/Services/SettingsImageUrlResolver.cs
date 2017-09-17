using AutoMapper;
using CCM.Data.Models;
using CCM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCM.Services
{
    public class SettingsImageUrlResolver : IMemberValueResolver<AppSettings, AppSettingsVm, byte[], string>
    {
        public string Resolve(AppSettings source, AppSettingsVm destination, byte[] sourceMember, string destMember, ResolutionContext context)
        {
            if (sourceMember == null || sourceMember.Length == 0)
            {
                return "";
            }
            else
            {
                if (sourceMember.Length == source.Pic1.Length) return "/AppSettings/GetImage/1";
                if (sourceMember.Length == source.Pic2.Length) return "/AppSettings/GetImage/2";
                if (sourceMember.Length == source.Pic3.Length) return "/AppSettings/GetImage/3";
                if (sourceMember.Length == source.Pic4.Length) return "/AppSettings/GetImage/4";
                if (sourceMember.Length == source.Pic5.Length) return "/AppSettings/GetImage/5";
            }
            return "";
        }
    }
}
