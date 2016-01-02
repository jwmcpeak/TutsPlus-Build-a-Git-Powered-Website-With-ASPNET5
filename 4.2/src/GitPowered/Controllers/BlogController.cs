﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Hosting;
using GitPowered.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GitPowered.Controllers
{
    public class BlogController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly PostRepository _posts;
        public BlogController(IHostingEnvironment environment)
        {
            _environment = environment;
            _posts = new PostRepository(_environment.WebRootPath);
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var posts = _posts.GetAll().OrderByDescending(p => p.PublishDate);
            return View(posts);
        }
    }
}
