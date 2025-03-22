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
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("api/Users/all")]
        public HttpResponseMessage Get()
        {
            var data = UserService.Get();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
        [HttpPost]
        [Route("api/User/create")]
        public HttpResponseMessage Create(UserDTO c)
        {
            UserService.Create(c);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/User/update")]
        public HttpResponseMessage Update(UserDTO c)
        {
            UserService.Update(c);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/User/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            UserService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/User/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var data = UserService.Get(id);
            if (data != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "User item not found");
            }
        }
        [HttpGet]
        [Route("api/user/{id}/recipes")]
        public HttpResponseMessage GetwithRecipes(int id)
        {
            var data = UserService.GetwithRecipes(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}
