namespace FoodUserAuth.WebApi.Contracts
{
    public class GenericResponse<TModel> : ResponseBase
    {
        public TModel Data { get; set; }
    }
}
