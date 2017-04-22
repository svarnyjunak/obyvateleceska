﻿(function () {
    var dataRows = document.querySelectorAll(".data-population-progress>tbody>tr");
    var data = Array.prototype.map.call(dataRows, function (tr, i) {
        return {
            year: Number(tr.children[0].innerText),
            population: Number(tr.children[1].innerText)
        };
    });
    
    var maxValue = data.reduce(function (result, value) { return result > value.population ? result : value.population; }, 0);
    var maxWidth = getBarMaxWidth();
    var scale = function (v) { return v * maxWidth / maxValue; };

    var chart = document.querySelector(".chart");
    var divs = data.map(function (v) {
        return "<div><span class='year-caption'>" + v.year + "</span><span class='bar' data-width='" + scale(v.population) + "px' style='width: 0;visibility: hidden;transition: width 2s ease-out'>" + formatNumber(v.population) + "</div>";
    });

    chart.innerHTML = divs.join("");

    function showBars() {
        var bars = document.querySelectorAll(".chart div .bar");
        var barsArray = Array.prototype.slice.call(bars);
        barsArray.forEach(function (s) {
            s.style.visibility = "visible";
            s.style.width = s.getAttribute("data-width");
        });
    }

    function formatNumber(n) {
        return n.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    }

    setTimeout(showBars, 500);

    function getBarMaxWidth() {
        var defaultMaxWidth = 330;
        var currentWidth = document.querySelector(".chart").offsetWidth - 40;
        return defaultMaxWidth < currentWidth ? defaultMaxWidth : currentWidth;
    }

})();