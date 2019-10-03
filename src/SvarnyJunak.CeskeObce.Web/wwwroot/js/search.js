(function () {
    document.getElementById("municipality-search").focus();

    new autoComplete({
        minChars: 1,
        selector: 'input#municipality-search',
        onSelect: function (e, term, item) {
            var value = item.getAttribute('data-val');

            document.querySelector("#municipality-search").value = value;
            document.querySelector("form#select-municipality-form")
                    .submit();
        },
        source: function (term, response) {
            var ajax = new XMLHttpRequest();
            ajax.open("GET", "/api/municipalities?name=" + encodeURIComponent(term), true);
            ajax.onload = function () {
                var data = JSON.parse(ajax.responseText);
                response(data);
            };
            ajax.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            ajax.send();
        }
    });
})();