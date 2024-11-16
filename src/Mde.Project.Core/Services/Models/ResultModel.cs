namespace Mde.Project.Core.Services.Models
{
    public class ResultModel<T> : BaseResultModel
    {
        public T Data { get; set; }
    }
}
