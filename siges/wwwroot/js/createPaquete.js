$(document).ready(function(){
  //feather.replace();
  //$('#btnCreaPaquete').submit(function() {
  //console.log(JSON.stringify($('#tPaquetesInsumo').bootstrapTable('getData')));
  //$("[name='JsonInsumos']").val(JSON.stringify($('#tPaquetesInsumo').bootstrapTable('getData')));
  //});
});

document.getElementById('btnCreaPaquete').addEventListener('click', e => {
  document.getElementById('JsonInsumos').value = JSON.stringify($('#tPaquetesInsumo').bootstrapTable('getData'));
});

document.getElementById('insumos').addEventListener('input', e => {
  var iId = e.srcElement.options[e.srcElement.options.selectedIndex].value;
console.log({iId});
  if(iId != '')
    $.ajax({
      url: '/Rest/GetInsumoById',
      data: {
        'iId': iId
      },
      success: (insumo, status, jqXHR) => {
        var tIdx = $('#tPaquetesInsumo').bootstrapTable('getData').length;
        var row = {};
        row.idxId = tIdx + 1;
        row.iId = insumo.id;
        row.descripcion = insumo.descripcion;
        row.clave = insumo.clave;
        row.marca = insumo.marca;
        row.tipo = insumo.tipo;
        row.actions = "<button id=idx_" + (tIdx + 1) + " type=\"button\" class=\"btn btn-danger btn-sm\"><i class=\"fas fa-trash\" width=\"10\" height=\"10\"></i></button>";
        $('#tPaquetesInsumo').bootstrapTable('append', row);
        //feather.replace();
        e.srcElement.options.selectedIndex = '';
      }
    });
});

$(document).on('click', '#tPaquetesInsumo :button', function (e) {
  var ids = $(this).attr('id').split('_')[1];
  var befDelete = $('#tPaquetesInsumo').bootstrapTable('getData');
  var data = [];
  var aidx = 0;
  $.each(befDelete, function (idx, d) {
    if (String(d["idxId"]) !== String(ids)) {
      var p = {};
      p.idxId = aidx + 1;
      p.iId = d.iId;
      p.descripcion = d.descripcion;
      p.clave = d.clave;
      p.marca = d.marca;
      p.tipo = d.tipo;
      p.actions = "<button id=idx_" + (aidx + 1) + " type=\"button\" class=\"btn btn-danger btn-sm\"><i class=\"fas fa-trash\" width=\"10\" height=\"10\"></i></button>";
      data.push(p);
      aidx++;
    }
  });
  $('#tPaquetesInsumo').bootstrapTable('load', data);
});
