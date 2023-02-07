using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using System.Dynamic;

namespace KitchHub.Controllers;

public class RecipeSearch : Controller
{
	private readonly ILogger<RecipeSearch> _logger;
	private readonly KitchHubDbContext _dbContext;

	public RecipeSearch(ILogger<RecipeSearch> logger, KitchHubDbContext dbContext)
	{
		_logger = logger;
		_dbContext = dbContext;
	}

	public IActionResult EnterIngredientsPage()
    {
        return View();
    }

	/// <summary>
	/// Метод для получения ингредиентов из "поля тегов" и поиск рецептов по имеющимся тегам.
	/// Ингредиенты получаются из представления в виде массива строк,
	/// после чего, происходит сравнение введенных ингредиентов с ингредиентами рецептов из БД
	/// </summary>
	/// <param name="ingredients"> Введенные игредиенты из "поля тегов"</param>
	[HttpGet]
	public JsonResult SearchRecipe([FromQuery]string ingredients)
	{
		// Создание массива 
		var ingParts = ingredients.Split(',');
		foreach (string ing in ingParts)
			Console.WriteLine(ing);

		var testName = _dbContext.DishTypes.FirstOrDefault();
		Console.WriteLine(testName?.Type);

		dynamic test = new ExpandoObject();
		test.one = "It's test result";
		test.two = 74;
		test.three = new [] { 1,2,3,4,5 };

		return Json(test);
	}

	public IActionResult ShowRecipes()
	{
		return View();
	}
}
