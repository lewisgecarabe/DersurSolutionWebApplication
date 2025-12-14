app.service("DersurSolutionWebApplicationService", function ($http) {

    this.collectiveConnect = function (userData) {
        return $http.post("/Main/CollectiveParameter", userData);
    };

    this.jsonService = function (userData) {
        return $http.post("/Main/JsonFunction", userData);
    };

    this.getContactMessage = function () {
        return $http.get("/Main/GetContact");
    };

});
