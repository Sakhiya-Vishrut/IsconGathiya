
//google.charts.load("current", {
//packages: ["corechart"]
//        });
//google.charts.setOnLoadCallback(drawChart);

//function drawChart()
//{
//    var data = google.visualization.arrayToDataTable([
//      ["Issue", "Percentage"],
//            ["No Water Supply", 25],
//            ["Leakage in Pipeline", 20],
//            ["Tank Overflow", 18],
//            ["Drainage Blockage", 12],
//            ["Low Water Pressure", 10],
//            ["Tap or Mixer Broken", 8],
//            ["Water Heater Not Working", 7]
//    ]);

//    var options = {
//            legend: { position: "bottom" }
//          };

//var chart = new google.visualization.PieChart(
//  document.getElementById("googlePieChart")
//);
//chart.draw(data, options);
//        }

google.charts.load("current", { packages: ["corechart"] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {

    $.ajax({
        url: '/Home/GetProblemChartData',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            var dataArray = [["Problem", "Count"]];
            data.forEach(function (item) {
                dataArray.push([item.problemName, item.count]);
            });

            var chartData = google.visualization.arrayToDataTable(dataArray);
            var options = {
                //is3D: true,
                legend: { position: 'bottom' },
                pieSliceTextStyle: {
                    color: 'white',
                    fontSize: 9
                   
                }
                
            };

            var chart = new google.visualization.PieChart(document.getElementById("googlePieChart"));
            chart.draw(chartData, options);
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });

}



