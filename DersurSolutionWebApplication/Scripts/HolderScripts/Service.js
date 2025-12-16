app.service("DersurSolutionWebApplicationService", function ($http) {

    this.collectiveConnect = function (userData) {
        return $http.post("/Main/CollectiveParameter", userData);
    };

    this.jsonService = function (userData) {
        return $http.post("/Main/JsonFunction", userData);
    };

    /* =======================
       CONTACT MESSAGE (CMS)
    ======================== */

    // ACTIVE contact messages
    this.getContact = function () {
        return $http.get("/Main/GetContact");
    };

    // ARCHIVED contact messages
    this.getArchivedContact = function () {
        return $http.get("/Main/GetArchivedContact");
    };

    // ARCHIVE
    this.archiveContact = function (id) {
        return $http.post("/Main/ArchiveContact", { id: id });
    };

    // UNARCHIVE
    this.unarchiveContact = function (id) {
        return $http.post("/Main/UnarchiveContact", { id: id });
    };

    //Project

    this.getProjects = function () {
        return $http.get("/Main/GetProjects");
    };

    this.getArchivedProjects = function () {
        return $http.get("/Main/GetArchivedProjects");
    };

    this.saveProject = function (data) {
        return $http.post("/Main/SaveProject", data);
    };

    this.updateProject = function (data) {
        return $http.post("/Main/UpdateProject", data);
    };

    this.archiveProject = function (id) {
        return $http.post("/Main/ArchiveProject", { id: id });
    };

    this.unarchiveProject = function (id) {
        return $http.post("/Main/UnarchiveProject", { id: id });
    };

    this.getProjectStats = function () {
        return $http.get("/Main/GetProjectStats");
    };

    this.getTestimonials = function () {
        return $http.get("/Main/GetTestimonials");
    };

    //Services
    this.getServices = function () {
        return $http.get("/Main/GetServices");
    };

    this.getArchivedServices = function () {
        return $http.get("/Main/GetArchivedServices");
    };

    this.saveService = function (data) {
        return $http.post("/Main/SaveService", data);
    };

    this.updateService = function (data) {
        return $http.post("/Main/UpdateService", data);
    };

    this.archiveService = function (id) {
        return $http.post("/Main/ArchiveService", { id: id });
    };

    this.unarchiveService = function (id) {
        return $http.post("/Main/UnarchiveService", { id: id });
    };

   

});
