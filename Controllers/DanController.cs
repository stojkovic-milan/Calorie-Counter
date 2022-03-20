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
    public class DanController : ControllerBase
    {
        public CalorieCounterContext Context { get; set; }
        public DanController(CalorieCounterContext context)
        {
            Context=context;
        }

        [HttpPost]
        [Route("NoviDan/{idOsobe}/{datum}")]
        public async Task<ActionResult> NoviDan([FromRoute]int idOsobe,DateTime datum)
        {
            Dan dan=new Dan();
            if(idOsobe<0)
                return BadRequest("Pogresan ID");
            if(datum>DateTime.Today)
                return BadRequest("Nemoguce dodavanje dana unapred");
                //
                //TESTIRANJE
            dan.Datum=datum.Date;
            //
            //
                //return BadRequest("Pogresno ime osobe");
            var ciljOsoba=Context.Osobe.Where(p=>p.ID==idOsobe).Include(p=>p.Dani).FirstOrDefault();
            if(ciljOsoba==null)
                return BadRequest("Ne postoji osoba sa datim ID");

            if(ciljOsoba.Dani.Where(p=>p.Datum.Date==datum.Date).FirstOrDefault()!=null)
                return BadRequest("Vec postoji dan sa datim datumom za ovu osobu");

            dan.Osoba=ciljOsoba;
            dan.Proteini=0;
            dan.Masti=0;
            dan.Kalorije=0;
            dan.UgljeniHidrati=0;
            dan.Kilaza=dan.Osoba.TrenutnoKg;
            //Dodaj dorucak,rucak,uzinu i veceru za ovaj dan pozivom controllera za obroke
            //Dorucak
            Obrok dorucak=new Obrok();
            dorucak.Tip=TipObroka.Dorucak.ToString();
            dorucak.Kalorije=0;
            dorucak.UgljeniHidrati=0;
            dorucak.Masti=0;
            dorucak.Proteini=0;
            dorucak.Dan=dan;
            Context.Obroci.Add(dorucak);
            dan.Obroci.Add(dorucak);
            //Rucak
            Obrok rucak=new Obrok();
            rucak.Tip=TipObroka.Rucak.ToString();
            rucak.Kalorije=0;
            rucak.UgljeniHidrati=0;
            rucak.Masti=0;
            rucak.Proteini=0;
            rucak.Dan=dan;
            Context.Obroci.Add(rucak);
            dan.Obroci.Add(rucak);
            //Vecera
            Obrok vecera=new Obrok();
            vecera.Tip=TipObroka.Vecera.ToString();
            vecera.Kalorije=0;
            vecera.UgljeniHidrati=0;
            vecera.Masti=0;
            vecera.Proteini=0;
            vecera.Dan=dan;
            Context.Obroci.Add(vecera);
            dan.Obroci.Add(vecera);
            //Uzina
            Obrok uzina=new Obrok();
            uzina.Tip=TipObroka.Uzina.ToString();
            uzina.Kalorije=0;
            uzina.UgljeniHidrati=0;
            uzina.Masti=0;
            uzina.Proteini=0;
            uzina.Dan=dan;
            Context.Obroci.Add(uzina);
            dan.Obroci.Add(uzina);
            try
            {
                Context.Dani.Add(dan);
                ciljOsoba.Dani.Add(dan);
                await Context.SaveChangesAsync();
                var d= new {
                    dan.ID,dan.Datum.Date
                };
                return Ok(d);
                //return Ok(dan);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("NoviDanasnjiDan")]
        public async Task<ActionResult> NoviDanasnjiDan([FromBody]int idOsobe)
        {
            Dan dan=new Dan();
            if(idOsobe<0)
                return BadRequest("Pogresan ID");

            dan.Datum=DateTime.Today;

            var ciljOsoba=Context.Osobe.Where(p=>p.ID==idOsobe).Include(p=>p.Dani).FirstOrDefault();
            if(ciljOsoba==null)
                return BadRequest("Ne postoji osoba sa datim ID="+idOsobe);
            if(ciljOsoba.Dani.Where(p=>p.Datum.Date==DateTime.Today).FirstOrDefault()!=null)
                return BadRequest("Vec postoji dan sa datim datumom za ovu osobu");
            dan.Osoba=ciljOsoba;
            dan.Proteini=0;
            dan.Masti=0;
            dan.Kalorije=0;
            dan.UgljeniHidrati=0;
            dan.Kilaza=dan.Osoba.TrenutnoKg;
            //Dodaj dorucak,rucak,uzinu i veceru za ovaj dan pozivom controllera za obroke
            //Dorucak
            Obrok dorucak=new Obrok();
            dorucak.Tip=TipObroka.Dorucak.ToString();
            dorucak.Kalorije=0;
            dorucak.UgljeniHidrati=0;
            dorucak.Masti=0;
            dorucak.Proteini=0;
            dorucak.Dan=dan;
            Context.Obroci.Add(dorucak);
            dan.Obroci.Add(dorucak);
            //Rucak
            Obrok rucak=new Obrok();
            rucak.Tip=TipObroka.Rucak.ToString();
            rucak.Kalorije=0;
            rucak.UgljeniHidrati=0;
            rucak.Masti=0;
            rucak.Proteini=0;
            rucak.Dan=dan;
            Context.Obroci.Add(rucak);
            dan.Obroci.Add(rucak);
            //Vecera
            Obrok vecera=new Obrok();
            vecera.Tip=TipObroka.Vecera.ToString();
            vecera.Kalorije=0;
            vecera.UgljeniHidrati=0;
            vecera.Masti=0;
            vecera.Proteini=0;
            vecera.Dan=dan;
            Context.Obroci.Add(vecera);
            dan.Obroci.Add(vecera);
            //Uzina
            Obrok uzina=new Obrok();
            uzina.Tip=TipObroka.Uzina.ToString();
            uzina.Kalorije=0;
            uzina.UgljeniHidrati=0;
            uzina.Masti=0;
            uzina.Proteini=0;
            uzina.Dan=dan;
            Context.Obroci.Add(uzina);
            dan.Obroci.Add(uzina);
            try
            {
                Context.Dani.Add(dan);
                ciljOsoba.Dani.Add(dan);
                await Context.SaveChangesAsync();
                var d= new {
                    dan.ID,dan.Datum.Date
                };
                return Ok(d);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }       
        [HttpPost]
        [Route("NoviDanPoDatumu")]
        public async Task<ActionResult> NoviDanPoDatumu([FromBody]int idOsobe,DateTime datum)
        {
            Dan dan=new Dan();
            if(idOsobe<0)
                return BadRequest("Pogresan ID");

            dan.Datum=DateTime.Today;

            var ciljOsoba=Context.Osobe.Where(p=>p.ID==idOsobe).Include(p=>p.Dani).FirstOrDefault();
            if(ciljOsoba==null)
                return BadRequest("Ne postoji osoba sa datim ID="+idOsobe);
            if(ciljOsoba.Dani.Where(p=>p.Datum.Date==DateTime.Today).FirstOrDefault()!=null)
                return BadRequest("Vec postoji dan sa datim datumom za ovu osobu");
            dan.Osoba=ciljOsoba;
            dan.Proteini=0;
            dan.Masti=0;
            dan.Kalorije=0;
            dan.UgljeniHidrati=0;
            dan.Kilaza=dan.Osoba.TrenutnoKg;
            //Dodaj dorucak,rucak,uzinu i veceru za ovaj dan pozivom controllera za obroke
            //Dorucak
            Obrok dorucak=new Obrok();
            dorucak.Tip=TipObroka.Dorucak.ToString();
            dorucak.Kalorije=0;
            dorucak.UgljeniHidrati=0;
            dorucak.Masti=0;
            dorucak.Proteini=0;
            dorucak.Dan=dan;
            Context.Obroci.Add(dorucak);
            dan.Obroci.Add(dorucak);
            //Rucak
            Obrok rucak=new Obrok();
            rucak.Tip=TipObroka.Rucak.ToString();
            rucak.Kalorije=0;
            rucak.UgljeniHidrati=0;
            rucak.Masti=0;
            rucak.Proteini=0;
            rucak.Dan=dan;
            Context.Obroci.Add(rucak);
            dan.Obroci.Add(rucak);
            //Vecera
            Obrok vecera=new Obrok();
            vecera.Tip=TipObroka.Vecera.ToString();
            vecera.Kalorije=0;
            vecera.UgljeniHidrati=0;
            vecera.Masti=0;
            vecera.Proteini=0;
            vecera.Dan=dan;
            Context.Obroci.Add(vecera);
            dan.Obroci.Add(vecera);
            //Uzina
            Obrok uzina=new Obrok();
            uzina.Tip=TipObroka.Uzina.ToString();
            uzina.Kalorije=0;
            uzina.UgljeniHidrati=0;
            uzina.Masti=0;
            uzina.Proteini=0;
            uzina.Dan=dan;
            Context.Obroci.Add(uzina);
            dan.Obroci.Add(uzina);
            try
            {
                Context.Dani.Add(dan);
                ciljOsoba.Dani.Add(dan);
                await Context.SaveChangesAsync();
                var d= new {
                    dan.ID,dan.Datum.Date
                };
                return Ok(d);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("AzuriranjeTezine/{id}/{date}/{kg}")]
        public async Task<ActionResult> AzuriranjeTezine(int id,DateTime date,int kg)
        {
            if(id<=0)
                return BadRequest("Pogresan ID osobe");
            if(kg<=0)
                return BadRequest("Pogresna kilaza");
            try{
                var dan=Context.Dani.Where(p=>p.Datum.Date==date.Date).Where(p=>p.Osoba.ID==id).FirstOrDefault();
                if(dan==null)
                    return BadRequest("Ne postojeci dan");
                dan.Kilaza=kg;
                if(dan.Datum.Date==DateTime.Today)
                    dan.Osoba.TrenutnoKg=dan.Kilaza;
                await Context.SaveChangesAsync();
                return Ok("Promenjena trenutna tezina osobe na trazeni dan");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("AzuriranjeTrenutneTezine/{id}/{kg}")]
        public async Task<ActionResult> AzuriranjeTrenutneTezine(int id,int kg)
        {
            if(id<=0)
                return BadRequest("Pogresan ID osobe");
            if(kg<=0)
                return BadRequest("Pogresna kilaza");
            try{
                var dan=Context.Dani.Include(p=>p.Osoba).Where(p=>p.Datum.Date==DateTime.Today).Where(p=>p.Osoba.ID==id).FirstOrDefault();
                if(dan==null)
                    return BadRequest("Ne postojeci dan");
                dan.Kilaza=kg;
                dan.Osoba.TrenutnoKg=kg;
                await Context.SaveChangesAsync();
                if(dan.Osoba.TrenutnoKg<=dan.Osoba.CiljKg)
                    return Ok("Cestitamo, dostigli ste zeljenu kilazu!");
                else
                return Ok("Promenjena trenutna tezina osobe");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //Pretraga dana po ID ili datum+id osobe
        [HttpPut]
        [Route("AzurirajNutrijente/{id}")]
        public async Task<ActionResult> AzurirajNutrijente(int id)
        {
            if(id<=0)
                return BadRequest("Pogresan ID dana");
            var dan=await Context.Dani.Where(p=>p.ID==id).Include(p=>p.Obroci).ThenInclude(q=>q.Porcija).FirstOrDefaultAsync();
                dan.Kalorije=0;
                dan.Masti=0;
                dan.Proteini=0;
                dan.UgljeniHidrati=0;

            foreach (Obrok o in dan.Obroci)
            {
                dan.Kalorije+=o.Kalorije;
                dan.Masti+=o.Masti;
                dan.Proteini+=o.Proteini;
                dan.UgljeniHidrati+=o.UgljeniHidrati;
            }
            try
            {
                await Context.SaveChangesAsync();
                return Ok("Uspesno azuriran dan");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //Get za dan ceo
        [HttpGet]
        [Route("VratiDan/{ID}")]
        public ActionResult VratiDan(int ID)
        {
            if(ID<=0)
                return BadRequest("Pogresan id dana");
            //var p=await Context.Porcije.FindAsync(ID);
            var p=Context.Dani.Include(d=>d.Osoba).Where(p=>p.Osoba.ID==ID).Include(p=>p.Obroci).ThenInclude(p=>p.Porcija).ThenInclude(p=>p.Hrana).FirstOrDefault();
            if(p==null)
                return BadRequest("Ne postojeci dan");
            return Ok(p);
        }
        [HttpGet]//id osobe i datum
        [Route("VratiDanID/{id}/{date}")]
        public ActionResult VratiDanID(int id,DateTime date)
        {
            if(id<=0)
                return BadRequest("Pogresan id dana");
                
            var dan=Context.Dani.Include(d=>d.Osoba).Where(p=>p.Osoba.ID==id).Where(p=>p.Datum.Date==date.Date).Select(p=>new{p.ID,p.Datum.Date}).FirstOrDefault();
            if(dan==null)
                return BadRequest("Ne postojeci dan "+date.Date);
            return Ok(dan);
        }
        [HttpGet]//id dana
        [Route("VratiDanasnjiDan/{id}")]
        public ActionResult VratiDanasnjiDan(int id)
        {
            if(id<=0)
                return BadRequest("Pogresan id dana");
            var dan=Context.Dani.Where(p=>p.ID==id)
            .Where(p=>p.Datum.Date==DateTime.Today)
            .Include(p=>p.Obroci)
            .ThenInclude(p=>p.Porcija)
            .ThenInclude(p=>p.Hrana)
            .FirstOrDefault();
            if(dan==null)
                return BadRequest("Ne postojeci dan");
            return Ok(dan);
        }
                [HttpGet]//id osobe
        [Route("VratiDaneOsobe/{id}")]
        public ActionResult VratiDaneOsobe(int id)
        {
            if(id<=0)
                return BadRequest("Pogresan id dana");
            var dan=Context.Dani.Where(p=>p.Osoba.ID==id)
            .Where(p=>p.Datum.Date==DateTime.Today)
            .Include(p=>p.Obroci)
            .ThenInclude(p=>p.Porcija)
            .ThenInclude(p=>p.Hrana);
            if(dan.Count()==0)
                return BadRequest("Ne postoje dani za trazenu osobu");
            return Ok(dan);
        }
    
        [HttpGet]//id osobe
        [Route("VratiDanasnjiDanOsobe/{id}")]
        public ActionResult VratiDanasnjiDanOsobe(int id)
        {
            if(id<=0)
                return BadRequest("Pogresan id osobe");
            var dan=Context.Dani.Where(p=>p.Osoba.ID==id)
            .Where(p=>p.Datum.Date==DateTime.Today).Select(p=>new{p.ID,p.Datum.Date}).FirstOrDefault();
            // .Include(p=>p.Obroci)
            // .ThenInclude(p=>p.Porcija)
            // .ThenInclude(p=>p.Hrana);
            if(dan==null)
                return BadRequest("Ne postoje dani za trazenu osobu");
            return Ok(dan);
            //return Ok(await dan.Select(p=>new{p.ID,p.Datum.Date,p.Osoba}).ToListAsync());
            //return Ok(await Context.Osobe.Select(p=>new{p.ID,p.Ime,p.Prezime}).ToListAsync());
        }
        [HttpGet]//id dana
        [Route("VratiNutrijenteUDanu/{id}")]
        public ActionResult VratiNutrijenteUDanu(int id)
        {
            if(id<=0)
                return BadRequest("Pogresan id osobe");
            var dan=Context.Dani.Where(p=>p.ID==id)
            .Select(p=>new{p.Kalorije,p.Masti,p.UgljeniHidrati,p.Proteini})
            .FirstOrDefault();
            // .Include(p=>p.Obroci)
            // .ThenInclude(p=>p.Porcija)
            // .ThenInclude(p=>p.Hrana);
            if(dan==null)
                return BadRequest("Ne postoje dani sa datim ID");
            return Ok(dan);
            //return Ok(await dan.Select(p=>new{p.ID,p.Datum.Date,p.Osoba}).ToListAsync());
            //return Ok(await Context.Osobe.Select(p=>new{p.ID,p.Ime,p.Prezime}).ToListAsync());
        }
    }
}