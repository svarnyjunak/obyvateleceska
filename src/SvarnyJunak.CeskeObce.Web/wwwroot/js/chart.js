(function () {
    var dataRows = document.querySelectorAll(".data-population-progress>tbody>tr");
    var data = Array.prototype.map.call(dataRows, function (tr, i) {
        return {
            year: Number(tr.children[0].innerText),
            population: Number(tr.children[1].innerText)
        };
    })
    
    var maxValue = data.reduce(function (result, value) { return result > value.population ? result : value.population }, 0);
    var scale = function (v) { return v * 330 / maxValue; };

    var chart = document.querySelector(".chart");
    var divs = data.map(function (v) {
        return "<div><span class='year-caption'>" + v.year + "</span><span class='bar' data-width='" + scale(v.population) + "px' style='width: 0;visibility: hidden;transition: width 2s ease-out'>" + formatNumber(v.population) + "</div>";
    });

    chart.innerHTML = divs.join("");

    function showBars() {
        document.querySelectorAll(".chart div .bar").forEach(function (s) {
            s.style.visibility = "initial",
            s.style.width = s.getAttribute("data-width");
        });
    }

    function formatNumber(n) {
        return n.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    }

    setTimeout(showBars, 500);

})();