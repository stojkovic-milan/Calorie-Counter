document.body.onload
{
    let roditelj=document.querySelector("#roditelj");
    let nizLabela=["Ime","Prezime","Indeks","Pass","Datum"];
    let nizTipova=["text","text","number","password","date"]
    let forma=document.createElement("div");
    forma.className="forma";
    roditelj.appendChild(forma);

    let divLab=document.createElement("div");
    divLab.classList.add("zaLabele");
    forma.appendChild(divLab);

    let divPolj=document.createElement("div");
    divPolj.classList.add("zaPolja");
    forma.appendChild(divPolj);

    let l;
    let p;
    nizLabela.forEach((p,i) => {
        let l=document.createElement("label");
        l.innerHTML=p+" ";
        divLab.appendChild(l);

        p=document.createElement("input");
        p.name=nizLabela[i];
        p.type=nizTipova[i];
        p.className="polja";
        divPolj.appendChild(p);
    });

    let dugme=document.createElement("button");
    dugme.innerHTML="Submit";
    dugme.style.alignSelf="flex-end";
    dugme.style.flexDirection="column";
    roditelj.appendChild(dugme);

    l=document.createElement("label");
    l.innerHTML="Labela";
    roditelj.appendChild(l);

    dugme.addEventListener("click",dogadjaj);
    let rbd=document.createElement("input");
    rbd.type="radio";
    rbd.value="1";
    roditelj.appendChild(rbd);

    function dogadjaj()
    {
        let f=document.querySelector(".forma");
        if(f!=undefined)
        f.className="NovaForma";
        
        let polja=document.querySelectorAll(".polja");
        console.log(polja);
        polja.forEach((element,i) => {
            console.log(`${element.name}:  ${element.value}`);
        });
    }


}