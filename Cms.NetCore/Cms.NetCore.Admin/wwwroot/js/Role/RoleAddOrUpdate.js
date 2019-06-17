layui.use(['form', 'layer'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery;

    form.on("submit(addRole)", function (data) {
        //弹出loading
        var index = top.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        //获取防伪标记
        $.ajax({
            type: 'POST',
            url: '/Role/CreateOrUpdate/',
            data: {
                Id: $("#Id").val(),  //主键
                RoleName: $(".RoleName").val(),
                IsDefault: $("#IsDefault").get(0).checked,
                Remarks: $(".Remarks").val(),
            },
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
            top.layer.msg("角色添加成功！");
            layer.closeAll("iframe");
            //刷新父页面
           parent.location.reload();
        }, 2000);
        return false;
    });

});