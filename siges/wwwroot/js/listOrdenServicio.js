'use strict';

var selectMenuCount = 0;
var currentData = {};
var filterDataEvent = {};
var filters = [];

function fCtrlFecha(){
  var df = document.querySelector('.fa.glyphicon-trash.icon-clear');
  df.setAttribute('data-feather', 'delete');
  df.setAttribute('width', '18');
  df.setAttribute('height', '18');
  var $meses = new Set();
  var $anio = new Set();
  var $filterControlFecha = document.querySelector('.form-control.bootstrap-table-filter-control-fecha');
  var $parentFilterCtrlFecha = $filterControlFecha.parentElement;
  Array.prototype.forEach.call($filterControlFecha.children, (c, i, l) => {
    $meses.add(moment(reOrderDate(c.value)).format('MMMM'));
    $anio.add(moment(reOrderDate(c.value)).format('YYYY'));
  });
  for (var i = $filterControlFecha.childElementCount; i > 0; i--){
    $filterControlFecha.removeChild($filterControlFecha.firstElementChild);
  }
  var option = document.createElement('option');
  option.setAttribute('value', '');
  option.setAttribute('selected', 'true');
  var selectYear = document.createElement('option');
  selectYear.setAttribute('value', 'year');
  selectYear.innerHTML = 'Filtrar por año';
  $filterControlFecha.appendChild(option);
  $filterControlFecha.appendChild(selectYear);
  for(var m of $meses){
    var mes = document.createElement('option');
    mes.setAttribute('value', m);
    mes.innerHTML = m;
    $filterControlFecha.appendChild(mes);
  }
  console.log({$meses}, $meses.length, {$anio});
}

function filterCtrlDatepicker(){
  var $filterControlFecha = document.querySelector('.form-control.bootstrap-table-filter-control-fecha');
  $filterControlFecha.setAttribute('id', 'fdatetimepicker');
  $filterControlFecha.setAttribute('data-toggle', 'datetimepicker');
  $filterControlFecha.setAttribute('data-target', '#fdatetimepicker');
  var classFCF = $filterControlFecha.attributes.getNamedItem('class');
  $filterControlFecha.removeAttribute('class');
  $filterControlFecha.setAttribute('class', 'form-control datetimepicker-input' + ' ' + classFCF.value);
  $('#fdatetimepicker').datetimepicker({
    locale: 'es',
    format: 'L'
  });
  console.log({classFCF});
}

function filtersselectors(dataTable){
  if(Object.entries(dataTable).length > 0){
    var selectors = new Map();
    Array.prototype.forEach.call(getTableHeads(), (fs, fsi, fsl)=>{
      var items = new Set();
      Array.prototype.forEach.call(dataTable, (c, i, l) => {
        if(fs.attributes.getNamedItem('data-field').value == 'fecha'){
          selectors.set(fs.attributes.getNamedItem('data-field').value, items.add(moment(reOrderDate((new Map(Object.entries(c))).get(fs.attributes.getNamedItem('data-field').value))).format("MMMM") != 'Invalid date' ? moment(reOrderDate((new Map(Object.entries(c))).get(fs.attributes.getNamedItem('data-field').value))).format("MMMM") : (new Map(Object.entries(c))).get(fs.attributes.getNamedItem('data-field').value)));
        } else {
          selectors.set(fs.attributes.getNamedItem('data-field').value, items.add((new Map(Object.entries(c))).get(fs.attributes.getNamedItem('data-field').value)));
        }
      });
    });
    return selectors;
  }
}

function executeFilter(event, tableData){
  const tOrdenes = $('#tableOrdenesServicio');
  var m = [];
  var i = {};
  Array.prototype.forEach.call(tableData, (c, i, l) => {
    console.log({i}, {c});
    //if(event.srcElement.value == (new Map(Object.entries(c))).get(event.target.id)){
    //console.log(event.srcElement.value, ' == ', (new Map(Object.entries(c))).get(event.target.id));
    //m.push((new Map(Object.entries(c))).get(event.target.id));
    //}
  });
  //i[event.target.id] = m;
  //tOrdenes.bootstrapTable('filterBy', i);
}

function getTableHeads(){
  var thArray = [];
  var regex = /\d/i;
  Array.prototype.forEach.call(document.getElementById('tableOrdenesServicio').querySelectorAll('th'), function(current, index, list){
    if(current.attributes.getNamedItem('data-field') != null && current.attributes.getNamedItem('data-field').value != 'folio'){
      if(regex.exec(current.attributes.getNamedItem('data-field').value) == null){
        thArray.push(current);
      }
    }
  });
  //console.log('getTableHeads:', {thArray});
  return thArray;
}

$('select.form-control.bootstrap-table-filter-control-estatus').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
  console.log('algo haz');
});

/* ******************    PRESTAR ATENCIÓN A ESTA LÍNEA ******************** */
//$('select.bootstrap-table-filter-control-servicio').selectpicker();
/* ******************    ATENCIÓN PARA MULTISELECT ******************** */
//$('select.bootstrap-table-filter-control-servicio').on('changed.bs.select', function(e, clickedIndex, isSelectectd, previousValue){
  //console.log({e});
//});

