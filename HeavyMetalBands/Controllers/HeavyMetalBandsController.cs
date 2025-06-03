
using HeavyMetalBands.Models;
using HeavyMetalBands.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeavyMetalBands.Controllers
{
    [Route("HeavyMetalBands")]
    public class HeavyMetalBandsController : Controller
    {
        private readonly IBandsService _bandsService;
        public HeavyMetalBandsController(IBandsService bandsService)
        {
            _bandsService = bandsService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index() 
        {
            // using ReadDbContext
            var bands = await _bandsService.GetAllAsync();
            return View(bands);
        }


        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BandDTO band)
        {
            if (!ModelState.IsValid)
                return View(band);

            // using WriteDbContext
            await _bandsService.AddAsync(band);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            // using WriteDbContext
            await _bandsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
