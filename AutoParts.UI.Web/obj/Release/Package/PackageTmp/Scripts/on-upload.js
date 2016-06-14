function uploadImageProfile(id) {
    $('#upload').data('upload-id', id);
    $('#upload').click();
}

$("document").ready(function () {
    $('#upload').on('change', function () {
        var id = $(this).data('upload-id');
        var entidade = $(this).data('upload-entidade');

        var data = new FormData();

        var files = $(this).get(0).files;

        // Add the uploaded image content to the form data collection
        if (files.length > 0) {
            data.append("UploadedImage", files[0]);
        }

        // Make Ajax request with the contentType = false, and procesDate = false
        $.ajax({
            type: 'POST',
            url: '/Painel/Home/Upload?id=' + id + '&entidade=' + entidade,
            contentType: false,
            processData: false,
            cache: false,
            data: data,
            success: function (result) {
                d = new Date();
                $('img[data-upload-id=' + id + ']').attr('src', '../' + result + '?' + d.getTime());
            }
        });
    });
});