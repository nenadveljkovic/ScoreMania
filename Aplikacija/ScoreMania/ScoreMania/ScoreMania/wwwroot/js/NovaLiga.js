function dodaj() {
    var cont = document.createElement("div");
    cont.classList = "container liga";

    var cont2 = document.createElement("div");
    cont2.classList = "container2";
    var naziv = document.createElement("div");
    naziv.classList = "naziv-div";
    var lab = document.createElement("label");
    lab.classList = "naziv-liga"
    lab.innerHTML = "Italija - Seria A";
    naziv.appendChild(lab);
    cont2.appendChild(naziv);
    cont.appendChild(cont2);

    var utakmice = document.createElement("div");
    utakmice.classList = "utakmice";

    //neki loop za sve utakmice
    var utakmica = document.createElement("div");
    utakmica.classList = "utakmica row";
    var vreme = document.createElement("div");
    vreme.classList = "datum-vreme col-3";
    var labv = document.createElement("label");
    labv.innerHTML = "23.12.2020 20:45";
    vreme.appendChild(labv);
    var naziv_utakmica = document.createElement("div");
    naziv_utakmica.classList = "timovi col-5";
    var labt = document.createElement("label");
    labt.classList = "labeltext";
    labt.innerHTML = "Milan - Lazio";
    naziv_utakmica.appendChild(labt);
    var omiljeno = document.createElement("div");
    omiljeno.classList = "omiljena col-1";
    var slika = document.createElement("i");
    slika.classList = "far fa-star";
    slika.onmouseover = function () { this.style.cursor = "pointer"; }
    slika.onmouseout = function () { this.style.cursor = "context-menu"; }
    slika.onclick = function () { this.classList.toggle("fas") }
    omiljeno.appendChild(slika);
    utakmica.appendChild(vreme);
    utakmica.appendChild(naziv_utakmica);
    utakmica.appendChild(omiljeno);
    utakmice.appendChild(utakmica);

    var linija = document.createElement("hr");
    linija.classList = "sidebar-divider d-none d-md-block col-8";
    utakmice.appendChild(linija);

    utakmica = document.createElement("div");
    utakmica.classList = "utakmica row";
    vreme = document.createElement("div");
    vreme.classList = "datum-vreme col-3";
    labv = document.createElement("label");
    labv.innerHTML = "23.12.2020 20:45";
    vreme.appendChild(labv);
    naziv_utakmica = document.createElement("div");
    naziv_utakmica.classList = "timovi col-5";
    labt = document.createElement("label");
    labt.classList = "labeltext";
    labt.innerHTML = "Milan - Lazio";
    naziv_utakmica.appendChild(labt);
    omiljeno = document.createElement("div");
    omiljeno.classList = "omiljena col-1";
    slika = document.createElement("i");
    slika.classList = "far fa-star";
    slika.onmouseover = function () { this.style.cursor = "pointer"; }
    slika.onmouseout = function () { this.style.cursor = "context-menu"; }
    slika.onclick = function () { this.classList.toggle("fas") }
    omiljeno.appendChild(slika);
    utakmica.appendChild(vreme);
    utakmica.appendChild(naziv_utakmica);
    utakmica.appendChild(omiljeno);
    utakmice.appendChild(utakmica);

    linija = document.createElement("hr");
    linija.classList = "sidebar-divider d-none d-md-block col-8";
    utakmice.appendChild(linija);

    utakmica = document.createElement("div");
    utakmica.classList = "utakmica row";
    vreme = document.createElement("div");
    vreme.classList = "datum-vreme col-3";
    labv = document.createElement("label");
    labv.innerHTML = "23.12.2020 20:45";
    vreme.appendChild(labv);
    naziv_utakmica = document.createElement("div");
    naziv_utakmica.classList = "timovi col-5";
    labt = document.createElement("label");
    labt.classList = "labeltext";
    labt.innerHTML = "Milan - Lazio";
    naziv_utakmica.appendChild(labt);
    omiljeno = document.createElement("div");
    omiljeno.classList = "omiljena col-1";
    slika = document.createElement("i");
    slika.classList = "far fa-star";
    slika.onmouseover = function () { this.style.cursor = "pointer"; }
    slika.onmouseout = function () { this.style.cursor = "context-menu"; }
    slika.onclick = function () { this.classList.toggle("fas") }
    omiljeno.appendChild(slika);
    utakmica.appendChild(vreme);
    utakmica.appendChild(naziv_utakmica);
    utakmica.appendChild(omiljeno);
    utakmice.appendChild(utakmica);

    linija = document.createElement("hr");
    linija.classList = "sidebar-divider d-none d-md-block col-8";
    utakmice.appendChild(linija);

    utakmica = document.createElement("div");
    utakmica.classList = "utakmica row";
    vreme = document.createElement("div");
    vreme.classList = "datum-vreme col-3";
    labv = document.createElement("label");
    labv.innerHTML = "23.12.2020 20:45";
    vreme.appendChild(labv);
    naziv_utakmica = document.createElement("div");
    naziv_utakmica.classList = "timovi col-5";
    labt = document.createElement("label");
    labt.classList = "labeltext";
    labt.innerHTML = "Milan - Lazio";
    naziv_utakmica.appendChild(labt);
    omiljeno = document.createElement("div");
    omiljeno.classList = "omiljena col-1";
    slika = document.createElement("i");
    slika.classList = "far fa-star";
    slika.onmouseover = function () { this.style.cursor = "pointer"; }
    slika.onmouseout = function () { this.style.cursor = "context-menu"; }
    slika.onclick = function () { this.classList.toggle("fas") }
    omiljeno.appendChild(slika);
    utakmica.appendChild(vreme);
    utakmica.appendChild(naziv_utakmica);
    utakmica.appendChild(omiljeno);
    utakmice.appendChild(utakmica);

    cont.appendChild(utakmice);

    var lige = document.getElementById("lige");
    lige.appendChild(cont);
}

function hoveritem(id) {
    id.style.cursor = "pointer";
}

function leaveitem(id) {
    id.style.cursor = "context-menu";
}

function izabrana(id) {
    id.classList.toggle("fas");
}