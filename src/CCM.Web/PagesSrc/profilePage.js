require('./Styles/pageStyles.scss');

require('bootstrap-sass');
require('jquery-validation');
require('jquery-validation-unobtrusive');

$(document).ready(function () {
    $(':file').change(function (event) {

        var input = $(this).parents('.input-group').find('#pictureUpload');
         

        if (input.length) {
            input.val(event.target.files[0].name);
        }

    });
});