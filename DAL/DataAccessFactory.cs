using DAL.Interface;
using DAL.Repos;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccessFactory
    {
        public static IRepo<User, int, User> UserData()
        {
            return new UserRepo();
        }
        public static IRepo<Recipe, int, Recipe> RecipeData()
        {
            return new RecipeRepo();
        }
        public static IRecipeFeatures RecipeFeatures()
        {
            return new RecipeRepo();
        }
    }
}
