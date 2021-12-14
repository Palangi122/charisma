using AutoMapper;
using DomainModel.Customers;
using DomainModel.Customers.dto;
using DomainModel.Others;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace palangi_api.Utilities
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(des => des.Addresses, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<Address>>(src.Addresses)));
            CreateMap<CustomerDto, Customer>()
                .ForMember(des => des.Addresses, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Addresses)));
        }
    }
}
