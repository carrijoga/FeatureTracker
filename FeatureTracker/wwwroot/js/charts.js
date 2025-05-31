function chartBar() {
    google.charts.load('current', { packages: ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);

    var data = google.visualization.arrayToDataTable([
        ['Pending', '12' ],
        ['Under Review', '8' ],
        ['In Development', '14' ],
        ['In Testing', '6' ],
        ['Completed', '18' ],
    ]);

    var chart = new google.visualization.BarChart(document.getElementById('chart_div'));
}
