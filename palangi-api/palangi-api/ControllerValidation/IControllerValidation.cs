using Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace palangi_api.ControllerValidation
{
    public interface IControllerValidation<TValue>
    {
        public string Key { get;  }
        Task<List<Message>> Validate(TValue value);
    }

}
