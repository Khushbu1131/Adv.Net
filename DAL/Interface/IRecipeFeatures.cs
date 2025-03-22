using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IRecipeFeatures
    {
        List<Recipe> SearchByTitleOrIngredients(string keyword);
    }
}
