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
    public class PorcijaController : ControllerBase   
    {
        public CalorieCounterContext Context { get; set; }
        public PorcijaController(CalorieCounterContext context)
        {
            Context=context;
        }
        [HttpPost]
        [Route("DodajPorciju")]
        public async Task<ActionResult>DodajPorciju([FromBody]Porcija porcija,int idObroka)
        {
            if(porcija.ID<=0)
                return BadRequest("Pogresan id porcije");
            if(porcija.Hrana==null)
                return BadRequest("Ne validna hrana");
            if(porcija.Velicina<=0)
                return BadRequest("Pogresna tezina hrane");

            var obrok=Context.Obroci.Find(idObroka);
            if(obrok==null)
                return BadRequest("Ne postojeci obrok");
            try{
            int m=porcija.Velicina;
            float koef=(float)(m)/100;
            porcija.Kalorije=(int)(porcija.Hrana.Kalorije*koef);
            porcija.UgljeniHidrati=(int)(porcija.Hrana.UgljeniHidrati*koef);
            porcija.Masti=(int)(porcija.Hrana.Masti*koef);
            porcija.Proteini=(int)(porcija.Hrana.Proteini*koef);
            porcija.Obrok=obrok;
            obrok.Porcija.Add(porcija);
            Context.Porcije.Add(porcija);
            await Context.SaveChangesAsync();
            return Ok(porcija.ID);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost]
        [Route("DodajPorciju/{hrana}/{g}")]//
        public async Task<ActionResult>DodajPorciju(string hrana,int g)
        {
            if(string.IsNullOrEmpty(hrana))
                return BadRequest("Ne validan naziv hrane");
            if(g<=0)
                return BadRequest("Pogresna tezina hrane");

            Porcija porcija=new Porcija();
            var h=Context.Hrana.Where(p=>p.Naziv==hrana);
            if(h==null)
                return BadRequest("Ne postoji hrana sa zadatim nazivom");

            try{
                //
            int m=g;
            float koef=(float)(m)/100;
            porcija.Hrana=h.FirstOrDefault();
            porcija.Velicina=g;
            porcija.Kalorije=(int)(porcija.Hrana.Kalorije*koef);
            porcija.UgljeniHidrati=(int)(porcija.Hrana.UgljeniHidrati*koef);
            porcija.Masti=(int)(porcija.Hrana.Masti*koef);
            porcija.Proteini=(int)(porcija.Hrana.Proteini*koef);

            Context.Porcije.Add(porcija);
            await Context.SaveChangesAsync();
            return Ok(porcija.ID);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //
        [HttpPut]//
        [Route("AzurirajPorciju/{id}/{g}")]//
        public async Task<ActionResult>AzurirajPorciju(int id,int g)
        {
            if(id<0)
                return BadRequest("Nevalidan id porcije");
            if(g<=0)
                return BadRequest("Pogresna tezina hrane");

            var por=await Context.Porcije.Where(r=>r.ID==id).Include(q=>q.Hrana).FirstOrDefaultAsync();
            if(por==null)
                return BadRequest("Ne postojeca porcija");
            var o=await Context.Obroci.Include(q=>q.Porcija).Where(p=>p.Porcija.Contains(por)).FirstOrDefaultAsync();
            if(o==null)
                return BadRequest("Nepostojeci obrok");
            var d=await Context.Dani.Include(p=>p.Obroci).Where(q=>q.Obroci.Contains(o)).FirstOrDefaultAsync();
            if(d==null)
                return BadRequest("Nepostojeci dan");
            int tmpKcal=por.Kalorije;
            int tmpProt=por.Proteini;
            int tmpMasti=por.Masti;
            int tmpUh=por.UgljeniHidrati;
            try{
                //Azuriramo porciju
                int m=g;
                float koef=(float)(m)/100;
                por.Velicina=g;
                por.Kalorije=(int)(por.Hrana.Kalorije*koef);
                por.UgljeniHidrati=(int)(por.Hrana.UgljeniHidrati*koef);
                por.Masti=(int)(por.Hrana.Masti*koef);
                por.Proteini=(int)(por.Hrana.Proteini*koef);
                //Azuriramo obrok
                o.Kalorije=o.Kalorije+por.Kalorije-tmpKcal;
                o.Proteini=o.Proteini+por.Proteini-tmpProt;
                o.Masti=o.Masti+por.Masti-tmpMasti;
                o.UgljeniHidrati=o.UgljeniHidrati+por.UgljeniHidrati-tmpUh;
                //Azuriramo dan
                d.Kalorije=d.Kalorije+por.Kalorije-tmpKcal;
                d.Proteini=d.Proteini+por.Proteini-tmpProt;
                d.Masti=d.Masti+por.Masti-tmpMasti;
                d.UgljeniHidrati=d.UgljeniHidrati+por.UgljeniHidrati-tmpUh;

                await Context.SaveChangesAsync();
                return Ok(por.ID);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //
        [HttpGet]
        [Route("VratiPorciju/{ID}")]
        public  ActionResult VratiPorcijuID(int ID)
        {
            if(ID<=0)
                return BadRequest("Pogresan id porcije");
            
            //var p=await Context.Porcije.FindAsync(ID);
            var p=Context.Porcije.Where(p=>p.ID==ID).Include(p=>p.Hrana);
            if(p==null)
                return BadRequest("Ne postojeca porcija");
            return Ok(p);
        }

        [HttpGet]//
        [Route("VratiPorcijeObroka/{ID}")]//
        public async Task<ActionResult> VratiPorcijeObroka(int ID)
        {
            if(ID<=0)
                return BadRequest("Pogresan id porcije");
            
            var p = Context.Porcije.Include(p=>p.Obrok).Where(p=>p.Obrok.ID==ID);
            if(p==null)
                return BadRequest("Ne postojece porcije");
            return Ok(p);
        }

        [HttpGet]
        [Route("VratiPorciju/{naziv}/{g}")]
        public ActionResult VratiPorciju(string naziv,int g)
        {
            if(g<=0)
                return BadRequest("Pogresna tezina porcije");
            if(string.IsNullOrEmpty(naziv))
                return BadRequest("Pogresan naziv hrane");

            var porcija=Context.Porcije.Where(p=>p.Hrana.Naziv==naziv).Where(p=>p.Velicina==g);
            if(porcija==null)
                return BadRequest("Ne postoji trazena porcija");
            porcija.Include(p=>p.Hrana);
            return Ok(porcija);
        }
        [HttpDelete]//
        [Route("ObrisiPorciju/{id}")]//
        public async Task<ActionResult>ObrisiPorciju(int id)
        {
            if(id<=0)
                return BadRequest("Pogresan id porcije");
            var p=await Context.Porcije.FindAsync(id);
            if(p==null)
                return BadRequest("Ne postoji porcija sa trazenim atributom");
            try{
                var porcija=Context.Porcije.Include(p=>p.Obrok).ThenInclude(q=>q.Dan).Where(p=>p.ID==id).FirstOrDefault();
                //Azuriram obrok
                porcija.Obrok.Kalorije-=porcija.Kalorije;
                porcija.Obrok.Masti-=porcija.Masti;
                porcija.Obrok.UgljeniHidrati-=porcija.UgljeniHidrati;
                porcija.Obrok.Proteini-=porcija.Proteini;
                //Azuriram dan
                porcija.Obrok.Dan.Kalorije-=porcija.Kalorije;
                porcija.Obrok.Dan.Masti-=porcija.Masti;
                porcija.Obrok.Dan.UgljeniHidrati-=porcija.UgljeniHidrati;
                porcija.Obrok.Dan.Proteini-=porcija.Proteini;
                Context.Porcije.Remove(porcija);
                await Context.SaveChangesAsync();
                return Ok("Obrisana porcija");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("PromeniPorciju/{id}/{g}")]
        public async Task<ActionResult>PromeniPorciju(int id,int g)
        {
            if(id<=0)
                return BadRequest("Pogresan id porcije");
            if(g<=0)
                return BadRequest("Pogresna velicina porcije");
            //var p=await Context.Porcije.FindAsync(id);
            var p=Context.Porcije.Where(p=>p.ID==id).Include(p=>p.Hrana).FirstOrDefault();
            if(p==null)
                return BadRequest("Ne postoji porcija sa trazenim atributom");
            if(p.Velicina==g)
                return Ok("Velicina porcije ostaje ista");
            try{
                //
            int m=g;
            float koef=(float)(m)/100;
            p.Velicina=g;
            p.Kalorije=(int)(p.Hrana.Kalorije*koef);
            p.UgljeniHidrati=(int)(p.Hrana.UgljeniHidrati*koef);
            p.Masti=(int)(p.Hrana.Masti*koef);
            p.Proteini=(int)(p.Hrana.Proteini*koef);
                //
                // p.Velicina=g;
                // p.Kalorije=(int)(Convert.ToDouble(p.Hrana.Kalorije)/100*g);
                // p.UgljeniHidrati=(int)(Convert.ToDouble(p.Hrana.UgljeniHidrati)/100*g);
                // p.Masti=(int)(Convert.ToDouble(p.Hrana.Masti)/100*g);
                // p.Proteini=(int)(Convert.ToDouble(p.Hrana.Proteini)/100*g);
                await Context.SaveChangesAsync();
                return Ok($"Porcija sa ID={p.ID} uspesno promenjena");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}