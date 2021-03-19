(function () {
    document.getElementById("municipality-search").focus();

    var xhr;
    new autoComplete({
        minChars: 1,
        cache: false,
        selector: 'input#municipality-search',
        onSelect: function (e, term, item) {
            var value = item.getAttribute('data-val');

            document.querySelector("#municipality-search").value = value;
            document.querySelector("form#select-municipality-form")
                .submit();
        },
        source: function (term, response) {
            try { xhr.abort(); } catch (e) { }
            xhr = new XMLHttpRequest();
            xhr.open("GET", "/api/municipalities?name=" + encodeURIComponent(term), true);
            xhr.onload = function () {
                var data = JSON.parse(xhr.responseText);
                response(data);
            };
            xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            xhr.send();
        }
    });
})();