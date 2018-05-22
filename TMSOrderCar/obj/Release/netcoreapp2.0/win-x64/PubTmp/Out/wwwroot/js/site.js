// Write your JavaScript code.
(function () {
    var $this = {
        data: {
            name: $('input[name="name"]'),
            password: $('input[name="password"]')
        },
        addEvent: function () {
            $('#submit').on('click', function () {
                var data = {
                    Name: $this.data.name.val(),
                    Password: $this.data.password.val(),
                    Type: "CP"
                };
                if (!data.Name || !data.Password) {
                    alert("账号或密码输入不正确");
                    return;
                }
                $.ajax({
                    type: "post",
                    dataType: 'json',
                    url: Api.login,
                    contentType: 'application/json',
                    data: JSON.stringify(data),
                    success: function (res) {
                        if (res.resultcode !== -1) {
                            if (res.token) {
                                sessionStorage.token = res.token;
                                sessionStorage.user = data.Name;
                                setCookie("user", data.Name,1);
                            }
                            location.href = location.origin + '/' + res.file;
                        } else {
                            alert('账号或密码错误!');
                        }
                    },
                    error: function (err) {
                        //console.log(err);
                        alert('服务器内部错误，请稍后再试！');
                    }
                });
            });
        },
        init: function () {
            $this.addEvent();
            return this;
        }
    };
    return $this.init();

}());