$().onLoad
var cpuChart = new SmoothieChart({
    tooltip: true,
    tooltipLine: { strokeStyle: '#bbbbbb' },
    minValue: 0,
    maxValue: 100
});
var ramChart = new SmoothieChart({
    tooltip: true,
    tooltipLine: { strokeStyle: '#bbbbbb' },
    minValue: 0,
    maxValue: 100
});
var diskChart = new SmoothieChart({
    tooltip: true,
    tooltipLine: { strokeStyle: '#bbbbbb' },
    minValue: 0,
    maxValue: 100
});
var cpuSeries = new TimeSeries();
var diskSeries = new TimeSeries();
var ramSeries = new TimeSeries();

cpuChart.addTimeSeries(cpuSeries, {
    strokeStyle: '#4673ce',
    fillStyle: 'rgba(0,45,179,0.30)',
    lineWidth: 4,
    interpolation: 'linear'
});
diskChart.addTimeSeries(diskSeries, {
    strokeStyle: 'rgba(0, 255, 0, 1)',
    fillStyle: 'rgba(0, 255, 0, 0.2)',
    lineWidth: 4,
    interpolation: 'linear'
});
ramChart.addTimeSeries(ramSeries, {
    strokeStyle: '#db4848',
    fillStyle: 'rgba(217,120,120,0.30)',
    lineWidth: 4,
    interpolation: 'linear'
});
cpuChart.streamTo(document.getElementById("smoothie-chart-cpu"), 500);
diskChart.streamTo(document.getElementById("smoothie-chart-disk"), 500);
ramChart.streamTo(document.getElementById("smoothie-chart-ram"), 500);
setInterval(function () {

    $.ajax({
        type: "GET",
        url: "/Home/GetCurrentMetrics",
        dataType: "json",
        contentType: "application/json; charcset=utf8",
        success: (ans) => {
            console.log(ans);

            cpuSeries.append(Date.now(), ans.CpuUsagePercent);
            ramSeries.append(Date.now(), ans.MemoryUsagePercent);
            diskSeries.append(Date.now(), ans.DiskUsagePercent);
        },
        error: (ans) => { console.log(ans) }
    })
}, 500);