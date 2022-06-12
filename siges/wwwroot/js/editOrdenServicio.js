'use strict';
$("#stepOne").focus();

/*document.getElementById('btnCancel').addEventListener('click', e => {
  var storage = localStorage;
  if(storage.length > 0 ){
    e.preventDefault();
    console.log('Hay información pendiente por validar');
    localStorage.setItem('this', this);
  }
});*/

$('#iFechaInicio').on('change.datetimepicker', (a) => {
  var inicio = document.getElementById('iFechaInicio').querySelector('input').value.split(' ')[0].split('/')[2]+'-'+document.getElementById('iFechaInicio').querySelector('input').value.split(' ')[0].split('/')[1]+'-'+document.getElementById('iFechaInicio').querySelector('input').value.split(' ')[0].split('/')[0]+' '+document.getElementById('iFechaInicio').querySelector('input').value.split(' ')[1];
  var fin = document.getElementById('iFechaFin').querySelector('input').value.split(' ')[0].split('/')[2]+'-'+document.getElementById('iFechaFin').querySelector('input').value.split(' ')[0].split('/')[1]+'-'+document.getElementById('iFechaFin').querySelector('input').value.split(' ')[0].split('/')[0]+' '+document.getElementById('iFechaFin').querySelector('input').value.split(' ')[1];
  if(moment(fin).isBefore(moment(inicio))){
    $('#fechaFinMenorFechaInicio').modal('show');
    $('#iFechaFin').datetimepicker('clear');
  } else {
    localStorage.setItem('iFechaInicio', a.date.format());
  }
});

$('#iFechaFin').on('change.datetimepicker', (a) => {
  var inicio = document.getElementById('iFechaInicio').querySelector('input').value.split(' ')[0].split('/')[2]+'-'+document.getElementById('iFechaInicio').querySelector('input').value.split(' ')[0].split('/')[1]+'-'+document.getElementById('iFechaInicio').querySelector('input').value.split(' ')[0].split('/')[0]+' '+document.getElementById('iFechaInicio').querySelector('input').value.split(' ')[1];
  var fin = document.getElementById('iFechaFin').querySelector('input').value.split(' ')[0].split('/')[2]+'-'+document.getElementById('iFechaFin').querySelector('input').value.split(' ')[0].split('/')[1]+'-'+document.getElementById('iFechaFin').querySelector('input').value.split(' ')[0].split('/')[0]+' '+document.getElementById('iFechaFin').querySelector('input').value.split(' ')[1];
  if(moment(fin).isBefore(moment(inicio))){
    $('#fechaFinMenorFechaInicio').modal('show');
    $('#iFechaFin').datetimepicker('clear');
  } else {
    localStorage.setItem('iFechaFin', a.date.format());
  }
});

function getAllOIbyInsumo(iId, lId){
  $.ajax({
    url: '/Rest/GetAllByInsumoId',
    dataType: 'json',
    data: {
      'iId': iId
    },
    success: (d, s, x) => {
      var asignado = document.getElementById('osLoteAsignado');
      var teorico = document.getElementById('osLoteTeorico');
      var fisico = document.getElementById('osLoteFisico');
      var a = 0;
      var t = 0;
      var f = 0;
      Array.prototype.forEach.call(d, (c, i, l) => {
        if(c.OrdenServicio.EstatusServicio == 'programado' && c.Lote.Id == lId)
          a = a + c.Cantidad;
        if(c.OrdenServicio.EstatusServicio == 'finalizado' && c.Lote.Id == lId)
          t = t + c.Cantidad;
        if(c.OrdenServicio.EstatusServicio == 'programado' || c.OrdenServicio.EstatusServicio == 'finalizado' && c.Lote.Id == lId)
          f = f + c.Cantidad;
      });
      asignado.innerText = 'Asignados: ' + a;
      teorico.innerText = 'Teórico: ' + t;
      fisico.innerText = 'Físico: ' + f;
    }
  });
}

document.getElementById('osPaquete').addEventListener('input', e => {
  const pqId = e.srcElement.value;
  var osInsumo = document.getElementById('osInsumo');
  //var osLote = document.getElementById('osLote');
  var osCantidad = document.getElementById('osCantidad');
  //var osLoteCaducidad = document.getElementById('osLoteCaducidad');
  //var osLoteTeorico = document.getElementById('osLoteTeorico');
  //var osLoteFisico = document.getElementById('osLoteFisico');

  var option1 = document.createElement('option');
  option1.setAttribute('value', '');
  option1.setAttribute('id', 'opt1Init');
  option1.innerText = ' -- Selecciona una opción -- ';
  //var option2 = document.createElement('option');
  //option2.setAttribute('value', '');
  //option2.innerText = ' -- Selecciona una opción -- ';

  if(pqId != '' && pqId != null){
    $.ajax({
      url: '/Rest/GetInsumosByPaquete',
      data: {'pqId': pqId},
      success: (piL, s, x) => {
        while(osInsumo.firstChild)
          osInsumo.removeChild(osInsumo.firstChild);
        //osInsumo.appendChild(option1);
        //while(osLote.firstChild)
        //osLote.removeChild(osLote.firstChild);
        //osLote.appendChild(option2);
        //osCantidad.value = '';
        //osLoteCaducidad.removeAttribute('style');
        //osLoteCaducidad.innerText = '';
        //osLoteAsignado.innerText = '';
        //osLoteFisico.innerText = '';
        Array.prototype.forEach.call(piL, (c, i, l) => {
          var option = document.createElement('option');
          option.setAttribute('value', c.insumo.id);
          option.innerText = c.insumo.clave + ' - ' + c.insumo.descripcion;
          osInsumo.appendChild(option);
        });
        $('#osInsumo').selectpicker('refresh');

        if(pqId == 1 || pqId == 2 || pqId == 3){
          $('#osInsumo').selectpicker('selectAll');
          $('#osInsumo').selectpicker('toggle');
          document.getElementById('addInsumo').click();
        } else if( pqId == 4 || pqId == 5) {
          $('#osInsumo').selectpicker('toggle');
        }
      }
    });
    $('#osInsumo').selectpicker('refresh');
    // Habilita osInsumoSearch
    /*if(isMobile.any()){
      if(document.getElementById('osInsumoSearch').attributes.getNamedItem('disabled') != null){
        document.getElementById('osInsumoSearch').attributes.removeNamedItem('disabled');
        document.getElementById('osInsumoSearch').focus({preventScroll: true});
        crearBusqueda();
        document.querySelector('button.btn.dropdown-toggle.btn-light[data-id="osInsumo"]').setAttribute('style', 'display:none');
        document.querySelector('div.dropdown-menu[x-placement="bottom-start"]').setAttribute('style', 'display:none');
      }
    }*/
  } else {
    while(osInsumo.firstChild)
      osInsumo.removeChild(osInsumo.firstChild);
    //while(osLote.firstChild)
    //osLote.removeChild(osLote.firstChild);
    //osInsumo.appendChild(option1);
    //osLote.appendChild(option2);
    osCantidad.value = '';
    //osLoteCaducidad.removeAttribute('style');
    //osLoteCaducidad.innerText = '';
    //osLoteAsignado.innerText = '';
    //osLoteFisico.innerText = '';
    $('#osInsumo').selectpicker('refresh');
    document.getElementById('osInsumoSearch').setAttribute('disabled', '');
  }
  $('#osInsumo').selectpicker('refresh');
  //feather.replace();
});

