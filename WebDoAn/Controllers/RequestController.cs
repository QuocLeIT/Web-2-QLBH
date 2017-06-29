using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoAn.Filters;
using WebDoAn.Helpers;
using WebDoAn.Models;

namespace WebDoAn.Controllers
{
    public class RequestController : Controller
    {
        //
        // GET: /Request/
        public ActionResult Index()
        {
            var rq = CurrentContext.GetReQuest();

            return View(rq.reQuestSellItem);
        }

        //
        // POST: /Cart/Add2
        [HttpPost]
        public ActionResult AddSell(int? id)
        {
            if (id.HasValue == false)
            {
                return RedirectToAction("Profile", "Account");
            }
            else 
            {
                using (var ctx = new QLBHEntities())
                {
                    var pro = ctx.Users
                        .Where(p => p.f_ID == id)
                        .FirstOrDefault();

                    if (pro != null)
                    {
                        CurrentContext.GetReQuest().AddSellItem(pro);
                        //ViewBag.ErrorMsg = "Đang chờ duyệt yêu cầu";
                    }                  
                }

                return RedirectToAction("Profile", "Account", new { id = CurrentContext.GetCurUser().f_ID });
            }           
        }

        [HttpPost]
        public ActionResult DuyetSell(int? userid)
        {
            if (userid.HasValue == false)
            {

                return RedirectToAction("Index", "ReQuest");
            }
            else
            {
                using (var ctx = new QLBHEntities())
                {
                    User model = ctx.Users
                        .Where(c => c.f_ID == userid)
                        .FirstOrDefault();

                    if (model != null)
                    {
                        model.f_Seller = 1;
                        ctx.SaveChanges();
                        RemoveCheckSell(model.f_ID);
                    }
                }               
                return RedirectToAction("Index", "ReQuest");
            }          
        }

        //
        // POST: /ReQuest/Remove
        [HttpPost]
        public ActionResult RemoveCheckSell(int proId)
        {

            CurrentContext.GetReQuest().RemoveSellItem(proId);
            return RedirectToAction("Index", "ReQuest");

        }
    }
}