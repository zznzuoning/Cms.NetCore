layui.use(['formSelects', 'form', 'layer'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        formSelects = layui.formSelects;

    formSelects.render('role', {
        skin: "danger",                 //多选皮肤
        radio: false,                   //是否设置为单选模式s
        showCount: 0,           //多选的label数量, 0,负值,非数字则显示全部
    });

    //server
    formSelects.data('role', 'server', {
        url: '/UserManager/GetRole?id=' + $("#Id").val()
    });

    form.on("submit(setRole)", function (data) {
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        //获取防伪标记
        $.ajax({
            type: 'POST',
            url: '/UserManager/SetRole/',
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
            top.layer.msg("设置成功！");
            layer.closeAll("iframe");
            //刷新父页面
            parent.location.reload();
        }, 2000);
        return false;
    });

});