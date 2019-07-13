layui.use(['form', 'jquery', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,

        //layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        layer = layui.layer,

        laytpl = layui.laytpl,
        table = layui.table;

    //用户列表
    var tableIns = table.render({
        elem: '#userList',
        url: '/UserManager/GetList',
        cellMinWidth: 95,
        toolbar: "#userListBar",
        page: true,
        height: "full-125",
        limits: [10, 15, 20, 25],
        limit: 20,
        id: "userListTable",
        cols: [[
            { type: "radio", fixed: "left", width: 50 },
            { type: "numbers", fixed: "left", width: 50 },
            { field: 'id', title: 'Id', fixed: "left", hide: true },
            //{ field: 'sid', width:10, align: "center" },
            { field: 'userName', title: '用户名', width: 100, align: "center" },
            {
                field: 'email', title: '用户邮箱', width: 200, align: 'center', templet: function (d) {
                    return '<a class="layui-blue" href="mailto:' + d.email + '">' + d.email + '</a>';
                }
            },
            { field: 'mobilephone', title: '手机号', width: 150, align: "center" },
            {
                field: 'isEnabled', title: '用户状态', width: 100, align: 'center', templet: function (d) {
                    return !d.isEnabled ? "正常使用" : "限制使用";
                }
            },
            { field: 'createUser', title: '创建人', align: 'center', width: 100 },
            { field: 'createTime', title: '创建时间', align: 'center', minWidth: 150 }
        ]]
    });

    //搜索【此功能需要后台配合，所以暂时没有动态效果演示】
    $(".search_btn").on("click", function () {
        table.reload("userListTable", {
            page: {
                curr: 1 //重新从第 1 页开始
            },
            where: {
                Name: $(".searchName").val(),  //搜索的关键字
                IsEnabled: $("#searchIsEnabled").val()  //搜索的关键字
            }
        })

    });

    //添加用户
    function addorUpdateUser(obj, data) {
        var index;
        var title = "添加用户"
        if (obj.event == "edit") {
            if (data.length == 0) {
                layer.alert("请选择要修改的用户", { icon: 5 });
                return;
            }
            if (data.length > 1) {
                layer.alert("不支持批量修改", { icon: 5 });
                return;
            }
            title = "修改用户";
            $.ajax({
                type: 'GET',
                url: '/UserManager/GetUserById?id=' + data[0].id,
                success: function (res) {//res为相应体,function为回调函数

                    if (res.code === 0) {
                        index = layer.open({
                            title: title,
                            type: 2,
                            content: "/UserManager/CreateOrUpdate",
                            success: function (layero, index) {

                                var body = layui.layer.getChildFrame('body', index);

                                body.find("#Id").val(res.data.id);
                                body.find(".UserName").val(res.data.userName);
                                body.find(".RealName").val(res.data.realName);
                                body.find(".Mobilephone").val(res.data.mobilephone);
                                body.find(".Email").val(res.data.email);
                                body.find(".Remarks").text(res.data.remarks);
                                form.render();

                                setTimeout(function () {
                                    layui.layer.tips('点击此处返回用户列表', '.layui-layer-setwin .layui-layer-close', {
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
                content: "/UserManager/CreateOrUpdate",
                success: function (layero, index) {
                    setTimeout(function () {
                        layui.layer.tips('点击此处返回用户列表', '.layui-layer-setwin .layui-layer-close', {
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
    function UsableOrDelete(btn, event, data) {
        var msgText,
            usableText = "是否确定禁用此用户？",
            btnText = "启用";
        if (event === "usable") {
            msgText = "请选择要禁用或启用的用户";
            if (btn.text() == "启用") {
                usableText = "是否确定启用此用户？",
                    btnText = "禁用";
            }
        }
        else {
            msgText = "请选择要删除的用户";
            usableText = "是否确定删除此用户";
        }
        if (data.length == 0) {
            layer.alert(msgText, { icon: 5 });
            return;
        }

        layer.confirm(usableText, {
            icon: 3,
            title: '系统提示',
            cancel: function (index) {
                layer.close(index);
            }
        }, function (index) {

            $.ajax({
                type: 'POST',
                url: '/UserManager/SetIsEnableOrDelete',
                data: { Id: data[0].id, IsDelete: event === "del" },
                success: function (res) {//res为相应体,function为回调函数
                    if (res.code === 200) {
                        if (event === "usable") {
                            btn.text(btnText);
                        }
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
    function SetRole(data) {
        if (data.length == 0) {
            layer.alert("请选择要设置的用户", { icon: 5 });
            return;
        }
        var index = layer.open({
            title: "设置角色",
            type: 2,
            content: "/UserManager/SetRole",
            success: function (layero, index) {

                var body = layui.layer.getChildFrame('body', index);

                body.find("#Id").val(data[0].id);
                body.find(".UserName").val(data[0].userName);
                form.render();
                setTimeout(function () {
                    layui.layer.tips('点击此处返回用户列表', '.layui-layer-setwin .layui-layer-close', {
                        tips: 3
                    });
                }, 500)


            }
        })
        layer.full(index);

        window.sessionStorage.setItem("index", index);
        //改变窗口大小时，重置弹窗的宽高，防止超出可视区域（如F12调出debug的操作）
        $(window).on("resize", function () {
            layui.layer.full(window.sessionStorage.getItem("index"));
        })
    }
    //列表操作
    table.on('toolbar(userList)', function (obj) {

        var checkStatus = table.checkStatus("userListTable");
        var layEvent = obj.event;
        switch (obj.event) {
            case "add":
            case "edit":
                addorUpdateUser(obj, checkStatus.data);
                break;
            case "usable":
            case "del":
                UsableOrDelete($(this), obj.event, checkStatus.data)
                break;
            case "setRole":
                SetRole(checkStatus.data);
                break;
            default:
                break;
        }
    });
    table.on('radio(userList)', function (obj) {
        var btnText = "";
        if (obj.data.isEnabled) {
            btnText = "启用";
        }
        else {
            btnText = "禁用";
        }
        $("#usable").text(btnText);
    });
})
