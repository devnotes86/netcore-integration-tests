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


        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            // using ReadDbContext
            var band = await _bandsService.GetByIdAsync(id);
            return View(band);

        }


        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }


       [HttpPost("Create")] 
        public async Task<IActionResult> Create(BandDTO band)
        {
            if (!ModelState.IsValid)
                return View(band);

            // using WriteDbContext
            await _bandsService.AddAsync(band);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost("Delete/{id:int}")] 
        public async Task<IActionResult> Delete(int id)
        {

            // using WriteDbContext
            await _bandsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
