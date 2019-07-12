layui.use(['treeSelect', 'form', 'layer'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        treeSelect = layui.treeSelect;
    treeSelect.render({
        // 选择器
        elem: '#tree',
        // 数据
        data: '/Menu/GetTreeList',
       
        // 异步加载方式：get/post，默认get
        type: 'get',
        // 占位符
        placeholder: '请选择父级菜单',
        // 是否开启搜索功能：true/false，默认false
        style: {
            folder: { // 父节点图标
                enable: false // 是否开启：true/false
            },
            line: { // 连接线
                enable: true // 是否开启：true/false
            }
        },
        // 点击回调
        click: function (d) {
            $("#ParentId").val(d.current.id);
        },
        // 加载完成后的回调函数
        success: function (d) {
           
            if ($("#ParentId").val() != "") {
                treeSelect.checkNode('tree', $("#ParentId").val());
            }
        }
    });
    form.on("submit(addMenu)", function (data) {
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        //获取防伪标记
        $.ajax({
            type: 'POST',
            url: '/Menu/CreateOrUpdate/',
            data: data.field,
            dataType: "json",
            success: function (res) {//res为相应体,function为回调函数
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
            top.layer.msg("菜单添加成功！");
            layer.closeAll("iframe");
            //刷新父页面
            parent.location.reload();
        }, 2000);
        return false;
    });

});