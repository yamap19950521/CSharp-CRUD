using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test1.Models; 

namespace test1.Controllers
{ 
    public class HomeController : Controller
    {
       dbCustomEntities db = new dbCustomEntities();
        // GET: Home
        public ActionResult Index()
        {
            var customer = db.tCustomer.OrderBy(m=>m.fid).ToList();
            return View(customer);
        }

        public ActionResult Create() {
            return View(); //呈現新增的畫面
        }
        [HttpPost] //當按下submit按紐 會去 呼叫 HttpPost這種型態的create方法(下面這個方法)
        public ActionResult Create(tCustomer vCustomer) { 
            db.tCustomer.Add(vCustomer); //將使用者填的資料加入
            db.SaveChanges(); //執行SaveChanges() 方法 進行 異動資料庫
            return RedirectToAction("Index"); //導向 index動作
        }
        
        public ActionResult Delete(int fid) {
            var customer = db.tCustomer.Where(m=>m.fid == fid).FirstOrDefault(); //找出符合
            db.tCustomer.Remove(customer); //移出選取的那筆資料
            db.SaveChanges(); //進行 異動資料庫
            return RedirectToAction("Index"); //導向 index動作
        }

        
        public ActionResult Edit(int fid) { 
            var customer = db.tCustomer.Where(m => m.fid == fid).FirstOrDefault(); //找出符合
            return View(customer); //秀出這筆資料的view
        }

        [HttpPost] //當按下submit按紐 會去 呼叫 HttpPost這種型態的Edit方法(下面這個方法)
        public ActionResult Edit(tCustomer vCustomer) { 
            int fid = vCustomer.fid; 
            var customer = db.tCustomer.Where(m => m.fid == fid).FirstOrDefault();  //找出符合
            customer.fName= vCustomer.fName; //把修改後的fName塞回fName欄位
            customer.fPhone= vCustomer.fPhone;//把修改後的fPhone塞回fPhone欄位
            customer.fAddress= vCustomer.fAddress;//把修改後的fAddress塞回fAddress欄位
            db.SaveChanges(); //進行 異動資料庫
            return RedirectToAction("Index");//導向 index動作
        }

    }
}