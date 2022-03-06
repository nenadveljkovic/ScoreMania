$("#Save").click(function () {
    $("#oglasForm").validate({
        onfocusout: false,
        onkeydown: false,
        onkeyup: false,
        onclick: false,
        focusInvalid: false,
        rules: {
            grad: {
                required: true,
                minlength: 2,
                maxlength: 20
            },
            adresa: {
                required: true,
                minlength: 5,
                maxlength: 30
            },
            cena: {
                required: true,
                minvalue: 0,
                maxvalue: 9999
            },
            kvadratura: {
                required: true
            },
            BrojSoba: {
                required: true
            },
            opis: {
                required: true,
                minlength: 10,
                maxlength: 2000
            }          
        },
        messages: {
            grad: {
                required: "Grad je obavezno polje",
                minlength: "Grad mora sadržati minimum 2 karaktera",
                maxlength: "Grad može imati najviše 20 karaktera"
            },
            adresa: {
                required: "Adresa je obavezno polje",
                minlength: "Adresa mora sadržati minimum 5 karaktera",
                maxlength: "Adresa može imati najviše 30 karaktera"
            },
            cena: {
                required: "Cena je obavezno polje",
                minvalue: "Minimalna vrednost za ovo polje je 0",
                maxvalue: "Maksimalna vrednost za pvp polje je 9999"
            },
            kvadratura: {
                required: "Kvadratura je obavezno polje"
            },
            username: {
                required: "Korisničko ime je obavezno polje",
                minlength: "Korisničko ime mora sadržati minimum 5 karaktera",
                maxlength: "Korisničko ime može imati najviše 30 karaktera",
            },
            BrojSoba: {
                required: "Broj soba je obavezno polje"
            },
            opis: {
                required: "Opis je obavezno polje",
                minlength: "Opis mora sadržati minimum 10 karaktera",
                maxlength: "Opis može imati najviše 2000 karaktera"
            }
        }
    });

    $(".form-control").keydown(function () {
        $(this).off('validate');
    })

    $(".form-control").keypress(function () {
        $(this).off('validate');
    })

    $(".form-control").keyup(function () {
        $(this).off('validate');
    })

    $("#grad").focusin(function () {
        $("#grad-error").remove();
    });
    $("#adresa").focusin(function () {
        $("#adresa-error").remove();
    });
    $("#cena").focusin(function () {
        $("#cena-error").remove();
    });
    $("#kvadratura").focusin(function () {
        $("#kvadratura-error").remove();
    });
    $("#BrojSoba").focusin(function () {
        $("#BrojSoba-error").remove();
    });
    $("#opis").focusin(function () {
        $("#opis-error").remove();
    });
    $("#form-control").focusin(function () {
        $("#error").remove();
    });
});

