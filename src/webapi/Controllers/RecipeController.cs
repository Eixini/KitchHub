using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Controllers;

[ApiController]
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
    [HttpPost]
    [Route("[action]")]
    public async Task<List<string>> GetValidIngredients([FromBody]string search)
	{
		return await _dbContext.Ingredients
			.Select(i => i.Name)
			.Where(x => x.StartsWith(search))
			.Take(5)
			.ToListAsync();
	}

    /// <summary>
    /// Создание пользовательского рецепта
    /// </summary>
    /// <param name="request">Данные, введенные пользователем на стороне клиента</param>
    /// <returns>Сообщение об процессе создания рецепта</returns>
    [HttpPost]
    [Route("[action]")]
    [Authorize]
    public IActionResult CreateRecipe([FromBody] CreateRecipeModel request)
	{
        var ingredientsArray = new List<Ingredient>();
        foreach (var ingredient in request.Ingredients)
        {
            var currentIngredient = new Ingredient();
            currentIngredient.Name = request.Name;
            //Console.WriteLine("Тип ингредиента:" + _dbContext.Ingredients.FirstOrDefault(n => n.Name == ingredient).IngredientsType);
            currentIngredient.IngredientsType = _dbContext.Ingredients
                                                          .FirstOrDefault(n => n.Name == ingredient)
                                                          .IngredientsType;


            ingredientsArray.Add(currentIngredient);
        }

        var newRecipe = new Recipe {
			Name = request.Name,
			Description = request.Description,
            DishType = _dbContext.DishTypes.FirstOrDefault(d => d.Type == request.Type),
            NationalKitch = _dbContext.NationalKitches.FirstOrDefault(nk => nk.National == request.NationalKitch),
            Ingredients = ingredientsArray,
            WhoAdded = _dbContext.Users.FirstOrDefault(u => u.NickName == request.WhoAdded),
			DateAdded = DateTime.UtcNow,
			DateUpdated = DateTime.UtcNow,
			Published = false
		};

        _dbContext.Recipes.Add(newRecipe);
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
    [Authorize]
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
    /// Метод для публикации рецепта.
    /// </summary>
    /// <param name="recipeId">Id рецепта, который нужно опуубликовать</param>
    /// <returns>Сообщение о процессе публикации</returns>
    [HttpPost]
    [Route("[action]")]
    [Authorize]
    public IActionResult PublishRecipe(string recipeId)
    {
        var item = _dbContext.Recipes.Find(recipeId);

        if (item == null)
        {
            return NotFound();
        }

        item.Published = true;
        item.DateUpdated = DateTime.UtcNow;

        //_dbContext.Recipes.Update(item);
        _dbContext.SaveChanges();

        return Ok("Recipe published");
    }

    /// <summary>
    /// Метод для удаления неопубликованного рецепта из базы данных
    /// </summary>
    /// <param name="recipeId">id удаляемого рецепта</param>
    /// <returns>Сообщение о процессе</returns>
    [HttpDelete]
    [Route("[action]")]
    [Authorize]
    public IActionResult DeleteUnpublishedRecipes([FromBody]long recipeId)
    {
        var item = _dbContext.Recipes.Find(recipeId);

        if (item == null)
        {
            return NotFound();
        }

        _dbContext.Recipes.Remove(item);
        _dbContext.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Метод для получения ингредиентов из "поля тегов" и поиск рецептов по имеющимся тегам.
    /// Ингредиенты получаются из представления в виде массива строк,
    /// после чего, происходит сравнение введенных ингредиентов с ингредиентами рецептов из БД
    /// </summary>
    /// <param name="enteringIngredients"> Введенные игредиенты из "поля тегов"</param>
    [HttpPost]
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

}
