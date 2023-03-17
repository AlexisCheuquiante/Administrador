var _movilidadReducida = [];
var _menorEdad = [];
var _idPersona = 0;

$(document).ready(function () {
   
    ObtenerTipoInquilino();
    ObtenerJefeHogar();

    var input1 = document.getElementById("txtRUT");

    input1.addEventListener("keypress", function (event) {
        if (event.key === "Enter") {

            FormateaRut()

        }
    });

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
function ObtenerJefeHogar() {
    $.ajax({
        url: window.urlObtenerJefeHogar,
        type: 'POST',
        success: function (data) {
            $('#cmbFiltroJefeHogar').dropdown('clear');
            $('#cmbFiltroJefeHogar').empty();
            $('#cmbFiltroJefeHogar').append('<option value="-1">[Seleccione]</option>');
            $.each(data,
                function (value, item) {

                    var texto = '<option value="' + item.Id + '">' + item.JefeHogar + '</option>';
                    $('#cmbFiltroJefeHogar').append(texto);

                }
            );

        },
        error: function () {
            alert('Error al cargar la consulta');
        }
    });

}
function BusquedaFiltro() {
    $('#btnBuscarFiltro').addClass("loading");
    $('#btnBuscarFiltro').addClass("disabled");

    var entity = {
        Per_Id: $('#cmbFiltroJefeHogar').val(),
    }
    $.ajax({
        url: window.urlBusquedaFiltro,
        type: 'POST',
        data: { entity: entity },
        success: function (data) {

            window.location.href = '/AdminPersonas?actualizar=1';

        },
        error: function () {
            showMessage('#divMensajePublicacionViaje', 'danger', 'Ocurrió un error al guardar la información. Por favor intente nuevamente.');
            //hideLoading();
        }
    });

}
function FormateaRut() {

    var strParams = {

        Rut: $('#txtRUT').val(),
    };

    $.ajax({
        url: window.urlFormateaRut,
        type: 'POST',
        data: { entity: strParams },
        success: function (data) {
            $('#txtRUT').val(data)

        },
        //error: function () {
        //    alert('Error al cargar los copropietarios existentes');
        //}
    });
}
function ObtenerPersonas(id) {
    $('#idPersonaSeleccionada').val(id);

    id = $('#idPersonaSeleccionada').val();
    $.ajax({
        url: window.urlObtenerPersonas,
        type: 'POST',
        data: { id: id },
        success: function (data) {
            $('#txtRUT').val(data.Rut)
            $('#txtFechaNacimiento').val(data.Fecha_Nacimiento_Mostrar);
            $('#txtNombreCompleto').val(data.Nombre)
            $('#txtCorreo').val(data.Correo)
            $('#txtTelefono1').val(data.Telefono_1)
            $('#txtTelefono2').val(data.Telefono_2)
            if (data.Es_Movilidad_Reducida == true) {

                document.getElementById('checkMovilidadReducida').checked = true;
                
            }
            if (data.Es_Menor_Edad == true) {

                document.getElementById('checkMenorEdad').checked = true;
                
            }
            $("#cmbTipoInquilino").dropdown('set selected', data.Tiin_Id);
            $('#txtPiso').val(data.Piso)
            $('#txtDepto').val(data.Depto)
            $('#txtCasa').val(data.Casa)
        },

        error: function () {
            alert('Error al cargar el vehiculo seleccionado');
        }
    });
    _idPersona = id;
}
function PreparaEliminarPersona(id) {
    $('#idPersonaSeleccionada').val(id);

}
function EliminaPersona() {
    $('#btnEliminar').addClass('loading');
    $('#btnEliminar').addClass('disabled');

    id = $('#idPersonaSeleccionada').val();
    $.ajax({
        url: window.urlEliminarPersona,
        type: 'POST',
        data: { id: id },
        success: function (data) {
            if (data === 'exito') {
                $('#divConsultaElimina').addClass("hidden");
                $('#divExitoElimina').removeClass("hidden");
                setTimeout(() => { window.location.href = '/AdminPersonas?limpiar=1' }, 2000);
            }
        },
        error: function (data) {
            console.log(data);
            showMessage('body', 'danger', 'Ocurrió un error al eliminar el usuario seleccionado.' + data);
        }
    });
}