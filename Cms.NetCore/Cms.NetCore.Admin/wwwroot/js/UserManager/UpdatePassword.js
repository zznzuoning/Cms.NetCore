﻿layui.use(['form', 'layer', 'laydate', 'table', 'laytpl'], function () {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laydate = layui.laydate,
        laytpl = layui.laytpl,
        table = layui.table;

    //添加验证规则
    form.verify({
        newPwd: function (value, item) {
            if (/(^\_)|(\__)|(\_+$)/.test(value)) {
                return '密码首尾不能出现下划线\'_\'';
            }
            if (value.length > 32 || value.length < 4) {
                return '验证码长度必须符合规则';
            }
        },
        confirmPwd: function (value, item) {
            if (!new RegExp($("#NewPassword").val()).test(value)) {
                return "两次输入密码不一致，请重新输入！";
            }
        }
    });

    form.on("submit(updatePwd)", function (data) {
        var obj = $(this);
        obj.text("提交中...").attr("disabled", "disabled").addClass("layui-disabled");
        //获取防伪标记
        $.ajax({
            type: 'POST',
            url: '/UserManager/UpdatePassWord',
            data: data.field,
            dataType: "json",
            success: function (res) {//res为相应体,function为回调函数
                if (res.code === 200) {
                    layer.alert(res.ResultMsg, { icon: 1 }, function (index) {
                        layer.close(index);
                        parent.location.href = "/Account/LoginOut";

                    });

                } else {
                    layer.alert(res.msg, { icon: 5 }, function (index) {
                        layer.close(index);
                        location.reload();
                    });
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.alert('操作失败！！！' + XMLHttpRequest.status + "|" + XMLHttpRequest.readyState + "|" + textStatus, { icon: 5 });
            },
            complete: function () {
                obj.text("登录").removeAttr("disabled").removeClass("layui-disabled");

            }
        });
        return false;
    });
});