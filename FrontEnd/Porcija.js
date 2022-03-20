import { Hrana } from "./Hrana.js";
import { Obrok } from "./Obrok.js";
export class Porcija{
    constructor(id,velicina,kalorije,uh,proteini,masti,obrok){
        this.id=id;
        this.velicina=velicina;
        this.kalorije=kalorije;
        this.masti=masti;
        this.uh=uh;
        this.proteini=proteini;
        this.hrana=null;
        this.vratiHranu();
        this.host=null;
        this.obrok=obrok;
    }
    vratiHranu(){
        fetch("https://localhost:5001/Hrana/VratiHranuPorcije/"+this.id)
        .then(p=>{
            if(p.status==400){
                console.log("Nema hrane u porciji");
            }
            else
            {
                p.json().then(hrana=>{
                    hrana.forEach(hr => {
                        let h = new Hrana(hr.id,hr.naziv,hr.kalorije,hr.masti,hr.ugljeniHidrati,hr.proteini);
                        this.hrana=h;
                    })
                }).then(p=>
                    {
                        this.crtajPorcije();
                        // this.obrok.crtajObrok();
                    })
            }
        });
        //console.log(this.listaPorcija);
    }


    async crtajPorcije(){
        
        let tr=document.createElement("tr");
        tr.className="redPorcija";
        this.red=tr;

        if(this.hrana!=null){
            let td1=document.createElement("td");
            td1.className="tdNaziv";
            let icon=document.createElement("img");
            await fetch("http://127.0.0.1:5500/FrontEnd/FoodIcons/"+this.hrana.naziv+".png",{
                method:"HEAD"}).then(p=>{
                    if(p.ok)
                    icon.src="./FoodIcons/"+this.hrana.naziv+".png";
                    else
                    icon.src="./FoodIcons/no.png";
                }
                );
            //icon.src="./FoodIcons/no.png"
           
            td1.appendChild(icon);
            td1.innerHTML=td1.innerHTML+this.hrana.naziv;
            this.red.appendChild(td1);

            var td2=document.createElement("td");
            td2.className="tdKol";
            td2.innerHTML=this.velicina+"g";
            this.red.appendChild(td2);

            var td3=document.createElement("td");
            td3.innerHTML=this.kalorije;
            this.red.appendChild(td3);
            
            let td4=document.createElement("button");
            td4.innerHTML="❌";
            td4.onclick=(ev)=>{this.obrisiPorciju(this,this.red)};
            //
            let td5=document.createElement("button");
            td5.innerHTML="✏";
            var potvrdi=false;
            td5.onclick=(ev)=>{
                //this.obrisiPorciju(this,this.red)
                if(potvrdi==false){
                td2.innerHTML="";
                let input=document.createElement("input");
                input.type="number";
                input.style.width="20%";
                td2.appendChild(input);}
                else{
                console.log("Azuriram potvrdjenu porciju!");
                this.azurirajPorciju(this,this.red,this.red.querySelector("input").value,td2,td3);
                }
                potvrdi=!potvrdi;
            };
            //
            let kockica=document.createElement("td");
            kockica.className="redKontrola"
            //kockica.style.width="11%";
            //
            kockica.appendChild(td5);
            //
            kockica.appendChild(td4);
            this.red.appendChild(kockica);

            this.host=document.body.querySelector("."+this.obrok.tip.toLowerCase()+"Div").querySelector("table");
            this.host.appendChild(this.red);


        }
        //this.obrok.crtajObrok();
    }
    
    async obrisiPorciju(porcija,redDugmeta){
        let index=porcija.obrok.listaPorcija.indexOf(porcija);
        console.log(index);
        console.log(porcija.obrok.listaPorcija);
        if(index>-1){
            await fetch("https://localhost:5001/Porcija/ObrisiPorciju/"+porcija.id,{
                method: "DELETE"}).then(p=>{
                    if(p.ok)
                    {
                        console.log("Brisem porciju "+porcija);
                        porcija.obrok.listaPorcija.splice(index,1);
                        this.host.removeChild(redDugmeta);
                    }
        })
            // await porcija.obrok.azurirajObrok();
            porcija.obrok.dan.azurirajNutrijente();


        //await porcija.obrok.ukloniPorciju(porcija,redDugmeta);
        delete(this);
        }
    }
    async azurirajPorciju(porcija,redDugmeta,kol,td2,td3){
        let index=porcija.obrok.listaPorcija.indexOf(porcija);
        console.log(index);
        console.log(porcija.obrok.listaPorcija);
        if(index>-1){
            await fetch("https://localhost:5001/Porcija/AzurirajPorciju/"+porcija.id+"/"+kol,{
                method: "PUT"}).then(p=>{
                    if(p.ok)
                    {
                        console.log("Azuriram porciju "+porcija);
                        porcija.obrok.listaPorcija.splice(index,1);
                        td2.innerHTML=kol+"g";
                        td3.innerHTML=Math.round(porcija.hrana.kalorije*kol/100);
                        //this.host.removeChild(redDugmeta);
                        //porcija.crtajPorcije();
                        //porcija.obrok.dan.azurirajNutrijente();
                    }
        }).then(q=>{
            porcija.obrok.dan.osveziNutrijente();
            //porcija.obrok.dodajPorciju(this);
        });
            // await porcija.obrok.azurirajObrok();
            //porcija.obrok.dan.azurirajNutrijente();
        //await porcija.obrok.ukloniPorciju(porcija,redDugmeta);
        delete(this);
        }
    }
}
