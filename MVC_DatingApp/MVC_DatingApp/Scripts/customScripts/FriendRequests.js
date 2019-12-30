$(document).ready(() => {
    console.log("Script loaded: FriendRequests.js");
});
$("#RequestFalse").on("click","#SendReqBtn", RequestSend)
$("#RequestTrue").on("click", "#CancelReqBtn", RequestCancel)
$(".ReqBtnsAccept").on("click", RequestAccept)
$(".ReqBtnsDecline").on("click", RequestDecline)


function RequestSend() {
    console.log("im runnin");
    var currentUrl = window.location.href;
    var urlArray = currentUrl.split("/User/Index/");
    var Id = urlArray[1];
    $.ajax({
        type: "POST",
        url: "/Friends/SendFriendRequest/" + Id,
        contentType: "application/json;charset=UTF-8",
        success: () => {
            console.log("Success!!");    
            location.reload();
        },
        error: () => {
            alert("Error: Failure to send friend request");
        }
    });
}
function RequestCancel() {
    console.log("im runnin");
    var currentUrl = window.location.href;
    var urlArray = currentUrl.split("/User/Index/");
    var Id = urlArray[1];
    $.ajax({
        type: "POST",
        url: "/Friends/CancelFriendRequest/" + Id,
        contentType: "application/json;charset=UTF-8",
        success: () => {
            console.log("Success!!");
            location.reload();
        },
        error: () => {
            alert("Error: Failure to send friend request");
        }
    });
}
function RequestAccept() {
    console.log("im runnin")
    var RequestId = this.getAttribute("data-id");
    $.ajax({
        type: "POST",
        url: "/Friends/AcceptFriendRequest/" + RequestId,
        contentType: "application/json;charset=UTF-8",
        success: () => {
            console.log("Success!!");
            location.reload();
        },
        error: () => {
            alert("Error: Failure to send friend request");
        }
    });
}
function RequestDecline() {
    console.log("im runnin")
    var RequestId = this.getAttribute("data-id");
    $.ajax({
        type: "POST",
        url: "/Friends/DeclineFriendRequest/" + RequestId,
        contentType: "application/json;charset=UTF-8",
        success: () => {
            console.log("Success!!");
            location.reload();
        },
        error: () => {
            alert("Error: Failure to send friend request");
        }
    });
}

