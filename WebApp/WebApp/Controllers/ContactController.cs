using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using WebApp.Controllers.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HandleError(View = "Error")]
        public ActionResult GetContacts()
        {
            IEnumerable<ContactDetail> contactDetails = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(new WebApiHelper().GetWebApiUri());
                var responseTask = client.GetAsync("Contact");
                try
                {
                    responseTask.Wait();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ContactDetail>>();
                    readTask.Wait();
                    contactDetails = readTask.Result;
                    return Json(new { data = contactDetails }, JsonRequestBehavior.AllowGet);
                }
                else
                    return View();
            }
        }

        // GET: Contact/Create
        [HandleError(View = "Error")]
        public ActionResult Create()
        {
            return View();
        }
        // POST: Contact/Create
        [HttpPost]
        [HandleError(View = "Error")]
        public ActionResult Create(ContactDetail contactDetail)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(new WebApiHelper().GetWebApiUri());

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync("Contact", contactDetail);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                        return RedirectToAction("Index");
                    else
                        return View(contactDetail);
                }
            }
            else
            {
                return View(contactDetail);
            }
        }

        // GET: Contact/Edit/5
        [HandleError(View = "Error")]
        public ActionResult Edit(Guid id)
        {
            ContactDetail contactDetail = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(new WebApiHelper().GetWebApiUri());
                //HTTP GET
                var responseTask = client.GetAsync("Contact?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ContactDetail>();
                    readTask.Wait();

                    contactDetail = readTask.Result;
                }
            }
            return View(contactDetail);
        }
        // POST: Contact/Edit/5
        [HttpPost]
        [HandleError(View = "Error")]
        public ActionResult Edit(ContactDetail contactDetail)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(new WebApiHelper().GetWebApiUri());

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<ContactDetail>("Contact", contactDetail);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                        return RedirectToAction("Index");
                    else
                        return View(contactDetail);
                }
            }
            else
            {
                return View(contactDetail);
            }
        }

        // GET: Contact/Delete/5
        [HandleError(View = "Error")]
        public ActionResult Delete(Guid id)
        {
            ContactDetail contactDetail = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(new WebApiHelper().GetWebApiUri());
                //HTTP GET
                var responseTask = client.GetAsync("Contact?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ContactDetail>();
                    readTask.Wait();

                    contactDetail = readTask.Result;
                }
            }
            return View(contactDetail);
        }
        // POST: Contact/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [HandleError(View = "Error")]
        public ActionResult DeleteContact(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(new WebApiHelper().GetWebApiUri());

                //HTTP POST
                var putTask = client.DeleteAsync("Contact?id=" + id.ToString());
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
                else
                    return View();
            }
        }

        public ActionResult Error(string message)
        {
            ViewBag.Error = message;
            return View("Error");
        }

        public ActionResult Requirements(string message)
        {
            return View();
        }
    }
}
