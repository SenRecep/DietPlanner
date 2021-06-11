function closeModal(modalName) {
    $(`#${modalName}`).modal('hide');
}

function showModal(modalName) {
    $(`#${modalName}`).modal('show');
}

function PreventEnterKey(id) {
    $(`#${id}`).keydown(function (event) {
        if (event.keyCode == 13 && event.target.nodeName != "TEXTAREA") {
            event.preventDefault();
            return false;
        }
    });
}