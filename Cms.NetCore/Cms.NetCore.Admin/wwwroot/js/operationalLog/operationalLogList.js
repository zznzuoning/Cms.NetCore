layui.use([ 'jquery', 'layer', 'table'], function () {
    var

        //layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        table = layui.table;

    //用户列表
    var tableIns = table.render({
        elem: '#operationalLogList',
        url: '/operationalLog/GetList',
        cellMinWidth: 95,
        page: true,
        height: "full-20",
        limits: [10, 15, 20, 25],
        limit: 20,
        cols: [[
            { type: "numbers", fixed: "left", width: 50 },
            { field: 'operationalName', title: '功能名称', minWidth: 100, align: "center" },
            { field: 'controller', title: '控制器', minWidth: 100, align: "center" },
            {
                field: 'action', title: '方法', minWidth: 100, align: "center"
            },
            { field: 'operationalTime', title: '操作时间', minWidth: 200, align: "center" },
            {
                field: 'operationalIp', title: 'IP', minWidth: 100, align: "center"
            },
            {
                field: 'operationalState', title: '状态', minWidth: 100, align: "center" ,templet: function (d) {
                    var state = d.operationalState === 1 ? "正常" : "异常"; 
                    var calss = d.operationalState === 1 ? "layui-bg-green" : "layui-bg-red";
                    return '<i class=' + calss+'>' + state + '</i>';
                } },
            { field: 'userName', title: '操作人', align: 'center', minWidth: 100 }
        ]]
    });
})
