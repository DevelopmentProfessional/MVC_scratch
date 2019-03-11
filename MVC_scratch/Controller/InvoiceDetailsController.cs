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
    public class InvoiceDetailsController : ApiController
    {
        private SnacksEntities db = new SnacksEntities();

        // GET: api/InvoiceDetails
        public IQueryable<InvoiceDetail> GetInvoiceDetails()
        {
            return db.InvoiceDetails;
        }

        // GET: api/InvoiceDetails/5
        [ResponseType(typeof(InvoiceDetail))]
        public IHttpActionResult GetInvoiceDetail(string id)
        {
            InvoiceDetail invoiceDetail = db.InvoiceDetails.Find(id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            return Ok(invoiceDetail);
        }

        // PUT: api/InvoiceDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInvoiceDetail(string id, InvoiceDetail invoiceDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceDetail.InvoiceID)
            {
                return BadRequest();
            }

            db.Entry(invoiceDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceDetailExists(id))
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

        // POST: api/InvoiceDetails
        [ResponseType(typeof(InvoiceDetail))]
        public IHttpActionResult PostInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InvoiceDetails.Add(invoiceDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InvoiceDetailExists(invoiceDetail.InvoiceID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = invoiceDetail.InvoiceID }, invoiceDetail);
        }

        // DELETE: api/InvoiceDetails/5
        [ResponseType(typeof(InvoiceDetail))]
        public IHttpActionResult DeleteInvoiceDetail(string id)
        {
            InvoiceDetail invoiceDetail = db.InvoiceDetails.Find(id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            db.InvoiceDetails.Remove(invoiceDetail);
            db.SaveChanges();

            return Ok(invoiceDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceDetailExists(string id)
        {
            return db.InvoiceDetails.Count(e => e.InvoiceID == id) > 0;
        }
    }
}