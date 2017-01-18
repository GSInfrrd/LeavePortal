function showMessage(message, Type, fadeOut) {
    //To clear existing toastr
    toastr.clear();
    //fadeOut = false;
    var position = "bottom-full-width";
    toastr.options.positionClass = 'toast-' + position;
    if (typeof fadeOut !== "undefined" && fadeOut == false) { toastr.options.timeOut = 0; }
    else {
        toastr.options.timeOut = 5000;
    }
    switch (Type) {
        case "1":
            toastr.success(message);
            break;
        case "2":
            toastr.error(message);
            break;
    }
}

function ToJavaScriptDate(value) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));
    var hours = dt.getHours();
    var minutes = dt.getMinutes();
    var seconds = dt.getSeconds();
    var currentHour = parseInt("0");
    var time = "";
    if (hours > 12) {
        currentHour = hours % 12;
        time = currentHour + ":" + minutes + ":" + seconds + " " + "PM";
    }
    else if (hours == 12) {
        currentHour = hours;
        time = currentHour + ":" + minutes + ":" + seconds + " " + "PM";
    }
    else {
        currentHour = hours;
        time = currentHour + ":" + minutes + ":" + seconds + " " + "AM";
    }

    return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + +dt.getFullYear();
}