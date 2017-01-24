document.getElementById('submitDeleteProfileForm').onclick = function () {
    var isConfirm = confirm("Do you want delete your profile?");
    if (isConfirm) {
        $('#deleteProfileForm').submit();
    }
}