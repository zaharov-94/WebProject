$(function () {
    // перебрать все элементы a меню navbar
    $('div.sidebar ul li a').each(function () {
        // определяем, является ли искомый пункт нужным
        if (($(this).prop('href').toString()).indexOf((location.pathname.slice(1).toString())) !== -1) {
            // добавляем к родительскому пункту класс active
            $(this).parent('li').addClass('active');
            // прерываем дальнейшее выполнения метода each
            return false;
        }
    });
    $('.container-fluid ul li a').each(function () {
        // определяем, является ли искомый пункт нужным
        if (($(this).prop('href').toString()).indexOf((location.pathname.slice(1).toString())) !== -1) {
            // добавляем к родительскому пункту класс active
            $(this).parent('li').addClass('active');
            // прерываем дальнейшее выполнения метода each
            return false;
        }
    });
});
