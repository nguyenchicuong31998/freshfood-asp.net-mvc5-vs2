

$(document).ready(function () {

    $('.add-cart').off('click').on('click', function (e) {
        e.preventDefault();
        let id = $(this).data('id');
        let quantity = $("#soluong").val();
        quantity = !quantity ? 1 : parseInt(quantity);
        $.ajax({
            url: "/Home/AddCart",
            data: {
                id: id,
                quantity: quantity
            },
            dataType: "JSON",
            type: "POST",
            success: function (res) {
                if (res.status == true) {
                    location.href = "#info-cart";
                    $('.header').load("/Header/HeaderPartial");
                }
            }
        })
    });




    //$('#related-products').slick({
    //    //slidesToShow: 3,
    //    //centerMode: true,
    //    //focusOnSelect: true
    //    //centerMode: true,
    //    //centerPadding: '60px',
    //    //swipeToSlide: true,  
    //    prevArrow: true,
    //    nextArrow: true,
    //    slidesToScroll: 4,
    //    slidesToShow: 4,
    //    autoplay: true,
    //    autoplaySpeed: 2000,
    //});

    $(".quick-view").click(function (e) {
        e.preventDefault();
        let id = $(this).data("id");
        $("#modal-product").load("/Home/ModalProductPartial/" + id, function () {
            $("#modal-quick-view").modal('show');
            $('.add-cart').off('click').on('click', function (e) {
                e.preventDefault();
                let id = $(this).data('id');
                let quantity = $("#soluong").val();
                quantity = !quantity ? 1 : parseInt(quantity);
                $.ajax({
                    url: "/Home/AddCart",
                    data: {
                        id: id,
                        quantity: quantity
                    },
                    dataType: "JSON",
                    type: "POST",
                    success: function (res) {
                        if (res.status == true) {
                            location.href = "#info-cart";
                            $('.header').load("/Header/HeaderPartial");
                            $("#modal-quick-view").modal('hide');
                        }
                    }
                })
            });
        });
    });


    $("#slider-related-products").slick({
        slidesToShow: 4,
        slidesToScroll: 4,
        dots: false,
        arrows: false,
        centerMode: false,
        focusOnSelect: true,
        autoplay: true,
        autoplaySpeed: 2000,
        responsive: [
            {
                breakpoint: 992,
                settings: {
                    arrows: false,
                    slidesToShow: 3,
                    slidesToScroll: 3
                }
            },
            {
                breakpoint: 768,
                settings: {
                    arrows: false,
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    arrows: false,
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
        ]
    });

    $(".slider-news-hot").slick({
            slidesToShow: 4,
            slidesToScroll: 4,
            dots: false,
            arrows: true,
            centerMode: false,
            focusOnSelect: true,
            autoplay: true,
            autoplaySpeed: 2000,
            responsive: [
                    {
                        breakpoint: 1200,
                        settings: {
                            arrows: true,
                            slidesToShow: 3,
                            slidesToScroll: 3,
                        }
                    },
                    {
                        breakpoint: 768,
                        settings: {
                            arrows: false,
                            slidesToShow: 2,
                            slidesToScroll: 2
                        }
                    },
                    {
                        breakpoint: 480,
                        settings: {
                            arrows: false,
                            slidesToShow: 1,
                            slidesToScroll: 1
                        }
                    }
            ]
     });

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

    function toggleIconMenuLeft(e) {
        //$(e.target)
        //    .prev('.categories-item')
        //    .find('.category-icon')
        //    .toggleClass('fa-plus fa-minus');
        //$('.categories-item').click(function (e) {
        //    $(e.target).prev(".category-icon").toggleClass('fa-plus fa-minus');
        //});
        $(e.target).parent().find(".category-icon").toggleClass('fa-plus');
        $(e.target).parent().find(".category-icon").toggleClass('fa-plus fa-minus');
    }
    $('.categories-group').on('hidden.bs.collapse', toggleIconMenuLeft);
    $('.categories-group').on('shown.bs.collapse', toggleIconMenuLeft);
})

