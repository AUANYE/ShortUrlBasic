using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContextDatabase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortsLink.Core.Interface;
using ShortsLink.Core.Model;
using ShortsLink.Core.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShortsLinkGenerate.Controllers
{
    public class GenerateController : Controller
    {
        private readonly ShortsLinkContext _context;
        private readonly IGenerateRepository _generateRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenerateController(ShortsLinkContext context, IGenerateRepository generateRepository, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _generateRepository = generateRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: /<controller>/
        public IActionResult GenerateForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateFormPost([FromBody]PostURLModel postURLModel)
        {
            postURLModel.HostName = "http://" + _httpContextAccessor.HttpContext.Request.Host.Value + "/";
            var data = _generateRepository.GenShortURL(postURLModel);
            return Ok(data);
        }

        public IActionResult GoToURL(string shortUrl)
        {
            var data = _generateRepository.GetOriginalUrl(shortUrl);
            return Redirect(data);
        }
    }
}