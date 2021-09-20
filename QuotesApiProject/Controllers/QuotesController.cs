using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotesApiProject.Data;
using QuotesApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuotesApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class QuotesController : ControllerBase
    {
        QuotesDbContext quotes;
        public QuotesController(QuotesDbContext quotes)
        {
            this.quotes = quotes;
        }
        [HttpGet]
        [ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
        public IActionResult Get(string sort)
        {
            IQueryable<Quote> q;
            switch(sort)
            {
                case "desc":
                    q = quotes.Quotes.OrderByDescending(q => q.CreatedAt);
                    break;
                case "asc":
                     q = quotes.Quotes.OrderBy(q => q.CreatedAt);
                    break;
                default:
                    q = quotes.Quotes;
                    break;
            }
            return Ok(q);
        }
        [HttpGet("{id}",Name ="Get")]
        public IActionResult Get(int id)
        {
            var quote= quotes.Quotes.Find(id);
           
            if (quote == null)
            {
                return NotFound("Not found");
            }
            return Ok(quote);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Quote q)
        {
            quotes.Quotes.Add(q);
            quotes.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] Quote q)
        {
            var quote = quotes.Quotes.Find(id);
            if(quote==null)
            {
                return NotFound("Not found");
            }
            quote.Title = q.Title;
            quote.Author = q.Author;
            quote.Description = q.Description;
            quote.Type = q.Type;
            quote.CreatedAt = q.CreatedAt;
            quotes.SaveChanges();
            return Ok("Updated record");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quote = quotes.Quotes.Find(id);
            if (quote == null)
            {
                return NotFound("Not found");
            }
            quotes.Quotes.Remove(quote);
            quotes.SaveChanges();
            return Ok("Record deleted");
        }
        [HttpGet("[action]")]
        public IActionResult PagingQuote(int pagenumber,int pagesize)
        {
            var q = quotes.Quotes;
            return Ok(q.Skip((pagenumber - 1) * pagesize).Take(pagesize));
        }
        [HttpGet("[action]")]
        public IActionResult SearchQuote(string type)
        {
            var q=quotes.Quotes.Where(q => q.Type == type);
            return Ok(q);
        }
    }
}
