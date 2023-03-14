using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[Route("[controller]")]
public class EnteringIngredientsController : Controller
{
    private readonly ILogger<EnteringIngredientsController> _logger;
    private readonly KitchHubDbContext _context;

    public EnteringIngredientsController(ILogger<EnteringIngredientsController> logger, 
        KitchHubDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        _logger.LogDebug("Open Entering Ingredients Page");
        return View();
    }

    // GET: EnteringIngredientsController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: EnteringIngredientsController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: EnteringIngredientsController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: EnteringIngredientsController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: EnteringIngredientsController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: EnteringIngredientsController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
