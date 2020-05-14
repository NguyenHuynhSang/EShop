
function menuTrigger(){
        $('#kt-menu__item').on('click', function (e) {
            e.preventDefault();
            $('#kt-menu__item').attr("class","kt-menu__item--active");
        });
}