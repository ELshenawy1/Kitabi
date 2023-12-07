function remove(controller , id) {
    $.ajax({
        url: `/${controller}/Delete/${id}`,
        success: function (result) {
            document.getElementById(id).remove();
        }
    });
}