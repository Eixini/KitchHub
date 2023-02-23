using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
		// Для хранения результирующего списка рецептов, удовлетворяющих введенным ингредиентам
		var recipeResultList = new List<Recipe>();

		// Создание массива 
		var ingParts = ingredients.Split(',').ToList();
		//foreach (string ing in ingParts)
		//	_logger.LogInformation(ing);

		var recipes = _dbContext.Recipes
			.Include(i => i.Ingredients)
			.ToList();
		foreach (var recipe in recipes)
		{
			//var ing = rec.Ingredients.Include().ToList();
			var recipeIngredients = recipe?.Ingredients
				.Select(x => x.Name)
				.ToList();

            if ((bool)!recipeIngredients?.Except(ingParts).Any())
            {
				_logger.LogInformation($"{recipe?.Name} подходит под введенные ингредиенты");
				recipeResultList.Add(recipe);
            }

        }

		dynamic test = new ExpandoObject();
		test.one = $"Recipe search: {recipeResultList.Count()} resipe(s)";

		return Json(test);
	}

	public IActionResult ShowRecipes()
	{
		return View();
	}
}
