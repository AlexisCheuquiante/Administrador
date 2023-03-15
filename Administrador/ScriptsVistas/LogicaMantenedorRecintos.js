var _arrayTiposPropiedades = [];
var _idRecinto = 0;

$(document).ready(function () {

    ObtenerRegiones();
    ObtenerComunas();
    ObtenerTiposPropiedades();
    $('#cmbTipo').change(function () {
        var idTipoPropiedad = $('#cmbTipo').val();

        if (idTipoPropiedad == 1) {

            $('#divtxtPisos').show();
            $('#divtxtCasas').hide();
            $('#divtxtViviendas').hide();
        }
        if (idTipoPropiedad == 2) {
            $('#divtxtCasas').show();
            $('#divtxtPisos').hide();
            $('#divtxtViviendas').hide();
        }
        if (idTipoPropiedad == 3) {
            $('#divtxtViviendas').show();
            $('#divtxtPisos').hide();
            $('#divtxtCasas').hide();
        }
    });
});

function ObtenerRegiones() {
    $.ajax({
        url: window.urlObtenerRegiones,
        type: 'POST',
        success: function (data) {
            $.each(data,
                function (value, item) {

                    var texto = '<option value="' + item.Id + '">' + item.NombreRegion + '</option>';
                    $('#cmbRegion').append(texto);

                }
            );

        },
        error: function () {
            alert('Error al cargar las regiones');
        }
    });

}
function ObtenerComunas() {

    var strParams = {

        Reg_Id: $('#cmbRegion').val(),
    };

    $.ajax({
        url: window.urlObtenerComunas,
        type: 'POST',
        data: { entity: strParams },
        success: function (data) {
            $('#cmbComuna').dropdown('clear');
            $('#cmbComuna').empty();
            $('#cmbComuna').append('<option value="-1">[Seleccione]</option>');


            $.each(data,
                function (value, item) {

                    var texto = '<option value="' + item.Id + '">' + item.NombreComuna + '</option>';
                    $('#cmbComuna').append(texto);


                }
            );

        },
        //error: function () {
        //    alert('Error al cargar los copropietarios existentes');
        //}
    });
}
function ObtenerTiposPropiedades() {
    $.ajax({
        url: window.urlObtenerTiposPropiedades,
        type: 'POST',
        success: function (data) {
            $('#cmbTipo').dropdown('clear');
            $('#cmbTipo').empty();
            $('#cmbTipo').append('<option value="-1">[Seleccione]</option>');
            $.each(data,
                function (value, item) {

                    var texto = '<option value="' + item.Id + '">' + item.Descripcion + '</option>';
                    $('#cmbTipo').append(texto);
                    _arrayTiposPropiedades.push(item);
                }
            );

        },
        error: function () {
            alert('Error al cargar los tipos de propiedades');
        }
    });

}
function GuardarRecinto() {

    //if (ValidaGuardar() === false) {
    //    //alert('no valido');
    //    return;
    //}

    $('#btnGuardar').addClass('loading');
    $('#btnGuardar').addClass('disabled');
    $('#btnVolver').addClass('loading');
    $('#btnVolver').addClass('disabled');

    var strParams = {
        Id: _idRecinto,
        Tipo_Id: $('#cmbTipo').val(),
        NombreRecinto: $('#txtNombreRecinto').val(),
        DireccionRecinto: $('#txtDireccion').val(),
        Reg_Id: $('#cmbRegion').val(),
        Com_Id: $('#cmbComuna').val(),
        Pisos: $('#txtPisos').val(),
        Casas: $('#txtCasas').val(),
        Viviendas: $('#txtViviendas').val(),
    };

    $.ajax({
        url: window.urlInsertarRecinto,
        type: 'POST',
        data: { entity: strParams },
        success: function (data) {
            if (data === 'exito') {
                $('#divExito').removeClass("hidden");
                setTimeout(() => { window.location.href = '/MantenedorRecintos' }, 2000);
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
            alert('Error al guardar el recinto');
        }
    });

}
function ObtenerRecintos(id) {
    $('#idRecintoSeleccionado').val(id);

    id = $('#idRecintoSeleccionado').val();
    $.ajax({
        url: window.urlObtenerRecintos,
        type: 'POST',
        data: { id: id },
        success: function (data) {
            $("#cmbRegion").dropdown('set selected', data.Reg_Id);
            $("#cmbComuna").dropdown('set selected', data.Com_Id);
            $("#cmbTipo").dropdown('set selected', data.Tipo_Id);
            $('#txtNombreRecinto').val(data.NombreRecinto)
            $('#txtDireccion').val(data.DireccionRecinto);
            $('#txtPisos').val(data.Pisos)
            $('#txtCasas').val(data.Casas)
            $('#txtViviendas').val(data.Viviendas)
            
        },

        error: function () {
            alert('Error al cargar el vehiculo seleccionado');
        }
    });
    _idRecinto = id;
}
function PreparaEliminarRecinto(id) {
    $('#idRecintoSeleccionado').val(id);

}
function EliminaRecinto() {
    $('#btnEliminar').addClass('loading');
    $('#btnEliminar').addClass('disabled');

    id = $('#idRecintoSeleccionado').val();
    $.ajax({
        url: window.urlEliminarRecinto,
        type: 'POST',
        data: { id: id },
        success: function (data) {
            if (data === 'exito') {
                $('#divConsultaElimina').addClass("hidden");
                $('#divExitoElimina').removeClass("hidden");
                setTimeout(() => { window.location.href = '/MantenedorRecintos' }, 2000);
            }
        },
        error: function (data) {
            console.log(data);
            showMessage('body', 'danger', 'Ocurrió un error al eliminar el usuario seleccionado.' + data);
        }
    });
}