using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using AutoMapper;
using WebApi.Intro.Models;
// ReSharper disable MemberCanBePrivate.Global

namespace WebApi.Intro.Controllers
{
   [SuppressMessage("ReSharper", "UnusedMember.Global")]
   public class ProductController : ApiController
   {
      private readonly Repository _repo = new Repository();

      // GET api/<controller>
      public IEnumerable<ProductView> Get()
      {
         return _repo.Products.Select(Mapper.Map<ProductView>);
      }

      // GET api/<controller>?categoryFilter=filterValue
      public IEnumerable<ProductView> Get([Form] string categoryFilter)
      {
         return categoryFilter == null || categoryFilter == "All"
            ? Get()
            : _repo.Products.Where(product => product.Category == categoryFilter).Select(Mapper.Map<ProductView>);
      }

      // GET api/<controller>/5
      public ProductView Get(int id)
      {
         return Mapper.Map<ProductView>(_repo.Products.FirstOrDefault(product => product.ProductId == id));
      }      

      // POST api/<controller>
      public void Post([FromBody] ProductView value)
      {
         _repo.Add(Mapper.Map<Product>(value));
      }

      // PUT api/<controller>/5
      public HttpResponseMessage Put(int id, [FromBody] ProductView value)
      {
         HttpResponseMessage responseMessage;
         if (ModelState.IsValid)
         {
            var productToUpdate = _repo.Products.FirstOrDefault(product => product.ProductId == id);
            if (productToUpdate != null)
            {
               _repo.Save(Mapper.Map<Product>(value));
            }

            responseMessage = Request.CreateResponse(HttpStatusCode.OK);
         }
         else
         {
            var errors= (ModelState.SelectMany(state => state.Value.Errors, (state, error) => error.ErrorMessage)).ToList();
            responseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, errors);
         }

         return responseMessage;
      }

      // DELETE api/<controller>/5
      public void Delete(int id)
      {
         var productToDelete = _repo.Products.FirstOrDefault(product => product.ProductId == id);
         if (productToDelete != null)
         {
            _repo.Delete(productToDelete);
         }
      }
   }
}