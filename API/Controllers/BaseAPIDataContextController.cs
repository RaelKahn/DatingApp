using API.Data;

namespace API.Controllers
{
    public class BaseAPIDataContextController: BaseAPIController
    {
        public DataContext Context;

        public BaseAPIDataContextController(DataContext context)
        {
            Context = context;
        }
    }
}