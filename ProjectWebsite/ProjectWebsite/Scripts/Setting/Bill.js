$(document).ready(function () {
    var billJS = new BillJS();

})

var tukhoa = '';
var trang = 1;
class BillJS {
    constructor() {
        this.loadData(tukhoa, trang);
        this.initEvents();
    }
    initEvents() {
        $('#txtSearch').keypress(this.SeachOnChanges.bind(this));       
    }
    loadData(search, trang) {
        $.ajax({
            url: '/Admin/GetBill',
            type: 'get',
            data: {
                tukhoa: search,
                trang: trang
            },
            success: function (data) {
                if (data.code == 200) {
                    var x = 1;
                    $('#tblBill').empty();
                    $.each(data.ListBill, function (i, v) {
                        var dt = new Date(parseInt(v.DateCreated.replace('/Date(', '')))
                        var dtFinal = AddLeadingZeros(dt.getDate(), 2) + '/' +
                            AddLeadingZeros(dt.getMonth() + 1, 2) + '/' +
                            AddLeadingZeros(dt.getFullYear(), 4)
                        let tr = '<tr id="' + v.ID + '">'
                        tr += '<td style="color:black;text-align:center;padding-bottom: 10px;padding-top: 12px;">' + x + '</td>';
                        tr += '<td style="color:black;text-align:center;padding-bottom: 10px;padding-top: 12px;">' + dtFinal + '</td>';
                        tr += '<td style="color:black;text-align:center;padding-bottom: 10px;padding-top: 12px;">' + v.CustomerName + '</td>';
                        tr += '<td style="color:black;text-align:center;padding-bottom: 10px;padding-top: 12px;">' + v.FullName + '</td>';
                        tr += '<td style="color:black;text-align:center;padding-bottom: 10px;padding-top: 12px;">' + v.Address + '</td>';
                        tr += '<td style="color:black;text-align:center;padding-bottom: 10px;padding-top: 12px;">' + v.Phone + '</td>';
                        tr += '<td style="color:black;text-align:center;padding-bottom: 10px;padding-top: 12px;">' + v.TotalMoney.toLocaleString('it-IT', { style: 'currency', currency: 'VND' }) + '</td>';
                        if (v.Status == 0) {
                            tr += '<td style="color:black;text-align:center;padding-bottom: 10px;padding-top: 12px;">  <a name = "status" class="btn btn-primary" style="cursor:pointer">Chưa thanh toán</a>  </td>';
                        }
                        else {
                            tr += '<td style="color:black;text-align:center;padding-bottom: 10px;padding-top: 12px;">  <a name = "status" class="btn btn-danger" style="cursor:pointer">Đã thanh toán</a>  </td>';
                        }
                        tr += '<td style="text-align:center;padding-bottom: 10px;padding-top: 10px;">';
                        tr += '<a class="btn btn-danger btn-xs" style="height:30px;width:31px;text-align:center" name = "delete"><i class="fa fa-trash-o" style="font-size: 25px;"></i></a>';
                        tr += '<a class="btn btn-info btn-xs" style="margin-left:5px;height:30px;width:31px;text-align:center;" name = "detailds"><i style="font-size: 25px;"   class="fa fa-pencil"></i></a>';
                        tr += '</td>';
                        tr += '</tr>';
                        $('#tblBill').append(tr);
                        x++;
                    });
                    if (data.Sotrang > 1) {
                        $('#pageNumber').empty();

                        for (i = 1; i <= data.Sotrang; i++) {
                            let li = '';
                            if (i == 1) {
                                li = '<li style="cursor:pointer;margin-left:4px" class = "active"><a>Trang ' + i + '</a></li >';
                            }
                            else {
                                li = '<li style="cursor:pointer;margin-left:4px"><a>Trang ' + i + '</a></li >';
                            }
                            $('#pageNumber').append(li);
                        }
                    }
                    else {
                        $('#pageNumber').empty();
                    }
                }
            }
        });
    }

    SeachOnChanges() {
        tukhoa = $("#txtSearch").val();
        this.loadData(tukhoa, trang);
    }
}
//Hàm xử lý thời gian
function AddLeadingZeros(number, size) {
    var s = "0000" + number;
    return s.substr(s.length - size);
}

// Sự kiện change status
$(document).on('click', "a[name='status']", function () {
    var idBill = $(this).closest('tr').attr('id');
    $.ajax({
        url: '/Admin/ChangStatus',
        type: 'post',
        data: {
            id: idBill
        },
        success: function (data) {
            if (data.code == 200) {
                var billJS = new BillJS();
                billJS.loadData(tukhoa, trang);
            } else {
                toastr.error(data.msg);
            }
        }
    });
});

// Sự kiện xóa dữ liệu

//Sự kiện xóa
$(document).on('click', "a[name='delete']", function () {
    var billJS = new BillJS();
    let IdBill = $(this).closest('tr').attr('id');
    if (confirm("Bạn có chắc chắn muốn xóa?")) {
        $.ajax({
            url: '/Admin/DeleteBill',
            type: 'post',
            data: {
                id: IdBill
            },
            success: function (data) {
                if (data.code == 200) {
                    toastr.success(data.msg);
                    billJS.loadData(tukhoa, trang);
                } else {
                    toastr.error(data.msg);
                }
            }
        });
    }
});

$(document).on('click', "a[name='detailds']", function () {
    var billJS = new BillJS();
    let IdBill = $(this).closest('tr').attr('id');
    $('#modalTitle').text("Chi tiết đơn hàng");
    $.ajax({
        url: '/Admin/GetTableDetails',
        type: 'get',
        data: {
            id: IdBill
        },
        success: function (data) {
            if (data.code == 200) {
                var x = 1;
                $('#tableBills').empty();
                $.each(data.itemdt, function (i, v) {
                    let tr = '<tr>'
                    tr += '<td style="text-align:center;padding-bottom: 30px;padding-top: 30px;color:#000;">' + x + '</td>';
                    tr += '<td style="text-align:center;"><img src="../Content/images/' + v.ProductImage + '" style="border: 1px solid #ddd; border-radius: 4px; padding: 5px; width: 80px;height:80px" /></td>';
                    tr += '<td style="text-align:center;padding-bottom: 30px;padding-top: 30px;color:#000">' + v.ProductName + '</td>';
                    tr += '<td style="text-align:center;padding-bottom: 30px;padding-top: 30px;color:#000">' + v.SizeName + '</td>';                 
                    tr += '<td style="text-align:center;padding-bottom: 30px;padding-top: 30px;color:#000">' + v.ProductCount + '</td>';
                    tr += '<td style="text-align:center;padding-bottom: 30px;padding-top: 30px;color:#000">' + v.ProductPrice.toLocaleString('it-IT', { style: 'currency', currency: 'VND' }) + '</td>';
                    tr += '</tr>';
                    $('#tableBills').append(tr);
                    x++;
                });                
            }
        }
    })
    $('#modalProduct').show();
});

//Phân trang
$('#pageNumber').on("click", "li", function (e) {
    var billJS = new BillJS();
    e.preventDefault();
    trang = $(this).text().substr(6);
    billJS.loadData(tukhoa, trang);
});
//Đóng trang
function CloseModel() {
    $('#modalProduct').hide();
}

