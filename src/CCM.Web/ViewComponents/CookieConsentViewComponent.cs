using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CCM.Services;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;

namespace CCM.ViewComponents
{
    public class CookieConsentViewComponent : ViewComponent
    {
        private IOptions<CookiePolicyOptions> _options;

        public CookieConsentViewComponent(IOptions<CookiePolicyOptions> options)
        {
            _options = options;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var showBanner = (!HttpContext.Features.Get<ITrackingConsentFeature>()?.CanTrack ?? false) && _options.Value.ConsentCookie != null;

            // Might be nice if the feature/builder could simply generate and return the cookie string for use in JS for me

            var cookieName = _options.Value.ConsentCookie?.Name;
            var cookieValue = "yes"; // NOTE: Replace this with value from incoming public constant, see https://github.com/aspnet/Security/issues/1590
            var options = _options.Value.ConsentCookie?.Build(HttpContext);

            var setCookieHeaderValue = new SetCookieHeaderValue(
                Uri.EscapeDataString(cookieName),
                Uri.EscapeDataString(cookieValue))
            {
                Domain = options.Domain,
                Path = options.Path,
                Expires = options.Expires,
                //MaxAge = options.MaxAge, // NOTE: Uncomment in 2.1
                Secure = options.Secure,
                SameSite = (Microsoft.Net.Http.Headers.SameSiteMode)options.SameSite,
                HttpOnly = options.HttpOnly
            };

            var cookieString = setCookieHeaderValue.ToString();
            ViewData["CookieString"] = cookieString;
            ViewData["ShowBanner"] = showBanner;
            return View();
    }
}
}
