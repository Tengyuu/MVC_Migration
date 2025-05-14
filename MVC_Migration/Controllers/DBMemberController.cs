using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Migration.Data;
using System.Data;
using System.Data.SqlClient;
using MVC_Migration.Models;
using X.PagedList;
using X.PagedList.Extensions;
using X.PagedList.Mvc;
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
            if (id == null || _context.Tablemytables1121645 == null)
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
        //Get
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age")]Members members)
        {
            if (ModelState.IsValid)
            {
                //將實體加入DbSet
                _context.Tablemytables1121645.Add(members);
                //將資料異動儲存到資料庫
                await _context.SaveChangesAsync();
                //導向至Index
                return RedirectToAction(nameof(Index));
            }
            return View(members);
        }
        //Edit
        //Get
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tablemytables1121645 == null)
            {
                return NotFound();
            }
            var member = await _context.Tablemytables1121645.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age")]Members members)
        {
            if(id != members.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Tablemytables1121645.Update(members);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExist(members.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw new Exception("錯誤!!嘻嘻");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(members);
        }
        private bool MemberExist(int id)
        {
            //檢查使否有資料Id等於傳入的id
            return _context.Tablemytables1121645.Any(m => m.Id == id);
        }
        //Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }
            var member = await _context.Tablemytables1121645.FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(_context.Tablemytables1121645 == null)
            {
                return Problem("Entity set 'CmsContext.Database' is null");
            }
            //以Id搜尋實體
            var member = await _context.Tablemytables1121645.FindAsync(id);
            //刪除
            if (member != null)
            {
                _context.Tablemytables1121645.Remove(member);
                await _context.SaveChangesAsync();
            }       
            return RedirectToAction(nameof(Index));
        }
        //分頁
        public async Task<IActionResult> Index2(int? page = 1)
        {
            //每頁幾筆
            const int pageSize = 3;
            //處理頁數
            ViewBag.userModel = GetPagedProcess(page, pageSize);
            //填入頁面資料
            return View(await _context.Tablemytables1121645.Skip<Members>(pageSize * ((page ?? 1) - 1)).Take(pageSize).ToArrayAsync());
        }
        protected IPagedList<Members> GetPagedProcess(int? page,int pageSize)
        {
            //過濾從client傳送過來有問題頁數
            if (page.HasValue && page < 1) 
                return null;
            //從資料庫取得頁數
            var listUnpaged = GetStufFromDatabase();
            IPagedList<Members> pagelist = listUnpaged.ToPagedList(page ?? 1, pageSize);
            //過濾從client傳送過來有問題頁數，包含判斷有問題的頁數邏輯
            if (pagelist.PageNumber != 1 && page.HasValue && page > pagelist.PageCount)
                return null;
           return pagelist;
        }
        protected IQueryable<Members> GetStufFromDatabase()
        {
            return _context.Tablemytables1121645;
        }

        //查詢
        //[HttpGet]
        //public IActionResult InputQuery()
        //{
        //    return View();
        //}
        //[HttpGet]
        public IActionResult Query()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Query(int Id,int Age)
        {
            var users = await (from p in _context.Tablemytables1121645
                               where p.Id == Id && p.Age == Age
                               //where p.Id >= Id
                               orderby p.Name
                               select p).ToArrayAsync();
            return View(users);
        }
    }
}
