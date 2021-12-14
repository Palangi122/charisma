using Shared;
using System.Threading.Tasks;

namespace palangi_api.ControllerValidation
{
    //public interface IControllerValidation
    //{
    //    Message Validate();
    //}
    public interface IModelValidationItem<TValue>
    {
        public string Key { get; }
        Task<Message> Validate(TValue value);
    }

}
