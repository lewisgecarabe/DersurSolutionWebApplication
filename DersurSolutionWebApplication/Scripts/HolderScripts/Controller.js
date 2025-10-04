
app.controller("DersurSolutionWebApplicationController", function ($scope) {
    // Form submit function using ng-click
    $scope.submitData = function () {
        if (!$scope.formData.firstName || !$scope.formData.lastName || !$scope.formData.email || !$scope.formData.phone || !$scope.formData.message) {
            alert("Please fill out all fields!");
            return;
        }

        // Store data in multidimensional array
        $scope.submittedData.push({
            firstName: $scope.formData.firstName,
            lastName: $scope.formData.lastName,
            email: $scope.formData.email,
            phone: $scope.formData.phone,
            message: $scope.formData.message
        });

        // Clear form after submission
        $scope.formData = {};
    };
});
