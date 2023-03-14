using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.ComponentModel;
using System.Dynamic;

namespace webapi.Controllers;

[Route("[controller]")]
public class RecipeController : Controller
{
	private readonly ILogger<RecipeController> _logger;
	private readonly KitchHubDbContext _dbContext;

	public RecipeController(ILogger<RecipeController> logger, KitchHubDbContext dbContext)
	{
		_logger = logger;
		_dbContext = dbContext;
	}

	public IActionResult EnterIngredientsPage()
	{
		return View();
	}

	/// <summary>
	/// Метод, который валидирует пользоватетельский ввод,
	/// чтобы избежать некорректных данных при ввводе ингредиентов.
	/// </summary>
	/// <param name="search">Вводимая строка, которую нужно валидировать</param>
	/// <returns>Список ингредиентов, которые имеются в базе данных</returns>
	[HttpGet]
	public async Task<List<string>> GetValidIngredients(string search)
	{
		return await _dbContext.Ingredients
			.Select(i => i.Name)
			.Where(x => x.StartsWith(search))
			.Take(5)
			.ToListAsync();
	}

	/// <summary>
	/// Метод для получения ингредиентов из "поля тегов" и поиск рецептов по имеющимся тегам.
	/// Ингредиенты получаются из представления в виде массива строк,
	/// после чего, происходит сравнение введенных ингредиентов с ингредиентами рецептов из БД
	/// </summary>
	/// <param name="enteringIngredients"> Введенные игредиенты из "поля тегов"</param>
	[HttpPost("{enteringIngredients}")]
    [Route("[action]/{enteringIngredients}")]
    public JsonResult FindRecipeByIngredients([FromRoute]string enteringIngredients)
	{
		Console.WriteLine($"BackEnd input data: {enteringIngredients}");
		// Для хранения результирующего списка рецептов, удовлетворяющих введенным ингредиентам
		var recipeResultList = new List<Recipe>();

		// Создание массива 
		var ingParts = enteringIngredients?.Split(',').ToList();
		//foreach (string ing in ingParts)
		//	_logger.LogInformation(ing);
		Console.WriteLine($"Введено {ingParts?.Count()} ингредиента");

		var recipes = _dbContext.Recipes
			.Include(dt => dt.DishType)
			.Include(nk => nk.NationalKitch)
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

		if(recipeResultList.Count == 0)
		{
			_logger.LogInformation("Рецепты, удовлетворяющие введенным ингредиентам не найдены!");
			return Json("Рецепты, удовлетворяющие введенным ингредиентам не найдены!");
		}

		dynamic jsonResult = new ExpandoObject();
        jsonResult.one = $"Recipe search: {recipeResultList.Count()} resipe(s)";

		return Json(jsonResult);
	}

	public IActionResult ShowRecipes()
	{
		return View();
	}
}
