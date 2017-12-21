using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestingList.Domain;
using TestingList.Interfaces;
using TestingList.Models.Requests;
using TestingList.Models.Responses;
using TestingList.Services;



namespace TestingList.Controllers.Api
{
    [RoutePrefix("api/lists")]

    public class ListsApiController : ApiController
    {
        readonly IListService listService;

        public ListsApiController(IListService listService)
        {
            this.listService = listService;
        }

        [Route, HttpGet]
        public HttpResponseMessage GetAll()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, ModelState);
            }

            ItemsResponse<ToDoListDomain> Response = new ItemsResponse<ToDoListDomain>();
            Response.Items = listService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, Response);
        }

        [Route(), HttpPost]
        public HttpResponseMessage Insert(ToDoListDomain model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, ModelState);
            }
            else
            {
                ItemResponse<int> response = new ItemResponse<int>();
                response.Item = listService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(ToDoListDomain model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                listService.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("soft"), HttpPost]
        public HttpResponseMessage Delete(DeleteIdsRequest model)
        {
            var response = new ItemResponse<Dictionary<int, DeleteIdsRequest>>();
            response.Item = listService.SoftDelete(model);
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [Route("hard"), HttpPost]
        public HttpResponseMessage HardDelete(DeleteIdsRequest model)
        {
            var response = new ItemResponse<Dictionary<int, DeleteIdsRequest>>();
            response.Item = listService.HardDelete(model);
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
    }
}
