$('#tableAdminUsers').on('click-row.bs.table', async (row, $element, field) => {
    const name = field[0].children[0].innerText;
    Swal.fire({
        icon: 'question',
        title: `Usuario ${name}`,
        text: `¿Qué deseas hacer?`,
        reverseButtons: true,
        showCancelButton: true,
        confirmButtonText:"Editar",
        cancelButtonText:"Eliminar",
        confirmButtonColor: "#67E7CF",
        cancelButtonColor:"#E74C3C",
        allowOutsideClick: false,
        allowEscapeKey: false,
        showCloseButton: true,
        closeButtonHtml: '<i class="fas fa-times"></i>',
    }).then((result) => {
        if (result.value) {
                window.location.replace(`${apihost}/Admin/EditRIU?email=${field[0].children[1].innerText}`);
        }
        else if(result.dismiss === Swal.DismissReason.cancel){
            Swal.fire({
                icon: 'error',
                title: `${name} se eliminará`,
                text: `¿Seguro que deseas eliminarlo?`,
                reverseButtons: true,
                showCancelButton: true,
                confirmButtonText:"Si, Eliminar",
                cancelButtonText:"No, Cancelar",
                confirmButtonColor:"#E74C3C",
                cancelButtonColor: "#67E7CF",
                allowOutsideClick: false,
                allowEscapeKey: false,
                showCloseButton: true,
                closeButtonHtml: '<i class="fas fa-times"></i>',
            }).then((result) => {
                if(result.value){
                    $.ajax({
                        url: apihost + "/Admin/DeleteRIU",
                        method: "POST",
                        data: {
                          "email":`${field[0].children[1].innerText}`
                        },
                        async: true,
                        dataType: "json",
                        success: function (respuesta){
                            Swal.fire({
                                icon: 'success',
                                title: '¡Éxito!',
                                text: `El usuario ${name} ha sido eliminado`,
                                onClose: ()=>{
                                    location.reload(true);
                                }
                            }).then((result) => {
                                if (result.value) {
                                    location.reload(true);
                                }
                            })
                        }
                    });
                }
                else{
                    Swal.Close();
                }
            });
        }
    })
});
