using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MVC_scratch.Model;

namespace MVC_scratch.Controller
{
    public class SnacksController : ApiController
    {
        private SnacksEntities db = new SnacksEntities();

        // GET: api/Snacks
        public IQueryable<Snack> GetSnacks()
        {
            return db.Snacks;
        }

        // GET: api/Snacks/5
        [ResponseType(typeof(Snack))]
        public IHttpActionResult GetSnack(string id)
        {
            Snack snack = db.Snacks.Find(id);
            if (snack == null)
            {
                return NotFound();
            }

            return Ok(snack);
        }

        // PUT: api/Snacks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSnack(string id, Snack snack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != snack.SnackID)
            {
                return BadRequest();
            }

            db.Entry(snack).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SnackExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Snacks
        [ResponseType(typeof(Snack))]
        public IHttpActionResult PostSnack(Snack snack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Snacks.Add(snack);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SnackExists(snack.SnackID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = snack.SnackID }, snack);
        }

        // DELETE: api/Snacks/5
        [ResponseType(typeof(Snack))]
        public IHttpActionResult DeleteSnack(string id)
        {
            Snack snack = db.Snacks.Find(id);
            if (snack == null)
            {
                return NotFound();
            }

            db.Snacks.Remove(snack);
            db.SaveChanges();

            return Ok(snack);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SnackExists(string id)
        {
            return db.Snacks.Count(e => e.SnackID == id) > 0;
        }
    }
}