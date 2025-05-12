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
        //Detail
        public async Task<IActionResult> Details(int? id)
        {
            //檢查是否有員工id
            if(id == null || _context.Tablemytables1121645 == null)
            {
                var msgObject = new
                {
                    statuscode = StatusCodes.Status400BadRequest,
                    error = "無效的請求,必須提供Id編號!"
                };
                return new BadRequestObjectResult(msgObject);
            }
            //以id找尋員工資料
            var member = await _context.Tablemytables1121645.FirstOrDefaultAsync(m => m.Id == id);
            //如果沒有找到員工，回傳NotFound
            if (member == null)
            {
                return NotFound();//404
            }
            return View(member);
        }
        //Create新增資料功能

    }
}
