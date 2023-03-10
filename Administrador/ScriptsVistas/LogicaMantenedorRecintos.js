var _arrayTiposPropiedades = [];

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