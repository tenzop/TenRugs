using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TenRugs.Models;
using TenRugs.Services;
using WikiWebStarter.Web.Models.Responses;

namespace TenRugs.Controllers.Api
{
    
   [RoutePrefix("api/rugs")]
    public class RugsApiController : ApiController
    {
        // GET api/<controller>
        [Route, HttpGet]
        public HttpResponseMessage Get()
        {
            ItemsResponse<Rug> response = new ItemsResponse<Rug>();
            try
            {
                RugsService svc = new RugsService();
                List<Rug> rug = svc.SelectAll();
                response.Items = rug;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [Route, HttpPost]
        public HttpResponseMessage Post(Rug model)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            ItemResponse<int> response = new ItemResponse<int>();
            try
            {
                RugsService svc = new RugsService();
                response.Item = svc.Insert(model);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route, HttpPut]
        public HttpResponseMessage Put(Rug model)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ModelState);

            try
            {
                RugsService svc = new RugsService();
                svc.Update(model);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
        }

        [Route, HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                RugsService svc = new RugsService();
                svc.Delete(id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
        }
    }
}
