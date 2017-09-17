var updateFormActionWithFragment = function () {
    $(document).ready(function () {
        var form = $("form")[0];
        var hash = document.location.hash;
        if (hash) {
            if (form) {
                if (form.action) {
                    form.action = document.location.href;
                }
            }
        }
    });
}

module.exports = updateFormActionWithFragment;