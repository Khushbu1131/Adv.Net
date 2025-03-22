using BLL.DTOs;
using DAL.Tables;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL.Services
{
    public class UserService
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<User, UserRecipeDTO>();
                cfg.CreateMap<Recipe, RecipeDTO>();
            })
            {

            };
            var mapper = new Mapper(config);
            return mapper;
        }
        public static void Create(UserDTO userDto)
        {
            var repo = DataAccessFactory.UserData(); 
            var user = GetMapper().Map<User>(userDto);
            repo.Create(user);
        }

        public static List<UserDTO> Get()
        {
            var repo = DataAccessFactory.UserData();
            return GetMapper().Map<List<UserDTO>>(repo.Get());
        }

        public static UserDTO Get(int id)
        {
            var repo = DataAccessFactory.UserData();
            var user = repo.Get(id);
            return GetMapper().Map<UserDTO>(user);
        }

        public static void Update(UserDTO userDto)
        {
            var repo = DataAccessFactory.UserData();
            var user = GetMapper().Map<User>(userDto);
            repo.Update(user);
        }

        public static void Delete(int id)
        {
            var repo = DataAccessFactory.UserData();
            repo.Delete(id);
        }
        public static UserRecipeDTO GetwithRecipes(int id)
        {
            var repo = DataAccessFactory.UserData();
            var user = repo.Get(id);
            var ret = GetMapper().Map<UserRecipeDTO>(user);
            return ret;

        }
    }
}
