﻿layui.use(['form', 'jquery', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,

        //layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        layer = layui.layer,

        laytpl = layui.laytpl,
        table = layui.table;

    //用户列表
    var tableIns = table.render({
        elem: '#buttonList',
        url: '/Button/GetList',
        cellMinWidth: 95,
        toolbar: "#buttonListBar",
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 20,
        id: "buttonListTable",
        cols: [[
            { type: "radio", fixed: "left", width: 50 },
            { field: 'id', title: 'Id', fixed: "left", hide: true },
            { field: 'sid', fixed: "left", width: 10, align: "center" },
            { field: 'name', title: '按钮名', width: 100, align: "center" },
            { field: 'code', title: '标识', width: 100, align: "center" },
            { field: 'sort', title: '排序', width: 100, align: "center" },
            { field: 'updateUser', title: '最后更新人', align: 'center', minWidth: 100 },
            { field: 'updateTime', title: '最后更新时间', align: 'center', minWidth: 150 },
            { field: 'description', title: '说明', minWidth: 150, align: "center" },
        ]]
    });
    //搜索【此功能需要后台配合，所以暂时没有动态效果演示】
    $(".search_btn").on("click", function () {
        table.reload("buttonListTable", {
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: {
                Name: $(".searchName").val()
            }
        })

    });

    //添加用户
    function addorUpdateRole(obj, data) {
        var index;
        var title = "添加按钮"
        if (obj.event == "edit") {
            if (data.length == 0) {
                layer.alert("请选择要修改的按钮", { icon: 5 });
                return;
            }
            if (data.length > 1) {
                layer.alert("不支持批量修改", { icon: 5 });
                return;
            }
            title = "修改按钮";
            $.ajax({
                type: 'GET',
                url: '/Button/GetButtonById?id=' + data[0].id,
                success: function (res) {//res为相应体,function为回调函数

                    if (res.code === 0) {
                        index = layer.open({
                            title: title,
                            type: 2,
                            content: "/Button/CreateOrUpdate",
                            success: function (layero, index) {

                                var body = layui.layer.getChildFrame('body', index);

                                body.find("#Id").val(res.data.id);
                                body.find(".Name").val(res.data.name);
                                body.find(".Description").text(res.data.description);
                                body.find(".Sort").val(res.data.sort);
                                body.find("#Icon").val(res.data.icon);
                                body.find("#Code").val(res.data.code);
                                form.render();

                                setTimeout(function () {
                                    layui.layer.tips('点击此处返回按钮列表', '.layui-layer-setwin .layui-layer-close', {
                                        tips: 3
                                    });
                                }, 500)


                            }
                        })
                        layer.full(index);
                    }
                    else {
                        layer.alert(res.msg, { icon: 5 });
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.alert('操作失败！！！' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5 });
                }
            });
        }
        else {

            index = layer.open({
                title: title,
                type: 2,
                content: "/Button/CreateOrUpdate",
                success: function (layero, index) {
                    setTimeout(function () {
                        layui.layer.tips('点击此处返回按钮列表', '.layui-layer-setwin .layui-layer-close', {
                            tips: 3
                        });
                    }, 500)

                }
            })
        }
        layer.full(index);
        window.sessionStorage.setItem("index", index);
        //改变窗口大小时，重置弹窗的宽高，防止超出可视区域（如F12调出debug的操作）
        $(window).on("resize", function () {
            layui.layer.full(window.sessionStorage.getItem("index"));
        })
    }
    function Delete(data) {
        if (data.length == 0) {
            layer.alert("请选择要删除的按钮", { icon: 5 });
            return;
        }
        layer.confirm("是否确定删除此按钮", {
            icon: 3,
            title: '系统提示',
            cancel: function (index) {
                layer.close(index);
            }
        }, function (index) {

            $.ajax({
                type: 'POST',
                url: '/Button/Delete',
                data: { id: data[0].id },
                success: function (res) {//res为相应体,function为回调函数
                    if (res.code === 200) {
                        tableIns.reload();
                        layer.close(index);
                    }
                    else {
                        layer.alert(res.msg, { icon: 5 });
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.alert('操作失败！！！' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5 });
                }
            });

        }, function (index) {
            layer.close(index);
        });
    }

    //批量删除
    $(".delAll_btn").click(function () {
        var checkStatus = table.checkStatus('userListTable'),
            data = checkStatus.data,
            newsId = [];
        if (data.length > 0) {
            for (var i in data) {
                newsId.push(data[i].newsId);
            }
            layer.confirm('确定删除选中的用户？', { icon: 3, title: '提示信息' }, function (index) {
                // $.get("删除文章接口",{
                //     newsId : newsId  //将需要删除的newsId作为参数传入
                // },function(data){
                tableIns.reload();
                layer.close(index);
                // })
            })
        } else {
            layer.msg("请选择需要删除的用户");
        }
    })

    //列表操作
    table.on('toolbar(buttonList)', function (obj) {

        var checkStatus = table.checkStatus("buttonListTable");
        var layEvent = obj.event;
        switch (obj.event) {
            case "add":
            case "edit":
                addorUpdateRole(obj, checkStatus.data);
                break;
            case "del":
                Delete(checkStatus.data)
                break;
            default:
                break;
        }
    });
})
