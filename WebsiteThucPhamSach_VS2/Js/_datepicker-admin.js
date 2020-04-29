$(document).ready(function () {

    $.datepicker.regional["vi-VN"] =
        {
            closeText: "Đóng",
            prevText: "Trước",
            nextText: "Sau",
            currentText: "Hôm nay",
            monthNames: ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13"],
            monthNamesShort: ["Một", "Hai", "Ba", "Bốn", "Năm", "Sáu", "Bảy", "Tám", "Chín", "Mười", "Mười một", "Mười hai"],
            dayNames: ["Chủ nhật", "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy"],
            dayNamesShort: ["CN", "Hai", "Ba", "Tư", "Năm", "Sáu", "Bảy"],
            dayNamesMin: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
            weekHeader: "Tuần",
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ""
        };

    $.datepicker.setDefaults($.datepicker.regional["vi-VN"]);


    $('#date_of_birth').datepicker({
        clearBtn: true,
        changeMonth: true,
        dateFormat: 'dd/MM/yy',
        changeYear: true,
        defaultDate: +7
    });
})