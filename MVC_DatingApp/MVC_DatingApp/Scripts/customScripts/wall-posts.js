$(document).ready(() => {
    console.log("Script loaded: wall-posts.js");
});

$('#SubmitPost').click(function () {
    var currentUrl = window.location.href;
    var urlArray = currentUrl.split("/User/Index/");
    var Id = "";
    var Message = $("#PostMessage").val();
    if (Message != "" && Message.length < 280) {
        var post;
        if (urlArray.length >= 2) {
            Id = urlArray[1];
        }
        if (!Id == "") {
            post = { Message: Message, PostToId: Id };

        } else {
            post = { Message: Message};
        }
        Create_Post(post);
    }
    else {
        alert("Your post needs to consist of between 1 and 280 characters.")
    }
})

function Update_Posts(profileId) {
    var actionUrl = "/Post/CreatedPosts/" + profileId;
    var request = $.post(actionUrl);
    request.done(function (data) {
        console.log(data);
        $("#postWall").html(data);
    }).fail(() => {
        console.log("Error: Failure to update posts");
        });
    location.reload();
}
function Create_Post(post) {
    $.ajax({
        type: "POST",
        url: "/api/PostApi/",
        data: JSON.stringify(post),
        contentType: "application/json;charset=UTF-8",
        success: () => {
            console.log("Success!!");
            Update_Posts(post.PostToId);
        },
        error: () => {
            alert("Error: Failure to add post");
        }
    });
}