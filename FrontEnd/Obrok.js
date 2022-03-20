import {Porcija} from "./Porcija.js"; 
export class Obrok{
    constructor(id,tip,kalorije,masti,uh,proteini,dan){
        this.id=id;
        this.tip=tip;
        this.kalorije=kalorije;
        this.masti=masti;
        this.uh=uh;
        this.proteini=proteini;
        this.dan=dan;
        //this.listaIdPorcija=listaIdPorcija;
        this.vratiPorcije();
        this.listaPorcija=[];
        this.host=null;
        this.red=null;
        this.btn=null;
    }

    vratiPorcije(){
//         if(this.host!=null){
//         let redoviPorcija=this.host.querySelectorAll(".redPorcija");
//         if(redoviPorcija.count>0){
//         let tabela=redoviPorcija[0].parentNode;
//         redoviPorcija.forEach(element => {
//             tabela.removeChild(element);
//         });
//     }
// }
        this.listaPorcija=[];
            fetch("https://localhost:5001/Porcija/VratiPorcijeObroka/"+this.id)
            .then(p=>{
                if(p.status==400){
                    console.log("Nema porcija");
                }
                else
                {
                    p.json().then(porcije=>{
                        porcije.forEach(porcija => {
                            //console.log(porcija);
                            let p = new Porcija(porcija.id,porcija.velicina,porcija.kalorije,porcija.ugljeniHidrati,porcija.proteini,porcija.masti,this);
                            this.listaPorcija.push(p);
                            p.crtajPorcije();
                        });
                    })
                }
            });
    } //OBRISI IZ BAZU PORCIJE PA PROBAJ JOS JEDNO DODAVANJE PORCIJE OBROKU, PA ONDA NAPRAVI FUNKCIJU DA OSVEZI OBROK U KOJ JE DODATO I HEADER I GRAPH
    crtajObrok(){
         this.host=document.querySelector("."+this.tip.toLowerCase()+"Div").querySelector("table");
        // let tmpRed=document.createElement("tr");
        // let tmpTd=document.createElement("td");
         let pretraga=document.createElement("input");
         pretraga.type="text";
         pretraga.placeholder="Pretraga hrane..";
         this.host.appendChild(pretraga);
        // tmpTd.appendChild(pretraga);
        // tmpRed.appendChild(tmpTd);

        // this.host.appendChild(tmpRed);
    }

    async azurirajObrok(){
        let odg=await fetch("https://localhost:5001/Obrok/AzurirajObrok/"+this.id,{
            method: "PUT"});/*.then(p=>{
                    console.log(this);
                     this.vratiPorcije();
                     this.dan.azurirajDan();
            });*/
            console.log(odg);
            //PROVERENO DO OVDE
            this.dan.azurirajDan();

            this.vratiPorcije(); //PROVERAVAM VRATI PORCIJE
            // this.dan.azurirajDan();
    }

    dodajPorciju(porcija){
        this.listaPorcija.push(porcija);
        porcija.crtajPorcije();
    }
}