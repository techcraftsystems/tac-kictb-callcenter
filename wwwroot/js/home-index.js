// Dashboard - Analytics
//----------------------

(function(window, document, $) {
   //Trending line chart
   var revenueLineChartCTX = $("#revenue-line-chart");

   var revenueLineChartOptions = {
      responsive: true,
      // maintainAspectRatio: false,
      legend: {
         display: false
      },
      hover: {
         mode: "label"
      },
      scales: {
         xAxes: [
            {
               display: true,
               gridLines: {
                  display: false
               },
               ticks: {
                  fontColor: "#fff"
               }
            }
         ],
         yAxes: [
            {
               display: true,
               fontColor: "#fff",
               gridLines: {
                  display: true,
                  color: "rgba(255,255,255,0.3)"
               },
               ticks: {
                  beginAtZero: false,
                  fontColor: "#fff"
               }
            }
         ]
      }
   };

   var revenueLineChartData = {
      labels: ["Jul", "Jun", "May", "Apr", "Mar", "Feb", "Jan"],
      datasets: [
         {
            label: "Today",
            data: [100, 50, 20, 40, 80, 50, 80],
            backgroundColor: "rgba(128, 222, 234, 0.5)",
            borderColor: "#d1faff",
            pointBorderColor: "#d1faff",
            pointBackgroundColor: "#00bcd4",
            pointHighlightFill: "#d1faff",
            pointHoverBackgroundColor: "#d1faff",
            borderWidth: 2,
            pointBorderWidth: 2,
            pointHoverBorderWidth: 4,
            pointRadius: 4
         },
         {
            label: "Second dataset",
            data: [60, 20, 90, 80, 50, 85, 40],
            borderDash: [15, 5],
            backgroundColor: "rgba(128, 222, 234, 0.2)",
            borderColor: "#80deea",
            pointBorderColor: "#80deea",
            pointBackgroundColor: "#00bcd4",
            pointHighlightFill: "#80deea",
            pointHoverBackgroundColor: "#80deea",
            borderWidth: 2,
            pointBorderWidth: 2,
            pointHoverBorderWidth: 4,
            pointRadius: 4
         }
      ]
   };

   var revenueLineChart;
   setInterval(function() {
      // Get a random index point
      var indexToUpdate = Math.round(Math.random() * (revenueLineChartData.labels.length - 1));
      if (typeof revenueLineChart != "undefined") {
         // Update one of the points in the second dataset
         if (revenueLineChartData.datasets[0].data[indexToUpdate]) {
            revenueLineChartData.datasets[0].data[indexToUpdate] = Math.round(Math.random() * 100);
         }
         if (revenueLineChartData.datasets[1].data[indexToUpdate]) {
            revenueLineChartData.datasets[1].data[indexToUpdate] = Math.round(Math.random() * 100);
         }
         revenueLineChart.update();
      }
   }, 2000);

   var revenueLineChartConfig = {
      type: "line",
      options: revenueLineChartOptions,
      data: revenueLineChartData
   };

   /*
Doughnut Chart Widget
*/

   var doughnutSalesChartCTX = $("#doughnut-chart");
   var browserStatsChartOptions = {
      cutoutPercentage: 70,
      legend: {
         display: false
      }
   };

   var doughnutSalesChartData = {
      labels: ["Confirmed", "Others", "No Case"],
      datasets: [
         {
            label: "Calls",
            data: [3000, 500, 1000],
            backgroundColor: ["#F7464A", "#46BFBD", "#FDB45C"]
         }
      ]
   };

   var doughnutSalesChartConfig = {
      type: "doughnut",
      options: browserStatsChartOptions,
      data: doughnutSalesChartData
   };

   /*
Monthly Revenue : Trending Bar Chart
*/

   var monthlyRevenueChartCTX = $("#trending-bar-chart");
   var monthlyRevenueChartOptions = {
      responsive: true,
      // maintainAspectRatio: false,
      legend: {
         display: false
      },
      hover: {
         mode: "label"
      },
      scales: {
         xAxes: [
            {
               display: true,
               gridLines: {
                  display: false
               }
            }
         ],
         yAxes: [
            {
               display: true,
               fontColor: "#fff",
               gridLines: {
                  display: false
               },
               ticks: {
                  beginAtZero: true
               }
            }
         ]
      },
      tooltips: {
         titleFontSize: 0,
         callbacks: {
            label: function(tooltipItem, data) {
               return tooltipItem.yLabel;
            }
         }
      }
   };
   var monthlyRevenueChartData = {
      labels: ["Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sept"],
      datasets: [
         {
            label: "Calls",
            data: [6, 9, 8, 4, 6, 7, 9, 4, 8],
            backgroundColor: "#46BFBD",
            hoverBackgroundColor: "#009688"
         }
      ]
   };

   var nReloads1 = 0;
   var min1 = 1;
   var max1 = 10;
   var monthlyRevenueChart;
   function updateMonthlyRevenueChart() {
      if (typeof monthlyRevenueChart != "undefined") {
         nReloads1++;
         var x = Math.floor(Math.random() * (max1 - min1 + 1)) + min1;
         monthlyRevenueChartData.datasets[0].data.shift();
         monthlyRevenueChartData.datasets[0].data.push([x]);
         monthlyRevenueChart.update();
      }
   }
   setInterval(updateMonthlyRevenueChart, 2000);

   var monthlyRevenueChartConfig = {
      type: "bar",
      options: monthlyRevenueChartOptions,
      data: monthlyRevenueChartData
   };

   /*
Trending Bar Chart
*/

   var browserStatsChartCTX = $("#trending-radar-chart");

   var browserStatsChartOptions = {
      responsive: true,
      // maintainAspectRatio: false,
      legend: {
         display: false
      },
      hover: {
         mode: "label"
      },
      scale: {
         angleLines: { color: "rgba(255,255,255,0.4)" },
         gridLines: { color: "rgba(255,255,255,0.2)" },
         ticks: {
            display: false
         },
         pointLabels: {
            fontColor: "#fff"
         }
      }
   };

   var browserStatsChartData = {
      labels: ["Tuberculosis", "Others", "Bronchitis", "Pneumonia", "Asthma"],
      datasets: [
         {
            label: "Diseases",
            data: [5, 6, 7, 8, 6],
            fillColor: "rgba(255,255,255,0.2)",
            borderColor: "#fff",
            pointBorderColor: "#fff",
            pointBackgroundColor: "#00bfa5",
            pointHighlightFill: "#fff",
            pointHoverBackgroundColor: "#fff",
            borderWidth: 2,
            pointBorderWidth: 2,
            pointHoverBorderWidth: 4
         }
      ]
   };

   var nReloads2 = 0;
   var min2 = 1;
   var max2 = 10;
   var browserStatsChart;
   function browserStatsChartUpdate() {
      if (typeof browserStatsChart != "undefined") {
         nReloads2++;
         var x = Math.floor(Math.random() * (max2 - min2 + 1)) + min2;
         browserStatsChartData.datasets[0].data.pop();
         browserStatsChartData.datasets[0].data.push([x]);
         browserStatsChart.update();
      }
   }
   setInterval(browserStatsChartUpdate, 2000);

   var browserStatsChartConfig = {
      type: "radar",
      options: browserStatsChartOptions,
      data: browserStatsChartData
   };

   /*
   Revenue by country - Line Chart
*/

   var countryRevenueChartCTX = $("#line-chart");

   var countryRevenueChartOption = {
      responsive: true,
      // maintainAspectRatio: false,
      legend: {
         display: false
      },
      hover: {
         mode: "label"
      },
      scales: {
         xAxes: [
            {
               display: true,
               gridLines: {
                  display: false
               },
               ticks: {
                  fontColor: "#fff"
               }
            }
         ],
         yAxes: [
            {
               display: true,
               fontColor: "#fff",
               gridLines: {
                  display: false
               },
               ticks: {
                  beginAtZero: false,
                  fontColor: "#fff"
               }
            }
         ]
      }
   };

   var countryRevenueChartData = {
      labels: ["NBO", "MBS", "NKR", "ELD", "MER", "KIL"],
      datasets: [
         {
            label: "Cases",
            data: [65, 45, 50, 30, 63, 45],
            fill: false,
            lineTension: 0,
            borderColor: "#fff",
            pointBorderColor: "#fff",
            pointBackgroundColor: "#009688",
            pointHighlightFill: "#fff",
            pointHoverBackgroundColor: "#fff",
            borderWidth: 2,
            pointBorderWidth: 2,
            pointHoverBorderWidth: 4,
            pointRadius: 4
         }
      ]
   };
   var countryRevenueChartConfig = {
      type: "line",
      options: countryRevenueChartOption,
      data: countryRevenueChartData
   };



   // Create the chart
   window.onload = function() {
      revenueLineChart = new Chart(revenueLineChartCTX, revenueLineChartConfig);
      monthlyRevenueChart = new Chart(monthlyRevenueChartCTX, monthlyRevenueChartConfig);
      var doughnutSalesChart = new Chart(doughnutSalesChartCTX, doughnutSalesChartConfig);
      browserStatsChart = new Chart(browserStatsChartCTX, browserStatsChartConfig);
      var countryRevenueChart = new Chart(countryRevenueChartCTX, countryRevenueChartConfig);
   };

   $(function() {
        jq('a.call-redirect').click(function(){
            var uuid = "&uuid=" + jq(this).data('uuid');
            if (jq(this).data('uuid') == "")
                uuid = "";
                
            window.location.href = "/contacts/registration?src=1&phone=" + jq(this).data('phone') + uuid;
        });

        setInterval(function(){
            //outgoing call
            var field = jq('p.current-call');
            var res = field.text().split(":");

            var hh = res[0];
            var mm = eval(res[1]);
            var ss = eval(res[2])+1;

            if (ss == 60){
                ss = 0;
                mm = eval(mm+1);
            }

            field.text(hh+':'+(mm.toString().length == 1 ? '0' + mm.toString() : mm)+':'+(ss.toString().length == 1 ? '0' + ss.toString() : ss));

            //Call waiting
            field = jq('p.call-wait span');
            res = field.text().split(":");

            hh = res[0];
            mm = eval(res[1]);
            ss = eval(res[2])+1;

            if (ss == 60){
                ss = 0;
                mm = eval(mm+1);
            }

            field.text(hh+':'+(mm.toString().length == 1 ? '0' + mm.toString() : mm)+':'+(ss.toString().length == 1 ? '0' + ss.toString() : ss));

        }, 1000);
   });
})(window, document, jQuery);
