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
            DersurSolutionWebApplicationService.getContactMessage()
                .then(function (returnedData) {
                    $scope.contactData = returnedData.data;
                });
        };

    });
