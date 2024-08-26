let datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url": "/Admin/Catalago/ObtenerTodos"
        },
        "columns": [
            { "data": "nombre", "width": "20%" },
            { "data": "descripcion", "width": "30%"},
            {
                "data": "inHomePage",
                "render": function (data) {
                    if (data == true) {
                        return "Activo";
                    } else {
                        return "Inactivo";
                    }
                }, "width": "10%"
            },
            {
                "data": "fechaInicio",
                "render": function (data) {
                    return formatDate(data);
                },
                "width": "10%"
            },
            {
                "data": "fechaFinal",
                "render": function (data) {
                    return formatDate(data);
                },
                "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                                <a href="/Admin/Catalago/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                <a onclick=Delete("/Admin/Catalago/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="bi bi-trash3-fill"></i>
                                </a>
                            </div>

                    `;
                }, "width": "20%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay datos disponibles en la tabla",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ entradas totales",
            "infoEmpty": "Mostrando de 0 a 0 en 0 entradas",
            "infoFiltered": "(Filtrado de MAX entradas totales)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "",
            "search": "Buscar:",
            "zeroRecords": "No se encontraron registros coincidentes",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            "aria": {
                "orderable": "Ordenar por esta columna",
                "orderableReverse": "Ordena al revés por esta columna"
            }
        }
    });
}

function Delete(url) {
    swal({
        title: "Seguro que quieres eliminar el catalago?",
        text: "Este registro no se podra recuperar",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        //actualizar la tabla
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

function formatDate(dateString) {
    if (!dateString) return "";
    var date = new Date(dateString);
    var dd = String(date.getDate()).padStart(2, '0');
    var mm = String(date.getMonth() + 1).padStart(2, '0'); // Enero es 0!
    var yyyy = date.getFullYear();
    return dd + '/' + mm + '/' + yyyy;
}
