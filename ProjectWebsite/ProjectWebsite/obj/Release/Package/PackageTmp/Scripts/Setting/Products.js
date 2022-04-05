var tukhoa = '';
var trang = 1;
$(document).ready(function () {
    LoadListProduct(tukhoa, trang);
    LoadBrand();
    LoadType();
    LoadSize();
    LoadSale();
})

$('#txtSearch').on('keypress', () => {
    tukhoa = $('#txtSearch').val();
    LoadListProduct(tukhoa, trang);
});


//Sự kiện thêm mới


//Sự kiện phân trang:
$('#pageNumber').on("click", "li", function (e) {
    e.preventDefault();
    trang = $(this).text().substr(6);
    LoadListProduct(tukhoa, trang);
});
//Sự kiện addd
function CloseModel() {
    $('#modalProduct').hide();
}
$('#btnAdd').click(function () {
    $('#modalTitle').text("Thêm mới sản phẩm");
    $('#idproduct').val('');//đánh dấu cho trường hợp thêm mới
    ClearText();
    $('#modalProduct').show();
});


//Sự kiện xóa
$(document).on('click', "a[name='delete']", function () {
    let idCus = $(this).closest('tr').attr('id');
    if (confirm("Bạn có chắc chắn muốn xóa?")) {
        $.ajax({
            url: '/Admin/DeleteProduct',
            type: 'post',
            data: {
                id: idCus
            },
            success: function (data) {
                if (data.code == 200) {
                    toastr.success(data.msg);
                    LoadListProduct(tukhoa, trang);
                } else {
                    toastr.error(data.msg);
                }
            }
        });
    }
});

$(document).on('click', "a[name='update']", function () {
    let idPro = $(this).closest('tr').attr('id');
    $('#idproduct').val(idPro);
    $('#modalTitle').text('Cập nhật sản phẩm');
    $.ajax({
        url: '/Admin/Detail',
        type: 'get',
        data: {
            id: idPro
        },
        success: function (data) {
            var dt = new Date(parseInt(data.allprocduct.DateInsert.replace('/Date(', '')))
            var dtFinal = AddLeadingZeros(dt.getFullYear(), 4) + '-' +
                AddLeadingZeros(dt.getMonth() + 1, 2) + '-' +
                AddLeadingZeros(dt.getDate(), 2)
            $('#selectsale').val(data.allprocduct.Sale);
            $('#selecttype').val(data.allprocduct.Type);
            $('#selectsize').val(data.allprocduct.Size);
            $('#selectbrand').val(data.allprocduct.Brand);
            $('#txtprice').val(data.allprocduct.ProductPrice);
            $('#txtcount').val(data.allprocduct.ProductCount);
            $('#txtname').val(data.allprocduct.ProductName);
            $('#dateStart').val(dtFinal);
            $('#previewImage').attr("src", "../Content/images/" + data.allprocduct.ProductImage)
            $('#modalProduct').show();
        }
    })
});


