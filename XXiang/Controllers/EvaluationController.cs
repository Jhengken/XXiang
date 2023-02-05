using Microsoft.AspNetCore.Mvc;
using XXiang.Models;

namespace XXiang.Controllers
{
    public class EvaluationController : Controller
    {
        private readonly dbXContext _conetxt;
        public EvaluationController(dbXContext conetxt)
        {
            _conetxt = conetxt;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
