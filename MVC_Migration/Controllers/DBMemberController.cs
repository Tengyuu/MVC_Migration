using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Migration.Data;
using System.Data;
using System.Data.SqlClient;
namespace MVC_Migration.Controllers
{
    public class DBMemberController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly CmsContext _context;
        public DBMemberController(CmsContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _context.Tablemytables1121645.ToListAsync();
            return View(users);
        }
    }
}
