namespace Technical.Business.Domain
{
    public class Response<T>
    {
        public Response(T model)
        {
            Model = model;
        }
        public Response(T model, bool isSuccess)
        {
            Model = model;
            IsSuccess = isSuccess;
        }

        public  Response<S> Handle<S>( S returnValue)
        {
            if (IsSuccess)
            {
                return new Response<S>(returnValue);
            }

            return new Response<S>(default(S));
        }
        public bool IsSuccess { get; set; }
        public T Model { get; set; }
    }
}
