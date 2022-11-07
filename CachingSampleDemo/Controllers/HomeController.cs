using CachingSampleDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace CachingSampleDemo.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private IMemoryCache memoryCache;

        public HomeController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }


        public IActionResult Index()
        {
            DateTime currentTime;
            bool AlreadyExit = memoryCache.TryGetValue("CachedTime", out currentTime);
            if (!AlreadyExit)
            {
                currentTime = DateTime.Now;
                var cacheEntryOptions = new MemoryCacheEntryOptions().
                    SetSlidingExpiration(TimeSpan.FromSeconds(20));

                memoryCache.Set("CachedTime", currentTime, cacheEntryOptions);
            }

            return View(currentTime);
        }
    }
}