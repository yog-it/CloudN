/* Module Script */
var YogIT = YogIT || {};

YogIT.LargeAzFileUpload = {
    copyText: function (text, copiedid) {
        navigator.clipboard.writeText(text).then(function () {
            var copied = document.getElementById(copiedid);
            copied.classList.add("show");
            setTimeout(function () {
                copied.classList.remove('show');
            }, 500)
        })
            .catch(function (error) {
                alert(error);
            });
    }
};