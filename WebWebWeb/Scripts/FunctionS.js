

function like() {
    $(this).parent().parent().children(".shareStreemLike").css("display", "block");
    $(this).parent().parent().children(".shareStreemDislike").css("display", "none");
}
function dislike() {
    $(this).parent().parent().children(".shareStreemDislike").css("display", "block");
    $(this).parent().parent().children(".shareStreemLike").css("display", "none");
}


function Comment() {
    $(".ShareCommentTextInput").each(function () { $(this).val(""); });  
    commentDeleteSlide();    
}
function deleteComment() {    
commentDeleteSlide();
}

function commentDeleteSlide(){
    $(".commentDeleteTrigger").click(function () {
        $(this).parent().parent().children(".deleteCommenthide").stop(true).slideDown("slow");
    });
    $(".commentDeleteClose").click(function () {
        $(this).parent().parent().stop(true).slideUp("slow");
    });
}
