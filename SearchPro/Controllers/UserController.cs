using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SearchPro.Models;

namespace SearchPro.Controllers
{

    public class UserController : Controller
    {
        private GymContext db = new GymContext();
        // GET: User
        public ActionResult Index(string searchadr, string searchname, string ortOrder, string searchtime)
        {
            var gym = from t in db.Gyms
                      select t;

            if (!String.IsNullOrEmpty(searchname))
            {
                var query = from s in db.Gyms
                            where s.GymName == searchname && s.GymAddress.Contains(searchadr) // may || will be better ?
                            select s;

                return View(query.ToList());
            }
            else if (!String.IsNullOrEmpty(searchtime))
            {
                var query = from s in db.Gyms
                            where s.GymOpenTime == searchtime && s.GymAddress.Contains(searchadr) // may || will be better ?
                            select s;

                return View(query.ToList());
            }
            else if (!String.IsNullOrEmpty(searchadr))
            {
                var query = from s in db.Gyms
                            where s.GymAddress.Contains(searchadr)
                            select s;

                return View(query.ToList());
            }

            return View(gym.ToList());

        }

        // GET: Gyms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gyms gyms = db.Gyms.Find(id);
            if (gyms == null)
            {
                return HttpNotFound();
            }
            return View(gyms);
        }

