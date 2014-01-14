function JAlert(json) {
    var data = eval(json);
    var jqueryAlert = "$('#dialog').dialog({resizable: " + (data.Resizable ? data.Resizable : "false") + ",height:'auto',closeOnEscape: false,minHeight:" + (data.MinHeight ? data.MinHeight : "160") + ",modal: true,draggable:false,buttons: {";
    if (data.DialogType == undefined || data.DialogType == "Ok") {
        if (data.URL && data.URL.length > 0) {
            data.BtnOkClick += " window.location='" + data.URL + "';";
        }
        if (data.Controller && data.Action && data.Controller.length > 0 && data.Action.length > 0) {
            data.BtnOkClick += " window.location='/" + data.Controller + '/' + data.Action + "';";
        }
        jqueryAlert += (data.BtnOk ? data.BtnOk : "OK") + ":function () {" + (data.BtnOkClick ? data.BtnOkClick : "$(this).dialog('close');") + "}}";
      //  jqueryAlert += (data.BtnOk ? data.BtnOk : "OK") + ":function () {" + (data.BtnOkClick ? data.BtnOkClick : "alert($(this).dialog());") + "}}";
    } else {
        jqueryAlert += (data.BtnOk ? data.BtnOk : "OK") + ":function () {" + (data.BtnOkClick ? data.BtnOkClick : "$(this).dialog('close');") + "},";
        jqueryAlert += data.BtnCancel + ":function () {" + data.BtnCancelClick + "}}";
    }
    jqueryAlert += "}); $('#dialog').dialog('open');";
    $('#dialog').attr("title", (data.Title ? data.Title : "消息"));
    $('#dialog').html(data.Message);
    //console.log(jqueryAlert);
    eval(jqueryAlert);
}

function AppDelete(msg, url, fun) {
    var f = "";
    if (fun && fun != null) {
        f = fun;
    }
    var u = "";
    if (url && url != null) {
        u = "$.post('" + url + "', null, function (data) { if(data){ $('#alert').html(data);} });"
    }

    JAlert({
        Message: msg,
        DialogType: "confirm",
        BtnOk: "确定",
        BtnOkClick: "$(this).dialog('close');" + f + u,
        BtnCancel: "取消",
        BtnCancelClick: "$(this).dialog('close');"
    });
    return false;
}

/*省份，城市，地区级联插件*/
(function ($) {
    $.fn.Province = function (options) {
        var defaults = {
            value: 0
        };
        var options = $.extend(defaults, options);
        var thisSelect = $(this);
        $.ajax({
            type: "get",
            url: provinceUrl,
            success: function (result) {
                thisSelect.empty();
                thisSelect.append($("<option value='0'>请选择</option>"));
                $.each(result, function (i, n) {
                    thisSelect.append($("<option value='" + n.ID + "'>" + n.Name + "</option>"));
                });
                thisSelect.val(options.value);
                thisSelect.change();
            }
        });
    };
})(jQuery);

(function ($) {
    $.fn.City = function (options) {
        var defaults = {
            parent: "ProvinceID",
            value: 0
        };
        var options = $.extend(defaults, options);
        var thisSelect = $(this);
        thisSelect.empty();
        thisSelect.append($("<option value='0'>请选择</option>"));
        $("#" + options.parent).bind("change", function () {
            var p = $(this).val();
            thisSelect.empty();
            thisSelect.append($("<option value='0'>请选择</option>"));
            $.ajax({
                type: "get",
                url: cityUrl,
                data: { "provinceID": p },
                success: function (result) {
                    $.each(result, function (i, n) {
                        thisSelect.append($("<option value='" + n.ID + "'>" + n.Name + "</option>"));
                    });
                    thisSelect.val(options.value);
                    thisSelect.change();
                }
            });
        });
    };
})(jQuery);

(function ($) {
    $.fn.District = function (options) {
        var defaults = {
            parent: "CityID",
            value: 0
        };
        var options = $.extend(defaults, options);
        var thisSelect = $(this);
        thisSelect.empty();
        thisSelect.append($("<option value='0'>请选择</option>"));
        $("#" + options.parent).bind("change", function () {
            var c = $(this).val();
            thisSelect.empty();
            thisSelect.append($("<option value='0'>请选择</option>"));
            $.ajax({
                type: "get",
                url: districtUrl,
                data: { "cityID": c },
                success: function (result) {
                    $.each(result, function (i, n) {
                        thisSelect.append($("<option value='" + n.ID + "'>" + n.Name + "</option>"));
                    });
                    thisSelect.val(options.value);
                }
            });
        });
    };
})(jQuery);

/*省份验证*/
jQuery.validator.unobtrusive.adapters.add(
    'dropdownlist',
    function (options) {
        options.rules['dropdownlist'] = options.params;
        options.messages['dropdownlist'] = options.message;
    }
);
jQuery.validator.addMethod('dropdownlist', function (value, element, params) {
    return value !== "0";
}, '');

///*唯一验证*/
//jQuery.validator.unobtrusive.adapters.add(
//    'unique',
//    ["table", "field"],
//    function (options) {
//        options.rules['unique'] = options.params;
//        options.messages['unique'] = options.message;
//    }
//);
//    jQuery.validator.addMethod('unique', function (value, element, params) {
//        $.ajax({
//            type: "get",
//            url: checkIsUniqueUrl,
//            data: { "tableName": params.table, "field": param.field, "value": value },
//            success: function (result) {
//                if (result != undefined && result.toLowerCase() == "false") {
//                    return false;
//                } else {
//                    return true;
//                }
//            }
//        });
//    }, '');

//BtnID 按钮ID,GIfID图片空间ID ,Status:1显示 2隐藏
function LandWaitFor(BtnID,GifID, Status) {
    if (Status == 1) {
        $("#" + BtnID).hide();
        $("#" + GifID).show();
    }
    else {
        $("#" + BtnID).show();
        $("#" + GifID).hide();
    }
}