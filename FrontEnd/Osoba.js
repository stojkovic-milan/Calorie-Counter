export class Osoba{
    
    constructor(id, ime, prezime,ciljKcal)
    {
        this.ime=ime;
        this.prezime=prezime;
        this.id=id;
        this.ciljKcal=ciljKcal
    }
prethodniDan(pocDatum){
    let prethodniDatum=new Date(pocDatum);
    prethodniDatum.setDate(prethodniDatum.getDate()-1);
}
    sledeciDan(){
    let prethodniDatum=new Date(this.datum);
    prethodniDatum.setDate(prethodniDatum.getDate()+1);
}
}