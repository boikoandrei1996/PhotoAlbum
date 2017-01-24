document.getElementById('submitAddLikeForm').onclick = function () {
    $('#addLikeForm').submit();
}

document.getElementById('submitDeleteLikeForm').onclick = function () {
    $('#deleteLikeForm').submit();
}

function ProcessListLikes(data) {
    var target = $("#ajaxListLikes");

    if (target.is(':empty')) {
        if (data.length == 0) {
            target.append('<li class="list-group-item">None</li>');
        }

        for (var i = 0; i < data.length; i++) {
            var user = data[i];
            target.append('<li class="list-group-item"><img src="/Photo/Avatar?userId=' +
                            user.Id + '" width="50" height="50" class="img-circle like-list-item-img">' +
                            '<a href="/Profile/' + user.Id + '">' +
                            user.Login + '</a></li>');
        }
        $("#ajaxBtnListLike").text("Hide likes");
    }
    else {
        target.empty();
        $("#ajaxBtnListLike").text("Show likes");
    }
}