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
    $.fn.Province = function (options, func) {
        var defaults = {
            value: 0
        };
        var options = $.extend(defaults, options);
        var thisSelect = $(this);
        if (func != undefined) {
            thisSelect.change(function () {
                func($(this).val());
            });
        }
        $.ajax({
            type: "get",
            url: provinceUrl,
            success: function (result) {
                thisSelect.empty();
                thisSelect.append($("<option value='0'>选择省</option>"));
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
    $.fn.City = function (options, func) {
        var defaults = {
            parent: "ProvinceID",
            value: 0
        };
        var options = $.extend(defaults, options);
        var thisSelect = $(this);
        if (func != undefined) {
            thisSelect.change(function () {
                func($(this).val());
            });
        }
        thisSelect.empty();
        thisSelect.append($("<option value='0'>选择市</option>"));
        $("#" + options.parent).bind("change", function () {
            var p = $(this).val();
            thisSelect.empty();
            thisSelect.append($("<option value='0'>选择市</option>"));
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
    $.fn.District = function (options, func) {
        var defaults = {
            parent: "CityID",
            value: 0
        };
        var options = $.extend(defaults, options);
        var thisSelect = $(this);
        if (func != undefined) {
            thisSelect.change(function () {
                func($(this).val());
            });
        }
        thisSelect.empty();
        thisSelect.append($("<option value='0'>选择区</option>"));
        $("#" + options.parent).bind("change", function () {
            console.log(thisSelect);
            var c = $(this).val();
            thisSelect.empty();
            thisSelect.append($("<option value='0'>选择区</option>"));
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


//BtnID 按钮ID,GIfID图片空间ID ,Status:1显示 2隐藏
function LandWaitFor(BtnID, GifID, Status) {
    if (Status == 1) {
        $("#" + BtnID).hide();
        $("#" + GifID).show();
    }
    else {
        $("#" + BtnID).show();
        $("#" + GifID).hide();
    }
}
function showMsg(error, fun) {
    $('<div>').simpledialog2({
        mode: 'button',
        headerText: '消息',
        headerClose: false,
        buttonPrompt: error,
        buttons: {
            '确定': {
                click: function () {
                    if (fun != undefined) {
                        fun();
                    }
                }
            }
        }
    });
}

/*
微商城代码
*/

function SetUserID(amid, userID) {
    localStorage.setItem("amid_" + amid + "_userid_", userID);
}
function GetUserID(amid) {
    var userID = localStorage.getItem("amid_" + amid + "_userid_");
    if (userID == undefined || userID == null) {
        userID = 0;
    }
    return userID;
}
function CheckIsLogin(amid) {
    var userID = GetUserID(amid);
    if (userID == 0) {
        var rawUrl = window.location.href;
        var newUrl = _loginUrl + "&backUrl=" + rawUrl;
        window.location.href = newUrl;
        return false;
    }
    return true;
}

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}