using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Laba2V19.Models;

namespace Laba2V19.Controllers
{
    public class MarkaObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TypeObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class WApiController : ApiController
    {
        Database19Entities _db = new Database19Entities();

        // Get list of brands
        [HttpGet]
        [ActionName("GetMarka")]
        public ICollection<MarkaObject> GetMarka()
        {
            var collectionMarka = new Collection<MarkaObject>();
            foreach (var m in (from marka in _db.MarkaClocks select marka).ToList())
                collectionMarka.Add(new MarkaObject { Id = m.IdMarka, Name = m.Name });
            return collectionMarka;
        }

        // Get type of clock
        [HttpGet]
        [ActionName("GetPos")]
        public ICollection<TypeObject> GetPos(int id)
        {
            var collectionType = new Collection<TypeObject>();
            foreach (var l in (from pos in _db.PosClocks where pos.IdMarka == id select pos.TypeClock).ToList())
                collectionType.Add(new TypeObject { Id = l.IdType, Name = l.Name });
            return collectionType;
        }

        // Add new brand
        [HttpPost]
        [ActionName("CreateMarka")]
        public HttpResponseMessage CreateMarka(MarkaClock markaClock)
        {
            var responseMessage = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                _db.MarkaClocks.Add(markaClock);
                _db.SaveChanges();
                responseMessage.Content = new StringContent("{" + $"Id:{markaClock.IdMarka},Name:{markaClock.Name},Country:{markaClock.Country}" + "}", Encoding.UTF8, "application/json");
            }
            catch (Exception ex)
            {
                responseMessage.Content = new StringContent("{Error:" + ex.Message + "}", Encoding.UTF8, "application/json");
            }
            return responseMessage;
        }

        // Edit brand
        [HttpPost]
        [ActionName("UpdateMarka")]
        public HttpResponseMessage UpdateMarka(MarkaClock markaClock)
        {
            var responseMessage = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                _db.MarkaClocks.Remove((from marka in _db.MarkaClocks where marka.IdMarka == markaClock.IdMarka select marka).
                    First());
                _db.MarkaClocks.Add(markaClock);
                _db.SaveChanges();
                responseMessage.Content = new StringContent("{" + $"Id:{markaClock.IdMarka},Name:{markaClock.Name},Country:{markaClock.Country}" + "}", Encoding.UTF8, "application/json");
            }
            catch (Exception ex)
            {
                responseMessage.Content = new StringContent("{Error:" + ex.Message + "}", Encoding.UTF8, "application/json");
            }
            return responseMessage;
        }

        // Delete brand
        [HttpPost]
        [ActionName("DeleteMarka")]
        public HttpResponseMessage DeleteMarka(MarkaClock markaClock)
        {
            var responseMessage = Request.CreateResponse(HttpStatusCode.OK);
            var clock = (from marka in _db.MarkaClocks where marka.IdMarka == markaClock.IdMarka select marka).First();
            try
            {
                _db.MarkaClocks.Remove(clock);
                _db.SaveChanges();
                responseMessage.Content = new StringContent("{" + $"Id:{clock.IdMarka},Name:{clock.Name},Country:{clock.Country}" + "}", Encoding.UTF8, "application/json");
            }
            catch (Exception ex)
            {
                responseMessage.Content = new StringContent("{Error:" + ex.Message + "}", Encoding.UTF8, "application/json");
            }
            return responseMessage;
        }
    }
}
