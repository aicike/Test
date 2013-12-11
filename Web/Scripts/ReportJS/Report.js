/* -----------------------------------控制显示AJAX--------------------------------*/

//根据权限 展示 "首页" 报表
//accountmainid: 销售端ID
//accountid:用户ID
(function ($) {
    $.fn.ReportPowerIndex = function (options) {
        var defaults = {
            accountmainid: 0,
            accountid: 0
        };
        var options = $.extend(defaults, options);
        var Url = "AjaxReport/GetAcctionMainReport";
        var Data = { AccountMainID: options.accountmainid };
 
        var thisController = $(this);

        $.ajax({
            type: "post",
            url: Url,
            data: Data,
            success: function (result) {
                var strs = result.split(',');
                for (i = 0; i < strs.length; i++) {
                    switch (strs[i]) {
                        case "DayAddPeople": //每日净增人数
                        case "DayAddNews":   //每日接收消息数
                            thisController.ShowReportIndex({
                                type: strs[i],
                                accountid: options.accountid
                            });
                            break;
                    }

                }

            }
        });



    };
})(jQuery);





/* -----------------------------------展示报表AJAX--------------------------------*/
/*展示报表 
type: 类型  
{
DayAddPeople：每日净增人数
DayAddNews:每日接收消息数
}
acccountid: 当前用户ID
*/
(function ($) {
    $.fn.ShowReportIndex = function (options) {
        var defaults = {
            type: "",
            accountid: 0
        };
        var options = $.extend(defaults, options);
        var thisController = $(this);
        var Url = "";
        var Data = {};
        switch (options.type) {
            //每日净增人数          
            case "DayAddPeople":
                Url = "AjaxReport/GetDayAddPeople";
                Data.AccountID = options.accountid;
                break;
            //每日接收消息数          
            case "DayAddNews":
                Url = "AjaxReport/GetDayAddNews";
                Data.AccountID = options.accountid;
                break;
        }

        

        $.ajax({
            type: "post",
            url: Url,
            data: Data,
            success: function (result) {
                switch (options.type) {
                    //每日净增人数          
                    case "DayAddPeople":
                        DayAddPeople(thisController, result);
                        break;
                    //每日接收消息数          
                    case "DayAddNews":
                        DayAddNews(thisController, result);
                        break;
                }

            }
        });


    };
})(jQuery);




/* -----------------------------------报表类型------------------------------------*/

//每日净增人数
function DayAddPeople(obj, data) {
    var id = MathRand();
    obj.append("<div id='main" + id + "'  style='display:none'><div class='ReportTitle'>每日净增人数</div><div id=" + id + " class='ReportShowDiv' ></div></div>");
    $("#" + id).highcharts({
        chart: {
            zoomType: 'xy',
            type: 'areaspline',
            backgroundColor: '#FdFdFd'
        },
        title: {
            text: '',
            align: 'left'
        },
        subtitle: {
            text: ''
        },
        credits: {
            enabled: false
        }
                ,
        xAxis: [{
            categories: data.categories.split(',')
        }],
        yAxis: [{
            labels: {
                format: '{value}人',
                style: {
                    color: '#89A54E'
                }
            },
            title: {
                text: '',
                style: {
                    color: '#89A54E'
                }
            }, min: 0, tickInterval: parseInt(data.tickInterval)
        }],
        tooltip: {
            shared: true
        },
        legend: {
            layout: 'vertical',
            align: 'left',
            x: 120,
            verticalAlign: 'top',
            y: 100,
            floating: true,
            backgroundColor: '#FFFFFF', enabled: false
        },
        series: [{
            name: '新增',
            color: '#89A54E',
            data: JSON.parse(data.data),
            tooltip: {
                valueSuffix: '人'
            }
        }]
    });
    $("#main" + id).fadeIn("slow");
}


//每日接收消息数
function DayAddNews(obj, data) {
    var id = MathRand();
    obj.append("<div id='main" + id + "'  style='display:none'><div class='ReportTitle'>每日接收消息数</div><div id=" + id + "  class='ReportShowDiv'></div></div>");
    $("#" + id).highcharts({
        chart: {
            zoomType: 'xy',
            backgroundColor: '#FdFdFd'
        },
        title: {
            text: '',
            align: 'left'
        },
        subtitle: {
            text: ''
        },
        credits: {
            enabled: false
        }
                ,
        xAxis: [{

            categories: data.categories.split(',')
        }],
        yAxis: [{ // Primary yAxis
            labels: {
                format: '{value}条',
                style: {
                    color: '#89A54E'
                }
            },
            title: {
                text: '',
                style: {
                    color: '#89A54E'
                }
            }, min: 0, tickInterval: parseInt(data.tickInterval)
        }],
        tooltip: {
            shared: true
        },
        legend: {
            layout: 'vertical',
            align: 'left',
            x: 120,
            verticalAlign: 'top',
            y: 100,
            floating: true,
            backgroundColor: '#FFFFFF', enabled: false
        },
        series: [{
            name: '接收',
            color: '#4e7bbf',
            type: 'areaspline',
            data: JSON.parse(data.data),
            tooltip: {
                valueSuffix: '条'
            }
        }]
    });


    $("#main" + id).fadeIn("slow");

}




/* -----------------------------------其他方法--------------------------------*/
//六位随机数
function MathRand() {
    var Num = "";
    for (var i = 0; i < 6; i++) {
        Num += Math.floor(Math.random() * 10);
    }
    return Num;
}


