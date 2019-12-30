$(document).ready(() => {
    console.log("Script loaded: search.js");
});
//On keyup on SearchTextField, run UserSearch
$("#SearchTextField").on("keyup", UserSearch);
function UserSearch() {
    var searchString = $("#SearchTextField").val();

    // Make an array of all users
    var users = document.getElementsByClassName("FirstNLastname");
    var userArray = jQuery.makeArray(users);
    // if name doesn't contain search string = remove
    for (var i = 0; i < userArray.length; i++) {
        if (!userArray[i].innerHTML.toString().toUpperCase().includes(searchString.toUpperCase())) { // Convert to uppercase to avoid case sensitivity
            // If the name don't include the searchstring, hide the element(the name) in the search window
            if (!$(userArray[i]).parents().eq(1).hasClass("d-none")) {
                $(userArray[i]).parents().eq(1).addClass("d-none");
                $(userArray[i]).parents().eq(1).hide();

            };
        }
    }

    // Same as above, but show the elements which DO contain the searchString
    for (var i = 0; i < userArray.length; i++) {
        if (userArray[i].innerHTML.toString().toUpperCase().includes(searchString.toUpperCase())) {
            if ($(userArray[i]).parents().eq(1).hasClass("d-none")) {
                $(userArray[i]).parents().eq(1).removeClass("d-none");
                $(userArray[i]).parents().eq(1).show();
            };
        }
    }
}