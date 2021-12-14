using Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace palangi_api.ControllerValidation
{
    public interface IExtraValidationItem
    {
        public List<string> Keys { get; }//when key is * that invoke for all request
        Task<Message> Validate();
    }

}
