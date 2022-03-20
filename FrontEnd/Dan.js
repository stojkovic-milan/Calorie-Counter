import { Obrok } from "./Obrok.js";
import { Hrana } from "./Hrana.js";
import { Osoba } from "./Osoba.js";
import { prethodniDan,sledeciDan} from "./main.js"
import { Porcija } from "./Porcija.js";
export class Dan{

    constructor(id, datum, osoba,cc){
        this.id=id;
        this.datum=datum;
        this.osoba=osoba;
        this.listaObroka=[];
        this.kalorije=0;
        this.proteini=0;
        this.ugljeniHidrati=0;
        this.masti=0;
        this.cc=cc;
        this.hranaModal=null;
    }
    
    vratiObroke(){
    fetch("https://localhost:5001/Obrok/VratiObrokeUDanu/"+this.id)
    .then(p=>{
        p.json().then(obroci=>{
            obroci.forEach(obrok => {
                //Za svaki obrok pozivamo f-ju za vracanje info o obroku na osnovu ID-a
                let o=new Obrok(obrok.id,obrok.tip,obrok.kalorije,obrok.masti,obrok.ugljeniHidrati,obrok.proteini,this);
                this.listaObroka.push(o);
            });
        }).then(p=>{
                this.ucitajNutrijente();
            }).then()
    })
    }
    osveziObroke(){
        fetch("https://localhost:5001/Obrok/VratiObrokeUDanu/"+this.id)
        .then(p=>{
            p.json().then(obroci=>{
                obroci.forEach(obrok => {
                    //Za svaki obrok pozivamo f-ju za vracanje info o obroku na osnovu ID-a
                    let o=new Obrok(obrok.id,obrok.tip,obrok.kalorije,obrok.masti,obrok.ugljeniHidrati,obrok.proteini,this);
                    this.listaObroka.push(o);
                });
            }).then(p=>{
                    this.osveziNutrijente();
                }).then()
        })
    }
    ucitajNutrijente(){
        fetch("https://localhost:5001/Dan/VratiNutrijenteUDanu/"+this.id)
        .then(p=>{
            p.json().then(nutr=>{
                console.log(nutr);
                this.kalorije=nutr.kalorije;
                this.masti=nutr.masti;
                this.ugljeniHidrati=nutr.ugljeniHidrati;
                this.proteini=nutr.proteini;
            }).then(q=>
            {
                console.log("Crtam header nakon azuriranja");
                console.log(this.kalorije);
                this.crtajHeader();
                this.crtajDan();
                this.crtajChart();
            })
    })
    }
    osveziNutrijente(){
        fetch("https://localhost:5001/Dan/VratiNutrijenteUDanu/"+this.id)
        .then(p=>{
            p.json().then(nutr=>{
                console.log(nutr);
                this.kalorije=nutr.kalorije;
                this.masti=nutr.masti;
                this.ugljeniHidrati=nutr.ugljeniHidrati;
                this.proteini=nutr.proteini;
            }).then(q=>
            {
                console.log("Crtam header nakon azuriranja");
                console.log(this.kalorije);
                this.crtajHeader(); 
                this.crtajChart();
            })
    })
    }
    azurirajNutrijente(){
        this.osveziNutrijente();
    }
    crtajDan(){
    this.kont=document.querySelector(".GlavniKontejner");

    let tmpNav=this.zaglavljeDana.querySelector(".navDani");
    if(tmpNav!=null){
        let datum=new Date(this.datum);
        tmpNav.querySelector("h2").innerHTML=datum.toLocaleDateString();
        let dugmad=tmpNav.querySelectorAll("button");
        dugmad[0].onclick=(ev)=>{prethodniDan(this.datum)};
        dugmad[1].onclick=(ev)=>{sledeciDan(this.datum)};
    }
    else{
    let h2=document.createElement("h2"); 
    let datum=new Date(this.datum);
    let navDani=document.createElement("div");
    navDani.className="navDani";
    h2.innerHTML=datum.toLocaleDateString();
    let btnPrev=document.createElement("button");
    //btnPrev.style.width="50px";
    //btnPrev.style.height="20px";
    btnPrev.innerHTML="<-";
    btnPrev.onclick=(ev)=>{prethodniDan(this.datum)};
    navDani.appendChild(btnPrev);

    navDani.appendChild(h2);
    let btnNext=document.createElement("button");
    //btnNext.style.width="50px";
    //btnNext.style.height="20px";
    btnNext.innerHTML="->";
    btnNext.onclick=(ev)=>{sledeciDan(this.datum)};
    navDani.appendChild(btnNext);
    this.zaglavljeDana.appendChild(navDani);
    this.kont.appendChild(this.zaglavljeDana);
    
    }

    //Crtanje novih obroka ili azuriranje postojecih
    let tmpObrociKont=this.kont.querySelector(".ObrociDiv");
    if(tmpObrociKont!=null){
        let dorucakDiv=tmpObrociKont.querySelector(".dorucakDiv");
        this.crtajObroke(dorucakDiv,"Dorucak",this.listaObroka[0]);///,this.listaDana[0].listaObroka[0]);

        let rucakDiv=tmpObrociKont.querySelector(".rucakDiv");
        this.crtajObroke(rucakDiv,"Rucak",this.listaObroka[1]);//,this.listaDana[0].listaObroka[1]);

        let veceraDiv=tmpObrociKont.querySelector(".veceraDiv");
        this.crtajObroke(veceraDiv,"Vecera",this.listaObroka[2]);//,this.listaDana[0].listaObroka[2]);

        let uzinaDiv=tmpObrociKont.querySelector(".uzinaDiv");
        this.crtajObroke(uzinaDiv,"Uzina",this.listaObroka[3]);//,this.listaDana[0].listaObroka[2]);
    }
    else{
    let obrociKont = document.createElement("div");
    obrociKont.className="ObrociDiv";
    this.kont.appendChild(obrociKont);

    let dorucakDiv = document.createElement("div");
    dorucakDiv.className="dorucakDiv";
    obrociKont.appendChild(dorucakDiv);
    this.crtajObroke(dorucakDiv,"Dorucak",this.listaObroka[0]);///,this.listaDana[0].listaObroka[0]);

    let rucakDiv = document.createElement("div");
    rucakDiv.className="rucakDiv";
    obrociKont.appendChild(rucakDiv);
    this.crtajObroke(rucakDiv,"Rucak",this.listaObroka[1]);//,this.listaDana[0].listaObroka[1]);
    
    let veceraDiv = document.createElement("div");
    veceraDiv.className="veceraDiv";
    obrociKont.appendChild(veceraDiv);
    this.crtajObroke(veceraDiv,"Vecera",this.listaObroka[2]);//,this.listaDana[0].listaObroka[2]);

    let uzinaDiv = document.createElement("div");
    uzinaDiv.className="uzinaDiv";
    obrociKont.appendChild(uzinaDiv);
    this.crtajObroke(uzinaDiv,"Uzina",this.listaObroka[3]);//,this.listaDana[0].listaObroka[2]);
    }
}

crtajObroke(host,nazivObroka,obrok)
{
    let tmpStrong=host.querySelector("strong");
    if(tmpStrong!=null){
        //Postoje tabele vec
        let redoviHrane=host.querySelectorAll(".redPorcija");
        let tabela;
        if(redoviHrane.length>0)
        tabela=redoviHrane[0].parentNode;
        for (let i = 0; i < redoviHrane.length; i++) {
            tabela.removeChild(redoviHrane[i]);
        }
        let dodajHranuBtn=host.querySelector(".dodajHranuBtn");
        dodajHranuBtn.onclick=(ev)=>{this.dodajHranu(obrok.id,host)};
        let kreirajBtn=host.querySelector(".dodajArtiklBtn");    
    }
    else{
    let tipObroka=document.createElement("strong");
    tipObroka.innerHTML=nazivObroka+":";
    host.appendChild(tipObroka);
    let t=document.createElement("table");
    let tr=document.createElement("tr");
    tr.className="zaglavljeTabele";
    t.appendChild(tr);
    let th1=document.createElement("th");
    th1.innerHTML="Hrana";
    tr.appendChild(th1);
    let th2=document.createElement("th");
    th2.innerHTML="Kolicina";
    tr.appendChild(th2);
    let th3=document.createElement("th");
    th3.innerHTML="Kalorije";
    tr.appendChild(th3);
    t.appendChild(tr);
    host.appendChild(t);
    let hranaPretraga=document.createElement("input");
    let listaHrane=document.createElement("datalist");
    listaHrane.id="hrana"+nazivObroka;
    hranaPretraga.setAttribute("list","hrana"+nazivObroka);
    hranaPretraga.name="hranaPretraga";
    let nizArtikala=[];
    hranaPretraga.oninput=function(){
        console.log("Pretrazi hranu");
        fetch("https://localhost:5001/Hrana/Preuzmi/"+hranaPretraga.value)
        .then(p=>
            p.json().then(Artikli=>
                {                
                    let kreirajBtn=host.querySelector(".dodajArtiklBtn");    
                    if(Artikli.length===0){
                        kreirajBtn.style.display="inline-block";
                    }
                    else{
                        kreirajBtn.style.display="none";
                    }
                    nizArtikala=[];
                    listaHrane.innerHTML="";
                    Artikli.forEach(hr => {
                        console.log(hr);
                        let h = new Hrana(hr.id,hr.naziv,hr.kalorije,hr.masti,hr.ugljeniHidrati,hr.proteini);
                        if(nizArtikala.indexOf(h)===-1){
                            nizArtikala.push(h);
                            let opcija=document.createElement("option");
                            opcija.value=h.naziv;
                            listaHrane.appendChild(opcija);
                        }
                        //
                        
                        //
                    });
    
                })
            )
    }
    host.appendChild(hranaPretraga);
    if(document.querySelector("#hrana")==null)
    host.appendChild(listaHrane);

    let l=document.createElement("label");
    l.innerHTML="Grama:";
    let kolPolje=document.createElement("input");
    host.appendChild(l);
    kolPolje.type="number";
    kolPolje.value=100;
    kolPolje.style.width="40px";

    host.appendChild(kolPolje);
    let dodajHranuBtn=document.createElement("button");
    dodajHranuBtn.innerHTML="Dodaj";
    dodajHranuBtn.className="dodajHranuBtn";
    dodajHranuBtn.onclick=(ev)=>{this.dodajHranu(obrok.id,host)};
    host.appendChild(dodajHranuBtn);
    let kreirajBtn=document.createElement("button");
    kreirajBtn.className="dodajArtiklBtn";
    kreirajBtn.innerHTML="Dodaj artikal";
    kreirajBtn.style.display="none";
    let unos=hranaPretraga.value;
    kreirajBtn.onclick=(ev)=>{this.novaHrana(hranaPretraga.value)}
    host.appendChild(kreirajBtn);
    }
}
novaHrana(unos){
//var modal = document.getElementById("myModal");//
//Kreiranje modala prilikom prvog poziva
if(this.hranaModal==null){
    var modal=document.createElement("div");
    modal.id="myModal";
    modal.className="modal";
    let modalContent=document.createElement("div");
    modalContent.className="modal-content";
    this.hranaModal=modal;
    let span=document.createElement("span");
    span.className="close";
    //SADRZAJ MODALA
    let novaHranaDiv=document.createElement("div");;
    novaHranaDiv.className="novaHrana"
    let uput=document.createElement("p");
    uput.innerHTML="Unesite vrednosti za 100g novog artikla:";
    novaHranaDiv.appendChild(uput);

    let nazivDiv=document.createElement("div");
    nazivDiv.className="divRed";
    let unosNaziv=document.createElement("input");
    unosNaziv.value=unos;
    let lab=document.createElement("label");
    lab.innerHTML="Naziv: ";
    unosNaziv.type="text";
    nazivDiv.appendChild(lab);
    nazivDiv.appendChild(unosNaziv);
    novaHranaDiv.appendChild(nazivDiv);
    //
    let kcalDiv=document.createElement("div");
    kcalDiv.className="divRed";
    let unosKcal=document.createElement("input");
    let lab2=document.createElement("label");
    lab2.innerHTML="Kalorije: ";
    unosKcal.type="number";
    kcalDiv.appendChild(lab2);
    kcalDiv.appendChild(unosKcal);
    novaHranaDiv.appendChild(kcalDiv);
    //
    let protDiv=document.createElement("div");
    protDiv.className="divRed";
    let unosProt=document.createElement("input");
    let lab3=document.createElement("label");
    lab3.innerHTML="Proteini: ";
    unosProt.type="number";
    protDiv.appendChild(lab3);
    protDiv.appendChild(unosProt);
    novaHranaDiv.appendChild(protDiv);
    //
    let mastiDiv=document.createElement("div");
    mastiDiv.className="divRed";
    let unosMasti=document.createElement("input");
    let lab4=document.createElement("label");
    lab4.innerHTML="Masti: ";
    unosMasti.type="number";
    mastiDiv.appendChild(lab4);
    mastiDiv.appendChild(unosMasti);
    novaHranaDiv.appendChild(mastiDiv);
    //
    let uhDiv=document.createElement("div");
    uhDiv.className="divRed";
    let unosUh=document.createElement("input");
    let lab5=document.createElement("label");
    lab5.innerHTML="Ugljeni hidrati: ";
    unosUh.type="number";
    uhDiv.appendChild(lab5);
    uhDiv.appendChild(unosUh);
    novaHranaDiv.appendChild(uhDiv);
    let btnDiv=document.createElement("div");
    btnDiv.className="divRed";
    let closeBtn=document.createElement("button");
    closeBtn.innerHTML="Nazad";
    closeBtn.onclick=function(ev){modal.style.display="none"}
    let posaljiBtn=document.createElement("button");
    posaljiBtn.innerHTML="Potvrdi";
    posaljiBtn.onclick=function(ev){
        let param='"naziv":';
        param+=unosNaziv.value;
        param+=",";
        param+='"kalorije":';
        param+=unosKcal.value;
        param+=",";
        param+='"ugljeniHidrati":';
        param+=unosUh.value;
        param+=",";
        param+='"proteini":';
        param+=unosProt.value;
        param+=",";
        param+='"masti":';
        param+=unosUh.value;
        param+=" ";
        console.log(param);
        param=JSON.stringify(param);
        console.log(param);
        let param2=JSON.stringify({naziv:unosNaziv.value, kalorije:parseInt(unosKcal.value), ugljeniHidrati:parseInt(unosUh.value), proteini:parseInt(unosProt.value), masti:parseInt(unosMasti.value) });
        console.log(param2);///Hrana/DodajHranu/{naziv}/{kcal}/{masti}/{uh}/{proteini}
        // fetch("https://localhost:5001/Hrana/DodajHranu/"+unosNaziv.value+"/"+unosKcal.value+"/"+unosMasti.value+"/"+unosUh.value+"/"+unosProt.value,{
        //     method: 'POST',
        // }).then(p=>p.text().then(q=>{
        //     console.log(q);
        //     modal.style.display="none";
        // }))
        //
        fetch("https://localhost:5001/Hrana/DodajHranu/"+unosNaziv.value+"/"+unosKcal.value+"/"+unosMasti.value+"/"+unosUh.value+"/"+unosProt.value,{
            method: 'POST',
        }).then(p=>
            {
                if(p.ok){
                    alert("Hrana uspesno dodata");
                    modal.style.display="none";
                }
                else{
                    //alert("Unete jedna ili vise nevalidnih vrednosti!")
                    p.text()
                    .then(r=>{alert(r)})
                }
            })
        // if(p.ok)
        // alert("Nova osoba dodata!")
        // else
        // {
        //     //alert("Unete jedna ili vise nevalidnih vrednosti!")
        //     p.json()
        //     .then(r=>{alert(r)})
        // }
        //
        
    }
    btnDiv.appendChild(closeBtn);
    btnDiv.appendChild(posaljiBtn);
    novaHranaDiv.appendChild(btnDiv);
    modalContent.appendChild(novaHranaDiv);
    modalContent.appendChild(span);
    modal.appendChild(modalContent);
    document.body.appendChild(modal);
}



this.hranaModal.style.display = "block";//

window.onclick = function(event) {
  if (event.target == this.hranaModal) {
    hranaModal.style.display = "none";
  }
}

}
dodajHranu(obrokID,host){
    let hrana=host.querySelector("[name=hranaPretraga]").value;
    let kolicina=host.querySelector("[type=number]").value;
    let idPorcije=0;
    //Kreiramo porciju
    fetch("https://localhost:5001/Porcija/DodajPorciju/"+hrana+"/"+kolicina,{
    method: "POST"}).then(p=>{
        if(p.ok)
        {
            p.json().then(idP=>{
                idPorcije=idP;
                console.log(idPorcije);
                console.log("Dodajemo porciju u obrok");
                fetch("https://localhost:5001/Obrok/DodajPorciju/"+obrokID+"/"+idPorcije,{
                    method: "POST"}).then(q=>{
                        q.json().then(porcija=>{
                            let izmenjenObrok=this.listaObroka.find(p=>p.id==obrokID);
                            let novaPorcija = new Porcija(porcija.id,porcija.velicina,porcija.kalorije,porcija.ugljeniHidrati,porcija.proteini,porcija.masti,izmenjenObrok);
                            console.log("Porcija dodata oborku");
                            this.osveziNutrijente();
                            izmenjenObrok.dodajPorciju(novaPorcija);
                        })
                        });
                    })
                }
                else{
                    alert("Ne postojeci artikal ili nevalidan broj grama!");
                }
            })
        }

postaviDugmad(){
        let dugmici=document.body.querySelectorAll(".dodajPorciju");
        console.log(dugmici);
        for (let i = 0; i < 4; i++) {
            dugmici[i].addEventListener("click",function(){
                this.listaObroka[i].dodajPorciju(this.listaObroka[i]);
            })
        }
    }
    crtajGlavniKont(host){
        this.kont=document.createElement("div");
        this.kont.className="GlavniKontejner";
        let zaglavljeDana=document.createElement("div");
        zaglavljeDana.className="zaglavljeDana";
        host.appendChild(this.kont);
    }
    crtajHeader(){
        //
        let tmpKont=document.body.querySelector(".GlavniKontejner");
        if(tmpKont==null){
        this.kont=document.createElement("div");
        this.kont.className="GlavniKontejner";
        document.body.appendChild(this.kont);
        }
        else this.kont=tmpKont;

        let tmpZaglavlje=this.kont.querySelector(".zaglavljeDana");
        if(tmpZaglavlje==null){
        this.zaglavljeDana=document.createElement("div");
        this.zaglavljeDana.className="zaglavljeDana";
        this.kont.appendChild(this.zaglavljeDana);
        }
        else this.zaglavljeDana=tmpZaglavlje;

        //
        let zaglavlje=this.zaglavljeDana;
        //
        //UBACI TABELU OVDE
        // let tabelaKcal=document.createElement("table");
        // tabelaKcal.className="tabelaKcal";
        // let tabelaKcalHead=document.createElement("thead");
        // let th1=document.createElement("th");
        // th1.innerHTML="Budzet"
        // tabelaKcalHead.appendChild(th1);
        // let th2=document.createElement("th");
        // th2.innerHTML="Uneseno"
        // tabelaKcalHead.appendChild(th2);
        // let th3=document.createElement("th");
        // th3.innerHTML="Preostalo"
        // tabelaKcalHead.appendChild(th3);
        // let redKcal=document.createElement("tr");
        //redKcal.className="redKcal";
        // let td1=document.createElement("td");
        // td1.innerHTML=this.osoba.ciljKcal;
        // td1.style.textAlign="center";
        // redKcal.appendChild(td1);
        // let td2=document.createElement("td");
        // td2.innerHTML=this.kalorije;
        // td2.style.textAlign="center";
        // redKcal.appendChild(td2);
        // let td3=document.createElement("td");
        // td3.innerHTML=this.osoba.ciljKcal-this.kalorije;
        // td3.style.textAlign="center";
        // redKcal.appendChild(td3);
        // tabelaKcal.appendChild(redKcal);
        // tabelaKcal.appendChild(tabelaKcalHead);
        // zaglavlje.appendChild(tabelaKcal);
        //
    //     let tabelaKcal=zaglavlje.querySelector("tabelaKcal");
    //    let td=tabelaKcal.querySelectorAll("td");
    //     td[0].innerHTML=this.osoba.ciljKcal;
    //     td[1].innerHTML=this.kalorije;
    //     td[2].innerHTML=this.osoba.ciljKcal-this.kalorije;
        //
        // tabelaKcal.appendChild(redKcal);
        // tabelaKcal.appendChild(tabelaKcalHead);
        // zaglavlje.appendChild(tabelaKcal);
        //
        let tmpHeaderdiv=zaglavlje.querySelector(".container");
        if(tmpHeaderdiv==null){
            //
                    //UBACI TABELU OVDE
        let tabelaKcal=document.createElement("table");
        tabelaKcal.className="tabelaKcal";
        let tabelaKcalHead=document.createElement("thead");
        let th1=document.createElement("th");
        th1.innerHTML="Budzet"
        tabelaKcalHead.appendChild(th1);
        let th2=document.createElement("th");
        th2.innerHTML="Uneseno"
        tabelaKcalHead.appendChild(th2);
        let th3=document.createElement("th");
        th3.innerHTML="Preostalo"
        tabelaKcalHead.appendChild(th3);
        let redKcal=document.createElement("tr");
        redKcal.className="redKcal";
        let td1=document.createElement("td");
        td1.innerHTML=this.osoba.ciljKcal;
        td1.style.textAlign="center";
        redKcal.appendChild(td1);
        let td2=document.createElement("td");
        td2.innerHTML=this.kalorije;
        td2.style.textAlign="center";
        redKcal.appendChild(td2);
        let td3=document.createElement("td");
        td3.innerHTML=this.osoba.ciljKcal-this.kalorije;
        td3.style.textAlign="center";
        redKcal.appendChild(td3);
        tabelaKcal.appendChild(redKcal);
        tabelaKcal.appendChild(tabelaKcalHead);
        zaglavlje.appendChild(tabelaKcal);
            //
        let headerDiv=document.createElement("div");
        headerDiv.className="container";
        let unetoDeo=document.createElement("div");
        unetoDeo.className="skill";
        console.log("Kalorije danas: "+this.kalorije);
        let odnos=this.kalorije/this.osoba.ciljKcal;
        unetoDeo.innerHTML=this.kalorije;
        unetoDeo.style.width=odnos*100+"%";
        if(odnos>1)
        unetoDeo.style.backgroundColor="red";
        else
        unetoDeo.style.backgroundColor="rgb(116, 194, 92)";
        headerDiv.appendChild(unetoDeo);
        zaglavlje.appendChild(headerDiv);
        // let label=document.createElement("label");
        // label.innerHTML=this.kalorije+" / "+this.osoba.ciljKcal+"Kcal";
        // zaglavlje.appendChild(label);
        }
        else{
            //
            let tabelaKcal=zaglavlje.querySelector(".tabelaKcal");
            let td=tabelaKcal.querySelectorAll("td");
             td[0].innerHTML=this.osoba.ciljKcal;
             td[1].innerHTML=this.kalorije;
             td[2].innerHTML=this.osoba.ciljKcal-this.kalorije;
            //
            let headerDiv=tmpHeaderdiv;
            let unetoDeo=headerDiv.querySelector(".skill");
            let odnos=this.kalorije/this.osoba.ciljKcal;
            unetoDeo.innerHTML=this.kalorije;
            unetoDeo.style.width=odnos*100+"%";
            if(odnos>1)
            unetoDeo.style.backgroundColor="red";
            else
            unetoDeo.style.backgroundColor="rgb(116, 194, 92)";
            // let label=zaglavlje.querySelector("label");
            // label.innerHTML=this.kalorije+" / "+this.osoba.ciljKcal+"Kcal";
        }
    }
    crtajChart(host)
    {
        let chartDivTmp=document.body.querySelector(".chartDiv");
        if(chartDivTmp!=null){
            host=document.body;
            let canvasTmp=chartDivTmp.querySelector("#myChart");
            canvasTmp.parentNode.removeChild(canvasTmp);
            let canvas=document.createElement("canvas");
            canvas.id="myChart";
            chartDivTmp.appendChild(canvas);
            host.appendChild(chartDivTmp);
            var xValues = ["Proteini "+this.proteini+"g", "Masti "+this.masti+"g","Ugljeni hidrati "+this.ugljeniHidrati+"g"];
        var yValues = [this.proteini,this.masti,this.ugljeniHidrati];
        var barColors = [
            "#b91d47",
            "#00aba9",
            "#c8a475"
        ];
        new Chart("myChart", {
        type: "pie",
        data: {
            labels: xValues,
            datasets: [{
            backgroundColor: barColors,
            data: yValues
            }]
        },
        options: {
            title: {
            display: true,
            text: "Makronutrijenti"
            },
            responsive: true,
            maintainAspectRatio:false
            }
        });
        }   
        else{
        host=document.body;
        let chartDiv=document.createElement("div");
        chartDiv.className="chartDiv";
        let canvas=document.createElement("canvas");
        canvas.id="myChart";
        chartDiv.appendChild(canvas);
        host.appendChild(chartDiv);
        var xValues = ["Proteini "+this.proteini+"g", "Masti "+this.masti+"g","Ugljeni hidrati "+this.ugljeniHidrati+"g"];
        var yValues = [this.proteini,this.masti,this.ugljeniHidrati];
        var barColors = [
            "#b91d47",
            "#00aba9",
            "#c8a475"
        ];
        new Chart("myChart", {
        type: "pie",
        data: {
            labels: xValues,
            datasets: [{
            backgroundColor: barColors,
            data: yValues
            }]
        },
        options: {
            title: {
            display: true,
            text: "Makronutrijenti"
            },
            responsive: true,
            maintainAspectRatio:false
            }
        });
    }

}
    async azurirajDan(){
        let resp= await fetch("https://localhost:5001/Dan/AzurirajNutrijente/"+this.id,{
            method: "PUT"});
                console.log(resp);
                this.azurirajNutrijente();
    }

}