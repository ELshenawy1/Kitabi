var lastnum = -1;
function deleteitem() {
    $.ajax({
        url: "Books/Delete/" + lastnum,
        success: function (result) {
            document.getElementById(lastnum).remove();
        }
    });
}

function savelast(id) {
    lastnum = id;
}