        public ActionResult getAddress()
        {
            List<String> address = new List<string>();
            var gym = from s in db.Gyms select s;
            foreach (var item in gym)
            {
                if (item.GymAddress != null)
                {
                    address.Add(item.GymAddress);
                }
            }

            return Json(address, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getPriceAvg()
        {
            double avgRishonLeTsiyon, avgTelAviv, avgHerzeliya;
            int lowgRishonLeTsiyon=0, midgRishonLeTsiyon=0, highgRishonLeTsiyon=0;
            int lowTelAviv=0, midTelAviv=0, highTelAviv=0;
            int lowHerzeliya=0, midHerzeliya=0, highHerzeliya=0;
            int countRishonLeTsiyon = 0;
            int sumRishonLeTsiyon=0;
            int countTelAviv = 0;
            int sumTelAviv = 0;
            int countHerzeliya = 0;
            int sumHerzeliya = 0;
            

            List<Graph> price = new List<Graph>();
            var gym = from s in db.Gyms select s;
            foreach (var item in gym)
            {
                if (item.GymAddress.Contains("Rishon LeTsiyon"))
                {
                    countRishonLeTsiyon++;
                    sumRishonLeTsiyon += item.GymPrice;
                }
                if (item.GymAddress.Contains("Tel Aviv"))
                {
                    countTelAviv++;
                    sumTelAviv += item.GymPrice;
                }
                if (item.GymAddress.Contains("Herzliya"))
               {
                   countHerzeliya++;
                   sumHerzeliya += item.GymPrice;
               }
            }
            avgRishonLeTsiyon = sumRishonLeTsiyon / countRishonLeTsiyon;
            avgTelAviv = sumTelAviv / countTelAviv;
            avgHerzeliya = sumHerzeliya / countHerzeliya;

            foreach (var item in gym)
            {
                if (item.GymAddress.Contains("Rishon LeTsiyon") && item.GymPrice > avgRishonLeTsiyon)
                 {
                     highgRishonLeTsiyon++;
                 }
                if (item.GymAddress.Contains("Rishon LeTsiyon") && item.GymPrice < avgRishonLeTsiyon)
                 {
                     lowgRishonLeTsiyon++;
                 }
                if (item.GymAddress.Contains("Rishon LeTsiyon") && item.GymPrice == avgRishonLeTsiyon)
                 {
                     midgRishonLeTsiyon++;
                 }
                /*-------------------------------Tel Aviv--------------------------------------*/
                if (item.GymAddress.Contains("Tel Aviv") && item.GymPrice > avgTelAviv)
                {
                    highTelAviv++;
                }
                if (item.GymAddress.Contains("Tel Aviv") && item.GymPrice < avgTelAviv)
                {
                    lowTelAviv++;
                }
                if (item.GymAddress.Contains("Tel Aviv") && item.GymPrice == avgTelAviv)
                {
                    midTelAviv++;
                }
                /*-------------------------------Herzliya--------------------------------------*/
                if (item.GymAddress.Contains("Herzliya") && item.GymPrice > avgHerzeliya)
                {
                    highHerzeliya++;
                }
                if (item.GymAddress.Contains("Herzliya") && item.GymPrice < avgHerzeliya)
                {
                    lowHerzeliya++;
                }
                if (item.GymAddress.Contains("Herzliya") && item.GymPrice == avgHerzeliya)
                {
                    midHerzeliya++;
                }
            }
            Graph rishon = new Graph("Rishon Lezion", new Freq(lowgRishonLeTsiyon, midgRishonLeTsiyon, highgRishonLeTsiyon));
            Graph telAviv = new Graph("Tel Aviv", new Freq(lowTelAviv, midTelAviv, highTelAviv));
            Graph Herzliya = new Graph("Herzliya", new Freq(lowHerzeliya, midHerzeliya, highHerzeliya));
            price.Add(rishon);
            price.Add(telAviv);
            price.Add(Herzliya);
            return Json(price, JsonRequestBehavior.AllowGet);
        }

         public ActionResult avg()
        {
            double sum=0,counter=0,avg=0;
            int lowgRishonLeTsiyon=0, midgRishonLeTsiyon=0, highgRishonLeTsiyon=0;
            int lowTelAviv=0, midTelAviv=0, highTelAviv=0;
            int lowHerzeliya=0, midHerzeliya=0, highHerzeliya=0;

            List<Graph> priceavg = new List<Graph>();
            var gym = from s in db.Gyms select s;
            foreach (var item in gym)
            {
                sum =+ item.GymPrice; 
                counter++;
            }
            avg = sum / counter;

            foreach (var item in gym)
            {
                if (item.GymAddress.Contains("Rishon LeTsiyon") && item.GymPrice > avg)
                 {
                     highgRishonLeTsiyon++;
                 }
                if (item.GymAddress.Contains("Rishon LeTsiyon") && item.GymPrice < avg)
                 {
                     lowgRishonLeTsiyon++;
                 }
                if (item.GymAddress.Contains("Rishon LeTsiyon") && item.GymPrice == avg)
                 {
                     midgRishonLeTsiyon++;
                 }
                /*-------------------------------Tel Aviv--------------------------------------*/
                if (item.GymAddress.Contains("Tel Aviv") && item.GymPrice > avg)
                {
                    highTelAviv++;
                }
                if (item.GymAddress.Contains("Tel Aviv") && item.GymPrice < avg)
                {
                    lowTelAviv++;
                }
                if (item.GymAddress.Contains("Tel Aviv") && item.GymPrice == avg)
                {
                    midTelAviv++;
                }
                /*-------------------------------Herzliya--------------------------------------*/
                if (item.GymAddress.Contains("Herzliya") && item.GymPrice > avg)
                {
                    highHerzeliya++;
                }
                if (item.GymAddress.Contains("Herzliya") && item.GymPrice < avg)
                {
                    lowHerzeliya++;
                }
                if (item.GymAddress.Contains("Herzliya") && item.GymPrice == avg)
                {
                    midHerzeliya++;
                }
            }
            Graph rishon = new Graph("Rishon Lezion", new Freq(lowgRishonLeTsiyon, midgRishonLeTsiyon, highgRishonLeTsiyon));
            Graph telAviv = new Graph("Tel Aviv", new Freq(lowTelAviv, midTelAviv, highTelAviv));
            Graph Herzliya = new Graph("Herzliya", new Freq(lowHerzeliya, midHerzeliya, highHerzeliya));
            priceavg.Add(rishon);
            priceavg.Add(telAviv);
            priceavg.Add(Herzliya);
            return Json(priceavg, JsonRequestBehavior.AllowGet);
             }
    }
}