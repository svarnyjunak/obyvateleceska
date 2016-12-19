(function () {
    var data = [
        { year: 2000, population: 100 },
        { year: 2001, population: 102 },
        { year: 2002, population: 103 }
    ];


    var maxValue = data.reduce(function (result, value) { return result > value.population ? result : value.population }, 0);
    var scale = function (v) { return v * 330 / maxValue; };

    console.log(maxValue);

    var chart = document.querySelector(".chart");
    var divs = data.map(function (v) {
        return "<div>" + v.year + "<span data-width='" + scale(v.population) + "px' style='width: 0;visibility: hidden;transition: width 2s ease-out'>" + v.population + "</div>";
    });

    chart.innerHTML = divs.join("");

    function showBars() {
        document.querySelectorAll(".chart div span").forEach(function (s) {
            s.style.visibility = "initial",
                s.style.width = s.getAttribute("data-width");
        });
    }

    setTimeout(showBars, 500);

})();