var _movilidadReducida = [];
var _menorEdad = [];
var _idPersona = 0;

$(document).ready(function () {
   
    ObtenerTipoInquilino();

    $('#checkMovilidadReducida').change(function () {

        if ($('#checkMovilidadReducida').is(':checked')) {
            _movilidadReducida = true;
        }
        else {
            _movilidadReducida = false;
        }

    });
    $('#checkMenorEdad').change(function () {

        if ($('#checkMenorEdad').is(':checked')) {
            _menorEdad = true;
        }
        else {
            _menorEdad = false;
        }

    });
});

function ObtenerTipoInquilino() {
    $.ajax({
        url: window.urlObtenerTipoInquilino,
        type: 'POST',
        success: function (data) {
            $('#cmbTipoInquilino').dropdown('clear');
            $('#cmbTipoInquilino').empty();
            $('#cmbTipoInquilino').append('<option value="-1">[Seleccione]</option>');
            $.each(data,
                function (value, item) {

                    var texto = '<option value="' + item.Id + '">' + item.Descripcion + '</option>';
                    $('#cmbTipoInquilino').append(texto);

                }
            );

        },
        error: function () {
            alert('Error al cargar la consulta');
        }
    });

}
function GuardarPersona() {

    //if (ValidaGuardar() === false) {
    //    //alert('no valido');
    //    return;
    //}

    $('#btnGuardar').addClass('loading');
    $('#btnGuardar').addClass('disabled');
    $('#btnVolver').addClass('loading');
    $('#btnVolver').addClass('disabled');

    var strParams = {
        Id: _idPersona,
        Rut: $('#txtRUT').val(),
        Nombre: $('#txtNombreCompleto').val(),
        Fecha_Nacimiento: $('#txtFechaNacimiento').val(),
        Correo: $('#txtCorreo').val(),
        Telefono_1: $('#txtTelefono1').val(),
        Telefono_2: $('#txtTelefono2').val(),
        Es_Movilidad_Reducida: _movilidadReducida,
        Es_Menor_Edad: _menorEdad,
        Tiin_Id: $('#cmbTipoInquilino').val(),
        Piso: $('#txtPiso').val(),
        Depto: $('#txtDepto').val(),
        Casa: $('#txtCasa').val(),
    };

    $.ajax({
        url: window.urlInsertarPersona,
        type: 'POST',
        data: { entity: strParams },
        success: function (data) {
            if (data === 'exito') {
                $('#divExito').removeClass("hidden");
                setTimeout(() => { location.reload(); }, 2000);
            }
            if (data === 'error') {

                $('#btnGuardar').removeClass('loading');
                $('#btnGuardar').removeClass('disabled');
                $('#btnVolver').removeClass('loading');
                $('#btnVolver').removeClass('disabled');
                $('#divError').removeClass("hidden");
            }

        },
        error: function (ex) {
            alert('Error al guardar el producto');
        }
    });

}