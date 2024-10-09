    $(function () {
        $('.dropdown').hover(
            function () {
                $(this).find('.dropdown-menu').stop(true, true).slideDown(200);
            },
            function () {
                $(this).find('.dropdown-menu').stop(true, true).slideUp(200);
            }
        );
  });