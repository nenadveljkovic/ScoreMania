$("#PosaljiP").click(function () {
    $("#ValidateForm").validate({
        onfocusout: false,
        onkeydown: false,
        onkeyup: false,
        onclick: false,
        focusInvalid: false,
        rules: {
            TextPoruke: {
                required: true
            }
        },
        messages: {
            TextPoruke: {
                required: "Poruka mora sadržati minimum 1 karakter"
            }
        }
    });
});