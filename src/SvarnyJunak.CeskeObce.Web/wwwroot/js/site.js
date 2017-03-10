(function () {
    new autoComplete({
        selector: 'input#municipality-search',
        source: function (term, response) {
            var ajax = new XMLHttpRequest();
            ajax.open("POST", "Home/FindMunicipalities", true);
            ajax.onload = function () {
                var data = JSON.parse(ajax.responseText);
                response(data);
            };
            ajax.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            ajax.send("name=" + term);
        }
    });
})();