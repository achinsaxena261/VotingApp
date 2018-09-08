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
     })
    .otherwise({
        redirect: '/login'
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

    this.submitVote = function (memberId, teamId) {
        return $http.post("http://vistahackathontest.azurewebsites.net/VistaAPI/UpdateVote", { "memberId": memberId, "teamId": teamId });
    }

    this.getVotingModel = function () {
        return this.votingModel;
    }

    this.setTeamsList = function (data) {
        this.votingModel = data;
    }
});

app.controller("mainCtrl", function ($scope, $timeout, $location) {
    $timeout(function () { $location.path('/login') }, 2000);
});

app.controller("loginCtrl", function ($scope, $location, apiService) {
    $scope.teams = [];
    $scope.selectedTeam = "";
    $scope.contactNum = "";
    $("#loadingModal").modal('show');
    apiService.getTeams().then(function (promise) {
        $scope.teams = promise.data;
        $("#loadingModal").modal('hide');
    });

    $scope.goForVote = function (team, num) {
        $("#loadingModal").modal('show');
        apiService.validateAndGetTeams(team, num).then(function (promise) {
            apiService.setTeamsList(promise.data);
            $("#loadingModal").modal('hide');
            $location.path('/vote');
        });
    }
});

app.controller("votingCtrl", function ($scope, $location,apiService) {
    $scope.votedTeam = {};
    $scope.votingData = apiService.getVotingModel();
    $scope.updatedModelValue = function (data) {
        console.log(data);
    }

    $scope.clickToVote = function (mid, tid) {
        $("#loadingModal").modal('show');
        apiService.submitVote(mid, tid).then(function (promise) {
            $("#loadingModal").modal('hide');
            if (promise.data) {
                $location.path('/');
            }
        });
    }
});