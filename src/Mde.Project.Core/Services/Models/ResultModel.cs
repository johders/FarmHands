namespace Mde.Project.Core.Services.Models
{
    public class ResultModel<T> : BaseResultModel
    {
        public IEnumerable<T> Data { get; set; }
    }
}
