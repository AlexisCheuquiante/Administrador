var _idVehiculo = 0;

$(document).ready(function () {

    ObtenerTipoVehiculo();
    ObtenerPersonas();
    ObtenerJefeHogar();
});

function ObtenerTipoVehiculo() {
    $.ajax({
        url: window.urlObtenerTipoVehiculo,
        type: 'POST',
        success: function (data) {
            $('#cmbTipoVehiculo').dropdown('clear');
            $('#cmbTipoVehiculo').empty();
            $('#cmbTipoVehiculo').append('<option value="-1">[Seleccione]</option>');
            $.each(data,
                function (value, item) {

                    var texto = '<option value="' + item.Id + '">' + item.Descripcion + '</option>';
                    $('#cmbTipoVehiculo').append(texto);

                }
            );

        },
        error: function () {
            alert('Error al cargar los tipos de vehiculos');
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
function ObtenerPersonas() {

    $.ajax({
        url: window.urlObtenerPersonas,
        type: 'POST',
        success: function (data) {
            $('#cmbCopropietario').dropdown('clear');
            $('#cmbCopropietario').empty();
            $('#cmbCopropietario').append('<option value="-1">[Seleccione]</option>');
            $.each(data,
                function (value, item) {

                    var texto = '<option value="' + item.Id + '">' + item.Nombre + '</option>';
                    $('#cmbCopropietario').append(texto);


                }
            );

        },
        //error: function () {
        //    alert('Error al cargar los copropietarios existentes');
        //}
    });
}
function GuardarVehiculo() {

    //if (ValidaGuardar() === false) {
    //    //alert('no valido');
    //    return;
    //}

    $('#btnGuardar').addClass('loading');
    $('#btnGuardar').addClass('disabled');
    $('#btnVolver').addClass('loading');
    $('#btnVolver').addClass('disabled');

    var strParams = {
        Id: _idVehiculo,
        Patente: $('#txtPatente').val(),
        Per_Id: $('#cmbCopropietario').val(),
        Tive_Id: $('#cmbTipoVehiculo').val(),
        Observacion: $('#txtObservación').val(),
        Estacionamiento: $('#txtEstacionamiento').val(),
    };

    $.ajax({
        url: window.urlInsertarVehiculo,
        type: 'POST',
        data: { entity: strParams },
        success: function (data) {
            if (data === 'exito') {
                $('#divExito').removeClass("hidden");
                setTimeout(() => { window.location.href = '/MantenedorVehiculos?limpiar=1' }, 2000);
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

            window.location.href = '/MantenedorVehiculos';

        },
        error: function () {
            showMessage('#divMensajePublicacionViaje', 'danger', 'Ocurrió un error al guardar la información. Por favor intente nuevamente.');
            //hideLoading();
        }
    });

}
function ObtenerVehiculos(id) {
    $('#idVehiculoSeleccionado').val(id);

    id = $('#idVehiculoSeleccionado').val();
    $.ajax({
        url: window.urlObtenerVehiculos,
        type: 'POST',
        data: { id: id },
        success: function (data) {
            $("#cmbRecinto").dropdown('set selected', data.Res_Id);
            $("#cmbTipoVehiculo").dropdown('set selected', data.Tive_Id);
            $('#txtPatente').val(data.Patente)
            $('#txtEstacionamiento').val(data.Estacionamiento);
            $('#txtObservación').val(data.Observacion)
            $("#cmbCopropietario").dropdown('set selected', data.Per_Id);
            $('#divtxtCopropietario').show()
            $('#txtCopropietario').val(data.NombrePersona)
        },

        error: function () {
            alert('Error al cargar el vehiculo seleccionado');
        }
    });
    _idVehiculo = id;
}
function PreparaEliminarVehiculo(id) {
    $('#idVehiculoSeleccionado').val(id);

}
function EliminaVehiculo() {
    $('#btnEliminar').addClass('loading');
    $('#btnEliminar').addClass('disabled');

    id = $('#idVehiculoSeleccionado').val();
    $.ajax({
        url: window.urlEliminarVehiculo,
        type: 'POST',
        data: { id: id },
        success: function (data) {
            if (data === 'exito') {
                $('#divConsultaElimina').addClass("hidden");
                $('#divExitoElimina').removeClass("hidden");
                setTimeout(() => { window.location.href = '/MantenedorVehiculos?actualizar=1' }, 2000);
            }
        },
        error: function (data) {
            console.log(data);
            showMessage('body', 'danger', 'Ocurrió un error al eliminar el usuario seleccionado.' + data);
        }
    });
}