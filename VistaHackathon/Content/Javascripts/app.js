var app = angular.module("voteApp", ["ngRoute"]);

app.config(function($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl : "Content/Templates/main.html",
        controller: "mainCtrl"        
    })
    .when("/login", {
        templateUrl: "Content/Templates/login.html",
        controller: "loginCtrl"
    })
    .when("/vote", {
        templateUrl: "Content/Templates/vote-your-team.html",
        controller: "votingCtrl"        
    });
});

app.service("apiService", function ($http) {
    this.getTeams = function () {
        return $http.get("http://vistahackathontest.azurewebsites.net/VistaAPI/GetTeams");
    }
});

app.controller("mainCtrl", function ($scope, $timeout, $location) {
    $timeout(function () { $location.path('\login') }, 3000);
});

app.controller("loginCtrl", function ($scope, apiService) {
    $scope.teams = [];
    $scope.selectedTeam = "";
    apiService.getTeams().then(function (promise) {
        $scope.teams = promise.data;
    });
});

app.controller("votingCtrl",function($scope){

});