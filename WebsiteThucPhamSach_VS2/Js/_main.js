

$(document).ready(function () {

    //$('#birthday').datepicker({
    //    dateFormat: 'dd/mm/yy',
    //    changeYear: true, 
    //    changeMonth: true
    //});
    $(window).scroll(function () {
        var currentPosition = $(this).scrollTop();
        if (currentPosition > 188) {
            $(".navigation").addClass("position-fixed w-100");
        } else {
            $(".navigation").removeClass("position-fixed");
        }
    });

    $.datepicker.regional["vi-VN"] =
        {
            closeText: "Đóng",
            prevText: "Trước",
            nextText: "Sau",
            currentText: "Hôm nay",
            monthNames: ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"],
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
        dateFormat: "dd/MM/yy",
        changeYear: true,
    });


    $('.topbar-md__arrow-down').click(function () {
        $('.wrap-info').toggleClass('show');
    });


    $('.menu__icon').click(function () {
        $('.sidebar-menu').addClass('showMenu');
        $('body').addClass("overflow-hidden")
    })

    $('.close').click(function () {
        $('.sidebar-menu').removeClass('showMenu');
        $('body').removeClass("overflow-hidden")
    });


    function toggleIcon(e) {
        $(e.target)
            .prev('.nav-vertical__item')
            .find(".nav-vertical__icon")
            .toggleClass('fa-plus fa-minus');
    }
    $('.panel-group').on('hidden.bs.collapse', toggleIcon);
    $('.panel-group').on('shown.bs.collapse', toggleIcon);

})

