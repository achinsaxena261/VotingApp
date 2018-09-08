var app = angular.module("voteApp", ["ngRoute"]);

app.config(function($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl : "/Content/Templates/main.html",
        controller: "mainCtrl"        
    })
    .when("/login", {
        templateUrl: "/Content/Templates/login.html",
        controller: "loginCtrl"
    })
    .when("/vote", {
        templateUrl: "/Content/Templates/voting.html",
        controller: "votingCtrl"
    });
});

app.service("apiService", function ($http) {
    this.votingModel = {};

    this.getTeams = function () {
        return $http.get("http://vistahackathontest.azurewebsites.net/VistaAPI/GetTeams");
    }

    this.validateAndGetTeams = function (id, phone) {
        return $http.get("http://vistahackathontest.azurewebsites.net/VistaAPI/ValidateMember?teamId="+id+"&phone="+phone);
    }

    this.getVotingModel = function () {
        return this.votingModel;
    }

    this.setTeamsList = function (data) {
        this.votingModel = data;
        console.log(this.votingModel);
    }
});

app.controller("mainCtrl", function ($scope, $timeout, $location) {
    $timeout(function () { $location.path('/login') }, 3000);
});

app.controller("loginCtrl", function ($scope, $location, apiService) {
    $scope.teams = [];
    $scope.selectedTeam = "";
    $scope.contactNum = "";
    apiService.getTeams().then(function (promise) {
        $scope.teams = promise.data;
    });

    $scope.goForVote = function (team, num) {
        apiService.validateAndGetTeams(team, num).then(function (promise) {
            apiService.setTeamsList(promise.data);
            $location.path('/vote');
        });
    }
});

app.controller("votingCtrl", function ($scope, apiService) {
    $scope.votedTeam = {};
    $scope.votingData = apiService.getVotingModel();
    $scope.updatedModelValue = function (data) {
        console.log(data);
    }
});