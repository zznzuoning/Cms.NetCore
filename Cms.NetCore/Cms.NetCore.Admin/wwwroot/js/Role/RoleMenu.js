layui.use(['form', 'tree', 'layer'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        tree = layui.tree;
    var arr = [];
    $.ajax({
        type: 'GET',
        url: '/Role/GetAllMenuButtonByRoleId?id=' + $("#Id").val(),
        success: function (res) {//res为相应体,function为回调函数
            if (res.code === 0) {
                tree.render({
                    elem: '#menuButtonTree'
                    , data: res.data
                    , showCheckbox: true  //是否显示复选框
                    , id: 'menuButton'
                    , oncheck: function (obj) {
                        var data = obj.data;  //获取当前点击的节点数据
                        data.checked = obj.checked;
                        //改变所有选中状态
                        SetChecked(data, obj.checked)
                    }
                });
            }
            else {
                layer.alert(res.msg, { icon: 5 });
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            layer.alert('操作失败！！！' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5 });
        }
    });

    form.on("submit(roleMenu)", function (data) {
        var checkedData = tree.getChecked('menuButton');
       
        if (checkedData.length == 0) {
            layer.alert("请选择要授权的功能", { icon: 5 });
            return false;
        }
        getChildren(checkedData);//获取选中节点的数据
        
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        //获取防伪标记
        $.ajax({
            type: 'POST',
            url: '/Role/RoleMenu/',
            data: {
                id: $("#Id").val(),  //主键
                menuButtonAttributes: arr
            },
            dataType: "json",
            success: function (res) {//res为相应体,function为回调函数
                arr = [];
                var alertIndex;
                if (res.code === 200) {
                    alertIndex = layer.alert(res.msg, { icon: 1 }, function () {
                        layer.closeAll("iframe");
                        //刷新父页面                     
                        parent.location.reload();
                        top.layer.close(alertIndex);
                    });
                    //$("#res").click();//调用重置按钮将表单数据清空
                }
                else {
                    layer.alert(res.msg, { icon: 5 });
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.alert('操作失败！！！' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5 });
            }
        });
        setTimeout(function () {
            top.layer.close(index);
            top.layer.msg("角色授权成功！");
            layer.closeAll("iframe");
            //刷新父页面
            parent.location.reload();
        }, 2000);
        return false;
    });
    function getChildren(data) {

        for (var i in data) {
            if (data[i].children != undefined) {
                getChildren(data[i].children);
            } else {
                if (data[i].checked) {
                    arr.push(data[i].attributes)
                }
            }
        }

    }
    function SetChecked(data, checked) {
        var children = [];
        if (data.children != undefined) {
            children = data.children;
        }
        else {
            children = data;
        }
        for (var i in children) {
            children[i].checked = checked;
            if (children[i].children != undefined) {
                SetChecked(children[i].children, checked);
            }
        }



    }
});