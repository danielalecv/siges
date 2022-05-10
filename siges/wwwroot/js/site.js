// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Campos a mayúsculas
Array.prototype.forEach.call(document.querySelectorAll('input'), function(current,index,list){
  if(current.type != "hidden" && current.type != "button" && current.type != "submit" && current.placeholder != "Buscar" && current.placeholder != "Search"){
    if(current.type == "text"){
      if(current.attributes.getNamedItem('class').nodeValue.indexOf('datetimepicker-input') < 0 && current.attributes.getNamedItem('class').nodeValue.indexOf('bootstrap-table-filter-control-fecha') < 0 && current.attributes.getNamedItem('class').nodeValue.indexOf('platformSettings') < 0){
        const regex = /[a-zA-ZÀ-ÿ\u00f1\u00d10-9,.#:(\)]*(\s*[a-zA-ZÀ-ÿ\u00f1\u00d10-9.,#:(\)]*\s+)*[a-zA-ZÀ-ÿ\u00f1\u00d10-9.,#:(\)]+/;
        current.addEventListener('blur', event => {
          var match = regex.exec(current.value);
          current.value = "";
          if(match != null){
            current.value = match[0].toUpperCase().substring(0, 1200);
          } else {
            current.placeholder="Ingresó caracteres no válidos";
          }
        });
      }
    } else if(current.type == "textarea"){
    } else if(current.type == "number"){
    } else if(current.type == "date"){
    } else if(current.type == "email"){
    }
  }
});

const apihost = window.location.protocol + '//' + window.location.host;


$(document).ready(function () {
    $("#sidebar").mCustomScrollbar({
        theme: "minimal"
    });

    if($(window).width() <= 575.98){
      $('.navbar-toggler').attr("style", "display:none;");
      $('#btnCloseSideBar').removeAttr("hidden");
    }
    
    if($(window).width() <= 768){
      $('#sidebar, #content').addClass('active');
      $('.home-carousel').attr("style", "display:none;");
      setTimeout(function(){ $('.home-carousel').removeAttr( "style" );}, 500); 
    }

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar, #content').toggleClass('active');
        $('.collapse.in').toggleClass('in');
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });

    const photography = localStorage.getItem('photography');
    const name = localStorage.getItem('name');
    const client = localStorage.getItem('client');

    $("#nameSideBar").text(`${name}`);
    $("#clientSideBar").text(`${client}`);
    $("#ImgDivSideBar").append((photography === "")?`<i class="fas fa-user"></i>`:`<img class="rounded-circle text-hide" id="fotoCuenta" src="data:image/jpeg;base64,${photography}" width="80" height="80">`);
});

const btnVersion = () =>{
  Swal.fire({
    icon: 'info',
    title: "Versión 1.0.0",
    text:"Sistema de Gestión de Servicios",
    footer: '<h6>Hecho con mucho <i class="fas fa-heart"></i> y demasiado <i class="fas fa-coffee"></i></h6>',
    showConfirmButton: false,
    showCloseButton: true,
    closeButtonHtml: '<i class="fas fa-times"></i>'
  });
}
