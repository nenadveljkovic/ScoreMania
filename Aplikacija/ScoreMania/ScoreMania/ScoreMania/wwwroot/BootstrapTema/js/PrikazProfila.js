$("#Save").click(function () {
    $("#signupForm").validate({
        onfocusout: false,
        onkeydown: false,
        onkeyup: false,
        onclick: false,
        focusInvalid: false,
        rules: {
            ime: {
                required: true,
                minlength: 3,
                maxlength: 20
            },
            prezime: {
                required: true,
                minlength: 3,
                maxlength: 20
            },
            email: {
                required: true,
                email: true
            },
            datumrodjenja: {
                required: true,
                date: true
            },
            username: {
                required: true,
                minlength: 5,
                maxlength: 30,
                usernamecheck: true
            },
            password: {
                required: true,
                minlength: 5,
                maxlength: 30
            },
            rpassword: {
                required: true,
                minlength: 5,
                maxlength: 30,
                equalTo: '[name="password"]'
            },
            pol: {
                required: true
            }
        },
        messages: {
            ime: {
                required: "Ime je obavezno polje",
                minlength: "Ime mora sadržati minimum 3 karaktera",
                maxlength: "Ime može imati najviše 20 karaktera"
            },
            prezime: {
                required: "Prezime je obavezno polje",
                minlength: "Prezime mora sadržati minimum 3 karaktera",
                maxlength: "Prezime može imati najviše 20 karaktera"
            },
            email: {
                required: "Email je obavezno polje",
                email: "Uneta vrednost nije email"
            },
            datumrodjenja: {
                required: "Datum rođenja je obavezno polje",
                date: "Uneta vrednost nije datum"
            },
            username: {
                required: "Korisničko ime je obavezno polje",
                minlength: "Korisničko ime mora sadržati minimum 5 karaktera",
                maxlength: "Korisničko ime može imati najviše 30 karaktera",
            },
            password: {
                required: "Šifra je obavezno polje",
                minlength: "Šifra mora sadržati minimum 5 karaktera",
                maxlength: "Šifra može imati najviše 30 karaktera"
            },
            rpassword: {
                required: "Šifra je obavezno polje",
                minlength: "Šifra mora sadržati minimum 5 karaktera",
                maxlength: "Šifra može imati najviše 30 karaktera",
                equalTo: "Unete šifre se ne poklapaju"
            },
            pol: {
                required: "Pol je obavezno polje"
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

    $("#ime").focusin(function () {
        $("#ime-error").remove();
    });
    $("#prezime").focusin(function () {
        $("#prezime-error").remove();
    });
    $("#datumrodjenja").focusin(function () {
        $("#datumrodjenja-error").remove();
    });
    $("#email").focusin(function () {
        $("#email-error").remove();
    });
    $("#username").focusin(function () {
        $("#username-error").remove();
    });
    $("#password").focusin(function () {
        $("#password-error").remove();
    });
    $("#rpassword").focusin(function () {
        $("#rpassword-error").remove();
    });
    $("#pol").focusin(function () {
        $("#pol-error").remove();
    });

    jQuery.validator.addMethod("usernamecheck", function (value, element) {
        var pom = true;
        $("#lista option").each(function () {
            if (value == $(this).val())
                pom = false;
        });
        return pom;
    }, "Korisničko ime je zauzeto.");
});


$("#Edit").click(function () {
    $('.labelepodaci').hide();
    $('.poljapodaci').show();
    $("#Save").show();
    $("#Cancel").show();
    $("#Edit").hide();
});

$("#Cancel").click(function () {
    $('.labelepodaci').show();
    $('.poljapodaci').hide();
    $("#Edit").show();
    $("#Save").hide();
    $("#Cancel").hide();
});

