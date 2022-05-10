'use strict';

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

$(document).ready(function () {
  $('#ccCheck1').change(function () {
    document.getElementById('Cc1').setAttribute('style', 'display: true');
    document.getElementById('ccCheck2').setAttribute('style', 'display: true');
  });
  $('#ccCheck2').change(function () {
    document.getElementById('Cc2').setAttribute('style', 'display: true');
  });
  //feather.replace();
  //$('#iFechaInicio').datetimepicker({
    //locale: 'es',
    //format: 'L',
    //date: eosFechaInicio,
  //});
  //$('#iFechaFin').datetimepicker({
    //locale: 'es',
    //format: 'L',
    //date: eosFechaFin,
  //});
  //$('#osTotal').val(0);
  //$('#osiTotal').val(0);
  // Wizard steps
  $('#stepOne').click(function (e) {
    e.preventDefault();
    var progressValue = 20;
    $("#stepOne").addClass("active");
    $("#stepTwo").removeClass("active");
    $("#stepThree").removeClass("active");
    $("#stepFour").removeClass("active");
    $("#stepFive").removeClass("active");
    $("#stepSix").removeClass("active");
    $("#infoGral").fadeIn('fast');
    $("#infoResponsable").fadeOut('fast');
    $("#infoPersonal").fadeOut('fast');
    $("#infoInsumos").fadeOut('fast');
    $("#evidencias").fadeOut('fast');
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
      $("#stepSix").removeClass("active");
      $("#infoResponsable").fadeIn('fast');
      $("#infoGral").fadeOut('fast');
      $("#infoPersonal").fadeOut('fast');
      $("#infoInsumos").fadeOut('fast');
      $("#evidencias").fadeOut('fast');
      $("#infoActivosFijos").fadeOut('fast');
      moveProgressBar(progressValue);

      $('#tab-lists a[href="#infoResponsable"]').tab('show');

    });
  });

  $('#stepThree').click(function (e) {
    e.preventDefault();

    $('#fEditOS').parsley().whenValidate({
      group: 'block-' + 0
    }).done(function () {

      var progressValue = 60;
      $("#stepThree").addClass("active");
      $("#stepOne").removeClass("active");
      $("#stepTwo").removeClass("active");
      $("#stepFour").removeClass("active");
      $("#stepFive").removeClass("active");
      $("#stepSix").removeClass("active");
      $("#infoPersonal").fadeIn('fast');
      $("#infoGral").fadeOut('fast');
      $("#infoResponsable").fadeOut('fast');
      $("#infoInsumos").fadeOut('fast');
      $("#evidencias").fadeOut('fast');
      $("#infoActivosFijos").fadeOut('fast');
      moveProgressBar(progressValue);

      $('#tab-lists a[href="#infoPersonal"]').tab('show');

      $.ajax({
        url: '/Rest/GetPersonas',
        success: function (data, status, jqXHR) {
          console.log(data);
          if (data != null) {
            var _list = data["listPersonas"];
            $('#osPersonal').empty().append('<option> -- Selecciona una opci&oacute;n -- </option>');
            $.each(_list, function (idx, d) {
              $('#osPersonal').append($('<option>', {
                value: d.id,
                text: d.nombre + " " + d.paterno + " " + d.materno
              }).data('sPuesto', d.categoria));
            });
          }
        },
        error: function (obj) {
          console.log(obj);
        },
      });
    });

  });

  $('#stepFour').click(function (e) {
    e.preventDefault();

    $('#fEditOS').parsley().whenValidate({
      group: 'block-' + 0
    }).done(function () {

      var progressValue = 80;
      $("#stepFour").addClass("active");
      $("#stepTwo").removeClass("active");
      $("#stepThree").removeClass("active");
      $("#stepFive").removeClass("active");
      $("#stepSix").removeClass("active");
      $("#stepOne").removeClass("active");
      $("#infoActivosFijos").fadeIn('fast');
      $("#infoInsumos").fadeOut('fast');
      $("#evidencias").fadeOut('fast');
      $("#infoGral").fadeOut('fast');
      $("#infoResponsable").fadeOut('fast');
      $("#infoPersonal").fadeOut('fast');
      moveProgressBar(progressValue);

      $('#tab-lists a[href="#infoActivosFijos"]').tab('show');

      $.ajax({
        url: '/Rest/GetActivoFijos',
        success: function (data, status, jqXHR) {
          console.log(data);
          if (data != null) {
            var _list = data["listActivos"];
            $('#osActivoFijo').empty().append('<option> -- Selecciona una opci&oacute;n -- </option>');
            $.each(_list, function (idx, d) {
              $('#osActivoFijo').append($('<option>', {
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
    });
  });

  $('#stepFive').click(function (e) {
    e.preventDefault();

    $('#fEditOS').parsley().whenValidate({
      group: 'block-' + 0
    }).done(function () {

      var progressValue = 100;
      $("#stepFive").addClass("active");
      $("#stepSix").removeClass("active");
      $("#stepFour").removeClass("active");
      $("#stepTwo").removeClass("active");
      $("#stepThree").removeClass("active");
      $("#stepOne").removeClass("active");
      $("#infoInsumos").fadeIn('fast');
      $("#evidencias").fadeOut('fast');
      $("#infoGral").fadeOut('fast');
      $("#infoResponsable").fadeOut('fast');
      $("#infoPersonal").fadeOut('fast');
      $("#infoActivosFijos").fadeOut('fast');
      moveProgressBar(progressValue);

      $('#tab-lists a[href="#infoInsumos"]').tab('show');

      $.ajax({
        url: '/Rest/GetInsumos',
        success: function (data, status, jqXHR) {
          console.log(data);
          if (data != null) {
            var _list = data["listInsumos"];
            $('#osInsumo').empty().append('<option> -- Selecciona una opci&oacute;n -- </option>');
            $.each(_list, function (idx, d) {
              $('#osInsumo').append($('<option>', {
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
    });
  });

  $('#stepSix').click(function (e) {
    e.preventDefault();

    $('#fEditOS').parsley().whenValidate({
      group: 'block-' + 0
    }).done(function () {

      var progressValue = 100;
      $("#stepSix").addClass("active");
      $("#stepFive").removeClass("active");
      $("#stepFour").removeClass("active");
      $("#stepTwo").removeClass("active");
      $("#stepThree").removeClass("active");
      $("#stepOne").removeClass("active");
      $("#evidencias").fadeIn('fast');
      $("#infoInsumos").fadeOut('fast');
      $("#infoGral").fadeOut('fast');
      $("#infoResponsable").fadeOut('fast');
      $("#infoPersonal").fadeOut('fast');
      $("#infoActivosFijos").fadeOut('fast');
      moveProgressBar(progressValue);

      $('#tab-lists a[href="#evidencias"]').tab('show');

      /*$.ajax({
                  url: '/Rest/GetInsumos',
                  success: function (data, status, jqXHR) {
                      console.log(data);
                      if (data != null) {
                          var _list = data["listInsumos"];
                          $('#osInsumo').empty().append('<option> -- Selecciona una opci&oacute;n -- </option>');
                          $.each(_list, function (idx, d) {
                              $('#osInsumo').append($('<option>', {
                                  value: d.id,
                                  text: d.descripcion
                              }));
                          });
                      }
                  },
                  error: function (obj) {
                      console.log(obj);
                  },
              });*/
    });
  });

  /* Step one */
  $('#iCliente').change(function () {
    var sCliente = $(this).find(":selected").val();

    $.ajax({
      url: '/Rest/GetContratosFromCliente',
      data: { 'clienteid': sCliente },
      success: function (data, status, jqXHR) {
        console.log(data);
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
        console.log(data);
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

  /* Step Two */

  /* Step Three */
  $('#btnAddPersonal').click(function () {
    var sPersona = $('#osPersonal').find(":selected");
    var tIdx = $('#tPersonal').bootstrapTable('getData').length;
    var data = {};

    if (sPersona.val() > 0) {
      data.id = tIdx + 1;
      data.idpersona = sPersona.val();
      data.idordper = 0;
      data.nombre = sPersona.text();
      data.puesto = sPersona.data('sPuesto');
      data.actions = "<button id=idx_" + (tIdx + 1) + " type=\"button\" class=\"btn btn-danger btn-sm\"><i data-feather=\"trash\" width=\"10\" height=\"10\"></i></button>"
        + " <input type=\"hidden\" name=\"Personal[" + tIdx + "].Id\" value=\"" + 0 + "\">"
        + " <input type=\"hidden\" name=\"Personal[" + tIdx + "].PersonaId\" value=\"" + sPersona.val() + "\">";

      $('#tPersonal').bootstrapTable('append', data);

      var total = $('#tPersonal').bootstrapTable('getData').length;
      //$('#osTotal').val(total);

      feather.replace();
    } else {
      $('#aPCont').text("No se pudo completar la asignación, no se tiene seleccionada una persona.");
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
      if (d["id"] != ids) {
        var p = {};
        p.id = aidx + 1;
        p.idpersona = d.idpersona;
        p.idordper = d.idordper;
        p.nombre = d.nombre;
        p.puesto = d.puesto;
        p.actions = "<button id=idx_" + (aidx + 1) + " type=\"button\" class=\"btn btn-danger btn-sm\"><i data-feather=\"trash\" width=\"10\" height=\"10\"></i></button>"
          + " <input type=\"hidden\" name=\"Personal[" + aidx + "].Id\" value=\"" + d.idordper + "\">"
          + " <input type=\"hidden\" name=\"Personal[" + aidx + "].PersonaId\" value=\"" + d.idpersona + "\">";
        data.push(p);
        aidx++;
      }
    });
    $('#tPersonal').bootstrapTable('load', data);
    var total = $('#tPersonal').bootstrapTable('getData').length;
    //$('#osTotal').val(total);
    feather.replace();
  });

  /* Step Four */
  $('#btnActivoFijo').click(function () {
    var sActivoFijo = $('#osActivoFijo').find(":selected");
    var tIdx = $('#tActivoFijo').bootstrapTable('getData').length;
    var data = {};

    if (sActivoFijo.val() > 0) {
      data.idaf = tIdx + 1;
      data.idactivofijo = sActivoFijo.val();
      data.idordact = 0;
      data.activo = sActivoFijo.text();
      data.actions = "<button id=idx_" + (tIdx + 1) + " type=\"button\" class=\"btn btn-danger btn-sm\"><i data-feather=\"trash\" width=\"10\" height=\"10\"></i></button>"
        + " <input type=\"hidden\" name=\"ActivosFijos[" + tIdx + "].Id\" value=\"" + 0 + "\">"
        + " <input type=\"hidden\" name=\"ActivosFijos[" + tIdx + "].ActivoFijoId\" value=\"" + sActivoFijo.val() + "\">";

      $('#tActivoFijo').bootstrapTable('append', data);
      var total = $('#tActivoFijo').bootstrapTable('getData').length;
      $('#osafTotal').val(total);

      //feather.replace();
    } else {
      $('#aAFCont').text("No se pudo completar la asignación, no se tiene seleccionada un activo fijo.");
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
      if (d["idaf"] != ids) {
        var p = {};
        p.idaf = aidx + 1;
        p.idactivofijo = d.idactivofijo;
        p.idordact = d.idordact;
        p.activo = d.activo;
        p.actions = "<button id=idx_" + (aidx + 1) + " type=\"button\" class=\"btn btn-danger btn-sm\"><i data-feather=\"trash\" width=\"10\" height=\"10\"></i></button>"
          + " <input type=\"hidden\" name=\"ActivosFijos[" + aidx + "].Id\" value=\"" + d.idordact + "\">"
          + " <input type=\"hidden\" name=\"ActivosFijos[" + aidx + "].ActivoFijoId\" value=\"" + d.idactivofijo + "\">";
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
    var sInsumo = $('#osInsumo').find(":selected");
    var tIdx = $('#tInsumos').bootstrapTable('getData').length;
    var data = {};

    if (sInsumo.val() > 0) {
      data.idi = tIdx + 1;
      data.idinsumo = sInsumo.val();
      data.idordins = 0;
      data.insumo = sInsumo.text();
      data.cantidad = $('#osCantidad').val();
      data.actions = "<button id=idx_" + (tIdx + 1) + " type=\"button\" class=\"btn btn-danger btn-sm\"><i data-feather=\"trash\" width=\"10\" height=\"10\"></i></button>"
        + " <input type=\"hidden\" name=\"Insumos[" + tIdx + "].Id\" value=\"" + 0 + "\">"
        + " <input type=\"hidden\" name=\"Insumos[" + tIdx + "].InsumoId\" value=\"" + sInsumo.val() + "\">"
        + " <input type=\"hidden\" name=\"Insumos[" + tIdx + "].Cantidad\" value=\"" + $('#osCantidad').val() + "\">";

      $('#tInsumos').bootstrapTable('append', data);

      var total = $('#tInsumos').bootstrapTable('getData').length;
      //$('#osiTotal').val(total);

      //feather.replace();
    } else {
      $('#aICont').text("No se pudo completar la asignación, no se tiene seleccionada un insumo.");
      $('#aICont').parent().removeClass('invisible');
      $('#aICont').parent().removeClass('visible');
    }
  });

  $(document).on('click', '#tInsumos :button', function (e) {
    var ids = $(this).attr('id').split('_')[1];
    var befDelete = $('#tInsumos').bootstrapTable('getData');
    var data = [];
    var aidx = 0;
    $.each(befDelete, function (idx, d) {
      if (d["idi"] != ids) {
        var p = {};
        p.idi = aidx + 1;
        p.idinsumo = d.idinsumo;
        p.idordins = d.idordins;
        p.insumo = d.insumo;
        p.cantidad = d.cantidad;
        p.actions = "<button id=idx_" + (aidx + 1) + " type=\"button\" class=\"btn btn-danger btn-sm\"><i data-feather=\"trash\" width=\"10\" height=\"10\"></i></button>"
          + " <input type=\"hidden\" name=\"Insumos[" + aidx + "].Id\" value=\"" + d.idordins + "\">"
          + " <input type=\"hidden\" name=\"Insumos[" + aidx + "].InsumoId\" value=\"" + d.idinsumo + "\">"
          + " <input type=\"hidden\" name=\"Insumos[" + aidx + "].Cantidad\" value=\"" + d.cantidad + "\">";
        data.push(p);
        aidx++;
      }
    });
    $('#tInsumos').bootstrapTable('load', data);
    var total = $('#tInsumos').bootstrapTable('getData').length;
    //$('#osiTotal').val(total);
    //feather.replace();
  });

});

// Move progress bar
function moveProgressBar(progressValue) {
  $('.progress-bar').css('width', progressValue + '%').attr('aria-valuenow', progressValue);
}
