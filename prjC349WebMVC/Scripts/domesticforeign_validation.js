jQuery.validator.addMethod("domesticforeign",
    function (value, element, param) {
        var Input = new string(value);
        //return ((Input == "內銷") || (Input == "外銷"));

        if (Input != "內銷" && Input != "外銷") {
            return false;
        }
        else {
            return true;
        }
    });
jQuery.validator.unobtrusive.adapters.addBool("domesticforeign");
