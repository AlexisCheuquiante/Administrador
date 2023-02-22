$(document).ready(function () {

    var input1 = document.getElementById("txtUsuario");
    var input2 = document.getElementById("txtPassword");

    input1.addEventListener("keypress", function (event) {
        if (event.key === "Enter") {
            $("#txtPassword").focus();

        }
    });

    input2.addEventListener("keypress", function (event) {
        if (event.key === "Enter") {
            event.preventDefault();
            document.getElementById("btnLogin").click();
        }
    });
});

function ValidarUsuario() {

    if ($('#txtUsuario').val() == '' || $('#txtPassword').val() == '') {
        $('#divErroLogin').removeClass("hidden");
        return;
    }


    $('#btnLogin').addClass("loading");
    $('#btnLogin').addClass("disabled");

    var strParams = {
        Usuario: $('#txtUsuario').val(),
        Password: $('#txtPassword').val()
    };

    $.ajax({
        url: window.urlValidaUsuario,
        type: 'POST',
        data: { entity: strParams },
        success: function (data) {
            if (data === 'exito') {
                window.location.href = "/home/index";
            }
            if (data === 'error') {
                $('#divErroLogin').removeClass("hidden");
            }

        },
        error: function () {
            alert('Error al cargar los Tipos');
        }
    });

}