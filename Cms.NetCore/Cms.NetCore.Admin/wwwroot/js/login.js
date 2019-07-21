layui.use(['form','layer','jquery'],function(){
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer
        $ = layui.jquery;
    $("#CaptchaCodeImg").click(function () {
        d = new Date();
        $("#CaptchaCodeImg").attr("src", "/Account/CheckCode?" + d.getTime());
    });
    //登录按钮
    form.on("submit(login)", function (data) {
        var obj = $(this);
        obj.text("登录中...").attr("disabled","disabled").addClass("layui-disabled");
        $.ajax({
            type: 'POST',
            url: '/Account/Login',
            data: data.field,
            dataType: "json",
            success: function (res) {//res为相应体,function为回调函数
                if (res.code === 200) {
                    window.location.href = "/";
                } else {
                    layer.alert(res.msg, { icon: 5 });
                    d = new Date();
                    $("#code").val('');
                    $("#CaptchaCodeImg").attr("src", "/Account/CheckCode?" + d.getTime());
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
    })

    //表单输入效果
    $(".loginBody .input-item").click(function(e){
        e.stopPropagation();
        $(this).addClass("layui-input-focus").find(".layui-input").focus();
    })
    $(".loginBody .layui-form-item .layui-input").focus(function(){
        $(this).parent().addClass("layui-input-focus");
    })
    $(".loginBody .layui-form-item .layui-input").blur(function(){
        $(this).parent().removeClass("layui-input-focus");
        if($(this).val() != ''){
            $(this).parent().addClass("layui-input-active");
        }else{
            $(this).parent().removeClass("layui-input-active");
        }
    })
})
