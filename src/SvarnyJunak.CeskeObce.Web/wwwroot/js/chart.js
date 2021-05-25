(function () {
    var dataRows = document.querySelectorAll(".data-population-progress>tbody>tr");
    var data = Array.prototype.map.call(dataRows, function (tr, i) {
        return {
            year: Number(tr.children[0].innerText),
            population: Number(tr.children[1].innerText.replace(/\s/g, ''))
        };
    });
    
    var maxValue = data.reduce(function (result, value) { return result > value.population ? result : value.population; }, 0);
    var maxWidth = getBarMaxWidth();
    var scale = function (v) { return v * maxWidth / maxValue; };

    function initBars() {
        var bars = document.querySelectorAll(".chart td .bar");
        var barsArray = Array.prototype.slice.call(bars);

        barsArray.forEach(function (bar) {
            bar.style.width = 0;
            bar.style.visibility = "hidden";
            bar.style.transition = "width 2s ease-out";
            bar.dataset.width = scale(Number(bar.textContent.replace(/\s/g, '')));
        });
    };

    initBars();

    function showBars() {
        var bars = document.querySelectorAll(".chart td .bar");
        var barsArray = Array.prototype.slice.call(bars);

        barsArray.forEach(function (s) {
            s.style.visibility = "visible";
            s.style.width = s.dataset.width + "px";
        });
    }

    setTimeout(showBars, 500);

    function getBarMaxWidth() {
        var defaultMaxWidth = 330;
        var currentWidth = document.querySelector(".chart").offsetWidth - 60;
        return defaultMaxWidth < currentWidth ? defaultMaxWidth : currentWidth;
    }

})();