app.controller("DersurSolutionWebApplicationController",
    function ($scope, $http, DersurSolutionWebApplicationService) {

        $scope.formData = {};
        $scope.submittedData = [];
        $scope.contactData = [];

        // Mobile menu toggle
        const menuBtn = document.getElementById("menu-btn");
        const mobileMenu = document.getElementById("mobile-menu");

        if (menuBtn) {
            menuBtn.addEventListener("click", () => {
                mobileMenu.classList.toggle("hidden");
            });
        }

        // FAQ toggle
        document.querySelectorAll('.faq-toggle').forEach(button => {
            button.addEventListener('click', () => {
                const content = button.nextElementSibling;
                const icon = button.querySelector('span');
                content.classList.toggle('hidden');
                icon.textContent = content.classList.contains('hidden') ? '+' : '–';
            });
        });

        lucide.createIcons();

        function wordCount(str) {
            return str.trim().split(/\s+/).length;
        }

        $scope.emailValid = function (email) {
            const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return regex.test(email);
        };

        $scope.nameValid = function (name) {
            const regex = /^[A-Za-z\s\-]+$/;
            return regex.test(name || "");
        };

        // ----------------------------
        // SAVE CONTACT MESSAGE
        // ----------------------------
        $scope.jsonCall = function () {

            $scope.isSubmitted = true;

            if (!$scope.formData.firstName ||
                !$scope.formData.lastName ||
                !$scope.formData.email ||
                !$scope.formData.phone ||
                !$scope.formData.message) {
                return;
            }

            $http.post("/Main/Upsert", {
                FirstName: $scope.formData.firstName,
                LastName: $scope.formData.lastName,
                Email: $scope.formData.email,
                Phone: $scope.formData.phone,
                Message: $scope.formData.message
            }).then(function () {

                alert("Saved successfully!");

                $scope.submittedData.push(angular.copy($scope.formData));
                $scope.formData = {};
                $scope.isSubmitted = false;

            }, function () {
                alert("Error saving data");
            });
        };

        // ----------------------------
        // GET CONTACT MESSAGES
        // ----------------------------
        $scope.getContactMessage = function () {
            DersurSolutionWebApplicationService.getContact()
                .then(function (returnedData) {
                    $scope.contactData = returnedData.data;
                });
        };

        $scope.contactData = [];

        $scope.loadContacts = function () {
            DersurSolutionWebApplicationService.getContact()
                .then(function (res) {
                    $scope.contactData = res.data;
                    initDataTable('#contactTable');
                });
        };

        $scope.archiveContact = function (id) {
            if (!confirm("Archive this message?")) return;

            DersurSolutionWebApplicationService.archiveContact(id)
                .then(function () {
                    $scope.loadContacts();
                });
        };

        $scope.viewContact = function (contact) {
            alert(
                "Name: " + contact.FirstName + " " + contact.LastName +
                "\nEmail: " + contact.Email +
                "\nPhone: " + contact.Phone +
                "\nMessage: " + contact.Message
            );
        };

        $scope.archivedContactData = [];

        // Load archived messages
        $scope.loadArchivedContacts = function () {
            DersurSolutionWebApplicationService.getArchivedContact()
                .then(function (res) {
                    $scope.archivedContactData = res.data;
                    initDataTable('#archivedContactTable');
                });
        };

        // Unarchive
        $scope.unarchiveContact = function (id) {
            if (!confirm("Unarchive this message?")) return;

            DersurSolutionWebApplicationService.unarchiveContact(id)
                .then(function () {
                    $scope.loadArchivedContacts();
                    $scope.loadContacts();
                });
        };

        //Project
        $scope.projects = [];
        $scope.archivedProjects = [];
        $scope.projectStats = [];
        $scope.testimonials = [];

        $scope.loadProjects = function () {
            DersurSolutionWebApplicationService.getProjects()
                .then(res => $scope.projects = res.data);
            initDataTable('#projectTable');


            DersurSolutionWebApplicationService.getArchivedProjects()
                .then(res => $scope.archivedProjects = res.data);
            initDataTable('#archivedProjectTable');

        };
        $scope.loadProjectStats = function () {
            $http.get('/Main/GetProjectStats')
                .then(res => $scope.projectStats = res.data);
        };

        $scope.loadTestimonials = function () {
            $http.get('/Main/GetTestimonials')
                .then(res => $scope.testimonials = res.data);
        };
        $scope.archiveProject = function (id) {
            DersurSolutionWebApplicationService.archiveProject(id)
                .then(() => $scope.loadProjects());
        };

        $scope.unarchiveProject = function (id) {
            DersurSolutionWebApplicationService.unarchiveProject(id)
                .then(() => $scope.loadProjects());
        };


       
        //Services

        $scope.services = [];
        $scope.archivedServices = [];

        $scope.loadServices = function () {
            DersurSolutionWebApplicationService.getServices()
                .then(res => $scope.services = res.data);
            initDataTable('#serviceTable');

            DersurSolutionWebApplicationService.getArchivedServices()
                .then(res => $scope.archivedServices = res.data);
            initDataTable('#archivedServiceTable');

        };

        $scope.archiveService = function (id) {
            DersurSolutionWebApplicationService.archiveService(id)
                .then(() => $scope.loadServices());
        };

        $scope.unarchiveService = function (id) {
            DersurSolutionWebApplicationService.unarchiveService(id)
                .then(() => $scope.loadServices());
        };


        $scope.processLogin = function () {
            $scope.loginSubmitted = true;

            if (!$scope.login.email || !$scope.login.password)
                return;

            $http.post('/Main/AdminLogin', {
                email: $scope.login.email,
                password: $scope.login.password
            }).then(function (res) {
                if (res.data.success) {
                    window.location.href = '/Main/Dashboard';
                } else {
                    alert(res.data.message);
                }
            });
        };
        function initDataTable(tableId) {
            setTimeout(function () {

                if (!window.jQuery || !$.fn || !$.fn.DataTable) return;
                if ($(tableId).length === 0) return;

                if ($.fn.DataTable.isDataTable(tableId)) {
                    $(tableId).DataTable().destroy();
                }

                $(tableId).DataTable({
                    pageLength: 3,
                    lengthMenu: [3, 5, 10],
                    ordering: true,
                    searching: true,
                    responsive: true
                });

            }, 300);
        }



      

        // =======================
        // CHART DATA (FIXED)
        // =======================

        // LINE CHART – Monthly Contacts
        $scope.contactLabels = [];
        $scope.contactChartData = [];

        $http.get('/Main/ContactMonthlyReport').then(function (res) {
            var labels = [];
            var data = [];

            res.data.forEach(function (d) {
                labels.push("Month " + d.Month);
                data.push(d.Count);
            });

            // IMPORTANT: reassign array reference
            $scope.contactLabels = labels;
            $scope.contactChartData = [data];
        });

        // BAR CHART – Project Status
        $scope.projectLabels = ["Active", "Archived"];
        $scope.projectData = [];

        $http.get('/Main/ProjectStatusReport').then(function (res) {
            // MUST be wrapped in array
            $scope.projectData = [[
                res.data.active,
                res.data.archived
            ]];
        });

        // PIE CHART – Services Distribution
        $scope.serviceLabels = [];
        $scope.serviceData = [];

        $http.get('/Main/ServiceReport').then(function (res) {
            var labels = [];
            var data = [];

            res.data.forEach(function (s) {
                labels.push(s.ServiceName || s);
                data.push(1);
            });

            $scope.serviceLabels = labels;
            $scope.serviceData = data;
        });

        // DOUGHNUT – Contact Status
        $scope.contactStatusLabels = ["Active", "Archived"];
        $scope.contactStatusData = [];

        $http.get('/Main/ContactStatusReport').then(function (res) {
            $scope.contactStatusData = [
                res.data.active,
                res.data.archived
            ];
        });


        // ======================
        // PROJECT CMS
        // ======================
        $scope.projectForm = {};
        $scope.projectImageFile = null;

        $scope.editProject = function (project) {
            $scope.projectForm = angular.copy(project);
            $scope.projectImageFile = null;
        };

        $scope.resetProjectForm = function () {
            $scope.projectForm = {};
            $scope.projectImageFile = null;
        };

        $scope.onProjectImageSelect = function (input) {
            var file = input.files[0];
            if (!file) return;

            $scope.projectImageFile = file;

            var reader = new FileReader();
            reader.onload = function (e) {
                $scope.$apply(function () {
                    $scope.projectForm.ProjectImage = e.target.result; // preview
                });
            };
            reader.readAsDataURL(file);
        };

        $scope.saveProjectCMS = function () {

            if (!$scope.projectForm.ProjectTitle) {
                alert("Project title is required");
                return;
            }

            var fd = new FormData();

            fd.append("ProjectID", $scope.projectForm.ProjectID || 0);
            fd.append("ProjectTitle", $scope.projectForm.ProjectTitle);
            fd.append("ProjectDescription", $scope.projectForm.ProjectDescription);
            fd.append("ProjectLink", $scope.projectForm.ProjectLink);

            if ($scope.projectImageFile) {
                fd.append("ProjectImageFile", $scope.projectImageFile);
            }

            $http.post("/Main/SaveProject", fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).then(function () {
                $scope.loadProjects();
                $scope.resetProjectForm();
            });
        };
        // =======================
        // SERVICES CMS (FIXED)
        // =======================

        $scope.services = [];
        $scope.archivedServices = [];
        $scope.serviceForm = {};

        // LOAD (used by BOTH dashboard & service page)
        $scope.loadServices = function () {
            DersurSolutionWebApplicationService.getServices()
                .then(function (res) {
                    $scope.services = res.data;
                });

            DersurSolutionWebApplicationService.getArchivedServices()
                .then(function (res) {
                    $scope.archivedServices = res.data;
                });
        };

        // SAVE / UPDATE
        $scope.saveService = function () {

            if (!$scope.serviceForm.ServiceName || !$scope.serviceForm.ServicePrice) {
                alert("Service name and price are required");
                return;
            }

            // Ensure price is numeric
            $scope.serviceForm.ServicePrice =
                parseFloat($scope.serviceForm.ServicePrice);

            let action = $scope.serviceForm.ServiceID
                ? DersurSolutionWebApplicationService.updateService($scope.serviceForm)
                : DersurSolutionWebApplicationService.saveService($scope.serviceForm);

            action.then(function () {
                $scope.resetServiceForm();
                $scope.loadServices(); // 🔥 THIS IS WHAT YOU WERE MISSING
            });
        };

        $scope.editService = function (s) {
            $scope.serviceForm = angular.copy(s);
        };

        $scope.resetServiceForm = function () {
            $scope.serviceForm = {};
        };

        $scope.archiveService = function (id) {
            DersurSolutionWebApplicationService.archiveService(id)
                .then(function () {
                    $scope.loadServices();
                });
        };

        $scope.unarchiveService = function (id) {
            DersurSolutionWebApplicationService.unarchiveService(id)
                .then(function () {
                    $scope.loadServices();
                });
        };


        // ======================
        // PDF EXPORT (DASHBOARD)
        // ======================
        $scope.downloadDashboardPDF = function () {

            setTimeout(function () {

                var lineChart = document.querySelector('.chart-line')?.toDataURL();
                var barChart = document.querySelector('.chart-bar')?.toDataURL();
                var pieChart = document.querySelector('.chart-pie')?.toDataURL();
                var doughnutChart = document.querySelector('.chart-doughnut')?.toDataURL();

                var docDefinition = {
                    content: [
                        { text: 'Admin Dashboard Report', style: 'header' },
                        { text: 'Generated on: ' + new Date().toLocaleString(), margin: [0, 0, 0, 20] },

                        { text: 'Monthly Contact Messages', style: 'subheader' },
                        { image: lineChart, width: 500 },

                        { text: 'Project Status', style: 'subheader' },
                        { image: barChart, width: 500 },

                        { text: 'Services Distribution', style: 'subheader' },
                        { image: pieChart, width: 400 },

                        { text: 'Contact Status', style: 'subheader' },
                        { image: doughnutChart, width: 400 }
                    ],
                    styles: {
                        header: { fontSize: 20, bold: true },
                        subheader: { fontSize: 14, bold: true, margin: [0, 15, 0, 8] }
                    }
                };

                pdfMake.createPdf(docDefinition).download('Dashboard_Report.pdf');

            }, 500); // ⏳ wait for chart render

        };
    



    });