function crearBusqueda(){
  var bSearchInsumo = document.createElement('button');
  bSearchInsumo.setAttribute('class', 'btn btn-warning btn-sm m-1');
  bSearchInsumo.setAttribute('type', 'button');
  bSearchInsumo.setAttribute('id', 'bSearchInsumo');
  var icSearch = document.createElement('i');
  icSearch.setAttribute('data-feather', 'search');
  bSearchInsumo.appendChild(icSearch);
  document.getElementById('osInsumoSearch').parentElement.appendChild(bSearchInsumo);
  document.getElementById('osInsumoSearch').parentElement.insertBefore(document.getElementById('bSearchInsumo'), document.getElementById('osInsumoSearch'));
  bSearchInsumo.addEventListener('click', e => {
    if(document.getElementById('osInsumoSearch').value != null && document.getElementById('osInsumoSearch').value != ''){
      if(osInsumo != null){
        var options = [];
        Array.prototype.forEach.call(osInsumo.children, (c, i, l) => {
          var mt = c.innerText.match(new RegExp(document.getElementById('osInsumoSearch').value, 'i'));
          if(mt != null){
            options.push(c);
          }
        });
        if (options.length == 1){
          Array.prototype.forEach.call(osInsumo.children, (c, i, l) => {
            if(c.hasAttribute('selected')){
              c.removeAttribute('selected');
            }
            if(c.value == options[0].value){
              c.setAttribute('selected', '');
              document.getElementById('osInsumoSearch').setAttribute('placeholder', '');
              document.getElementById('osLoteI').focus();
            }
          });
          document.getElementById('osLoteI').focus();
        } else if(options.length > 1){
          document.getElementById('osInsumoSearch').setAttribute('placeholder', 'Demasiadas coincidencias');
          document.getElementById('osInsumoSearch').focus();
        } else {
          document.getElementById('osInsumoSearch').setAttribute('placeholder', 'No se encontraron coincidencias');
          document.getElementById('osInsumoSearch').focus();
        }
      }
    }
    document.getElementById('osInsumoSearch').value = '';
  });
  feather.replace();
}

// Necesario para validar cambios en los controles del formulario
// será inicializado cuando el documento esté listo
var fEditOSOriginal = {};

var $sections = $('.form-section');
$sections.each(function (index, section) {
  $(section).find(':input').each(function (idx, d) {
    $(d).attr({
      'data-parsley-group': 'block-' + index,
      'data-parsley-errors-wrapper': '<ul class="list-unstyled parsley-errors-list"></ul>',
      'data-parsley-error-template': '<li class="font-weight-bold"></li>'
    });
    $(d).hasClass('datetimepicker-input') ? $(d).attr('data-parsley-errors-container', '#ei' + $(d).attr('name')) : '';
  });
});

function toTitleCase(str){
  if(str != null || str != undefined)
    if(str != ''){
      str = str.toLowerCase().split(' ');
      let final = [ ];
      for(let  word of str){
        final.push(word.charAt(0).toUpperCase()+ word.slice(1));
      }
      return final.join(' ');
    }
  return '';
}

var iEstatus = document.getElementById('iEstatus');
var estatusOriginal = iEstatus.options.item(iEstatus.options.selectedIndex).value;

iEstatus.addEventListener('input', e => {
  document.getElementById('capturaComentarioTitle').innerHTML = '<b>'+toTitleCase(estatusOriginal)+'</b> -> <b>'+toTitleCase(e.srcElement.value)+'</b>';
  document.getElementById('iDeCambiaEstatus').setAttribute('value', estatusOriginal);
  document.getElementById('iACambiaEstatus').setAttribute('value', e.srcElement.value);
  $('#capturaComentario').modal({show: true});
});

$('#tPersonal').on('all.bs.table', () => {
  console.log('cambio la table del personal');
})

Array.prototype.forEach.call(document.forms.namedItem('fEditOS').elements, $c => {
  $c.addEventListener('change', $e => {
    console.log('=> cambio -> ', $e.srcElement);
  });
});

document.getElementById('cambiarEstatus').addEventListener('click', e => {
  var fEditOS = document.forms.namedItem('fEditOS');
  console.log({fEditOS}, {fEditOSOriginal});
});

var http_request = false;
function getProductsOfClient(url, cId) {
  http_request = false;
  if(window.XMLHttpRequest) { // Mozilla, Safari, ...
    http_request = new XMLHttpRequest();
    if (http_request.overrideMimeType) {
      http_request.overrideMimeType('text/xml');
    }
  } else if (window.ActiveXObject) { // IE
    try {
      http_request = new ActiveXObject("Msxml2.XMLHTTP");
    } catch (e) {
      try {
        http_request = new ActiveXObject("Microsoft.XMLHTTP");
      } catch (e) {}
    }
  }

  if (!http_request) {
    alert('Falla :( No es posible crear una instancia XMLHTTP');
    return false;
  }
  http_request.onreadystatechange = fillActivoFijoSelectBox;
  http_request.open('GET', url+'?cId='+cId.toString(), true);
  http_request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
  http_request.send();
}

function fillActivoFijoSelectBox() {
  if (http_request.readyState == 4) {
    if (http_request.status == 200) {
      var json = JSON.parse(http_request.responseText);
      console.log({json}, json.length);
    } else {
      console.log('Hubo problemas con la petición de Activo Fijo');
    }
  }
}

