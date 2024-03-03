using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Identity;

namespace BusinessLogicLayer.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Product, Product>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ApplicationUser, RegisterViewModel>().ReverseMap();
        }
    }
}
