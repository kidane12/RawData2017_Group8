﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("/api/categories")]
    public class CategoriesController : Controller
    {
        public CategoriesController(IDataService iDataService) => _ds = iDataService;

        private readonly IDataService _ds;

        // GET: api/Categories
        [HttpGet(Name = "GetCategories")]
        public IActionResult Get()
        {
            return Ok(_ds.GetCategories());
        }

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult Get(int id)
        {
            return _ds.GetCategory(id) != null ? (IActionResult)Ok(_ds.GetCategory(id)) : NotFound(_ds.GetCategory(id));

        }

        // POST: api/Categories
        [HttpPost(Name = "AddCategory")]
        public void Post([FromBody]string value)
        {

        }
        
        // PUT: api/Categories/5
        [HttpPut("{id}", Name = "UpdateOrAddCategory")]
        public IActionResult Put(int id, [FromBody]string values)
        {
            if (values == null) return NotFound();
            var content = ((string, string)) JsonConvert.DeserializeObject(values);
            var res = _ds.UpdateCategory(id, content.Item1, content.Item2);
            return res ? (IActionResult) Ok() : NotFound();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}", Name = "DeleteCategory")]
        public IActionResult Delete(int id)
        {
            return _ds.DeleteCategory(id) ? (IActionResult) Ok(_ds.DeleteCategory(id)) : NotFound(_ds.DeleteCategory(id));
        }

        // helpers
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
