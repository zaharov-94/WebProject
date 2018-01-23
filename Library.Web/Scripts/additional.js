
$(window).on('load', function () {
    $("div.sidebar ul li a").click(function () {
        $("div.sidebar ul li").removeClass('active');
        $(this.parentElement).addClass('active');
    })
});
