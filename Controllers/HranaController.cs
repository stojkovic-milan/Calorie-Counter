using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
namespace CalorieCounter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HranaController : ControllerBase   
    {
    public CalorieCounterContext Context { get; set; }

    public HranaController(CalorieCounterContext context)
    {
        Context=context;
    }

    [Route("DodajHranu/{naziv}/{kcal}/{masti}/{uh}/{proteini}")]
    [HttpPost]
    public async Task<ActionResult> DodajHranu(string naziv,int kcal,int masti,int uh,int proteini)
    {
        Hrana hrana=new Hrana();
        hrana.Naziv=naziv;
        hrana.Kalorije=kcal;
        hrana.Masti=masti;
        hrana.UgljeniHidrati=uh;
        hrana.Proteini=proteini;
        
        if(hrana.Kalorije<0 || hrana.Kalorije>2000)
            return BadRequest("Pogresan broj kalorija");

        if(string.IsNullOrEmpty(hrana.Naziv) || hrana.Naziv.Length>50)
            return BadRequest("Pogresan naziv hrane");

        if(hrana.UgljeniHidrati<0 || hrana.UgljeniHidrati>100)
            return BadRequest("Pogresan broj grama ugljenih hidrata");

        if(hrana.Proteini<0 || hrana.Proteini>100)
            return BadRequest("Pogresan broj grama proteina"); 

        if(hrana.Masti<0 || hrana.Masti>100)
            return BadRequest("Pogresan broj grama masti");
        
        try
        {
            Context.Hrana.Add(hrana);
            await Context.SaveChangesAsync();
            return Ok($"Hrana sa ID:{hrana.ID} je dodata!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet]
    public ActionResult PreuzmiSve()
    {
        return Ok(Context.Hrana);
    }

    //Koristi se za pretragu hrane prilikom dodavanja obroka
    [Route("Preuzmi/{naziv}")]
    [HttpGet]
    public ActionResult PreuzmiPoNazivu(string naziv)
    {
        if(string.IsNullOrEmpty(naziv))
            return BadRequest("Ne validan naziv");
        var hrana=Context.Hrana.Where(p=>p.Naziv.Contains(naziv));
        if(hrana==null)
            return BadRequest("Ne postoji hrana sa trazenim nazivom");
        else
            return Ok(hrana);
    }
    [Route("VratiHranuPorcije/{idPorcije}")]
    [HttpGet]
    public ActionResult VratiHranuPorcije(int idPorcije)
    {
        if(idPorcije<=0)
            return BadRequest("Ne validan id porcije");
        var hrana=Context.Porcije.Where(p=>p.ID==idPorcije).Select(p=>p.Hrana);
        if(hrana==null)
            return BadRequest("Ne postoji hrana u datoj porciji");
        else
            return Ok(hrana);
    }
    [Route("PromeniHranu")]
    [HttpPut]
    public async Task<ActionResult> Promeni([FromBody] Hrana hrana)
    {
        if(hrana.ID<=0)
        {
            return BadRequest("Pogresan ID");
        }
        if(hrana.Kalorije<0 || hrana.Kalorije>2000)
            return BadRequest("Pogresan broj kalorija");

        if(string.IsNullOrEmpty(hrana.Naziv) && hrana.Naziv.Length>50)
            return BadRequest("Pogresan naziv hrane");

        if(hrana.UgljeniHidrati<0 || hrana.UgljeniHidrati>100)
            return BadRequest("Pogresan broj grama ugljenih hidrata");

        if(hrana.Proteini<0 || hrana.Proteini>100)
            return BadRequest("Pogresan broj grama proteina"); 

        if(hrana.Masti<0 || hrana.Masti>100)
            return BadRequest("Pogresan broj grama masti");
        
        try
        {
            var hranaZaPromenu = await Context.Hrana.FindAsync(hrana.ID);
            if(hranaZaPromenu==null)
                return BadRequest("Pogresan ID hrane");
            hranaZaPromenu.Kalorije=hrana.Kalorije;
            hranaZaPromenu.Naziv=hrana.Naziv;
            hranaZaPromenu.Proteini=hrana.Proteini;
            hranaZaPromenu.Masti=hrana.Masti;
            hranaZaPromenu.UgljeniHidrati=hrana.UgljeniHidrati;
            //Context.Hrana.Update(hrana);
            await Context.SaveChangesAsync();
            return Ok($"Hrana sa ID:{hrana.ID} je promenjena!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [Route("IzbrisatiHranu/{id}")]
    [HttpDelete]
    public async Task<ActionResult> Izbrisi(int id)
    {
        if(id<=0)
            return BadRequest("Pogresan ID");
        try
        {
            var hrana=await Context.Hrana.FindAsync(id);
            Context.Hrana.Remove(hrana);
            await Context.SaveChangesAsync();
            return Ok("Uspesno obrisana hrana");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    }
}