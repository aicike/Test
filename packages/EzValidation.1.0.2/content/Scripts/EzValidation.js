var ezValidation = function () { };
ezValidation.is = function (value1, operator, value2) {
    var isNumeric = function (input) {
        return (input - 0) == input && input.length > 0;
    };

    var isADate = function (input) {
        var dateRegEx = new RegExp(/(?=\d)^(?:(?!(?:10\D(?:0?[5-9]|1[0-4])\D(?:1582))|(?:0?9\D(?:0?[3-9]|1[0-3])\D(?:1752)))((?:0?[13578]|1[02])|(?:0?[469]|11)(?!\/31)(?!-31)(?!\.31)|(?:0?2(?=.?(?:(?:29.(?!000[04]|(?:(?:1[^0-6]|[2468][^048]|[3579][^26])00))(?:(?:(?:\d\d)(?:[02468][048]|[13579][26])(?!\x20BC))|(?:00(?:42|3[0369]|2[147]|1[258]|09)\x20BC))))))|(?:0?2(?=.(?:(?:\d\D)|(?:[01]\d)|(?:2[0-8])))))([-.\/])(0?[1-9]|[12]\d|3[01])\2(?!0000)((?=(?:00(?:4[0-5]|[0-3]?\d)\x20BC)|(?:\d{4}(?!\x20BC)))\d{4}(?:\x20BC)?)(?:$|(?=\x20\d)\x20))?((?:(?:0?[1-9]|1[012])(?::[0-5]\d){0,2}(?:\x20[aApP][mM]))|(?:[01]\d|2[0-3])(?::[0-5]\d){1,2})?$/);
        return dateRegEx.test(input);
    };

    var isABool = function (input) {
        return input === true || input === false || input === "true" || input === "false";
    };

    if (isADate(value1) || isADate(value2)) {
        if (value1 == '' || value1 == null) return true;
        if (value2 == '' || value2 == null) return true;
        value1 = Date.parse(value1);
        value2 = Date.parse(value2);
    }
    else if (isABool(value1)) {
        if (value1 == "false") value1 = false;
        if (value2 == "false") value2 = false;
        value1 = !!value1;
        value2 = !!value2;
    }
    else if (isNumeric(value1)) {
        value1 = parseFloat(value1);
        value2 = parseFloat(value2);
    }

    switch (operator) {
        case "Equal": if (value1 == value2) return true; break;
        case "NotEqual": if (value1 != value2) return true; break;
        case "Greater": if (value1 > value2) return true; break;
        case "Less": if (value1 < value2) return true; break;
        case "GreaterOrEqual": if (value1 >= value2) return true; break;
        case "LessOrEqual": if (value1 <= value2) return true; break;
    }
    return false;
};

ezValidation.GetID = function (element, dependentPropety) {
    var pos = element.id.lastIndexOf("_") + 1;
    return element.id.substr(0, pos) + dependentPropety;
};

ezValidation.getName = function (element, dependentPropety) {
    var pos = element.name.lastIndexOf(".") + 1;
    return element.name.substr(0, pos) + dependentPropety;
};

Sys.Mvc.ValidatorRegistry.validators["comparison"] = function (rule) {
    var operator = rule.ValidationParameters["operator"];
    return function (value, context) {
        var dependentProperty = ezValidation.GetID(context.fieldContext.elements[0], rule.ValidationParameters["dependentproperty"]);
        var dependentValueControl = document.getElementById(dependentProperty);
        var dependentValue = null;
        if (document.getElementById(dependentProperty))
            dependentValue = dependentValueControl.value;
        else if (dependentProperty.indexOf('|') != -1) {
            var start = dependentProperty.indexOf('|') + 1;
            var end = dependentProperty.indexOf('|', start);
            dependentValue = dependentProperty.substring(start, end);
        }
        else if (dependentProperty.indexOf("CompareToCurrentDate") != -1) {
            var currentDate = new Date();
            dependentValue = currentDate.getMonth() + 1 + "/" + currentDate.getDate() + "/" + currentDate.getFullYear();
        }
        else if (dependentProperty.indexOf("CompareToYesterday") != -1) {
            var yesterdaysDate = new Date();
            yesterdaysDate.setDate(yesterdaysDate.getDate() - 1);
            dependentValue = yesterdaysDate.getMonth() + 1 + "/" + yesterdaysDate.getDate() + "/" + yesterdaysDate.getFullYear();
        }
        else if (dependentProperty.indexOf("CompareToTomorrow") != -1) {
            var tomorrowsDate = new Date();
            tomorrowsDate.setDate(tomorrowsDate.getDate() + 1);
            dependentValue = tomorrowsDate.getMonth() + 1 + "/" + tomorrowsDate.getDate() + "/" + tomorrowsDate.getFullYear();
        }
        if (ezValidation.is(value, operator, dependentValue))
            return true;

        return rule.ErrorMessage;
    };
};