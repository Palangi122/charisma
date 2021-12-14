using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace palangi_api.Utilities.Helpers
{
    public class ResponseFactory
    {
        public static async Task<Response<TResult>> Create<TResult>(ActResult actResult, TResult result=null,  Exception exception=null, List<Message> messageError = null) where TResult:class
        {
            return await Task.Run(() =>
            {
                var response = new Response<TResult>();
                response.ActionResult = actResult;
                response.Result = result;
                response.Exception = exception;
                response.Messages = messageError;
                return response;
            });
        } public static async Task<Response> Create(ActResult actResult,  Exception exception=null, List<Message> messageError = null) 
        {
            return await Task.Run(() =>
            {
                var response = new Response();
                response.ActionResult = actResult;
                response.Exception = exception;
                response.Messages = messageError;
                return response;
            });
        }
    }
}
