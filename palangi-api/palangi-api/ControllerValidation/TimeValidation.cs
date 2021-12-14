using palangi_api.Dto;
using Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace palangi_api.ControllerValidation
{
    public class TimeValidation : IExtraValidationItem
    {
        private readonly SellTimeConfig sellTimeConfig;

        public TimeValidation(SellTimeConfig sellTimeConfig)
        {
            this.sellTimeConfig = sellTimeConfig;
        }
        public List<string> Keys => new List<string> { "order" };

        public async Task<Message> Validate()
        {
            //get from main server with di
            TimeSpan time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            if (time<sellTimeConfig.Start || time>sellTimeConfig.End)
            
                return new Message { ActResult = ActResult.NotTimeRange, Body = "درخواست ثبت سفارش در زمان مناسب انجام نگرفته است." };
            
            return new Message { ActResult=ActResult.Successful};
        }
    } public class TimeValidation1 : IExtraValidationItem
    {
        private readonly SellTimeConfig sellTimeConfig;

        public TimeValidation1(SellTimeConfig sellTimeConfig)
        {
            this.sellTimeConfig = sellTimeConfig;
        }
        public List<string> Keys => new List<string> { "order" };

        public async Task<Message> Validate()
        {
            //get from main server with di
            TimeSpan time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            if (time<sellTimeConfig.Start || time>sellTimeConfig.End)
            
                return new Message { ActResult = ActResult.NotTimeRange, Body = "درخواست ثبت سفارش در زمان مناسب انجام نگرفته است." };
            
            return new Message { ActResult=ActResult.Successful};
        }
    }
}