function SaveData() {
    let ProductID = $('#idproduct').val().trim();
    let ProductName = $('#txtname').val();
    let ProductPrice = $('#txtprice').val();
    let ProductCount = $('#txtcount').val();
    let Size = $('#selectsize').val();
    let Type = $('#selecttype').val();
    let Brand = $('#selectbrand').val();
    let Sale = $('#selectsale').val();
    let DateInsert = $('#dateStart').val().trim();
    var file = $('#fileUpload').get(0).files;
    //Them cho truong hop them moi
    if (ProductID.length == 0) {
        var data = new FormData;
        data.append("ProductName", ProductName);
        data.append("ProductPrice", ProductPrice);
        data.append("ProductCount", ProductCount);
        data.append("Size", Size);
        data.append("Type", Type);
        data.append("Brand", Brand);
        data.append("Sale", Sale);
        data.append("DateInsert", DateInsert);
        data.append("ProductImage", file[0]);

        $.ajax({
            async: true,
            type: "Post",
            dataType: "JSON",
            url: "/Admin/AddProduct",
            data: data,
            processData: false,
            contentType: false,
            success: function (data) {
                toastr.success(data.msg);
                ClearText();
                LoadListProduct(tukhoa, trang);
                $('#modalProduct').hide();

            }, error: function (data) {
                toastr.error(data.msg);
            }
        });

    } else {
        var data = new FormData;
        data.append("ID", ProductID);
        data.append("ProductName", ProductName);
        data.append("ProductPrice", ProductPrice);
        data.append("ProductCount", ProductCount);
        data.append("Size", Size);
        data.append("Type", Type);
        data.append("Brand", Brand);
        data.append("Sale", Sale);
        data.append("DateInsert", DateInsert);
        data.append("ProductImage", file[0]);

        $.ajax({
            async: true,
            type: "Post",
            dataType: "JSON",
            url: "/Admin/EditProduct",
            data: data,
            processData: false,
            contentType: false,
            success: function (data) {
                toastr.success(data.msg);
                ClearText();
                LoadListProduct(tukhoa, trang);
                $('#modalProduct').hide();

            }, error: function (data) {
                toastr.error(data.msg);
            }
        });
    }
};
function LoadImage(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $("#previewImage").attr("src", e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

//Ham Load Thuong Hieu
function LoadBrand() {
    $.ajax({
        url: '/Admin/AllBrand',
        type: 'get',
        success: function (data) {
            if (data.code == 200) {
                //nếu lấy dữ liệu thành công, dùng vòng lặp để duyệt mảng trả về và đổ dữ liệu cho select có id là selectGiaoVien
                $('#selectbrand').empty();
                $.each(data.allbrand, function (k, v) {
                    let opt = '<option value = "' + v.Brand + '">' + v.BrandName + '</option>';
                    $('#selectbrand').append(opt);
                });
            } else {
                alert(data.msg);
            }
        }
    });
}
//Load Size
function LoadSize() {
    $.ajax({
        url: '/Admin/AllSize',
        type: 'get',
        success: function (data) {
            if (data.code == 200) {
                //nếu lấy dữ liệu thành công, dùng vòng lặp để duyệt mảng trả về và đổ dữ liệu cho select có id là selectGiaoVien
                $('#selectsize').empty();
                $.each(data.allsize, function (k, v) {
                    let opt = '<option value = "' + v.Size + '">' + v.SizeName + '</option>';
                    $('#selectsize').append(opt);
                });
            } else {
                alert(data.msg);
            }
        }
    });
}
//Load Type
function LoadType() {
    $.ajax({
        url: '/Admin/AllType',
        type: 'get',
        success: function (data) {
            if (data.code == 200) {
                //nếu lấy dữ liệu thành công, dùng vòng lặp để duyệt mảng trả về và đổ dữ liệu cho select có id là selectGiaoVien
                $('#selecttype').empty();
                $.each(data.alltype, function (k, v) {
                    let opt = '<option value = "' + v.Type + '">' + v.TypeName + '</option>';
                    $('#selecttype').append(opt);
                });
            } else {
                alert(data.msg);
            }
        }
    });
}
//Sale
function LoadSale() {
    $.ajax({
        url: '/Admin/AllSale',
        type: 'get',
        success: function (data) {
            if (data.code == 200) {
                //nếu lấy dữ liệu thành công, dùng vòng lặp để duyệt mảng trả về và đổ dữ liệu cho select có id là selectGiaoVien
                $('#selectsale').empty();
                $.each(data.allsale, function (k, v) {
                    let opt = '<option value = "' + v.Sale + '">' + v.SaleName + '</option>';
                    $('#selectsale').append(opt);
                });
            } else {
                alert(data.msg);
            }
        }
    });
}

//Sự kiện load
function LoadListProduct(search, trang) {
    $.ajax({
        url: '/Admin/GetProduct',
        type: 'get',
        data: {
            tukhoa: search,
            trang: trang
        },
        success: function (data) {
            if (data.code == 200) {
                var x = 1;
                $('#tblProduct').empty();
                $.each(data.ListPro, function (i, v) {
                    var dt = new Date(parseInt(v.DateInsert.replace('/Date(', '')))
                    var dtFinal = AddLeadingZeros(dt.getDate(), 2) + '/' +
                        AddLeadingZeros(dt.getMonth() + 1, 2) + '/' +
                        AddLeadingZeros(dt.getFullYear(), 4)
                    let tr = '<tr id="' + v.ID + '">'
                    tr += '<td style="color:black;text-align:center">' + x + '</td>';
                    tr += '<td style="color:black;text-align:center"> <img src="../Content/images/' + v.ProductImage + '" style="width:70px;height:70px"/></td>';
                    tr += '<td style="color:black;text-align:center;padding-bottom: 30px;padding-top: 30px;">' + v.ProductName + '</td>';
                    tr += '<td style="color:black;text-align:center;padding-bottom: 30px;padding-top: 30px;">' + v.ProductPrice.toLocaleString('it-IT', { style: 'currency', currency: 'VND' }) + '</td>';
                    tr += '<td style="color:black;text-align:center;padding-bottom: 30px;padding-top: 30px;">' + v.SizeName + '</td>';
                    tr += '<td style="color:black;text-align:center;padding-bottom: 30px;padding-top: 30px;">' + v.TypeName + '</td>';
                    tr += '<td style="color:black;text-align:center;padding-bottom: 30px;padding-top: 30px;">' + v.BrandName + '</td>';
                    tr += '<td style="color:black;text-align:center;padding-bottom: 30px;padding-top: 30px;">' + dtFinal + '</td>';
                    tr += '<td style="text-align:center;padding-bottom: 30px;padding-top: 30px;">';
                    tr += '<a class="btn btn-danger btn-xs" style="height:30px;width:31px;text-align:center" name = "delete"><i class="fa fa-trash-o" style="font-size: 25px;"></i></a>';
                    tr += '<a class="btn btn-info btn-xs" style="margin-left:5px;height:30px;width:31px;text-align:center;" name = "update"><i style="font-size: 25px;"   class="fa fa-pencil"></i></a>';
                    tr += '</td>';
                    tr += '</tr>';
                    $('#tblProduct').append(tr);
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
//Hàm xử lý thời gian
function AddLeadingZeros(number, size) {
    var s = "0000" + number;
    return s.substr(s.length - size);
}

function ClearText() {
    $('#txtname').val('');
    $('#txtprice').val('');
    $('#txtcount').val('');
    $('#fileUpload').val('');
    $('#dateStart').val('');
    $('#previewImage').attr("src", "../Content/images/addImg.png");
    LoadBrand();
    LoadType();
    LoadSize();
    LoadSale();
}
