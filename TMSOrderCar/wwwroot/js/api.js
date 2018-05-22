var ApiSever = {
    production: "http://120.79.37.127",
    localhost: null,//"http://192.168.1.180",
    test: "http://120.79.37.127:8009"//"http://192.168.1.180:8009"
}
var url = ApiSever.localhost || ApiSever.production;
Api = {
    getReportTable: url + "/ReportApi/api/ReportTable/GetReportTable", //获取看板列表数据
    getReportDet: url + "/ReportApi/api/ReportTable/GetReportDet", //获取储位详情数据
    getReport: url + "/ReportApi/api/ReportTable/GetReport", //获取平面图数据
    logintk: ApiSever.test + '/logintk/', 					//token验证
    login: ApiSever.test + '/login/' 						//登录
}