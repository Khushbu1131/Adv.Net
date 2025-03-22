using DAL.Interface;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class RecipeRepo : Repo, IRepo<Recipe, int, Recipe>, IRecipeFeatures
    {
        public Recipe Create(Recipe obj)
        {
            db.Recipes.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public bool Delete(int id)
        {
            var ex = Get(id);
            db.Recipes.Remove(ex);
            return db.SaveChanges() > 0;
        }

        public Recipe Get(int id)
        {
            return db.Recipes.Find(id);
        }

        public List<Recipe> Get()
        {
            return db.Recipes.ToList();
        }
        public List<Recipe> SearchByTitleOrIngredients(string keyword)
        {
            return db.Recipes.Where(r => r.Title.Contains(keyword) || r.Ingredients.Contains(keyword)).ToList();
        }
        public Recipe Update(Recipe obj)
        {
            var ex = Get(obj.RecipeId);
            db.Entry(ex).CurrentValues.SetValues(obj);
            db.SaveChanges();
            return ex;
        }

        public Recipe UpdateRating(int recipeId, float rating)
        {
            var recipe = db.Recipes.Find(recipeId);
            if (recipe != null)
            {
                recipe.Rating = rating;
                db.SaveChanges();
            }
            return recipe;
        }
    }
}
