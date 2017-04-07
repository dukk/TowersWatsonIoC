// Copyright 2017 Justin Long. All rights reserved.
// Licensed under the MIT License.

using MvcApplicationDemo.Services;
using System.Web.Mvc;

namespace MvcApplicationDemo.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IServerVariablesProvider requestDataProvider)
        {
            this.RequestDataProvider = requestDataProvider;
        }

        protected IServerVariablesProvider RequestDataProvider { get; private set; }

        // GET: Home
        [Route("")]
        public ActionResult Index()
        {
            return View(this.RequestDataProvider.GetRequestData(this.Request));
        }
    }
}