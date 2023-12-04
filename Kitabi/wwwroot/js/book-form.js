function putimg() {
    document.getElementById('coverpreview').src = URL.createObjectURL(document.getElementById('Cover').files[0]);
    document.getElementById('coverpreview').classList.remove('d-none')
}
