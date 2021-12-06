function doSomething() {
    var $v = "";
    switch ($("#sel1").val()) {
        case "Getting Started":
            $v = "A01"
            break;
        case "Presenter":
            $v = "A02"
            break;
        case "Leader":
            $v = "A03"
            break;
        case "Loyalty":
            $v = "A04"
            break;
        case "Business Star":
            $v = "A05"
            break;
        case "Good Practitioner":
            $v = "A06"
            break;
        case "Good Lecture":
            $v = "A07"
            break;
        case "Champion":
            $v = "A08"
    }
    $("#AchievementID1").val($v)
    $("#AchievementID1").css({ color: "red" })
}
$(function () {
    $("#sel1").change(doSomething);
});