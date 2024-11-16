namespace Mde.Project.Core.Services.Models
{
    public class BaseResultModel
    {
        public bool IsSuccess => !Errors.Any();
        public List<string> Errors { get; set; } = new();
    }
}
