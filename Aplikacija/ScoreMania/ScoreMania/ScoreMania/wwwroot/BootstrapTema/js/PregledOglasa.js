$("#PosaljiP").click(function () {
        $("#ValidateForm").validate({
            onfocusout: false,
            onkeydown: false,
            onkeyup: false,
            onclick: false,
            focusInvalid: false,
            rules: {
                TekstPoruke: {
                    required: true
                }
            },
            messages: {
                TekstPoruke: {
                    required: "Poruka mora sadržati minimum 1 karakter"
                }
            }
        });
});

$("#PosaljiPoruku").click(function () {
    $('.posalji').show();
    $("#PosaljiP").show();
    $("#PosaljiPoruku").hide();
});

$("#CancelPosalji").click(function () {
    $('.posalji').hide();
    $("#PosaljiP").hide();
    $("#PosaljiPoruku").show();
});