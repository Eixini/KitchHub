using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using webapi.Models;

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
	/// Данный метод необходим для начальной инициализации списка всех ингредиентов.
    /// </summary>
    /// <returns>Список ингредиентов, которые имеются в базе данных</returns>
    [HttpGet]
    [Route("[action]")]
    public async Task<List<string>> InitialGetValidIngredients()
    {
        return await _dbContext.Ingredients
            .Select(i => i.Name)
            .ToListAsync();
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<List<string>> GetDishType()
    {
        return await _dbContext.DishTypes
			.Select(dt => dt.Type)
			.ToListAsync();
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<List<string>> GetNationalKitch()
    {
        return await _dbContext.NationalKitches
            .Select(nk => nk.National)
            .ToListAsync();
    }

    /// <summary>
    /// Метод, который валидирует пользоватетельский ввод,
    /// чтобы избежать некорректных данных при ввводе ингредиентов.
    /// </summary>
    /// <param name="search">Вводимая строка, которую нужно валидировать</param>
    /// <returns>Список ингредиентов, которые имеются в базе данных</returns>
    [HttpPost("{search}")]
    [Route("[action]/{search}")]
    public async Task<List<string>> GetValidIngredients([FromRoute]string search)
	{
		return await _dbContext.Ingredients
			.Select(i => i.Name)
			.Where(x => x.StartsWith(search))
			.Take(5)
			.ToListAsync();
	}

    [HttpPost]
    [Route("[action]")]
    public IActionResult CreateRecipe([FromBody] CreateRecipeModel request)
	{
		Console.WriteLine($"Input data: {request.Type}");

		var newRecipe = new Recipe {
			Name = request.Name,
			Description = request.Description,
            DishType = _dbContext.DishTypes.FirstOrDefault(d => d.Type == request.Type),
            NationalKitch = _dbContext.NationalKitches.FirstOrDefault(nk => nk.National == request.NationalKitch),
            //	Ingredients = request.Ingredients,
            WhoAdded = _dbContext.Users.FirstOrDefault(u => u.NickName == request.WhoAdded),
			DateAdded = DateTime.UtcNow,
			DateUpdated = DateTime.UtcNow,
			Published = false
		};

        _dbContext.Recipes.Update(newRecipe);
        _dbContext.SaveChanges();

        return Ok($"Recipe added. Waiting for moderation.");
    }

    /// <summary>
    /// Метод возвращает рецепты, которые были созданы пользователями,
    /// но не были опубликованы, так как ожидают модерации
    /// </summary>
    /// <returns>Список необходимых данных рецептов</returns>
    [HttpGet]
    [Route("[action]")]
    public IActionResult GetUnpublishedRecipes()
    {
        return Ok(_dbContext.Recipes
            .Where(p => p.Published == false)
            .Select(r => 
                new
                {
                    r.RecipeId,
                    r.Name,
                    r.Description,
                    r.NationalKitch.National,
                    r.DishType.Type,
                    r.WhoAdded.NickName
                })
            .ToList());
    }

    /// <summary>
    /// Метод для получения ингредиентов из "поля тегов" и поиск рецептов по имеющимся тегам.
    /// Ингредиенты получаются из представления в виде массива строк,
    /// после чего, происходит сравнение введенных ингредиентов с ингредиентами рецептов из БД
    /// </summary>
    /// <param name="enteringIngredients"> Введенные игредиенты из "поля тегов"</param>
    [HttpPost("{enteringIngredients}")]
    [Route("[action]/{enteringIngredients}")]
    public IActionResult FindRecipeByIngredients([FromRoute]string enteringIngredients)
	{
		// Для хранения результирующего списка рецептов, удовлетворяющих введенным ингредиентам
		var recipeResultList = new List<ResultRecipe>();

		// Создание массива 
		var ingParts = enteringIngredients?.Split(',').ToList();
		//foreach (string ing in ingParts)
		//	_logger.LogInformation(ing);

		var recipes = _dbContext.Recipes
			.Include(dt => dt.DishType)
			.Include(nk => nk.NationalKitch)
			.Include(i => i.Ingredients)
            .Include(w => w.WhoAdded)
			.Include(d => d.DateAdded)
			.Include(d => d.DateUpdated)
			.Where(p => p.Published == true)
            .ToList();
		foreach (var recipe in recipes)
		{
			//var ing = rec.Ingredients.Include().ToList();
			var recipeIngredients = recipe.Ingredients
				.Select(x => x.Name)
				.ToList();

			if ((bool)!recipeIngredients.Except(ingParts).Any())
			{
				_logger.LogInformation($"{recipe.Name} подходит под введенные ингредиенты");
				
				var resultRecipe = new ResultRecipe();
				resultRecipe.Name = recipe.Name;
				resultRecipe.DishType = recipe.DishType?.Type;
				resultRecipe.NationalKitch = recipe.NationalKitch?.National;
				resultRecipe.Ingredients = new List<string> { };
                foreach (var ing in recipe.Ingredients)
                {
					resultRecipe?.Ingredients?.Add(new String(Convert.ToString(ing.Name)));
                }
                resultRecipe.Description = recipe.Description;
				
                recipeResultList.Add(resultRecipe);
			}

		}

		if(recipeResultList.Count == 0)
		{
			_logger.LogInformation("Рецепты, удовлетворяющие введенным ингредиентам не найдены!");
			return StatusCode(404);
		}

	   return Ok(recipeResultList);
	}

	public IActionResult ShowRecipes()
	{
		return View();
	}
}