function createFiltersSelectors(map){
console.log({map})
  if(map != null || map != undefined)
    Array.prototype.forEach.call(getTableHeads(), (c, i, l) => {
      if(map.get(c.attributes.getNamedItem('data-field').value).size > 1){
        var select = c.querySelector('select.form-control.bootstrap-table-filter-control-' + c.attributes.getNamedItem('data-field').value);
        if(select != null){
          for(let i = select.childElementCount; i > 0; i--){
            select.removeChild(select.firstElementChild);
          }
          map.get(c.attributes.getNamedItem('data-field').value).forEach( item => {
            console.log("map.get()", {item});
            var selectedOp = document.createElement('option');
            //selectedOp.setAttribute('selected', 'true');
            //selectedOp.setAttribute('value', item);
            //selectedOp.setAttribute('id', 'selectedOp-' + c.attributes.getNamedItem('data-field').value);
            //selectedOp.innerHTML = item;
            //selectedOp.innerText = item;
            //select.appendChild(selectedOp);
          });
          console.log("rellenar => ", {select});
        }
      } else {
        var select = c.querySelector('select.form-control.bootstrap-table-filter-control-' + c.attributes.getNamedItem('data-field').value);
        if(select != null){
          for(let i = select.childElementCount; i > 0; i--){
            select.removeChild(select.firstElementChild);
          }
          //console.log("map.get(c.attributes.getNamedItem('data-field').value): ", map.get(c.attributes.getNamedItem('data-field').value));
          //console.log('Dejar seleccionado: ', {select});
          //console.log('map.get(c.attributes.getNamedItem(\'data-field\').value): ', map.get(c.attributes.getNamedItem('data-field').value));
          map.get(c.attributes.getNamedItem('data-field').value).forEach( item => {
          console.log('item: ', {item});
            var selectedOp = document.createElement('option');
            selectedOp.setAttribute('selected', 'true');
            selectedOp.setAttribute('value', item);
            selectedOp.setAttribute('id', 'selectedOp-' + c.attributes.getNamedItem('data-field').value);
            selectedOp.innerHTML = item;
            selectedOp.innerText = item;
            select.appendChild(selectedOp);
          });
          select.setAttribute('disabled', 'true');
        }
      }
    });
}

function reOrderDate(a){
  if(a != undefined){
    var ano = a.substring(a.lastIndexOf('/')+1);
    var mes = a.substring(a.indexOf('/')+1, a.lastIndexOf('/'));
    var dia = a.substring(0, a.indexOf('/'));
    return ano+'-'+mes+'-'+dia;
  }
}

//function dateSort(b, a, rowB, rowA){
  //var currentDate = moment(reOrderDate(a));
  //var nextDate = moment(reOrderDate(b));
  //if(currentDate.isSame(nextDate))
    //return 0;
  //if(currentDate.isBefore(nextDate))
    //return 1;
  //if(currentDate.isAfter(nextDate))
    //return -1;
//}

$('#tableOrdenesServicio').on('reset-view.bs.table', function alguna(params) {
 // createFiltersSelectors(filtersselectors($('#tableOrdenesServicio').bootstrapTable('getData')));
  console.log('table Ordenes de servicio evento');
});

document.addEventListener('click', e => {
  console.log(e.srcElement);
  if(e.srcElement.attributes.getNamedItem('class') != null)
    if(e.srcElement.attributes.getNamedItem('class').value == 'btn btn-secondary filter-show-clear' /*|| e.srcElement.attributes.getNamedItem('class').value == 'feather feather-delete fa glyphicon-trash icon-clear'*/){
      location.reload();
      Array.prototype.forEach.call(getTableHeads(), (c, i, l) => {
        var select = c.querySelector('select.form-control.bootstrap-table-filter-control-' + c.attributes.getNamedItem('data-field').value);
        if(select != null){
          if(select.attributes.getNamedItem('disabled') != null)
            select.removeAttribute('disabled');
          if(c.querySelector('#selectedOp-'+c.attributes.getNamedItem('data-field').value) != null){
            c.querySelector('#selectedOp-'+c.attributes.getNamedItem('data-field').value).removeAttribute('value');
            c.querySelector('#selectedOp-'+c.attributes.getNamedItem('data-field').value).parentElement.removeChild(c.querySelector('#selectedOp-'+c.attributes.getNamedItem('data-field').value));
          }
          var emptyOp = document.createElement('option');
          emptyOp.setAttribute('value', '');
          select.appendChild(emptyOp);
          location.reload();//c.removeChild(c.querySelector('#selectedOp-'+c.attributes.getNamedItem('data-field').value));
        }
      });
    } else /*if(e.srcElement.attributes.getNamedItem())*/{
      console.log('===========', e.srcElement);
    }
});
    
//$(document).ready(function () {
  //var df = document.querySelector('.fa.glyphicon-trash.icon-clear');
  //df.setAttribute('data-feather', 'delete');
  //df.setAttribute('width', '17');
  //df.setAttribute('height', '17');
  //feather.replace();
  //$('[data-toggle="tooltip"]').tooltip();
  /*$(document).on('click', '#tableOrdenesServicio .btn-danger', function (e) {
    var dBtn = $(this);
    $('#delNo').text(dBtn.data("sFolio"));
    $('#delNo').data("sId", dBtn.data("sId"));
    });*/

    /*$('#delBtn').click(function () {
      var ids = $('#delNo').data('sId');
      console.log('Del: ' + ids);

      $.ajax({
      url: '/Servicio/DeleteOS',
      data: { 'osid': ids },
      success: function (data, status, jqXHR) {
      console.log(data);
      $('#tableOrdenesServicio').bootstrapTable('removeByUniqueId', ids);
      $('#delNo').removeData('sId');
      },
      error: function (obj) {
      console.log(obj);
      },
      });
      });*/

      /*document.addEventListener('input', e => {
        if(e.srcElement.parentElement.attributes.getNamedItem('class').value == 'filter-control')
        filterDataEvent = e;
        });*/

//});