$(document).ready(function () {
$("#stepOne").focus();
  if(localStorage.getItem('iFechaInicio')){
    var fechaInicioMem = moment(localStorage.getItem('iFechaInicio'));
    if(fechaInicioMem.isValid()) {
      if(!fechaInicioMem.isSame($('#iFechaInicio').datetimepicker('viewDate'))){
        $('#iFechaInicio').datetimepicker('destroy');
        $('#iFechaInicio').datetimepicker({
          date: moment(fechaInicioMem._d),
          locale: 'es',
          focusOnShow: false,
          //format: 'L'
          inline: true,
          sideBySide: true
        });
        if(localStorage.getItem('iFechaInicio'))
          localStorage.removeItem('iFechaInicio');
      } else {
        //$('#iFechaInicio').datetimepicker('destroy');
      }
    }
  }

        $('#iFechaInicio').datetimepicker({
          locale: 'es',
          focusOnShow: false,
          //format: 'L',
          inline: true,
          sideBySide: true
          /*date: $('#iFechaInicio').datetimepicker('viewDate'),*/
        });

  if(localStorage.getItem('iFechaFin')){
    var fechaFinMem = moment(localStorage.getItem('iFechaFin'));
    if(fechaFinMem.isValid()) {
      if(!fechaFinMem.isSame($('#iFechaFin').datetimepicker('viewDate'))){
        $('#iFechaFin').datetimepicker('destroy');
        $('#iFechaFin').datetimepicker({
          date: moment(fechaFinMem._d),
          locale: 'es',
          focusOnShow: false,
          //format: 'L'
          inline: true,
          sideBySide: true
        });
        if(localStorage.getItem('iFechaFin'))
          localStorage.removeItem('iFechaFin');
      } else {
        //$('#iFechaFin').datetimepicker('destroy');
      }
    }
  }

        $('#iFechaFin').datetimepicker({
          locale: 'es',
          focusOnShow: false,
          //format: 'L',
          inline: true,
          sideBySide: true
          /*date: $('#iFechaFin').datetimepicker('viewDate'),*/
        });

  fEditOSOriginal = document.forms.namedItem('fEditOS');
  document.getElementById('iOp2').addEventListener('change', e => {
    let v = e.srcElement.value;
    $.ajax({
      url: '/Rest/OSConfirm',
      data: {'osFolio': v},
      success: function(d, s, x){
        if(!d){
          if(e.srcElement.attributes.getNamedItem('data-toggle') == null){
            e.srcElement.setAttribute('data-toggle', 'popover');
            e.srcElement.setAttribute('data-content', 'La información capturada se tomará como datos genéricos');
            e.srcElement.setAttribute('title', 'Orden Servicio no identificada');
            e.srcElement.setAttribute('data-placement', 'top');
            //e.srcElement.setAttribute('data-trigger', 'focus hover click');
            $('#iOp2').popover('show');
          }
        } else {
          if(e.srcElement.attributes.getNamedItem('data-toggle') != null){
            $('#iOp2').popover('dispose');
            e.srcElement.removeAttribute('data-toggle');
            e.srcElement.removeAttribute('data-content');
            e.srcElement.removeAttribute('title');
            e.srcElement.removeAttribute('data-placement');
            //e.srcElement.removeAttribute('data-trigger');
          }
        }
      }
    });
  });

  $('#cancelaCapturaDatos1').on('click', function(event){
    $('#iEstatus').val(estatusOriginal)
  });

  $('#cancelaCapturaDatos2').on('click', function(event){
    $('#iEstatus').val(estatusOriginal)
  });

  $('#capturaComentario').on('hide.bs.modal', function (e) {
    $('#iEstatus').val(estatusOriginal)
  });

  $('#ccCheck1').change(function () {
    if(document.getElementById('Cc1').attributes.getNamedItem('style').textContent.indexOf('none') > 0){
      document.getElementById('Cc1').setAttribute('style', 'display: true');
      document.getElementById('ccCheck2').setAttribute('style', 'display: true');
    }else{
      document.getElementById('Cc1').setAttribute('style', 'display: none');
      document.getElementById('ccCheck2').setAttribute('style', 'display: none');
      if(document.getElementById('Cc2').attributes.getNamedItem('style').textContent.indexOf('true') > 0)
        document.getElementById('Cc2').setAttribute('style', 'display: none');
    }
  });

  $('#ccCheck2').change(function () {
    if(document.getElementById('Cc2').attributes.getNamedItem('style').textContent.indexOf('none') > 0){
      document.getElementById('Cc2').setAttribute('style', 'display: true');
    }else{
      document.getElementById('Cc2').setAttribute('style', 'display: none');
    }
  });

  //feather.replace();

  document.querySelectorAll('div.input-group-append').forEach((c, i, l) => {
    c.addEventListener('click', e => {
      $('#iFechaInicio').datetimepicker('inline', 'true');
      //$('#iFechaInicio').datetimepicker('format', 'L');
      $('#iFechaInicio').datetimepicker('sideBySide', 'true');
      //$('#iFechaInicio').datetimepicker('locale', 'es');
      $('#iFechaInicio').datetimepicker('minDate', moment());
      //$('#iFechaFin').datetimepicker('format', 'L');
      $('#iFechaFin').datetimepicker('inline', 'true');
      //$('#iFechaFin').datetimepicker('locale', 'es');
      $('#iFechaFin').datetimepicker('sideBySide', 'true');

      $('#iFechaFin').datetimepicker('minDate', moment());
    });
  });



  // Wizard steps
  $('#stepOne').click(function (e) {
    e.preventDefault();

    var progressValue = 20;

    $("#stepOne").addClass("active");
    $("#stepTwo").removeClass("active");
    $("#stepThree").removeClass("active");
    $("#stepFour").removeClass("active");
    $("#stepFive").removeClass("active");
    $("#infoGral").fadeIn('fast');
    $("#infoResponsable").fadeOut('fast');
    $("#infoPersonal").fadeOut('fast');
    $("#infoInsumos").fadeOut('fast');
    $("#infoInsumosP").fadeOut('fast');
    $("#infoActivosFijos").fadeOut('fast');

    moveProgressBar(progressValue);

    $('#tab-lists a[href="#infoGral"]').tab('show');
  });

  $('#stepTwo').click(function (e) {
    e.preventDefault();

    $('#fEditOS').parsley().whenValidate({
      group: 'block-' + 0
    }).done(function () {
      var progressValue = 40;
      $("#stepTwo").addClass("active");
      $("#stepOne").removeClass("active");
      $("#stepThree").removeClass("active");
      $("#stepFour").removeClass("active");
      $("#stepFive").removeClass("active");
      $("#infoResponsable").fadeIn('fast');
      $("#infoGral").fadeOut('fast');
      $("#infoPersonal").fadeOut('fast');
      $("#infoInsumos").fadeOut('fast');
      $("#infoInsumosP").fadeOut('fast');
      $("#infoActivosFijos").fadeOut('fast');

      moveProgressBar(progressValue);

      $('#tab-lists a[href="#infoResponsable"]').tab('show');
    });
  });

  $('#stepThree').click(function (e) {
    e.preventDefault();

    $('#fEditOS').parsley().whenValidate({
      group: 'block-' + 1
    }).done(function () {
      var progressValue = 60;
      $("#stepThree").addClass("active");
      $("#stepOne").removeClass("active");
      $("#stepTwo").removeClass("active");
      $("#stepFour").removeClass("active");
      $("#stepFive").removeClass("active");
      $("#infoPersonal").fadeIn('fast');
      $("#infoGral").fadeOut('fast');
      $("#infoResponsable").fadeOut('fast');
      $("#infoInsumos").fadeOut('fast');
      $("#infoInsumosP").fadeOut('fast');
      $("#infoActivosFijos").fadeOut('fast');

      moveProgressBar(progressValue);

      $('#tab-lists a[href="#infoPersonal"]').tab('show');

      $.ajax({
        url: '/Rest/GetPersonas',
        success: function (data, status, jqXHR) {
          if (data !== null) {
            var _list = data["listPersonas"];
            $('#osPersonal').empty().append('<option> -- Selecciona una opci&oacute;n -- </option>');
            $.each(_list, function (idx, d) {
              $('#osPersonal').append($('<option>', {
                value: d.id,
                text: d.nombre + " " + d.paterno + " " + d.materno + " " + "(" + d.categoria + ")"
              }).data('sPuesto', d.categoria));
            });
          }
        },
        error: function (obj) {
          console.log({obj});
        },
      });
    });
  });

  $('#stepFour').click(function (e) {
    e.preventDefault();

    $('#fEditOS').parsley().whenValidate({
      group: 'block-' + 1
    }).done(function () {
      var progressValue = 80;
      $("#stepFour").addClass("active");
      $("#stepTwo").removeClass("active");
      $("#stepThree").removeClass("active");
      $("#stepFive").removeClass("active");
      $("#stepOne").removeClass("active");
      $("#infoActivosFijos").fadeIn('fast');
      $("#infoInsumos").fadeOut('fast');
      $("#infoInsumosP").fadeOut('fast');
      $("#infoGral").fadeOut('fast');
      $("#infoResponsable").fadeOut('fast');
      $("#infoPersonal").fadeOut('fast');

      moveProgressBar(progressValue);

      $('#tab-lists a[href="#infoActivosFijos"]').tab('show');


      if(document.getElementById('iLineaNegocio').type !== 'hidden') {
        //
        //  Confirmar que los datos de la tabla pertenezcan a un OS
        //  con Línea de Negocio diferente a Garantía.
        //
        let lbl = document.querySelector('div.form-group label[for="osActivoFijo"]');
        if(lbl.innerText.match(/Folio/) !== null) {
          lbl.innerText = 'Activo fijo/Producto registrado - No. Serie - No. Producto';
          $('#tActivoFijo').bootstrapTable('hideColumn', 'os');
          $('#tActivoFijo').bootstrapTable('removeAll');
        }

        $.ajax({
          url: '/Rest/GetActivoFijos',
          success: function (data, status, jqXHR) {
            if (data !== null) {
              var _list = data["listActivos"];
              $('#osActivoFijo').empty().append('<option> -- Selecciona una opci&oacute;n -- </option>');
              $.each(_list, function (idx, d) {
                $('#osActivoFijo').append($('<option>', {
                  value: d.id,
                  text: d.descripcion + ' - ' + d.tipo
                }));
              });
            }
            $('#osActivoFijo').selectpicker({liveSearch: 'true'});
          },
          error: function (obj) {
            console.log(obj);
          },
        });
      } else {
        $.ajax({
          url: '/Rest/GetActivoFijoByClientAndOS',
          data: {
            'cId': parseInt(document.getElementById('iCliente').value),
            'lnNombre': 'INSTALACIÓN'
          },
          success: function(d, s, jqXHR) {
            //
            //  Confirmar que esté limpia la tabla de información no correspondiente
            //  a una OS con Línea de Negocio de Garantía.
            //
            if( $('#tActivoFijo').bootstrapTable('getData').length > 0 ) {
              if($('#tActivoFijo').bootstrapTable('getData')[0].os === undefined)
                $('#tActivoFijo').bootstrapTable('removeAll');
            }
            
            //
            //  Es necesario una columna más para referenciar el folio de la orden de servicio.
            //  th.text-center[data-field="os"]>div.th-inner+div.fht-cell
            //  
            //  Primero la cabecera
            //
            $('#tActivoFijo').bootstrapTable('showColumn', 'os');

            //
            //  Es necesario modificar el label del list box para anunciar el folio de la OS.
            //
            let lbl = document.querySelector('div.form-group label[for="osActivoFijo"]');
            lbl.innerText = 'Activo fijo/Producto registrado - No. Serie (Folio) - No. Producto';

            var datos = JSON.parse(d);
            var osActivoFijo = document.getElementById('osActivoFijo');
            while(osActivoFijo.firstChild){
              osActivoFijo.removeChild(osActivoFijo.firstChild);
            }
            var option1 = document.createElement('option');
            option1.setAttribute('value', '');
            option1.innerText = '-- Selecciona una opción --';
            osActivoFijo.appendChild(option1);
            Array.prototype.forEach.call(Object.entries(datos.ordenActivoFijo), (c, i, l) => {
              Array.prototype.forEach.call(c, (cu, ix, ls) => {
                if (typeof cu === 'object') {
                  var option = document.createElement('option');
                  option.setAttribute('value', cu.ActivoFijo.Id);
                  option.innerText = cu.ActivoFijo.Descripcion + ' - ' + cu.ActivoFijo.Tipo + ' (' + cu.OrdenServicio.Folio +')';
                  osActivoFijo.appendChild(option);
                }
              });
            });
          },
        });
      }


        /*if(document.getElementById('iLineaNegocio').options.item(document.getElementById('iLineaNegocio').options.selectedIndex).innerText != 'GARANTÍA') {
        $.ajax({
          url: '/Rest/GetActivoFijos',
          success: function (data, status, jqXHR) {
            if (data !== null) {
              var _list = data["listActivos"];
              $('#osActivoFijo').empty().append('<option> -- Selecciona una opci&oacute;n -- </option>');
              $.each(_list, function (idx, d) {
                $('#osActivoFijo').append($('<option>', {
                  value: d.id,
                  text: d.descripcion + ' - ' + d.tipo + ' - ' + d.opcional2
                }));
              });
            }
          },
          error: function (obj) {
            console.log(obj);
          },
        });
      } else {
        $.ajax({
          url: '/Rest/GetActivoFijoByClientAndOS',
          data: {'cId': parseInt(document.getElementById('iCliente').options.item(document.getElementById('iCliente').options.selectedIndex).value)},
          success: function(d, s, jqXHR) {
            var datos = JSON.parse(d);
            var osActivoFijo = document.getElementById('osActivoFijo');
            while(osActivoFijo.firstChild){
              osActivoFijo.removeChild(osActivoFijo.firstChild);
            }
            var option1 = document.createElement('option');
            option1.setAttribute('value', '');
            option1.innerText = '-- Selecciona una opción --';
            osActivoFijo.appendChild(option1);
            Array.prototype.forEach.call(Object.entries(datos), (c, i, l) => {
              Array.prototype.forEach.call(c, (cu, ix, ls) => {
                if (typeof cu === 'object') {
                  var option = document.createElement('option');
                  option.setAttribute('value', cu.afid);
                  option.innerText = cu.activofijo + ' - ' + cu.serie + ' (' + cu.os + ')';
                  osActivoFijo.appendChild(option);
                }
              });
            });
          },
        });
      }*/
      //getProductsOfClient('/Rest/GetActivoFijoByClientAndOS', parseInt(document.getElementById('iCliente').options.item(document.getElementById('iCliente').options.selectedIndex).value));
    });
  });

  $('#stepFive').click(function (e) {
    e.preventDefault();

    $('#fEditOS').parsley().whenValidate({
      group: 'block-' + 1
    }).done(function () {
      var progressValue = 100;
      $("#stepFive").addClass("active");
      $("#stepFour").removeClass("active");
      $("#stepTwo").removeClass("active");
      $("#stepThree").removeClass("active");
      $("#stepOne").removeClass("active");
if(!paqueteP){
      $("#infoInsumos").fadeIn('fast');
} else {
      $("#infoInsumosP").fadeIn('fast');
}
      $("#infoGral").fadeOut('fast');
      $("#infoResponsable").fadeOut('fast');
      $("#infoPersonal").fadeOut('fast');
      $("#infoActivosFijos").fadeOut('fast');

      moveProgressBar(progressValue);

if(!paqueteP){
      $('#tab-lists a[href="#infoInsumos"]').tab('show');
      $.ajax({
        url: '/Rest/GetInsumos',
        success: function (data, status, jqXHR) {
          if (data !== null) {
            var _list = data["listInsumos"];
            $('#os_Insumo').empty().append('<option> -- Selecciona una opci&oacute;n -- </option>');
            $.each(_list, function (idx, d) {
              $('#os_Insumo').append($('<option>', {
                value: d.id,
                text: d.descripcion
              }));
            });
          }
        },
        error: function (obj) {
          console.log(obj);
        },
      });
} else {
      $('#tab-lists a[href="#infoInsumosP"]').tab('show');
    $.ajax({
        url: '/Rest/GetAllPaquete',
        success: function (data, status, jqXHR){
            if(data !== null){
                console.log({data});
                //$('#osPaquete').empty().append('<option value=""> -- Selecciona una opci&oacute;n -- </option>');
                $.each(data, function (idx, d) {
                $('#osPaquete').append($('<option>', {
                    value: d.id,
                    text: d.descripcion
                    }));
                });
            }
        },
        error: function (obj) {
            console.log(obj);
        },
    });
}

    });
  });

  /* Step one */
  $('#iCliente').change(function () {
    var sCliente = $(this).find(":selected").val();

    $.ajax({
      url: '/Rest/GetContratosFromCliente',
      data: { 'clienteid': sCliente },
      success: function (data, status, jqXHR) {
        if (data !== null) {
          var _list = data["listContratos"];
          $('#iContrato').empty().append('<option> -- Selecciona una opci&oacute;n -- </option>');
          $.each(_list, function (idx, d) {
            $('#iContrato').append($('<option>', {
              value: d.id,
              text: d.nombre
            }));
          });
        }
      },
      error: function (obj) {
        console.log(obj);
      },
    });

    $.ajax({
      url: '/Rest/GetUbicacionesFromCliente',
      data: { 'clienteid': sCliente },
      success: function (data, status, jqXHR) {
        if (data !== null) {
          var _list = data["listUbicaciones"];
          $('#iUbicacion').empty().append('<option> -- Selecciona una opci&oacute;n -- </option>');
          $.each(_list, function (idx, d) {
            $('#iUbicacion').append($('<option>', {
              value: d.id,
              text: d.nombre
            }));
          });
        }
      },
      error: function (obj) {
        console.log(obj);
      },
    });
  });

  $('#iLineaNegocio').change(function () {
    var sLineaNegocio = $(this).find(":selected").val();

    $.ajax({
      url: '/Rest/GetServiciosFromLineaNegocio',
      data: { 'lineaid': sLineaNegocio },
      success: function (data, status, jqXHR) {
        if (data !== null) {
          var _list = data["listServicios"];
          $('#iServicio').empty().append('<option value=""> -- Selecciona una opci&oacute;n -- </option>');
          $.each(_list, function (idx, d) {
            $('#iServicio').append($('<option>', {
              value: d.id,
              text: d.nombre
            }));
          });
        }
      },
      error: function (obj) {
        console.log(obj);
      },
    });
  });
  /* Step Two */

  /* Step Three */
  $('#btnAddPersonal').click(function () {
    if (!($('#aPCont').parent().hasClass('invisible')))
      $('#aPCont').parent().addClass('invisible');
    var duplicidad = false;
    var sPersona = $('#osPersonal').find(":selected");
    var tIdx = $('#tPersonal').bootstrapTable('getData').length;
    var personalAsignado = $('#tPersonal').bootstrapTable('getData');

    $.each(personalAsignado, function (index, persona) {
      if (persona['idpersona'] === sPersona.val()) {
        duplicidad = true;
      }
    });

    if (!duplicidad) {
      var data = {};
      if (sPersona.val() > 0) {
        data.id = tIdx + 1;
        data.idpersona = sPersona.val();
        data.idordper = 0;
        data.nombre = sPersona.text();
        data.puesto = sPersona.data('sPuesto');
        data.actions = `<button id=idx_${tIdx + 1} type="button" class="btn btn-danger float_Table"><i class="fas fa-trash"></i></button>`;
        $('#tPersonal').bootstrapTable('append', data);

        var total = $('#tPersonal').bootstrapTable('getData').length;
        $('#osTotal').val(total);
        $('#osPersonal').prop('selectedIndex', "");

        //feather.replace();
      } else {
        $('#aPCont').text("No se pudo completar la asignación, no se tiene seleccionada una persona.");
        $('#aPCont').parent().removeClass('invisible');
        $('#aPCont').parent().removeClass('visible');
      }
    } else {
      $('#aPCont').text("No es posible asignar repetidamente la misma persona en la misma orden de servicio.");
      $('#aPCont').parent().removeClass('invisible');
      $('#aPCont').parent().removeClass('visible');
    }
  });

  $(document).on('click', '#tPersonal :button', function (e) {
    var ids = $(this).attr('id').split('_')[1];
    var befDelete = $('#tPersonal').bootstrapTable('getData');
    var data = [];
    var aidx = 0;
    $.each(befDelete, function (idx, d) {
      if (String(d["id"]) !== String(ids)) {
        var p = {};
        p.id = aidx + 1;
        p.idpersona = d.idpersona;
        p.idordper = d.idordper;
        p.nombre = d.nombre;
        p.puesto = d.puesto;
        p.actions = `<button id=idx_${aidx + 1} type="button" class="btn btn-danger float_Table"><i class="fas fa-trash"></i></button>`;
        data.push(p);
        aidx++;
      }
    });
    $('#tPersonal').bootstrapTable('load', data);
    var total = $('#tPersonal').bootstrapTable('getData').length;
    $('#osTotal').val(total);
    //feather.replace();
  });

  /* Step Four */
  $('#btnActivoFijo').click(function () {
    if (!($('#aAFCont').parent().hasClass('invisible')))
      $('#aAFCont').parent().addClass('invisible');
    var sActivoFijo = $('#osActivoFijo').find(":selected");
    var activoFijoSelected = $('#tActivoFijo').bootstrapTable('getData');
    var duplicidad = false;

    $.each(activoFijoSelected, function (index, activo) {
      if (activo['idactivofijo'] === sActivoFijo.val()) {
        duplicidad = true;
      }
    });

    if (!duplicidad) {
      var tIdx = $('#tActivoFijo').bootstrapTable('getData').length;
      var data = {};

      if (sActivoFijo.val() > 0) {
        data.idaf = tIdx + 1;
        data.idactivofijo = sActivoFijo.val();
        data.idordact = 0;
        data.activo = sActivoFijo.text().split('-')[0];
        data.tipo = sActivoFijo.text().split('-')[1].split(' ')[1];
        if(document.getElementById('iLineaNegocio').type === 'hidden') {
          data.opcional2 = sActivoFijo.text().split('-')[2];
          data.os = sActivoFijo.text().split('-')[1].split(' ')[2];
        } else {
          data.opcional2 = sActivoFijo.text().split('-')[2];
        }
        data.actions = `<button id=idx_${tIdx + 1} type="button" class="btn btn-danger float_Table"><i class="fas fa-trash"></i></button>`

        $('#tActivoFijo').bootstrapTable('append', data);

        var total = $('#tActivoFijo').bootstrapTable('getData').length;
        $('#osafTotal').val(total);
        $('#osActivoFijo').prop('selectedIndex', "");
        $('#osActivoFijo').selectpicker('refresh');

        //feather.replace();
      } else {
        $('#aAFCont').text("No se pudo completar la asignación, no se tiene seleccionada un activo fijo.");
        $('#aAFCont').parent().removeClass('invisible');
        $('#aAFCont').parent().removeClass('visible');
      }
    } else {
      $('#aAFCont').text("No es posible asignar el mismo activo fijo repetidamente.");
      $('#aAFCont').parent().removeClass('invisible');
      $('#aAFCont').parent().removeClass('visible');
    }
  });

  $(document).on('click', '#tActivoFijo :button', function (e) {
    var ids = $(this).attr('id').split('_')[1];
    var befDelete = $('#tActivoFijo').bootstrapTable('getData');
    var data = [];
    var aidx = 0;

    $.each(befDelete, function (idx, d) {
      if (String(d["idaf"]) !== String(ids)) {
        var p = {};
        p.idaf = aidx + 1;
        p.idactivofijo = d.idactivofijo;
        p.idordact = d.idordact;
        p.activo = d.activo;
        p.tipo = d.tipo;
        p.opcional2 = d.opcional2;
        p.os = d.os;
        p.actions = `<button id=idx_${aidx + 1} type="button" class="btn btn-danger float_Table"><i class="fas fa-trash"></i></button>`;
        data.push(p);
        aidx++;
      }
    });
    $('#tActivoFijo').bootstrapTable('load', data);
    var total = $('#tActivoFijo').bootstrapTable('getData').length;
    $('#osafTotal').val(total);
    //feather.replace();
  });

  /* Step Five */
  $('#btnAddInsumo').click(function () {
    if (!($('#aICont').parent().hasClass('invisible')))
      $('#aICont').parent().addClass('invisible');
    var sInsumo = $('#os_Insumo').find(":selected");
    var insumosUtilizados = $('#t_Insumos').bootstrapTable('getData');
    var duplicidad = false;

    $.each(insumosUtilizados, function (index, insumo) {
      console.log(insumo, "-", sInsumo.val());
      if (insumo['idinsumo'] === sInsumo.val()) {
        duplicidad = true;
      }
    });

    if(parseInt(document.getElementById('os_Insumo').options.item(document.getElementById('os_Insumo').options.selectedIndex).value) != NaN && document.getElementById('osCantidad').value == '') {
      document.getElementById('osCantidad').focus();
      return;
    } else if (!duplicidad) {
      var tIdx = $('#t_Insumos').bootstrapTable('getData').length;
      var data = {};

      if (sInsumo.val() > 0) {
        data.idi = tIdx + 1;
        data.idinsumo = sInsumo.val();
        data.idordins = 0;
        data.insumo = sInsumo.text();
        data.cantidad = $('#osCantidad').val();
        data.actions = `
        <button id=idx_${tIdx + 1} type="button" class="btn btn-danger float_Table"><i class="fas fa-trash"></i></button>
        <button id="Edit_${tIdx + 1}" type="button" class="btn btn-success float_Table"><i class="fas fa-edit"></i></button>`;

        $('#t_Insumos').bootstrapTable('append', data);
        $('#os_Insumo').prop('selectedIndex', "");
        $('#osCantidad').val("");

        var total = $('#t_Insumos').bootstrapTable('getData').length;
        document.getElementById('infoInsumos').getElementsByTagName('input').osiTotal.value = total;

        //feather.replace();
      } else {
        $('#aICont').text("No se pudo completar la asignación, no se tiene seleccionada un insumo.");
        $('#aICont').parent().removeClass('invisible');
        $('#aICont').parent().removeClass('visible');
      }
    } else {
      $('#aICont').text("No es posible asignar un insumo previamente asignado.");
      $('#aICont').parent().removeClass('invisible');
      $('#aICont').parent().removeClass('visible');
    }
  });

  $(document).on('click', '#t_Insumos :button', async function (e) {
    if($(this).attr('id').split('_')[0] === "Edit"){
      const data = $('#t_Insumos').bootstrapTable('getData');
      const ID = ($(this).attr('id').split('_')[1])-1;
      const { value } = await Swal.fire({
        title:"Editar Cantidad",
        confirmButtonColor: "#67E7CF",
        confirmButtonText:"Guardar",
        cancelButtonText:"Cancelar",
        input: 'text',
        inputValue: data[ID].cantidad,
        reverseButtons: true,
        showCancelButton: true,
        allowOutsideClick: false,
        allowEscapeKey: false,
        showCloseButton: true,
        closeButtonHtml: '<i class="fas fa-times"></i>',
        inputValidator: (value) => {
          if (!value) {
            return 'Se necesita escribir una cantidad'
          }
          else if(!value.match(/^([0-9])*$/)){
            return 'Algún caracter no es aceptado'
          }
        }
      });
      if(value){
        $('#t_Insumos').bootstrapTable('updateRow', {
          index: ID,
          row: {
            idi: ID + 1,
            idinsumo: data[ID].idinsumo,
            idordins:  data[ID].idordins,
            insumo: data[ID].insumo,
            cantidad: value,
            actions: data[ID].actions
          }
        });
        $(`[name='Insumos[${ID}].Cantidad']`).val(value); 
        Swal.fire({
          icon: 'success',
          title: '¡Éxito!',
          confirmButtonColor: "#67E7CF",
          confirmButtonText:"Aceptar",
          text: `La cantidad ha sido modificada`,
        });
      }
    }
    else{
      var ids = $(this).attr('id').split('_')[1];
      var befDelete = $('#t_Insumos').bootstrapTable('getData');
      var data = [];
      var aidx = 0;
      $.each(befDelete, function (idx, d) {
        if (String(d["idi"]) !== String(ids)) {
          var p = {};
          p.idi = aidx + 1;
          p.idinsumo = d.idinsumo;
          p.idordins = d.idordins;
          p.insumo = d.insumo;
          p.cantidad = d.cantidad;
          p.actions = `
          <button id=idx_${aidx + 1} type="button" class="btn btn-danger float_Table"><i class="fas fa-trash"></i></button>
          <button id="Edit_${aidx + 1}" type="button" class="btn btn-success float_Table"><i class="fas fa-edit"></i></button>`;
          data.push(p);
          aidx++;
        }
      });
      $('#t_Insumos').bootstrapTable('load', data);
      var total = $('#t_Insumos').bootstrapTable('getData').length;
      document.getElementById('infoInsumos').getElementsByTagName('input').osiTotal.value = total;
      //feather.replace();
    }
  });

  $(document).on('click', '#tInsumos :button', async function (e) {
    if($(this).attr('id').split('_')[0] === "Edit"){
      const data = $('#tInsumos').bootstrapTable('getData');
      const ID = ($(this).attr('id').split('_')[1])-1;
      const { value } = await Swal.fire({
        title:"Editar Cantidad",
        confirmButtonColor: "#67E7CF",
        confirmButtonText:"Guardar",
        cancelButtonText:"Cancelar",
        input: 'text',
        inputValue: data[ID].cantidad,
        reverseButtons: true,
        showCancelButton: true,
        allowOutsideClick: false,
        allowEscapeKey: false,
        showCloseButton: true,
        closeButtonHtml: '<i class="fas fa-times"></i>',
        inputValidator: (value) => {
          if (!value) {
            return 'Se necesita escribir una cantidad'
          }
          else if(!value.match(/^([0-9])*$/)){
            return 'Algún caracter no es aceptado'
          }
        }
      });
      if(value){
        $('#tInsumos').bootstrapTable('updateRow', {
          index: ID,
          row: {
            cantidad: value
          }
        });
        Swal.fire({
          icon: 'success',
          title: '¡Éxito!',
          confirmButtonColor: "#67E7CF",
          confirmButtonText:"Aceptar",
          text: `La cantidad ha sido modificada`,
        });
      }
    }
    else{
      var ids = $(this).attr('id').split('_')[1];
      var befDelete = $('#tInsumos').bootstrapTable('getData');
      var data = [];
      var aidx = 0;
      $.each(befDelete, function (idx, d) {
        console.log({d});
        if (String(d["idi"]) !== String(ids)) {
          var p = {};
          p.idi = aidx + 1;
          p.idinsumo = d.idinsumo;
          p.idordins = d.idordins;
          p.idpaquete = d.idpaquete;
          p.codigo = d.codigo;
          p.insumo = d.insumo;
          p.paquete = d.paquete;
          p.lote = d.lote;
          p.cantidad = d.cantidad;
          p.caducidad = d.caducidad;
          p.actions = `
            <button id=idx_${aidx + 1} type="button" class="btn btn-danger btn-sm"><i class="fas fa-trash"></i></button>
            <button id="Edit_${aidx + 1}" type="button" class="btn btn-success btn-sm"><i class="fas fa-edit"></i></button>
            `;
          data.push(p);
          aidx++;
        }
      });
      $('#tInsumos').bootstrapTable('load', data);
      var total = $('#tInsumos').bootstrapTable('getData').length;
      document.getElementById('infoInsumos').getElementsByTagName('input').osiTotal.value = total;
    }
  });
$('#osInsumo').on('changed.bs.select', (e, clickedIndex, isSelected, previousValue) => {
  var $t = e.target;
  console.log({$t},$t.innerText.split('\n')[clickedIndex], {clickedIndex}, {isSelected}, {previousValue});
  if(clickedIndex != undefined){
    if($t.innerText.split('\n')[clickedIndex].length > 60)
      document.querySelector('div.filter-option-inner-inner').innerText = document.querySelector('div.filter-option-inner-inner').innerText.slice(0, 60) + '...';
  } else if ($t.innerText != null){
    var lines = $t.innerText.split('\n');
    console.log({lines});
  }
});

/*
 *  Nuevo botón "addInsumo"
 *
 *  Despliega el modal "asignacionInsumos" para capturar los datos de cada uno de los insumos
 *
 * */
var indexCache = 0;
var items;
var clicked = false;

document.getElementById('addInsumo').addEventListener('click', e => {
  e.preventDefault();
  /*
   * Obtiene los Insumos seleccionados de la lista y los asigna a un 'NodeList'
   *
   * */
  items = document.querySelectorAll('ul.dropdown-menu.inner.show li.selected').length > 0 ? document.querySelectorAll('ul.dropdown-menu.inner.show li.selected') : null;
  if ( items !== null ) {
    Array.prototype.forEach.call(items, (c, i, l) => {
      console.log(c.innerText);
    });
  }

  /*
   *  Inicia la captura de los datos por cada Insumo, en caso de que haya alguno seleccionado.
   *
   * */
  if(items){
    indexCache = 0;
    loop();
  }
});

/*
 *  Muestra modal "asignacionInsumos", mientras haya Insumos seleccionados
 *
 * */
function loop() {
  if(indexCache < items.length ){
    document.getElementById('car-title-Insumos').innerText = items[indexCache].innerText;
    document.getElementById('asignacionInsumosTitle').innerText = 'Capturar datos del insumo ( ' + parseInt(indexCache + 1) + ' de ' + items.length + ' ) ';
    $('#osCaducidadI').datetimepicker('date', null);
    if(document.getElementById('osLoteI').value !== '')
      document.getElementById('osLoteI').value = '';
    if(document.getElementById('osCantidadI').value !== '')
      document.getElementById('osCantidadI').value = '';
    $('#asignacionInsumos').modal('show');
  } else {
    $('#osPaquete').val('');
    while(document.getElementById('osInsumo').firstChild)
      document.getElementById('osInsumo').removeChild(document.getElementById('osInsumo').firstChild);
    $('#osInsumo').selectpicker('refresh');
    //document.getElementById('osInsumoSearch').setAttribute('disabled', '');
  }
}

/*
 *  Llenar la tabla con la información del Insumo
 *
 * */
function moverAlListado(){
  let data = {};
  let idx = $('#tInsumos').bootstrapTable('getData').length;
  Array.prototype.forEach.call(document.getElementById('osInsumo').options, (op, iop, lop) => {
    if(op.selected)
      if(document.getElementById('car-title-Insumos').innerText.split('-')[0].trim() == op.innerText.split('-')[0].trim())
        data.idinsumo = op.value;
  });
  data.idi = ++idx;
  data.idpaquete = document.getElementById('osPaquete').options.item(document.getElementById('osPaquete').options.selectedIndex).value;
  data.idordins = 0;
  data.paquete = document.getElementById('osPaquete').options.item(document.getElementById('osPaquete').options.selectedIndex).innerText.split('-')[0].trim();
  data.codigo = document.getElementById('car-title-Insumos').innerText.split('-')[0].trim();
  data.insumo = document.getElementById('car-title-Insumos').innerText.split('-')[1].trim();
  data.lote = document.getElementById('osLoteI').value;
  data.cantidad = document.getElementById('osCantidadI').value < 1 ? 1 : document.getElementById('osCantidadI').value;
  //data.caducidad = $('#osCaducidadI').datetimepicker('date').format('DD/MM/YYYY');
  data.actions = `<button id="idx_${data.idi}" type="button" class="btn btn-danger btn-sm"><i class="fas fa-trash" width="10" height="10"></i></button>
  <button id="Edit_${data.idi}" type="button" class="btn btn-success btn-sm"><i class="fas fa-edit" width="10" height="10"></i></button>`;
  $('#tInsumos').bootstrapTable('append', data);
  console.log('algo se hizo en moverAlListado en addInsumoToTable');
}

/*
 *  Registrar Insumo en la tabla
 *
 * */
document.getElementById('addInsumoToTable').addEventListener('click', e => {
  e.preventDefault();
  if(document.getElementById('osLoteI').value !== '' && document.getElementById('osCantidadI').value !== '' /*&& $('#osCaducidadI').datetimepicker('date') !== null*/) {
    $('#asignacionInsumos').modal('hide');
    moverAlListado();
    ++indexCache;
  } else {
    if(document.getElementById('osLoteI').value === ''){
      document.getElementById('osLoteI').focus();
      document.getElementById('osLoteI').setAttribute('placeholder', 'Favor de capturar el lote al que pertenece este insumo');
    } else if(document.getElementById('osCantidadI').value === '' ){
      document.getElementById('osCantidadI').focus();
      document.getElementById('osCantidadI').setAttribute('placeholder', 'Favor de capturar la cantidad utilizada');
    }/* else if($('#osCaducidadI').datetimepicker('date') === null){
      document.getElementById('osCaducidadI').querySelector('input').focus();
    }*/
  }
});

/*
 *  Ignorar Insumo seleccionado previamente.
 *
 * */
document.getElementById('ignoreInsumo').addEventListener('click', e => {
  ++indexCache;
});

/*
 *  Inicia el loop en caso de que haya Insumos seleccionados por procesar.
 *
 * */
$('#asignacionInsumos').on('hidden.bs.modal', loop);

/*
 *  Listo para iniciar captura del Lote al mostrar el formulario.
 *
 * */
$('#asignacionInsumos').on('shown.bs.modal', function () {
  $('#osLoteI').trigger('focus');
});

/*
 *  Al inicializar 'osInsumo', configura el pintado de los Insumos seleccionados.
 *
 * */
$('#osInsumo').on('show.bs.select', function(e, clickedIndex, isSelected, previousValue){
  Array.prototype.forEach.call(document.getElementById('bs-select-1').querySelectorAll('li'), (c, i, l) => {
    c.addEventListener('click', e => {
      if(!c.classList.contains('selected'))
        c.classList.add('bg-info');
      else
        c.classList.remove('bg-info');
    });
    c.addEventListener('keydown', e => {
      console.log(e.key);
      if(!c.classList.contains('selected'))
        c.classList.add('bg-info');
      else
        c.classList.remove('bg-info');
    });
  });
});

/*
 * Al mostrar 'osInsumo', configura el panel de multiselección al centro de la pantall
 *
 * */
$('#osInsumo').on('shown.bs.select', function(e, clickedIndex, isSelected, previousValue){
  let $style='';
  var $regexminwidth = /min-width/i;
  let $maxtop = document.querySelector('header').offsetHeight;
  var $regextop = /top/i;
  Array.prototype.forEach.call(document.querySelector('div.dropdown-menu.show').attributes.getNamedItem('style').value.split(';'), (c, i, l) => {
    if($regextop.exec(c) !== null){
      $style += ' top: ' + document.querySelector('header').offsetHeight + 'px;';
    } else if ($regexminwidth.exec(c) !== null){
      $style+= ' max-width: ' + (screen.width - 70) + 'px;';
    } else {
      $style+=c + ';';
    }
  });
  document.querySelector('div.dropdown-menu.show').attributes.removeNamedItem('style');
  document.querySelector('div.dropdown-menu.show').setAttribute('style', $style);
});
    $('#fEditOS').submit(function (e) {
      $("[name='dtPersonas']").val(JSON.stringify($('#tPersonal').bootstrapTable('getData')));
      $("[name='dtActivos']").val(JSON.stringify($('#tActivoFijo').bootstrapTable('getData')));
if(!paqueteP){
    $("[name='dtInsumos']").val(JSON.stringify($('#t_Insumos').bootstrapTable('getData')));
}else{
    $("[name='dtInsumos']").val(JSON.stringify($('#tInsumos').bootstrapTable('getData')));
}
    });
});

// Move progress bar
function moveProgressBar(progressValue) {
  $('.progress-bar').css('width', progressValue + '%').attr('aria-valuenow', progressValue);
}
