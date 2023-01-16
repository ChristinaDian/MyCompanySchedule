/* globals Chart:false, feather:false */

(function () {
  'use strict'

  feather.replace({ 'aria-hidden': 'true' })

  // Graphs
  var ctx = document.getElementById('myChart')
  // eslint-disable-next-line no-unused-vars
  var myChart = new Chart(ctx, {
    type: 'line',
    data: {
      labels: [
        'January',
        'February',
        'Mart',
        'April',
        'May',
        'June',
        'July',
        'August',
        'September',
        'October',
        'November',
        'December'
      ],
        datasets: [{
        label: "Working hours",
        data: [
          339,
          345,
          483,
          443,
          489,
          492,
          464,
          478,
          498,
          456,
          466,
          402
        ],
        lineTension: 0,
        backgroundColor: 'transparent',
          borderColor: '#0d6efd',
        borderWidth: 4,
          pointBackgroundColor: '#0d6efd'
        }, {
            label: "Working hours-2021",
            data: [
                370,
                378,
                390,
                400,
                428,
                456,
                490,
                490,
                478,
                470,
                466,
                340
            ],
            lineTension: 0,
            backgroundColor: 'transparent',
            borderColor: '#ffea28',
            borderWidth: 4,
            pointBackgroundColor: '#ffea28'
            }
        ]
    },
    options: {
      scales: {
        yAxes: [{
          ticks: {
            beginAtZero: false
          }
        }]
      },
      legend: {
        display: false
      }
    }
  })
})()
