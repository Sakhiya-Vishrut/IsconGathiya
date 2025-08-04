google.charts.load("current", { packages: ["corechart"] });
google.charts.setOnLoadCallback(drawCurveTypes);

function drawCurveTypes() {
    $.ajax({
        url: '/Home/GetMonthlyComplaintData',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
                "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

            // Initialize a dictionary with 0 values
            let monthlyData = {};
            for (let i = 1; i <= 12; i++) {
                monthlyData[i] = { createdCount: 0, completedCount: 0 };
            }

            // Overwrite with actual data
            response.forEach(item => {
                monthlyData[item.month] = {
                    createdCount: Number(item.createdCount),
                    completedCount: Number(item.completedCount)
                };
            });

            var chartData = [['Month', 'Created', 'Completed']];
            for (let i = 1; i <= 12; i++) {
                const name = monthNames[i - 1];
                const created = monthlyData[i].createdCount;
                const completed = monthlyData[i].completedCount;
                chartData.push([name, created, completed]);
            }

            var data = google.visualization.arrayToDataTable(chartData);

            var options = {
                legend: { position: 'top', alignment: 'center' },
                colors: ['#FF0000', '#00FF00'],
                curveType: 'function',
                pointSize: 4,
                lineWidth: 1,
                //chartArea: { width: '85%', height: '70%' },
                vAxis: {
                    title: 'Count',
                    minValue: 0,
                    gridlines: {
                        color: '#d3d3d3',
                        count: -1
                    }
                },
                hAxis: {
                    title: 'Months',
                    gridlines: {
                        color: '#d3d3d3',
                        count: -1
                    },
                    slantedText: false,
                    showTextEvery: 1,
                    textStyle: {
                        fontSize: 12,
                        color: '#333'
                    }
                }
            };

            var chart = new google.visualization.LineChart(document.getElementById('monthlyLineChart'));
            chart.draw(data, options);
        },
        error: function () {
            console.error("Failed to load chart data.");
        }
    });
}




//google.charts.load("current", { packages: ["corechart"] });
//google.charts.setOnLoadCallback(drawCurveTypes);

//function drawCurveTypes() {
//    $.ajax({
//        url: '/Home/GetMonthlyComplaintData',
//        type: 'GET',
//        dataType: 'json',
//        success: function (response) {
//            // Map month numbers to short names
//            const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
//                "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

           
//            var chartData = [['Month', 'Created', 'Completed']];

//            response.forEach(item => {
//                const monthName = monthNames[item.month - 1];
//                const created = Number(item.createdCount);
//                const completed = Number(item.completedCount);
//                console.log("Row:", monthName, created, completed);
//                chartData.push([monthNames, created, completed]);
//            });

//            var data = google.visualization.arrayToDataTable(chartData);

//            var options = {
//                legend: { position: 'top', alignment: 'center' },
//                colors: ['#FF0000', '#00FF00'],
//                curveType: 'function',
//                pointSize: 4,
//                lineWidth: 1,
//                chartArea: { width: '85%', height: '70%' },
//                vAxis: {
//                    title: 'Count',
//                    minValue: 0,
//                    gridlines: {
//                        color: '#d3d3d3',
//                        count: -1
//                    }
//                },
//                hAxis: {
//                    title: 'Months',
//                    gridlines: {
//                        color: '#d3d3d3',
//                        count: -1
//                    },
//                    slantedText: false,
//                    showTextEvery: 1,
//                    textStyle: {
//                        fontSize: 12,
//                        color: '#333'
//                    }
//                }
//            };

//            var chart = new google.visualization.LineChart(document.getElementById('monthlyLineChart'));
//            chart.draw(data, options);
//        },
//        error: function () {
//            console.error("Failed to load chart data.");
//        }
//    });
//}
