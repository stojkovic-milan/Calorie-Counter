using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace CalorieCounter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OsobaController : ControllerBase
    {
        public CalorieCounterContext Context { get; set; }
        public OsobaController(CalorieCounterContext context)
        {
            Context=context;
        }
        [HttpPost]
        [Route("DodajOsobu")]
        public async Task<ActionResult> DodajOsobu([FromBody]Osoba osoba)
        {
            if(osoba.ID<0)
                return BadRequest("Pogresan ID");
            if(string.IsNullOrEmpty(osoba.Ime)||osoba.Ime.Length>20)
                return BadRequest("Pogresno ime osobe");
            if(string.IsNullOrEmpty(osoba.Prezime)||osoba.Prezime.Length>20)
                return BadRequest("Pogresno prezime osobe");
            if(osoba.Godine<18)
                return BadRequest("Osoba nije punoletna");
            if(osoba.FizAktivnost<1||osoba.FizAktivnost>3)
                return BadRequest("Nevalidna fizicka aktivnost");
            if(osoba.Visina<100)
                return BadRequest("Nevalidna visina");
            if(osoba.CiljKg<=0)
                return BadRequest("Nevalidna ciljna tezina");
            if(osoba.PocetakKg<=0)
                return BadRequest("Nevalidna pocetna tezina");
            if(osoba.Pol!='M'&&osoba.Pol!='Z')
                return BadRequest("Nevalidna vrednost za pol osobe");
            // For men:
            // BMR = 10W + 6.25H - 5A + 5
            // For women:
            // BMR = 10W + 6.25H - 5A - 161
            osoba.TrenutnoKg=osoba.PocetakKg;
            osoba.CiljKcal+=10*osoba.TrenutnoKg+(int)(6.25*osoba.Visina);
            osoba.CiljKcal-=5*osoba.Godine;
            if(osoba.Pol=='M')
                osoba.CiljKcal+=5;
            else if(osoba.Pol=='Z')
                osoba.CiljKcal-=161;

            if(osoba.FizAktivnost==1)
                osoba.CiljKcal=(int)(osoba.CiljKcal*1.2);
            else if(osoba.FizAktivnost==2)
                osoba.CiljKcal=(int)(osoba.CiljKcal*1.375);
            else if(osoba.FizAktivnost==3)
                osoba.CiljKcal=(int)(osoba.CiljKcal*1.55);
            osoba.CiljKcal-=500;
            // if(osoba.CiljKcal<1500)
            //     osoba.CiljKcal=1500;
            try
            {
                Context.Osobe.Add(osoba);
                await Context.SaveChangesAsync();
                return Ok($"Dodata osoba sa ID={osoba.ID}!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("IzmenaOsobe")]
        public async Task<ActionResult> IzmenaOsobe([FromBody]Osoba osoba)
        {
            if(osoba.ID<=0)
                return BadRequest("Pogresan ID");
            if(string.IsNullOrEmpty(osoba.Ime)||osoba.Ime.Length>20)
                return BadRequest("Pogresno ime osobe");
            if(string.IsNullOrEmpty(osoba.Prezime)||osoba.Prezime.Length>20)
                return BadRequest("Pogresno prezime osobe");
            if(osoba.Godine<18)
                return BadRequest("Osoba nije punoletna");
            if(osoba.FizAktivnost<1||osoba.FizAktivnost>3)
                return BadRequest("Nevalidna fizicka aktivnost");
            if(osoba.Visina<100)
                return BadRequest("Nevalidna visina");
            if(osoba.CiljKg<=0)
                return BadRequest("Nevalidna ciljna tezina");
            if(osoba.PocetakKg<=0)
                return BadRequest("Nevalidna pocetna tezina");
            if(osoba.Pol!='M'&&osoba.Pol!='Z')
                return BadRequest("Nevalidna vrednost za pol osobe");
        
        try
        {
            var osobaZaPromenu=await Context.Osobe.FindAsync(osoba.ID);
            if(osobaZaPromenu==null)
                return BadRequest("Pogresan ID osobe"); 
            osobaZaPromenu.Ime=osoba.Ime;
            osobaZaPromenu.Prezime=osoba.Prezime;
            osobaZaPromenu.Godine=osoba.Godine;
            osobaZaPromenu.FizAktivnost=osoba.FizAktivnost;
            osobaZaPromenu.CiljKg=osoba.CiljKg;
            osobaZaPromenu.Visina=osoba.Visina;
            osobaZaPromenu.Godine=osoba.Godine;
            if(osobaZaPromenu.TrenutnoKg!=osoba.TrenutnoKg){
                osobaZaPromenu.TrenutnoKg=osoba.TrenutnoKg;
                osoba.CiljKcal+=10*osoba.TrenutnoKg+(int)(6.25*osoba.Visina);
                osoba.CiljKcal-=5*osoba.Godine;

                if(osoba.Pol=='M')
                    osoba.CiljKcal+=5;
                else if(osoba.Pol=='Z')
                    osoba.CiljKcal-=161;

                if(osoba.FizAktivnost==1)
                    osoba.CiljKcal=(int)(osoba.CiljKcal*0.8);
                else if(osoba.FizAktivnost==3)
                    osoba.CiljKcal=(int)(osoba.CiljKcal*1.2);

            osoba.CiljKcal-=500;
            if(osoba.CiljKcal<1500)
                osoba.CiljKcal=1500;
            }
            await Context.SaveChangesAsync();
            return Ok($"Osoba sa ID:{osoba.ID} je promenjena!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        }
        [HttpPut]
        [Route("AzuriranjeTezine/{id}/{kg}")]
        public async Task<ActionResult> AzuriranjeTezine(int id,int kg)
        {
            if(id<=0)
                return BadRequest("Pogresan ID");
            try{
            var osobaZaPromenu=await Context.Osobe.FindAsync(id);
            if(osobaZaPromenu==null)
                return BadRequest("Pogresan ID osobe");
            osobaZaPromenu.TrenutnoKg=kg;
                osobaZaPromenu.TrenutnoKg=kg;
                osobaZaPromenu.CiljKcal+=10*osobaZaPromenu.TrenutnoKg+(int)(6.25*osobaZaPromenu.Visina);
                osobaZaPromenu.CiljKcal-=5*osobaZaPromenu.Godine;

                if(osobaZaPromenu.Pol=='M')
                    osobaZaPromenu.CiljKcal+=5;
                else if(osobaZaPromenu.Pol=='Z')
                    osobaZaPromenu.CiljKcal-=161;

                if(osobaZaPromenu.FizAktivnost==1)
                    osobaZaPromenu.CiljKcal=(int)(osobaZaPromenu.CiljKcal*0.8);
                else if(osobaZaPromenu.FizAktivnost==3)
                    osobaZaPromenu.CiljKcal=(int)(osobaZaPromenu.CiljKcal*1.2);

            osobaZaPromenu.CiljKcal-=500;
            if(osobaZaPromenu.CiljKcal<1500)
                osobaZaPromenu.CiljKcal=1500;
                return Ok("Promenjena trenutna tezina osobe");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("VratiOsobu/{id}")]
        public async Task<ActionResult> VratiOsobu(int id)
        {
            if(id<=0)
                return BadRequest("Pogresan ID");
            var osoba=await Context.Osobe.FindAsync(id);
            if(osoba==null)
                return BadRequest("Pogresan ID osobe");
            return Ok(osoba);
        }
        [HttpGet]
        [Route("VratiSveOsobe")]
        public async Task<ActionResult> VratiOsobe()
        {
            return Ok(await Context.Osobe.Select(p=>new{p.ID,p.Ime,p.Prezime,p.CiljKcal}).ToListAsync());
        }
        [Route("IzbrisiOsobu/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Izbrisi(int id)
        {
            if(id<=0)
                return BadRequest("Pogresan ID osobe");
            try
            {
                var osoba=await Context.Osobe.FindAsync(id);
                Context.Osobe.Remove(osoba);
                await Context.SaveChangesAsync();
                return Ok("Uspesno obrisana osoba");
                //TODO: Obrisi i sve dane vezane za datu osobu
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("VratiTezinu7Dana/{idOsobe}")]
        public async Task<ActionResult> VratiTezinuNedeljuDana([FromRoute]int idOsobe)
        {
            //Context.Osobe.Where(p=>p.ID==idOsobe).Include(q=>q.Dani).Where(r=>r.Dani.)
            var sviDani=Context.Dani.Include(p=>p.Osoba).Where(p=>p.Osoba.ID==idOsobe);
            //var poslednjiDan=sviDani.Where(p=>p.ID==idDana);
            var danasnjiDan=await sviDani.Where(p=>p.Datum.Date==DateTime.Today).FirstOrDefaultAsync();
            if(danasnjiDan==null)
                return BadRequest("Ne postoji danasnji dan");
            List<int> tezineNedeljuDana=new List<int>();
            tezineNedeljuDana.Add(danasnjiDan.Kilaza);
            for(int i=1;i<7;i++)
                {
                    var tmpDan=await sviDani.Where(p=>p.Datum.Date==DateTime.Today.AddDays(-i)).FirstOrDefaultAsync();
                    if(tmpDan==null)
                        tezineNedeljuDana.Add(0);
                    else
                        tezineNedeljuDana.Add(tmpDan.Kilaza);
                }
                return Ok(tezineNedeljuDana);
        }
    }
}