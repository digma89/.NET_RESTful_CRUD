using _300824022_Rodriguez__Ass5.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;



namespace _300824022_Rodriguez__Ass5.Controllers
{

    public class ContactController : ApiController
    {

        //get
        public IEnumerable<Contact> Get()
        {
            using (ContactEntities entity = new ContactEntities())
            {
                return entity.Contacts.ToList();
            }
        }

        //get by id
        public HttpResponseMessage Get(int id)
        {
            using (ContactEntities entity = new ContactEntities())
            {
                var query = entity.Contacts.FirstOrDefault(e => e.Id == id);
                if(query != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, query);
                }
                else {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact with Id = " + id.ToString() + "was not found");
                 }
                
            }
        }

    /* [Route("email/{string:email}")]
        public HttpResponseMessage GetByEmail(string email = "email") // equal to set a default value, not necessary 
        {
            using (ContactEntities entity = new ContactEntities())
            {
                var query = entity.Contacts.Where(e => e.EmailAddress.ToLower() == email.ToLower());
                if (query != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, query);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact with email address = " + email + "was not found");
                }

            }
        }
    */



    //create new 
    public HttpResponseMessage Post([FromBody] Contact contact)
        {
            try
            {
                using (ContactEntities entity = new ContactEntities())
                {
                    entity.Contacts.Add(contact);
                    entity.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, contact);   //Response message status code and contact created 
                    message.Headers.Location = new Uri(Request.RequestUri + contact.Id.ToString()); //header respond with uri and contact id 
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //delete by id
        public HttpResponseMessage Delete(int id)
        {
            using (ContactEntities entity = new ContactEntities())
            {
                try
                {
                    var query = entity.Contacts.FirstOrDefault(e => e.Id == id);
                    if (query != null)
                    {
                        entity.Contacts.Remove(query);
                        entity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact with Id = " + id.ToString() + "was not found");
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

                }
            }
        }

        public HttpResponseMessage Put(int id, [FromBody] Contact contact)
        {
            using (ContactEntities entity = new ContactEntities())
            {
                try
                {
                    var query = entity.Contacts.FirstOrDefault(e => e.Id == id);
                    if (query != null)
                    {
                        query.Name = contact.Name;
                        query.EmailAddress = contact.EmailAddress;
                        entity.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contact with Id = " + id.ToString() + "was not found");
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

                }
            }
        }



    }
}
