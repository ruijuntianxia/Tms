
//(function () {

//    var $this = {
    function verifyToken() {
            if (sessionStorage.token) {
                $.ajax({
                    type: "post",
                    dataType: 'json',
                    url: Api.logintk,
                    contentType: 'application/json',
                    data: JSON.stringify({ name: "CP", token: sessionStorage.token }),
                    success: function (res) {
                        //debugger;
                        if (res.resultcode === -1) {
                            location.href = location.origin + '/Home/index';
                        }
                    },
                    error: function (err) {
                        //debugger;
                        location.href = location.origin + '/Home/index';
                    }
                });
            } else {
                //debugger;
                location.href = location.origin + '/Home/index';
            }
};
function setCookie(c_name, value, expiredays) {
    var exdate = new Date()
    exdate.setDate(exdate.getDate() + expiredays)
    document.cookie = c_name + "=" + escape(value) +
        ((expiredays === null) ? "" : ";expires=" + exdate.toGMTString())
};
        //,
        //init: function () {
        //    $this.verifyToken();
        //    //return this;
        //}
    //};
    //return $this.init();


//}());
