class Point {
    constructor(x, y) {
        this.x = x;
        this.y = y;
    }
}
const ctx = document.getElementById('myChart');

const directionElement = document.getElementById('direction');
const changeValueElement = document.getElementById('value');
const dimensionElement = document.getElementById('dimension');

let direction = '';
let changeValue = '';
let color = '';

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
        maintainAspectRatio: false,
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

            changeValue = myListOfData[0].y - myListOfData[myListOfData.length - 1].y;

            if (changeValue < 0) {
                direction = 'Up:';
                color = '#449e48';
            }
            else if (changeValue > 0) {
                direction = 'Down:';
                color = '#ae0000';
            }
            else {
                direction = 'Same:';
                color = '#7c7c7c';
            }

            if (propertyName === 'weight') {

                myLabel = 'Kg';
                myTitle = 'Weight';
                myDimensions = 'kg';

                if (changeValue < 0) {
                    color = '#ae0000';
                }
                else if (changeValue > 0) {
                    color = '#449e48';
                }

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
                    maintainAspectRatio: false,
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
                                    return value.toFixed(1) + ' ' + myDimensions;
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

            directionElement.textContent = direction;
            changeValueElement.textContent = Math.abs(changeValue).toFixed(1);
            dimensionElement.textContent = myDimensions;

            directionElement.style.color = color;
            changeValueElement.style.color = color;
            dimensionElement.style.color = color;
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    getStats('Weight');
});

function capitalizeFirstLetter(str) {
    return str.charAt(0).toLowerCase() + str.slice(1);
}

