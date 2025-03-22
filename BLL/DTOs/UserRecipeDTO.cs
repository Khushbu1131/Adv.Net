using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class UserRecipeDTO
    {
        public List<RecipeDTO> Recipes { get; set; }
        public UserRecipeDTO()
        {
            Recipes = new List<RecipeDTO>();
        }
    }
}
