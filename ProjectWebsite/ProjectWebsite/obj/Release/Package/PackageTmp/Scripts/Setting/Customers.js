var tukhoa = '';
var trang = 1;
$(document).ready(function () {
    LoadListCUS(tukhoa, trang);
})
$('#txtSearch').on('keypress', () => {
    tukhoa = $('#txtSearch').val();
    LoadListCUS(tukhoa, trang);
});

//Sự kiện phân trang:
$('#pageNumber').on("click", "li", function (e) {

    e.preventDefault();
    trang = $(this).text().substr(6, 10);
    LoadListCUS(tukhoa, trang);
});
//Sự kiện xóa
$(document).on('click', "a[name='delete']", function () {
    let idCus = $(this).closest('tr').attr('id');
    if (confirm("Bạn có chắc chắn muốn xóa?")) {
        $.ajax({
            url: '/Admin/Delete',
            type: 'post',
            data: {
                id: idCus
            },
            success: function (data) {
                if (data.code == 200) {
                    alert(data.msg);
                    LoadListCUS(tukhoa, trang);
                } else {
                    alert(data.msg);
                }
            }
        });
    }
});
//Sự kiện load
function LoadListCUS(search, trang) {
    $.ajax({
        url: '/Admin/GetData',
        type: 'get',
        data: {
            tukhoa: search,
            trang: trang
        },
        success: function (data) {
            if (data.code == 200) {
                var x = 1;
                $('#tblCustomers').empty();
                $.each(data.ListCUS, function (i, v) {
                    var dt = new Date(parseInt(v.Birthday.replace('/Date(', '')))
                    var dtFinal = AddLeadingZeros(dt.getDate(), 2) + '/' +
                        AddLeadingZeros(dt.getMonth() + 1, 2) + '/' +
                        AddLeadingZeros(dt.getFullYear(), 4)
                    let tr = '<tr id="' + v.CustomerID + '">'
                    tr += '<td style="color:black;text-align:center">' + x + '</td>';
                    tr += '<td style="color:black;text-align:center">' + v.CustomerName + '</td>';
                    tr += '<td style="color:black;text-align:center">' + v.CustomerSex + '</td>';
                    tr += '<td style="color:black;text-align:center">' + dtFinal + '</td>';
                    tr += '<td style="color:black;text-align:center">' + v.CustomerAddress + '</td>';
                    tr += '<td style="color:black;text-align:center">' + v.Username + '</td>';
                    tr += '<td style="color:black;text-align:center">' + v.CustomerPhone + '</td>';
                    tr += '<td style="color:black;text-align:center">' + v.CustomerEmail + '</td>';
                    tr += '<td style="text-align:center">';
                    tr += '<a class="btn btn-danger btn-xs" name = "delete"><i class="fa fa-trash-o"></i></a>';
                    tr += '</td>';
                    tr += '</tr > ';
                    x++;
                    $('#tblCustomers').append(tr);


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
//Hàm xử lý thời gian
function AddLeadingZeros(number, size) {
    var s = "0000" + number;
    return s.substr(s.length - size);
}

