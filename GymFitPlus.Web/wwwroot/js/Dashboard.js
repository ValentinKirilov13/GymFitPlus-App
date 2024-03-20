const ctx = document.getElementById('myChart');

const data = {
    labels: [
        'Red',
        'Blue',
        'Yellow'
    ],
    datasets: [{
        label: 'My First Dataset',
        data: [300, 50, 100],
        backgroundColor: [
            'rgb(255, 99, 132)',
            'rgb(54, 162, 235)',
            'rgb(255, 205, 86)'
        ],
        hoverOffset: 4
    }]
};

const plugin = {
    id: 'customCanvasBackgroundColor',
    beforeDraw: (chart, args, options) => {
        const { ctx } = chart;
        ctx.save();
        ctx.globalCompositeOperation = 'destination-over';
        ctx.fillStyle = options.color || '#99ffff';
        ctx.fillRect(0, 0, chart.width, chart.height);
        ctx.restore();
    }
};



//
const cfg = {
    type: 'line',
    data: {
        datasets: [{
            label: 'Kg',
            data: [{ x: '2016-12-25', y: 20 },
            { x: '2016-12-26', y: 10 },
            { x: '2016-12-27', y: 15 },
            { x: '2016-12-28', y: 17 },
            { x: '2016-12-29', y: 28 },
            { x: '2016-12-30', y: 16.5 },
            { x: '2016-12-31', y: 32 }],

            borderWidth: 1,
            borderColor: '#ff206e'
        }]
    },
    options: {
        responsive: true,
        plugins: {
            title: {
                display: true,
                text: 'Custom Chart Title'
            },
            legend: {
                labels: {
                    // This more specific font property overrides the global property
                    font: {
                        size: 14,
                        weigt: 'bold',
                    }
                }
            }
        },
        scales: {
            y: {
                ticks: {
                    callback: function (value, index, ticks) {
                        return value + ' kg';
                    },
                    color: '#ff206e',
                },
            },
            x: {
                ticks: {
                    color: '#ff206e',
                },

            }
        }
    }
}




const options = {
    plugins: {
        colors: {
            enabled: false
        }
    }
};


const data2 = {
    labels: ['A', 'B', 'C'],
    datasets: [
        {
            label: 'Dataset 1',
            data: [1, 2, 3],
            borderColor: '#36A2EB',
            backgroundColor: '#9BD0F5',
        },
        {
            label: 'Dataset 2',
            data: [2, 3, 4],
            borderColor: '#FF6384',
            backgroundColor: '#FFB1C1',
            font: '#ff206e'
        }
    ]
};


new Chart(ctx, cfg);