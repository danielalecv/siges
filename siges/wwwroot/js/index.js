$('#btnLogin').on("click", async ()=>{
    const { value: formValues } = await Swal.fire({
        title:"Ingresar a SIGES",
        showCloseButton: true,
        allowOutsideClick: false,
        confirmButtonText:"Entrar",
        confirmButtonColor: "#67E7CF",
        customClass:{
            confirmButton:"btn-block"
        },
        html:`<form class="padd-R-L-5px">
            <div class="form-group text-left">
                <label for="inputUser" class="col-form-label">Usuario</label>
                <input type="email" class="form-control form-control-lg" id="inputUser" placeholder="Correo electrónico" />
            </div>
            <div class="form-group text-left">
                <label for="inputPassword">Contraseña</label>
                <div class="input-group mb-2">
                    <input type="password" class="form-control form-control-lg" id="inputPassword" placeholder="Contraseña" />
                    <div class="input-group-append">
                        <button id="showPassword" class="btn input-group-text" type="button"><i class="fas fa-eye"></i></button >
                    </div>
                </div>
            </div>
        </form>`,
        footer: `<button id="forgetPassword" type="button" class="btn btn-link">Olvidé mi contraseña</button>`,
        preConfirm: () => {
            let user = $("#inputUser").val();
            let password = $("#inputPassword").val();
            if(user==="" || password===""){
                Swal.showValidationMessage("Algún campo esta vacío");
            }
            return [user, password]
        },
        onRender: ()=>{
            $('#showPassword').on('mousedown', () => {
                $('#inputPassword').attr("type", "text");
            });
            $('#showPassword').on('mouseup mouseleave', () => {
                $('#inputPassword').attr("type", "password");
            });
            $("#forgetPassword").on("click", () =>{
                Swal.close();
                forgetPassword();
            });
        }
    });
    if (formValues) {
        Swal.fire(JSON.stringify(formValues));
    }
});


const forgetPassword = async () => {
    const { value: formValues } = await Swal.fire({
        title:"Recuperar Contraseña",
        showCloseButton: true,
        allowOutsideClick: false,
        confirmButtonText:"Enviar",
        confirmButtonColor: "#67E7CF",
        customClass:{
            confirmButton:"btn-block"
        },
        html:`<form class="padd-R-L-5px">
            <div class="form-group text-left">
                <label for="inputEmail" class="col-form-label">Correo Electrónico</label>
                <input type="email" class="form-control form-control-lg" id="inputEmail" placeholder="Correo electrónico" />
            </div>
        </form>`,
        preConfirm: () => {
            let email = $("#inputEmail").val();
            if(email===""){
                Swal.showValidationMessage("Algún campo esta vacío");
            }
            return email
        }
    });
    if (formValues) {
        Swal.fire(JSON.stringify(formValues));
    }
}

$('#btnRegister').on("click", () => {
    Swal.mixin({
        width: "50rem",
        title: "Registro a SIGES",
        confirmButtonText: 'Continuar',
        confirmButtonColor: "#67E7CF",
        showCancelButton: true,
        reverseButtons: true,
        progressSteps: ['1', '2', '3'],
        showCloseButton: true,
        allowOutsideClick: false,
        customClass:{
            cancelButton: 'btn-secondary'
        }
    }).queue([{
        

        },
        'Question 2',
        'Question 3'
    ]).then((result) => {
        if (result.value) {
          const answers = JSON.stringify(result.value)
          Swal.fire({
            title: 'All done!',
            html: `
              Your answers:
              <pre><code>${answers}</code></pre>
            `,
            confirmButtonText: 'Lovely!'
          })
        }
    });
});


// ------------------------------------------------------- //
// Homepage Slider
// ------------------------------------------------------ //
$('.homepage').owlCarousel({
    loop: true,
    margin: 0,
    dots: true,
    nav: false,
    autoplay: true,
    smartSpeed: 1000,
    addClassActive: true,
    navText: [
        "<i class='fa fa-angle-left'></i>",
        "<i class='fa fa-angle-right'></i>"
    ],
    responsiveClass: true,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 1
        },
        1000: {
            items: 1,
            loop: true
        }
    }
});
// ------------------------------------------------------- //
// Counter
// ------------------------------------------------------ //
$('.counter').counterUp({
    delay: 10,
    time: 1000
});