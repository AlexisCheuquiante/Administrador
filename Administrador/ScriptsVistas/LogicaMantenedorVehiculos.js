$(document).ready(function () {

    ObtenerRecintos();
   
});

function ObtenerRecintos() {
    $.ajax({
        url: window.urlObtenerRecintos,
        type: 'POST',
        success: function (data) {
            $('#cmbRecinto').dropdown('clear');
            $('#cmbRecinto').empty();
            $('#cmbRecinto').append('<option value="-1">[Seleccione]</option>');
            $.each(data,
                function (value, item) {

                    var texto = '<option value="' + item.Id + '">' + item.NombreRecinto + '</option>';
                    $('#cmbRecinto').append(texto);

                }
            );

        },
        error: function () {
            alert('Error al cargar los recintos');
        }
    });

}