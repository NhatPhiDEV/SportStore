$(document).ready(function () {
    var message = $('#Message').text();
    switch (message) {
        case 'Thêm Thành công':
            toastr.success('Thêm mới dữ liệu thành công!');
            break;
        case 'Cập nhật thành công':
            toastr.success('Cập nhật dữ liệu thành công!');
            break;
        case 'Xóa thành công':
            toastr.success('Xóa dữ liệu thành công!');
            break;
        case 'Ảnh tồn tại':
            toastr.warning('Lỗi nhập liệu vì ảnh đã tồn tại!');
            break;
    }
});