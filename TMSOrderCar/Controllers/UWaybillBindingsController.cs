using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TMSOrderCar.Models;

namespace TMSOrderCar.Controllers
{
    public class UWaybillBindingsController : Controller
    {
        private readonly ZH_LocationContext _context;

        public UWaybillBindingsController(ZH_LocationContext context)
        {
            _context = context;
        }

        //原始默认方法
        //GET: UWaybillBindings
        //public async Task<IActionResult> Index(int id=1)
        //{
        //    var artiles = _context.UWaybillBinding;
        //    var pageOption = new MoPagerOption
        //    {
        //        CurrentPage = id,
        //        PageSize = 2,
        //        Total = await artiles.CountAsync(),
        //        RouteUrl = "/UWaybillBindings/Index"
        //    };

        //    //分页参数
        //    ViewBag.PagerOption = pageOption;

        //    //数据
        //    return View(await artiles.OrderByDescending(b => b.OrderNo).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToListAsync());

            //return View(await _context.UWaybillBinding.ToListAsync());
        //}

        /// <summary>
        /// 单一查询方法
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        //public ActionResult Index(string searchString)
        //{

        //    var _UWaybillBinding = from m in _context.UWaybillBinding
        //                select m;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {

        //        _UWaybillBinding = _UWaybillBinding.Where(s => s.Carnumber.Contains(searchString));

        //    }
        //    return View(_UWaybillBinding);
        //}


        public async Task<IActionResult>  Index(string Carlist, string SearchString, int id = 1)
        {
            
            var cateLst = new List<string>();
            var cateQry = from d in _context.UWaybillBinding

                          orderby d.Carnumber

                          select d.Carnumber;

            cateLst.AddRange(cateQry.Distinct());

            ViewBag.Carlist = new SelectList(cateLst);

            
            
            ViewBag.SearchString = SearchString;

            var _UWaybillBinding = from m in  _context.UWaybillBinding

                        select m;

            if (!String.IsNullOrEmpty(SearchString))
            {

                _UWaybillBinding = _UWaybillBinding.Where(s => s.OrderNo.Contains(SearchString));
                

            }

            if (String.IsNullOrEmpty(Carlist))
            {
                var pageOption = new MoPagerOption
                {
                    CurrentPage = id,
                    PageSize = 15,
                    Total = await _UWaybillBinding.CountAsync(),
                    RouteUrl = "/UWaybillBindings/index",
                    SelectName = "Carlist",
                    SelectValue = Carlist,
                    TextName = "SearchString",
                    TextValue = SearchString
                };

                //分页参数
                ViewBag.PagerOption = pageOption;

                //数据
                return View(await _UWaybillBinding.OrderByDescending(b => b.OrderNo).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToListAsync());
                //return View(_UWaybillBinding);
            }
            else

            {
                _UWaybillBinding = _UWaybillBinding.Where(x => x.Carnumber == Carlist);
                var pageOption = new MoPagerOption
                {
                    CurrentPage = id,
                    PageSize = 15,
                    Total = await _UWaybillBinding.CountAsync(),
                    RouteUrl = "/UWaybillBindings/index",
                    SelectName= "Carlist",
                    SelectValue = Carlist,
                    TextName = "SearchString",
                    TextValue = SearchString
                };

                //分页参数
                ViewBag.PagerOption = pageOption;

                //数据
                return View(await _UWaybillBinding.OrderByDescending(b => b.OrderNo).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToListAsync());
                //return View();
            }
        }



        // GET: UWaybillBindings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uWaybillBinding = await _context.UWaybillBinding
                .SingleOrDefaultAsync(m => m.DocEntry == id);
            if (uWaybillBinding == null)
            {
                return NotFound();
            }

            return View(uWaybillBinding);
        }

        // GET: UWaybillBindings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UWaybillBindings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderNo,Carnumber,ExpectDateLoad,ActualDateLoad,CreateUser,CreateDate,UpdateUser,UpdateDate")] UWaybillBinding uWaybillBinding)
        {
            if (ModelState.IsValid)
            {
                uWaybillBinding.DocEntry = uWaybillBinding.OrderNo+ uWaybillBinding.Carnumber;
                uWaybillBinding.CreateDate = DateTime.Now;
                uWaybillBinding.CreateUser = HttpContext.Request.Cookies["user"];
                uWaybillBinding.UpdateDate = DateTime.Now;
                uWaybillBinding.UpdateUser = HttpContext.Request.Cookies["user"];
                _context.Add(uWaybillBinding);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uWaybillBinding);
        }

        // GET: UWaybillBindings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uWaybillBinding = await _context.UWaybillBinding.SingleOrDefaultAsync(m => m.DocEntry == id);
            if (uWaybillBinding == null)
            {
                return NotFound();
            }
            return View(uWaybillBinding);
        }

        // POST: UWaybillBindings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DocEntry,OrderNo,Carnumber,ExpectDateLoad,ActualDateLoad,CreateUser,CreateDate")] UWaybillBinding uWaybillBinding)
        {
            if (id != uWaybillBinding.DocEntry)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    uWaybillBinding.UpdateDate = DateTime.Now;
                    uWaybillBinding.UpdateUser = HttpContext.Request.Cookies["user"];
                    _context.Update(uWaybillBinding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UWaybillBindingExists(uWaybillBinding.DocEntry))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(uWaybillBinding);
        }

        // GET: UWaybillBindings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uWaybillBinding = await _context.UWaybillBinding
                .SingleOrDefaultAsync(m => m.DocEntry == id);
            if (uWaybillBinding == null)
            {
                return NotFound();
            }

            return View(uWaybillBinding);
        }

        // POST: UWaybillBindings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var uWaybillBinding = await _context.UWaybillBinding.SingleOrDefaultAsync(m => m.DocEntry == id);
            _context.UWaybillBinding.Remove(uWaybillBinding);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UWaybillBindingExists(string id)
        {
            return _context.UWaybillBinding.Any(e => e.DocEntry == id);
        }
    }
}
