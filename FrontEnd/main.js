import { Dan } from "./Dan.js";
import { Osoba } from "./Osoba.js";
import {CalorieCounter} from "./CalorieCounter.js";
var listaOsoba=[];
let aktivnaOsoba;
let aktivanDan;
let c;
fetch("https://localhost:5001/Osoba/VratiSveOsobe")
.then(p=>{
    p.json().then(osobe=>{
        osobe.forEach(osoba => {
            let o= new Osoba(osoba.id,osoba.ime,osoba.prezime,osoba.ciljKcal);
            listaOsoba.push(o);
            console.log(o);
        })
    }).then(q=>
            {
                aktivnaOsoba=listaOsoba[0];
                cratjNavigaciju();
                vratiDanasnjiDan(aktivnaOsoba.id);
            })
});
function cratjNavigaciju(){
    //PROVERA DA LI JE VEC NACRTANA NAVIGACIJA
    let tmpDiv=document.body.querySelector(".topnav");//
    if(tmpDiv==null){//
    let div=document.createElement("div");
    div.className="topnav";
    let linkHome=document.createElement("a");
    linkHome.innerHTML="Home";
    linkHome.href="./index.html";
    linkHome.className="active";
    div.appendChild(linkHome);

    let linkKilaza=document.createElement("a");
    linkKilaza.innerHTML="Kilaza";
    //linkKilaza.href="./kilaza.html";
    linkKilaza.onclick=(ev)=>{prikazTezine(aktivnaOsoba); /*linkKilaza.classList.add("active")*/};
    linkKilaza.classList.add("kilaza");
    div.appendChild(linkKilaza);

   
    let se=document.createElement("select");
    se.className="izborOsobe";
    listaOsoba.forEach(osoba=>{
        let opt=document.createElement("option");
        opt.innerHTML=osoba.ime+" "+osoba.prezime;
        opt.value=osoba.id;
        se.appendChild(opt);
    })
    //MENJANJE AKTIVNE OSOBE TEST
    se.onchange=(ev)=>{
        let idOsobe=se.value;
        let izabranaOsoba=listaOsoba.find(p=>p.id==idOsobe);
        aktivnaOsoba=izabranaOsoba;
        cratjNavigaciju();
        vratiDanasnjiDan(aktivnaOsoba.id);
    }
    //
    div.appendChild(se);
    let linkOsoba=document.createElement("a");
    linkOsoba.innerHTML="Dodaj osobu";
    linkOsoba.onclick=(ev)=>{console.log("Modal za novu osobu");
    prikazNovaOsoba()
};
    //linkOsoba.href="./osoba.html";
    linkOsoba.className="novaOsoba";
    div.appendChild(linkOsoba);

    document.body.appendChild(div);
}//

}
let nizDatuma=[];
let nizTezina=[];
var modalContainer=null;
function prikazTezine(aktivnaOsoba){

    //
    if(modalContainer==null){
        modalContainer=document.createElement("div");
        modalContainer.className="modalContainer";
        document.body.appendChild(modalContainer);
    }
    //
    //var modal = document.getElementById("modalTezina"+aktivnaOsoba.id);
    var modal = modalContainer.querySelector("#modalTezina"+aktivnaOsoba.id);//
    //Kreiranje modala prilikom prvog poziva
    if(modal==null){
        modal=document.createElement("div");
        modal.id="modalTezina"+aktivnaOsoba.id;
        modal.className="modal";
        let modalContent=document.createElement("div");
        modalContent.className="modal-content2";
        
        let span=document.createElement("span");
        span.className="close";
        //SADRZAJ MODALA
        let tezinaDiv=document.createElement("div");;
        tezinaDiv.className="tezinaDiv"
        let naslov=document.createElement("h4");
        naslov.innerHTML="Informacije o tezini";
        tezinaDiv.appendChild(naslov);

        let graphTezineDiv=document.createElement("div");
        graphTezineDiv.className="graphTezineDiv";
    //

            let host=modalContent;
            let canvas=document.createElement("canvas");
            canvas.id="myChartT";
            graphTezineDiv.appendChild(canvas);
            host.appendChild(graphTezineDiv);

            azurirajTezine();

            for (let index = 0; index < 7; index++) {
                nizDatuma.push(new Date().getDate()-index)
            }
            console.log(nizDatuma);
            //
        let unosTezineDiv=document.createElement("div");
        unosTezineDiv.className="divRed";
        let label=document.createElement("label");
        label.innerHTML="Trenutna tezina: ";
        unosTezineDiv.appendChild(label);
        let unosTezine=document.createElement("input");
        unosTezine.type="number";
        unosTezine.min="1";
        unosTezine.max="150";
        //unosTezine.style.width="10%";

        unosTezineDiv.appendChild(unosTezine);


        let btnDiv=document.createElement("div");
        btnDiv.className="divRed";
        let closeBtn=document.createElement("button");
        closeBtn.innerHTML="Nazad";
        closeBtn.onclick=function(ev){modal.style.display="none"}

        let submitBtn=document.createElement("button");
        submitBtn.innerHTML="Potvrdi";
        submitBtn.onclick=function(ev){
            console.log(aktivanDan.datum);
            if(unosTezine.value>150)
            unosTezine.value=150;
            fetch("https://localhost:5001/Dan/AzuriranjeTrenutneTezine/"+aktivnaOsoba.id+"/"+unosTezine.value,
            {
                method:'PUT'
            }).then(p=>{
                if(p.ok){
                    p.text().then(q=>{
                        alert(q);
                    })
                azurirajTezine();
                }
                else{
                    alert("Nevalidan broj kilograma");
                }
            })
        }

        btnDiv.appendChild(closeBtn);
        btnDiv.appendChild(submitBtn);
        tezinaDiv.appendChild(unosTezineDiv);
        tezinaDiv.appendChild(btnDiv);
        modalContent.appendChild(tezinaDiv);
        modalContent.appendChild(graphTezineDiv);
        modalContent.appendChild(span);
        modal.appendChild(modalContent);
        //document.body.appendChild(modal);//
        modalContainer.appendChild(modal);

    }
    console.log(nizDatuma);
    console.log(nizTezina);
    function azurirajTezine(){
        fetch("https://localhost:5001/Osoba/VratiTezinu7Dana/"+aktivnaOsoba.id)
        .then(p=>{
            if(p.ok)
            {
                p.json().then(q=>{
                    nizTezina=q;
                    console.log(nizTezina);
                    crtajChart();
                });
            }
        })
    }
    function crtajChart(){
        let modalOsobe=modalContainer.querySelector("#modalTezina"+aktivnaOsoba.id);
    //var modalOsobe = document.getElementById("modalTezina"+aktivnaOsoba.id);
    new Chart(modalOsobe.querySelector("#myChartT"), {
        type: 'line',
        data: {
          labels: [nizDatuma[6]+".",nizDatuma[5]+".",nizDatuma[4]+".",nizDatuma[3]+".",nizDatuma[2]+".",nizDatuma[1]+".",nizDatuma[0]+"."],
          datasets: [{ 
              data: [nizTezina[6],nizTezina[5],nizTezina[4],nizTezina[3],nizTezina[2],nizTezina[1],nizTezina[0]],
              label: "kg",
              borderColor: "#3e95cd",
              fill: false
          }
          ]
        },
        options: {
          title: {
            display: false,
            text: 'Kretanje tezine'
          },
          elements:{
            line:{
                tension:0
            }
          }
        }
      });
    }

    
    modal.style.display = "block";
    

    window.onclick = function(event) {
      if (event.target == modal) {

        modal.style.display = "none";
      }
    }
    

}
//
var modalOsoba=null;
function prikazNovaOsoba(){
    //Kreiranje modala prilikom prvog poziva
    if(modalOsoba==null){
        let modal=document.createElement("div");
        modal.id="myModalT";
        modal.className="modalT";
        let modalContent=document.createElement("div");
        modalContent.className="modal-contentT";
        modalOsoba=modal;
        let span=document.createElement("span");
        span.className="close";
        //SADRZAJ MODALA
        let novaOsobaDiv=document.createElement("div");;
        novaOsobaDiv.className="novaOsoba"
        let uput=document.createElement("h5");
        uput.innerHTML="Unesite vrednosti za novu osobu:";
        novaOsobaDiv.appendChild(uput);
    
        let imeDiv=document.createElement("div");
        imeDiv.className="divRed";
        let unosNaziv=document.createElement("input");
        let lab=document.createElement("label");
        lab.innerHTML="Ime: ";
        unosNaziv.type="text";
        imeDiv.appendChild(lab);
        imeDiv.appendChild(unosNaziv);
        novaOsobaDiv.appendChild(imeDiv);
        //
        let prezimeDiv=document.createElement("div");
        prezimeDiv.className="divRed";
        let unosNazivP=document.createElement("input");
        let labP=document.createElement("label");
        labP.innerHTML="Prezime: ";
        unosNazivP.type="text";
        prezimeDiv.appendChild(labP);
        prezimeDiv.appendChild(unosNazivP);
        novaOsobaDiv.appendChild(prezimeDiv);
        //
        let polDiv=document.createElement("div");
        polDiv.className="divRed";
        let unosPolM=document.createElement("input");
        unosPolM.type="radio";
        unosPolM.name="pol";
        unosPolM.value="M";
        let labM=document.createElement("label");
        labM.innerHTML="M";
        let unosPolZ=document.createElement("input");
        unosPolZ.type="radio";
        unosPolZ.name="pol";
        unosPolZ.value="Z";
        let labZ=document.createElement("label");
        labZ.innerHTML="Ž";
        let labPol=document.createElement("label");
        labPol.innerHTML="Pol: ";
        polDiv.appendChild(labPol);
        polDiv.appendChild(unosPolM);
        polDiv.appendChild(labM);
        polDiv.appendChild(unosPolZ);
        polDiv.appendChild(labZ);
        novaOsobaDiv.appendChild(polDiv);
        //
        let pocKgDiv=document.createElement("div");
        pocKgDiv.className="divRed";
        let unosPocKg=document.createElement("input");
        let lab2=document.createElement("label");
        lab2.innerHTML="Pocetna kilaza: ";
        unosPocKg.type="number";
        pocKgDiv.appendChild(lab2);
        pocKgDiv.appendChild(unosPocKg);
        novaOsobaDiv.appendChild(pocKgDiv);
        //
        let visinaDiv=document.createElement("div");
        visinaDiv.className="divRed";
        let unosVisina=document.createElement("input");
        let lab3=document.createElement("label");
        lab3.innerHTML="Visina: ";
        unosVisina.type="number";
        visinaDiv.appendChild(lab3);
        visinaDiv.appendChild(unosVisina);
        novaOsobaDiv.appendChild(visinaDiv);
        //
        let godineDiv=document.createElement("div");
        godineDiv.className="divRed";
        let unosGod=document.createElement("input");
        let lab4=document.createElement("label");
        lab4.innerHTML="Godine: ";
        unosGod.type="number";
        godineDiv.appendChild(lab4);
        godineDiv.appendChild(unosGod);
        novaOsobaDiv.appendChild(godineDiv);
        //
        let ciljKgDiv=document.createElement("div");
        ciljKgDiv.className="divRed";
        let unosCiljKg=document.createElement("input");
        let lab5=document.createElement("label");
        lab5.innerHTML="Ciljana kilaza: ";
        unosCiljKg.type="number";
        ciljKgDiv.appendChild(lab5);
        ciljKgDiv.appendChild(unosCiljKg);
        novaOsobaDiv.appendChild(ciljKgDiv);
        //
        let aktivnostDiv=document.createElement("div");
        aktivnostDiv.className="divRed";
        let unosAktivnost=document.createElement("select");
        let lab6=document.createElement("label");
        lab6.innerHTML="Fizicka aktivnost: ";
        let op1=document.createElement("option");
        op1.value=1;
        op1.innerHTML="Neaktivan";
        let op2=document.createElement("option");
        op2.value=1;
        op2.innerHTML="Srednje aktivan";
        let op3=document.createElement("option");
        op3.value=1;
        op3.innerHTML="Veoma aktivan";
        unosAktivnost.appendChild(op1);
        unosAktivnost.appendChild(op2);
        unosAktivnost.appendChild(op3);
        aktivnostDiv.appendChild(lab6);
        aktivnostDiv.appendChild(unosAktivnost);
        novaOsobaDiv.appendChild(aktivnostDiv);
        //
        let btnDiv=document.createElement("div");
        btnDiv.className="divRed";
        let closeBtn=document.createElement("button");
        closeBtn.innerHTML="Nazad";
        closeBtn.onclick=function(ev){modal.style.display="none"}
        let posaljiBtn=document.createElement("button");
        posaljiBtn.innerHTML="Dodaj";
        posaljiBtn.onclick=function(ev){
            let param=JSON.stringify({ime:unosNaziv.value, prezime:unosNazivP.value,pocetakKg:parseInt(unosPocKg.value),
                visina:parseInt(unosVisina.value),fizAktivnost:parseInt(unosAktivnost.options[unosAktivnost.selectedIndex].value),
                godine:parseInt(unosGod.value),ciljKg:parseInt(unosCiljKg.value),pol:polDiv.querySelector('input:checked').value});
            console.log(param);
            fetch("https://localhost:5001/Osoba/DodajOsobu/",{
                method: 'POST',
                headers:{
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'},
                    body:param}).then(p=>
                {
                if(p.ok){
                alert("Nova osoba dodata!")
                location.reload();
                }
                else
                {
                    //alert("Unete jedna ili vise nevalidnih vrednosti!")
                    p.json()
                    .then(r=>{alert(r)})
                }
                //modal.style.display="none";
                });
        }
        btnDiv.appendChild(closeBtn);
        btnDiv.appendChild(posaljiBtn);
        novaOsobaDiv.appendChild(btnDiv);
        modalContent.appendChild(novaOsobaDiv);
        modalContent.appendChild(span);
        modal.appendChild(modalContent);
        document.body.appendChild(modal);
    }
    
    
    
    modalOsoba.style.display = "block";//
    window.onclick = function(event) {
      if (event.target == modalOsoba) {
        modalOsoba.style.display = "none";
      }
    }
    
    }

let listaDana=[];
function vratiDanasnjiDan(idOsobe){
fetch("https://localhost:5001/Dan/VratiDanasnjiDanOsobe/"+idOsobe)
.then(p=>{
    if(!p.ok){
        console.log("Nema danasnjeg dana,kreiram novi dan");
        console.log(idOsobe);
        //Pozovi za kreiranje danasnjeg dana
        fetch("https://localhost:5001/Dan/NoviDanasnjiDan/",{
            method: "POST",
            headers: {'Content-Type': 'application/json'}, 
            body: JSON.stringify(idOsobe)
        }).then(q=>{
            if(q.ok)
                {
                    q.json().then(dan=>{
                        let d = new Dan(dan.id,dan.date,aktivnaOsoba)
                        listaDana.push(d);
                        aktivanDan=d;
                    }).then(m=>{
                        aktivanDan.vratiObroke();
                        c=new CalorieCounter(aktivnaOsoba,aktivanDan);
                        //this.vratiObroke();
                        //c.crtajChart(document.body);
                        });
                }
        })
    }
    else
    {
        p.json().then(dan=>{
                let d = new Dan(dan.id,dan.date,aktivnaOsoba);
                listaDana.push(d);
                aktivanDan=d;
            }).then(q=>{
                aktivanDan.vratiObroke();
                c=new CalorieCounter(aktivnaOsoba,aktivanDan);
                //c.crtajChart(document.body);
                })
        }
    });
}
export function prethodniDan(pocDatum){
    console.log("Potreban prethodni dan od "+aktivanDan.datum);
    // let prethodniDatum=new Date(aktivanDan.datum);
    // prethodniDatum.setUTCDate(prethodniDatum.getUTCDate());
    // prethodniDatum=prethodniDatum.toJSON();
    //
    let prethodniDatum=new Date(aktivanDan.datum);
    prethodniDatum.setHours(prethodniDatum.getHours()-2);
    prethodniDatum=prethodniDatum.toJSON();
    //
    console.log("Potreban dan "+ prethodniDatum);
    vratiDanDatum(aktivnaOsoba.id,prethodniDatum);
}
export function sledeciDan(pocDatum){
    console.log("Potreban sledeci dan od "+aktivanDan.datum);
    let sledeciDatum=new Date(aktivanDan.datum);
    let danasnjiDan=new Date();
    if(danasnjiDan.setHours(0,0,0,0)===sledeciDatum.setHours(0,0,0,0))
    alert("Ne moguce planiranje unapred!");
    else{
    sledeciDatum.setDate(sledeciDatum.getDate()+1);
    sledeciDatum.setHours(sledeciDatum.getHours()+2);
    sledeciDatum=sledeciDatum.toJSON();
    console.log("Potreban dan "+ sledeciDatum);
    vratiDanDatum(aktivnaOsoba.id,sledeciDatum);
    }
}
function vratiDanDatum(idOsobe,datum){
    console.log("Trazim "+datum+"za osobu sa id="+idOsobe);
    fetch("https://localhost:5001/Dan/VratiDanID/"+idOsobe+"/"+datum)
    .then(p=>{
        if(!p.ok){
            console.log("Nema trazenog dana,kreiram novi dan sa datim datumom");
            console.log(idOsobe);
            fetch("https://localhost:5001/Dan/NoviDan/"+idOsobe+"/"+datum,{
                 method: "POST"
             }).then(q=>{
                 q.json().then(
                     dan=>{
                        console.log(dan);
                        let d = new Dan(dan.id,dan.date,aktivnaOsoba)
                        listaDana.push(d);
                        aktivanDan=d;
                     }
                 ).then(m=>{
                aktivanDan.vratiObroke();
                });
            });
        }
        else{
            p.json().then(dan=>{
                    let d = new Dan(dan.id,dan.date,aktivnaOsoba)
                    listaDana.push(d);
                    aktivanDan=d;
                }).then(m=>{
                    //aktivanDan.vratiObroke();
                    aktivanDan.vratiObroke();
                    //c=new CalorieCounter(aktivnaOsoba,aktivanDan);
                    //this.vratiObroke();
                    //c.crtajChart(document.body);
                    });
            }
        })
}