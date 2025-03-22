using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntroRSP.Controllers
{
    public class RecipeController : ApiController
    {
        [HttpGet]
        [Route("api/Recipes/all")]
        public HttpResponseMessage Get()
        {
            var data = RecipeService.Get();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpPost]
        [Route("api/Recipe/create")]
        public HttpResponseMessage Create(RecipeDTO c)
        {
            RecipeService.Create(c);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/Recipe/update")]
        public HttpResponseMessage Update(RecipeDTO c)
        {
            RecipeService.Update(c);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/Recipe/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            RecipeService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/Recipe/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var data = RecipeService.Get(id);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Recipe item not found");
            }
        }

        [HttpGet]
        [Route("api/recipe/search/{keyword}")]
        public HttpResponseMessage Search(string keyword)
        {
            var data = RecipeService.SearchByKeyword(keyword);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        /* [HttpPut]
         [Route("api/Recipe/{id}/rate")]
         public HttpResponseMessage RateRecipe(int id, [FromBody] float?  rating)
         {
             if (!rating.HasValue || rating < 0 || rating > 5)
             {
                 return Request.CreateResponse(HttpStatusCode.BadRequest, "Rating must be between 0 and 5.");
             }

             RecipeService.UpdateRating(id, rating.Value);
             return Request.CreateResponse(HttpStatusCode.OK, "Rating updated successfully.");
         }*/
        [HttpPut]
        [Route("api/Recipe/{id}/rate")]
        public HttpResponseMessage RateRecipe(int id, [FromBody] float rating)
        {
            if (rating < 0 || rating > 5)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Rating must be between 0 and 5.");
            }

            // Call the service layer to update the rating
            RecipeService.UpdateRating(id, rating);
            return Request.CreateResponse(HttpStatusCode.OK, "Rating updated successfully.");
        }
        [HttpGet]
        [Route("api/recipes/user/{userId}/share")]
        public HttpResponseMessage GetShareableRecipeAchievement(int userId)
        {
            var message = RecipeService.GetShareableRecipeAchievement(userId);
            return Request.CreateResponse(HttpStatusCode.OK, new { shareableMessage = message });
        }
        [HttpGet]
        [Route("api/recipe/export/{id}")]
        public HttpResponseMessage ExportRecipeToPDF(int id)
        {
            var pdfBytes = RecipeService.ExportRecipeToPDF(id);
            if (pdfBytes != null)
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(pdfBytes)
                };
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Recipe.pdf"
                };
                return result;
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Recipe not found.");
        }

    }
}

