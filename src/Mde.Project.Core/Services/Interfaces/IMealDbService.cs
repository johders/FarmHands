using Mde.Project.Core.Services.Models;

namespace Mde.Project.Core.Services.Interfaces
{
    public interface IMealDbService
    {
        Task<ResultModel<List<Meal>>> GetMealsByIngredientAsync(string ingredient);
    }
}
