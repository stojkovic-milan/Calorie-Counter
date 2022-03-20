import { Dan } from "./Dan.js";
import { Hrana } from "./Hrana.js";
import { Obrok } from "./Obrok.js";
export class CalorieCounter{
    constructor(aktivnaOsoba, aktivanDan){
        this.aktivnaOsoba=aktivnaOsoba;
        this.aktivanDan=aktivanDan;
        this.kont=null;
        console.log(this.aktivanDan);
        console.log(this.aktivnaOsoba);
        //this.crtajGlavniKont(document.body);
    }

     crtajGlavniKont(host){
         this.kont=document.createElement("div");
         this.kont.className="GlavniKontejner";
         let zaglavljeDana=document.createElement("div");
         zaglavljeDana.className="zaglavljeDana";
         host.appendChild(this.kont);
     }
}