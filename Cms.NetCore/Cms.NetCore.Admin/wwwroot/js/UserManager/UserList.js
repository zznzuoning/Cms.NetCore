layui.use(['form', 'layer', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
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
            { type: "checkbox", fixed: "left", width: 50 },
            { field: 'id', title: 'Id', hide: true },
            { field: 'sid', title: '序列号', minWidth: 10, align: "center" },
            { field: 'userName', title: '用户名', minWidth: 100, align: "center" },
            {
                field: 'email', title: '用户邮箱', minWidth: 200, align: 'center', templet: function (d) {
                    return '<a class="layui-blue" href="mailto:' + d.email + '">' + d.email + '</a>';
                }
            },
            { field: 'mobilephone', title: '手机号', minWidth: 100, align: "center" },
            {
                field: 'isenabled', title: '用户状态', align: 'center', templet: function (d) {
                    return !d.isenabled ? "正常使用" : "限制使用";
                }
            },
            { field: 'createUser', title: '创建人', align: 'center', minWidth: 100 },
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
        debugger
        if (obj.event == "edit") {
           
            title = "修改用户";
            $.ajax({
                type: 'GET',
                url: '/UserManager/GetUserById?id=' + data.id,
                success: function (res) {//res为相应体,function为回调函数
                    if (res.code === 0) {
                        index = layui.layer.open({
                            title: title,
                            type: 2,
                            content: "/UserManager/CreateOrUpdate",
                            success: function (layero, index) {
                                debugger
                                var body = layui.layer.getChildFrame('body', index);

                                body.find("#Id").val(res.data.id);
                                body.find(".UserName").val(res.data.UserName);
                                body.find(".RealName").val(res.data.RealName);
                                body.find(".Mobilephone").val(res.data.Mobilephone);
                                body.find(".Email").val(res.data.Email);
                                body.find(".Remarks").text(res.data.Remarks);
                                form.render();

                                setTimeout(function () {
                                    layui.layer.tips('点击此处返回用户列表', '.layui-layer-setwin .layui-layer-close', {
                                        tips: 3
                                    });
                                }, 500)
                            }
                        })

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

            index = layui.layer.open({
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
        layui.layer.full(index);
        window.sessionStorage.setItem("index", index);
        //改变窗口大小时，重置弹窗的宽高，防止超出可视区域（如F12调出debug的操作）
        $(window).on("resize", function () {
            layui.layer.full(window.sessionStorage.getItem("index"));
        })
    }
    $(".addNews_btn").click(function () {
        addUser();
    })

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
    table.on('toolbar(userList)', function (obj) {
       
        var checkStatus = table.checkStatus("userListTable");
        var layEvent = obj.event;
        switch (obj.event) {
            case "add":
            case "edit":
                addorUpdateUser(obj, checkStatus.data[0]);
                break;
            case "usable":
                break;
            case "del":
                break;
            default:
                break;
        }
         if (layEvent === 'usable') { //启用禁用
            var _this = $(this),
                usableText = "是否确定禁用此用户？",
                btnText = "已禁用";
            if (_this.text() == "已禁用") {
                usableText = "是否确定启用此用户？",
                    btnText = "已启用";
            }
            layer.confirm(usableText, {
                icon: 3,
                title: '系统提示',
                cancel: function (index) {
                    layer.close(index);
                }
            }, function (index) {
                _this.text(btnText);
                layer.close(index);
            }, function (index) {
                layer.close(index);
            });
        } else if (layEvent === 'del') { //删除
            layer.confirm('确定删除此用户？', { icon: 3, title: '提示信息' }, function (index) {
                // $.get("删除文章接口",{
                //     newsId : data.newsId  //将需要删除的newsId作为参数传入
                // },function(data){
                tableIns.reload();
                layer.close(index);
                // })
            });
        }
    });

})
