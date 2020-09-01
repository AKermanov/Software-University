using AutoMapper;
using ProductShop.DTO.Product;
using ProductShop.DTO.User;
using ProductShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            //p5
            this.CreateMap<Product, ListProductsInRange>()
                .ForMember(x => x.SellerName, y => y.MapFrom(x => x.Seller.FirstName + ' ' + x.Seller.LastName));

            //p6
            this.CreateMap<Product, UserSoldProductDTO>()
                .ForMember(x => x.BuyerFirstName, y => y.MapFrom(x => x.Buyer.FirstName))
                .ForMember(x => x.BuyerLastName, y => y.MapFrom(x => x.Buyer.LastName));

            this.CreateMap<User, UserWithSoldProductsDTO>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(x => x.ProductsSold.Where(p => p.Buyer != null)));

            //ImportUsersDTO
            this.CreateMap<UserImprotDTO, User>();
        }
    }
}
