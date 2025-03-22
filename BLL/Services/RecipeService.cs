using AutoMapper;
using BLL.DTOs;
using DAL.Tables;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BLL.Services
{
    public class RecipeService
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Recipe, RecipeDTO>();
                cfg.CreateMap<RecipeDTO, Recipe>();
            });
            var mapper = new Mapper(config);
            return mapper;
        }
        public static void Create(RecipeDTO RecipeDto)
        {
            var repo = DataAccessFactory.RecipeData();
            var Recipe = GetMapper().Map<Recipe>(RecipeDto);
            repo.Create(Recipe);
        }

        public static List<RecipeDTO> Get()
        {
            var repo = DataAccessFactory.RecipeData();
            return GetMapper().Map<List<RecipeDTO>>(repo.Get());
        }

        public static RecipeDTO Get(int id)
        {
            var repo = DataAccessFactory.RecipeData();
            var Recipe = repo.Get(id);
            return GetMapper().Map<RecipeDTO>(Recipe);
        }

        public static void Update(RecipeDTO RecipeDto)
        {
            var repo = DataAccessFactory.RecipeData();
            var Recipe = GetMapper().Map<Recipe>(RecipeDto);
            repo.Update(Recipe);
        }

        public static void Delete(int id)
        {
            var repo = DataAccessFactory.RecipeData();
            repo.Delete(id);
        }
        public static List<RecipeDTO> SearchByKeyword(string keyword)
        {
            var repo = DataAccessFactory.RecipeFeatures();
            return GetMapper().Map<List<RecipeDTO>>(repo.SearchByTitleOrIngredients(keyword));
        }
        public static void UpdateRating(int recipeId, float rating)
        {
            var repo = DataAccessFactory.RecipeData();
            var recipe = repo.Get(recipeId);

            if (recipe != null)
            {
                recipe.Rating = rating;
                repo.Update(recipe);
            }
        }
        public static string GetShareableRecipeAchievement(int userId)
        {
            // Access the Recipe repository
            var repo = DataAccessFactory.RecipeData();
            var data = repo.Get().Where(r => r.UserId == userId).ToList();

            // Check if the user has contributed any recipes
            if (data.Count == 0)
            {
                return "No recipes shared yet. Start your culinary journey today and share your favorite dishes! 🍳👨‍🍳👩‍🍳";
            }

            // Get the most highly-rated recipe
            var topRecipe = data.OrderByDescending(r => r.Rating).First();

            // Prepare the shareable message
            var message = $"🍴 I've been crafting delicious recipes! Check out my culinary achievements: 🍳🍰🔥\n" +
                $"✅ Total Recipes Shared: {data.Count}\n" +
                $"🌟 Top Recipe: \"{topRecipe.Title}\" with a rating of {topRecipe.Rating}/5.0\n\n" +
                "🍽️ Cooking is an art, and sharing it is love! Join me in this flavorful journey! 💖\n\n" +
                "#RecipeSharing #CulinaryAdventures #DeliciousJourney";

            return message;
        }
        public static byte[] ExportRecipeToPDF(int recipeId)
        {
            var recipe = Get(recipeId);
            if (recipe != null)
            {
                // Example using iTextSharp
                using (var ms = new MemoryStream())
                {
                    using (var document = new iTextSharp.text.Document())
                    {
                        var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms);
                        document.Open();

                        document.Add(new iTextSharp.text.Paragraph($"Recipe: {recipe.Title}"));
                        document.Add(new iTextSharp.text.Paragraph($"Ingredients: {recipe.Ingredients}"));
                        document.Add(new iTextSharp.text.Paragraph($"Instructions: {recipe.Instructions}"));
                        //document.Add(new iTextSharp.text.Paragraph($"Rating: {recipe.Rating:F2}/5"));

                        document.Close();
                    }
                    return ms.ToArray(); // Return PDF as byte array
                }
            }
            return null;
        }
    }
}
