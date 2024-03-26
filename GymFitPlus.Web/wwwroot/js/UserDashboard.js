class Point {
    constructor(x, y) {
        this.x = x;
        this.y = y;
    }
}

const ctx = document.getElementById('myChart');

let myListOfData = [];
let myLabel = '';
let myTitle = '';
let myDimensions = '';

let cfg = {
    type: 'line',
    data: {
        datasets: [{
            label: myLabel,
            data: myListOfData,
            borderWidth: 3,
            borderColor: '#ff206e'
        }]
    },
    options: {
        responsive: true,
        plugins: {
            title: {
                display: true,
                text: myTitle,
                font: {
                    size: 20,
                    weigt: 'bold',
                    color: '#ff206e'
                }
            }
        },
        scales: {
            y: {
                ticks: {
                    callback: function (value, index, ticks) {
                        return value + ' ' + myDimensions;
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

let myChart = new Chart(ctx, cfg);

function getStats(statsTypeParam) {
    $.ajax({
        url: "/Statistic/GetStats",
        type: "GET",
        data: { statsType: encodeURIComponent(statsTypeParam) },
        dataType: "json",
        success: function (result) {
            myListOfData.length = 0;

            let propertyName = capitalizeFirstLetter(statsTypeParam);        

            result.forEach(function (res) {

                let date = (res.dateOfМeasurements).substring(0, 10);

                myListOfData.push(new Point(date, res[propertyName]));
            });

            console.log(myListOfData);           

            if (propertyName === 'weight') {

                myLabel = 'Kg';
                myTitle = 'Weight';
                myDimensions = 'kg';

            } else if (propertyName === 'height') {

                myLabel = 'Meters';
                myTitle = "Height";
                myDimensions = 'm';

            } else {

                myLabel = 'Centimeter';
                myTitle = statsTypeParam;
                myDimensions = 'cm';
            }


            if (typeof myChart !== 'undefined' && myChart.data.datasets.length > 0) {
                myChart.destroy();
            }

            cfg = {
                type: 'line',
                data: {
                    datasets: [{
                        label: myLabel,
                        data: myListOfData,
                        borderWidth: 3,
                        borderColor: '#ff206e'
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        title: {
                            display: true,
                            text: myTitle,
                            font: {
                                size: 20,
                                weigt: 'bold',
                                color: '#ff206e'
                            }
                        }
                    },
                    scales: {
                        y: {
                            ticks: {
                                callback: function (value, index, ticks) {
                                    return value + ' ' + myDimensions;
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

            myChart = new Chart(ctx, cfg);
            
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    getStats('Weight');
});

function capitalizeFirstLetter(str) {
    return str.charAt(0).toLowerCase() + str.slice(1);
}

