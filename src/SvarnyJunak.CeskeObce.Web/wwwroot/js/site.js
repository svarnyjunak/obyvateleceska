// Write your Javascript code.
(function () {
    document.querySelector("input#municipality-search").addEventListener("keyup", autocomplete);

    var awesomplete = new Awesomplete(document.querySelector("input#municipality-search"));

    function autocomplete() {
        var ajax = new XMLHttpRequest();
        ajax.open("POST", "Home/FindMunicipalities", true);
        ajax.onload = function () {
            console.log(JSON.parse(ajax.responseText));

            var list = JSON.parse(ajax.responseText);
            awesomplete.list = list;
        };
        ajax.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        ajax.send("name=" + this.value);
    }
})();