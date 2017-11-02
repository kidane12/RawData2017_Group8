﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WSL.Controllers
{
    [Produces("application/json")]
    [Route("api/Post_tags")]
    public class Post_tagsController : Controller
    {
        private readonly IDataService _ds;
        public Post_tagsController(IDataService iDataService)
        {
            _ds = iDataService;
        }
    }
}