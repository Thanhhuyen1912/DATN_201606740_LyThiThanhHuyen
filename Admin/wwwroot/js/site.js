// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function addColumnSearch(table) {

    //thêm ô input search cho từng cột của datatable
    //table phải có thẻ tfoot giống hệt thead
    $('tfoot th').each(function () {
        var title = $(this).text();
        if (title === "")
            return;
        if ($(this).data('search')==false)
            return;
        $(this).html(
            '<input type="text" class="form-control" placeholder="Search ' + title + '" />');
    });
    // thêm sự kiện tìm kiếm
    table.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change',
            function () {
                if ($(this).data('search') == false)
                    return;
                if (that.search() !== this.value) {
                    that.search(this.value)
                        .draw();
                }
            });
    });
    //gán các ô search lên trên
    $('tfoot').each(function () {
        $(this).insertAfter($(this).siblings('thead'));
    });
}


(function ($) {
    if (!$)
        throw new Error("JQuery is not imported");
    window.postFormData = function (url, formData) {
        return $.ajax(url,
            {
                method: "post",
                processData: false,
                contentType: false,
                data: formData
            }).done(function(res) {
                return displayResponse(res);
            }).promise();
    }



})(window.jQuery);

//(function (swal) {
//    if (!swal)
//        throw new Error("SweetAlert2 is not imported");
//    window.displayResponse = function (res) {

//        if (!res.message || !res.code)
//            throw new Error("Response is invalid");

//        res.message = res.message.split('$').join("</br>");;


//        return new Promise((resolve, reject) => {
//            if (res.code === "0")
//                return swal.fire({
//                    title: "",
//                    text: res.message,
//                    type: "success",
//                    confirmButtonText: "OK"
//                }).then(() => {
//                    return res;
//                });
//            else
//                return swal.fire({
//                    title: "",
//                    html: res.message,
//                    type: "error",
//                    confirmButtonText: "OK"
//                }).then(() => {
//                    return res;
//                });
//        });


//    }



//})(window.Swal);

//Message hiển thị lỗi
function showError(error, callbackAfterClose) {
    Swal.fire({
        html: error,
        type: 'error',
        confirmButtonText: 'OK',
        onAfterClose: callbackAfterClose
    });
}

//Message hiển thị kết quả
function showMess(tmess) {
    Swal.fire({
        html: tmess,
        type: 'success',
        confirmButtonText: 'OK'
    });
}
function displayResponse(res) {


    res.message = res.message.replace('$','</br>');
    if (res.code == "0")
        return Swal.fire({
            title: '',
            text: res.message,
            type: 'success',
            confirmButtonText: 'OK'
        });
    else
        return Swal.fire({
            title: '',
            html: res.message,
            type: 'error',
            confirmButtonText: 'OK'
        });
}
function displayWarringResponse(res) {


   
        return Swal.fire({
            title: '',
            text: res,
            type: 'warning',
            confirmButtonText: 'OK'
        });
    
}
//
//
//
//function postFormData(url, formData) {
//    return new Promise(function (resolve, reject) {
//        $.ajax(url,
//                {
//                    method: "post",
//                    processData: false,
//                    contentType: false,
//                    data: formData
//                })
//            .done(function (res) {
//                //hiển thị thông báo
//                displayResponse(res).then(function() {
//                    resolve(res);
//                });
//
//            })
//            .fail(function () {
//                 displayResponse({
//                    code: "-1",
//                    message: "Không thể kết nối tới máy chủ"
//                 }).then(function () {
//                     reject();
//                 })
//            });
//        
//    });
//    
//}
function getFormattedDate(date) {
    //Định dạng dd/MM/YYYY
    try {
        var tem = date.split("T")[0]
        var TemDate = tem.split("-")[2] + "/" + tem.split("-")[1] + "/" + tem.split("-")[0];
        return TemDate;
    }
    catch{ }
    return "01/01/0001";
}
function getFormattedDate1(date) {
    //Định dạng YYYY/MM/dd
    try {
        var tem = date.split("T")[0]
        var TemDate = tem.split("-")[0] + "-" + tem.split("-")[1] + "-" + tem.split("-")[2];
        return TemDate;
    }
    catch{ }
    return "01/01/0001";
}
function SelectRadioButton(name, value) {

    $("input[name='" + name + "'][value='" + value + "']").prop('checked', true);

    return false; // Returning false would not submit the form

}

$(function() {
    $(".toggle-password").click(function () {
        $(this).toggleClass("fa-eye-slash");
        const input = $($(this).attr("toggle"));
        if (input.attr("type") === "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });
});

/**
 * Format số VD: 100000 sang 100,000
 * @param {any} num
 */
const formatNumber = (num) => num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
const formatcomNumber = (num) => num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.');
const alignRightText = (data) => `<p class="text-right">${data == null ? '' : data}</p>`;
const alignCenterText = (data) => `<p class="text-center">${data == null ? '' : data}</p>`;
