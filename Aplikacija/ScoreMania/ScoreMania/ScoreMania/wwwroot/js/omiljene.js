function hoveritem(id) {
    id.style.cursor = "pointer";
}

function leaveitem(id) {
    id.style.cursor = "context-menu";
}

function izabrana(id) {
    id.classList.toggle("fas");
    id.classList.toggle("far");
}

$("label").click(function () {
    $(this).parent().find("label").css({ "background-color": "#D8D8D8" });
    $(this).css({ "background-color": "#7ED321" });
    $(this).nextAll().css({ "background-color": "#7ED321" });
});