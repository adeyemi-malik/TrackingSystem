namespace Mints.Models.ApiModels
{
    public abstract class BaseModel
    {
        public string Status { get; set; }

        public string Message { get; set; }

        public int Code { get; set; }

    }

    public class ErrorModel: BaseModel
    {
        public dynamic Data { get; set; }
    }
}