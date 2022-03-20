using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace CalorieCounter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ObrokController : ControllerBase
    {
        public CalorieCounterContext Context { get; set; }
        public ObrokController(CalorieCounterContext context)
        {
            Context=context;
        }
        [HttpPut]
        [Route("AzurirajObrok/{id}")]
        public async Task<ActionResult>AzurirajObrok(int id)
        {
            if(id<=0)
                return BadRequest("Nevalidan id obroka");
            var o =await Context.Obroci.Where(p=>p.ID==id).Include(p=>p.Porcija).FirstOrDefaultAsync();
            if(o==null)
                return BadRequest("Ne postoji obrok sa trazenim id");
            o.Kalorije=0;
            o.Masti=0;
            o.UgljeniHidrati=0;
            o.Proteini=0;
            foreach (Porcija p in o.Porcija)
            {
                o.Kalorije+=p.Kalorije;
                o.Masti+=p.Masti;
                o.UgljeniHidrati+=p.UgljeniHidrati;
                o.Proteini+=o.Proteini;
            }
            try
            {
                await Context.SaveChangesAsync();
                return Ok("Obrok azuriran");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("DodajPorciju/{idO}/{idP}")]
        public async Task<ActionResult>DodajPorciju(int idO,int idP)
        {
            if(idO<=0)
                return BadRequest("Nevalidan id obroka");
            if(idP<=0)
                return BadRequest("Nevalidan id porcije");

            var o = await Context.Obroci.Where(p=>p.ID==idO).Include(p=>p.Porcija).Include(q=>q.Dan).ThenInclude(r=>r.Obroci).FirstOrDefaultAsync();
            if(o==null)
                return BadRequest("Ne postoji obrok sa trazenim id");
            var p =  Context.Porcije.Where(p=>p.ID==idP).Include(p=>p.Obrok).FirstOrDefault();
            var d = Context.Dani.Where(p=>p.ID==o.Dan.ID).Include(q=>q.Obroci).ThenInclude(r=>r.Porcija).FirstOrDefault();
            try
            {
                o.Porcija.Add(p);
                o.Kalorije+=p.Kalorije;
                o.Proteini+=p.Proteini;
                o.Masti+=p.Masti;
                o.UgljeniHidrati+=p.UgljeniHidrati;
                p.Obrok=o;
                //
                d.Kalorije=0;
                d.Proteini=0;
                d.UgljeniHidrati=0;
                d.Masti=0;
                foreach (var obrok in d.Obroci)
                {
                    foreach (var porcija in obrok.Porcija)
                    {
                        d.Kalorije+=porcija.Kalorije;
                        d.Proteini+=porcija.Proteini;
                        d.UgljeniHidrati+=porcija.UgljeniHidrati;
                        d.Masti+=porcija.Masti;
                    }
                }
                //
                await Context.SaveChangesAsync();
                return Ok(p);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("VratiObrok/{id}")]
        public ActionResult VratiObrok(int id)
        {
            if(id<=0)
                return BadRequest("Nevalidan id obroka");
            var o = Context.Obroci.Where(p=>p.ID==id);
            if(o.Count()==0)
                return BadRequest("Ne postoji obrok sa trazenim id");
            o.Include(p=>p.Porcija).ThenInclude(p=>p.Hrana).FirstOrDefault();
            return Ok(o);
        }
        [HttpGet]//id dana
        [Route("VratiObrokeUDanu/{id}")]
        public ActionResult VratiObroke(int id)
        {
            if(id<=0)
                return BadRequest("Nevalidan id dana");
            var o = Context.Obroci.Where(p=>p.Dan.ID==id);
            if(o.Count()==0)
                return BadRequest("Ne postoje obroci sa trazenim id dana");
            o.Include(p=>p.Porcija).ThenInclude(p=>p.Hrana);
            return Ok(o);
        }
        [HttpGet]//id dana
        [Route("VratiIDObrokaUDanu/{id}")]
        public ActionResult VratiIDObrokaUDanu(int id)
        {
            if(id<=0)
                return BadRequest("Nevalidan id dana");
            var o = Context.Obroci.Where(p=>p.Dan.ID==id).Select(p=>p.ID);
            if(o.Count()==0)
                return BadRequest("Ne postoje obroci sa trazenim id dana");
            return Ok(o);
        }
    }
}