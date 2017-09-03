app.factory("akFileUploaderService", ["$q", "$http",
               function ($q, $http) {
                   var getModelAsFormData = function (data) {
                       var dataAsFormData = new FormData();
                       angular.forEach(data, function (value, key) {
                           debugger;
                           dataAsFormData.append(key, value);
                       });
                       debugger;
                       return dataAsFormData;
                   };

                   var saveModel = function (data, url) {
                       debugger;
                       var deferred = $q.defer();
                       /*
                       $http({
                           url: url,
                           method: "POST",
                           data: getModelAsFormData(data),
                           transformRequest: angular.identity,
                           headers: { 'Content-Type': 'application/json' }
                       }).success(function (result) {
                           debugger;
                           deferred.resolve(result);
                       }).error(function (result, status) {
                           debugger;
                           deferred.reject(status);
                       });
                       */
                       var xhr = new XMLHttpRequest;
                       xhr.open("POST", url);
                       xhr.addEventListener("load", function (e) {
                           debugger
                           if (this.status == 200) {                               
                               deferred.resolve("done");
                               callback(null, JSON.parse(this.response));
                           }
                           else {
                               deferred.reject('fail');
                               //callback("error");
                           }

                       }, false);
                       xhr.overrideMimeType("application/octet-stream; charset=x-user-defined;");
                       xhr.setRequestHeader("accept", "application/json")
                       xhr.send(getModelAsFormData(data));

                       return deferred.promise;
                   };

                   return {
                       saveModel: saveModel
                   }

               }]);