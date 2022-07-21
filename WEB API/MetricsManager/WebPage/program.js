const $wrapper = document.querySelector(".wrapper");
const $header = document.querySelector("#header");
const $metrics = document.querySelector(".metrics");
const $footer = document.querySelector("#footer");
const $networkMetricsBox = document.querySelector("#network");
const $clrMetricsBox = document.querySelector("#clr");
const $hddMetricsBox = document.querySelector("#hdd");
const $ramMetricsBox = document.querySelector("#ram");
const $cpuMetricsBox = document.querySelector("#cpu");
const $getMetricsForm = document.querySelector(".getMetricsForm");
const $fromDate = document.querySelector(".fromDate");
const $toDate = document.querySelector(".toDate");
const $agentId = document.querySelector(".agentId");
const $submitButton = document.querySelector(".submitButton");
const $resetButton = document.querySelector(".resetButton");

let ramXhr = new XMLHttpRequest();
let clrXhr = new XMLHttpRequest();
let cpuXhr = new XMLHttpRequest();
let networkXhr = new XMLHttpRequest();
let hddXhr = new XMLHttpRequest();

let CLR;
let CPU;
let RAM;
let HDD
let Network;

let ramDates;
let ramValues;

let cpuDates;
let cpuValues;

let hddDates;
let hddValues;

let clrDates;
let clrValues;

let networkDates;
let networkValues;

let count = 0;

initPage();

function initPage()
{
  let $heading = document.createElement("h1");

  $heading.innerText = "Metrics charts";

  $heading.classList.add("heading");

  $header.append($heading);

  let $license = document.createElement("h3");

  $license.innerHTML = "All rights reserved 2022 &#169;";

  $license.classList.add(".license");

  $footer.append($license);

  initMetrics();

  $fromDate.setAttribute("placeholder", "Write here 'from' date");

  $toDate.setAttribute("placeholder", "Write here 'to' date");

  $agentId.setAttribute("placeholder", "Write here agent id or leave this field empty");
}


function initMetrics()
{
  let labels = [
    'January',
    'February',
    'March',
    'April',
    'May',
    'June',
  ];

  let networkData = {
    labels: labels,
    datasets: [{
      label: 'Network download speed KB',
      backgroundColor: 'rgb(255, 99, 132)',
      borderColor: 'rgb(255, 99, 132)',
      data: [0, 0, 0, 0, 0, 0, 0],
    }]
  };

  let networkConfig = {
    type: 'line',
    data: networkData,
    options: {}
  };

  Network = new Chart(
    $networkMetricsBox,
    networkConfig
  );

  let ramData = {
    labels: labels,
    datasets: [{
      label: 'Available RAM MB',
      backgroundColor: 'rgb(255, 99, 132)',
      borderColor: 'rgb(255, 99, 132)',
      data: [0, 0, 0, 0, 0, 0, 0],
    }]
  };

  let ramConfig = {
    type: 'line',
    data: ramData,
    options: {}
  };

  RAM = new Chart(
    $ramMetricsBox,
    ramConfig
  );

  let cpuData = {
    labels: labels,
    datasets: [{
      label: 'CPU Usage %',
      backgroundColor: 'rgb(255, 99, 132)',
      borderColor: 'rgb(255, 99, 132)',
      data: [0, 0, 0, 0, 0, 0, 0],
    }]
  };

  let cpuConfig = {
    type: 'line',
    data: cpuData,
    options: {}
  };

  CPU = new Chart(
    $cpuMetricsBox,
    cpuConfig
  );

  let hddData = {
    labels: labels,
    datasets: [{
      label: 'HDD free memory MB',
      backgroundColor: 'rgb(255, 99, 132)',
      borderColor: 'rgb(255, 99, 132)',
      data: [0, 0, 0, 0, 0, 0, 0],
    }]
  };

  let hddConfig = {
    type: 'line',
    data: hddData,
    options: {}
  };

  HDD = new Chart(
    $hddMetricsBox,
    hddConfig
  );

  let clrData = {
    labels: labels,
    datasets: [{
      label: 'Heap allocated bytes',
      backgroundColor: 'rgb(255, 99, 132)',
      borderColor: 'rgb(255, 99, 132)',
      data: [0, 0, 0, 0, 0, 0, 0],
    }]
  };

  let clrConfig = {
    type: 'line',
    data: clrData,
    options: {}
  };

  CLR = new Chart(
    $clrMetricsBox,
    clrConfig
  );
}

$resetButton.addEventListener("click", ResetFormFields);

function ResetFormFields()
{
  $fromDate.value = "";
  $toDate.value = "";
  $agentId.value = "";
}
$submitButton.addEventListener("click", SendGetRequest);
function SendGetRequest()
{
  let fromdate = $fromDate.value;
  let todate = $toDate.value;
  let id = $agentId.value;
  RAM.destroy();
  CPU.destroy();
  CLR.destroy();
  HDD.destroy();
  Network.destroy();

  if (todate == "" && fromdate == "" && id == "")
  {
    let url = "http://localhost:7139/metrics/manage/ram/cluster/from/01.01.1990 00:00:00/to/01.01.2030 00:00:00";
    ramXhr.open("GET", url);
    ramXhr.send();
    url = "http://localhost:7139/metrics/manage/network/cluster/from/01.01.1990 00:00:00/to/01.01.2030 00:00:00";
    networkXhr.open("GET", url);
    networkXhr.send();
    url = "http://localhost:7139/metrics/manage/clr/cluster/from/01.01.1990 00:00:00/to/01.01.2030 00:00:00";
    clrXhr.open("GET", url);
    clrXhr.send();
    url = "http://localhost:7139/metrics/manage/hdd/cluster/from/01.01.1990 00:00:00/to/01.01.2030 00:00:00";
    hddXhr.open("GET", url);
    hddXhr.send();
    url = "http://localhost:7139/metrics/manage/cpu/cluster/from/01.01.1990 00:00:00/to/01.01.2030 00:00:00";
    cpuXhr.open("GET", url);
    cpuXhr.send();
  } else if (todate == "" && fromdate == "")
  {
    let url = "http://localhost:7139/metrics/manage/ram/agent/" + id + "/from/01.01.1990 00:00:00/to/01.01.2030 00:00:00";
    ramXhr.open("GET", url);
    ramXhr.send();
    url = "http://localhost:7139/metrics/manage/network/agent/" + id + "/from/01.01.1990 00:00:00/to/01.01.2030 00:00:00";
    networkXhr.open("GET", url);
    networkXhr.send();
    url = "http://localhost:7139/metrics/manage/clr/agent/" + id + "/from/01.01.1990 00:00:00/to/01.01.2030 00:00:00";
    clrXhr.open("GET", url);
    clrXhr.send();
    url = "http://localhost:7139/metrics/manage/hdd/agent/" + id + "/from/01.01.1990 00:00:00/to/01.01.2030 00:00:00";
    hddXhr.open("GET", url);
    hddXhr.send();
    url = "http://localhost:7139/metrics/manage/cpu/agent/" + id + "/from/01.01.1990 00:00:00/to/01.01.2030 00:00:00";
    cpuXhr.open("GET", url);
    cpuXhr.send();
  }
  else if (id == "")
  {
    let url = "http://localhost:7139/metrics/manage/ram/cluster/from/" + fromdate + "/to/" + todate;
    ramXhr.open("GET", url);
    ramXhr.send();
    url = "http://localhost:7139/metrics/manage/network/cluster/from/" + fromdate + "/to/" + todate;
    networkXhr.open("GET", url);
    networkXhr.send();
    url = "http://localhost:7139/metrics/manage/clr/cluster/from/" + fromdate + "/to/" + todate;
    clrXhr.open("GET", url);
    clrXhr.send();
    url = "http://localhost:7139/metrics/manage/hdd/cluster/from/" + fromdate + "/to/" + todate;
    hddXhr.open("GET", url);
    hddXhr.send();
    url = "http://localhost:7139/metrics/manage/cpu/cluster/from/" + fromdate + "/to/" + todate;
    cpuXhr.open("GET", url);
    cpuXhr.send();
  }
  else
  {
    let url = "http://localhost:7139/metrics/manage/ram/agent/" + id + "/from/" + fromdate + "/to/" + todate;
    ramXhr.open("GET", url);
    ramXhr.send();
    url = "http://localhost:7139/metrics/manage/network/agent/" + id + "/from/" + fromdate + "/to/" + todate;
    networkXhr.open("GET", url);
    networkXhr.send();
    url = "http://localhost:7139/metrics/manage/clr/agent/" + id + "/from/" + fromdate + "/to/" + todate;
    clrXhr.open("GET", url);
    clrXhr.send();
    url = "http://localhost:7139/metrics/manage/hdd/agent/" + id + "/from/" + fromdate + "/to/" + todate;
    hddXhr.open("GET", url);
    hddXhr.send();
    url = "http://localhost:7139/metrics/manage/cpu/agent/" + id + "/from/" + fromdate + "/to/" + todate;
    cpuXhr.open("GET", url);
    cpuXhr.send();
  }


}

ramXhr.addEventListener("readystatechange", function ()
{
  if (this.readyState === 4)
  {
    console.log(ramXhr.response);
  }

  let metrics = JSON.parse(ramXhr.response);

  let values = [];

  let dates = [];

  metrics.forEach(element =>
  {
    values.push(element.value);
    dates.push(element.time);
  });

  console.log(values);
  console.log(dates);

  let data = {
    labels: dates,
    datasets: [{
      label: "Available RAM MB",
      backgroundColor: 'rgb(255, 99, 132)',
      borderColor: 'rgb(255, 99, 132)',
      data: values,
    }]
  };

  let config = {
    type: 'line',
    data: data,
    options: {}
  };


  RAM = new Chart($ramMetricsBox, config);
});

cpuXhr.addEventListener("readystatechange", function ()
{
  if (this.readyState === 4)
  {
    console.log(cpuXhr.response);
  }

  let metrics = JSON.parse(cpuXhr.response);

  let values = [];

  let dates = [];

  metrics.forEach(element =>
  {
    values.push(element.value);
    dates.push(element.time);
  });

  console.log(values);
  console.log(dates);

  let data = {
    labels: dates,
    datasets: [{
      label: "CPU Usage %",
      backgroundColor: 'rgb(255, 99, 132)',
      borderColor: 'rgb(255, 99, 132)',
      data: values,
    }]
  };

  let config = {
    type: 'line',
    data: data,
    options: {}
  };

  CPU = new Chart($cpuMetricsBox, config);
});

hddXhr.addEventListener("readystatechange", function ()
{
  if (this.readyState === 4)
  {
    console.log(hddXhr.response);
  }

  let metrics = JSON.parse(hddXhr.response);

  let values = [];

  let dates = [];

  metrics.forEach(element =>
  {
    values.push(element.value);
    dates.push(element.time);
  });

  console.log(values);
  console.log(dates);

  let data = {
    labels: dates,
    datasets: [{
      label: "HDD free memory MB",
      backgroundColor: 'rgb(255, 99, 132)',
      borderColor: 'rgb(255, 99, 132)',
      data: values,
    }]
  };

  let config = {
    type: 'line',
    data: data,
    options: {}
  };

  HDD = new Chart($hddMetricsBox, config);
});

networkXhr.addEventListener("readystatechange", function ()
{
  if (this.readyState === 4)
  {
    console.log(networkXhr.response);
  }

  let metrics = JSON.parse(networkXhr.response);

  let values = [];

  let dates = [];

  metrics.forEach(element =>
  {
    values.push(element.value);
    dates.push(element.time);
  });

  console.log(values);
  console.log(dates);

  let data = {
    labels: dates,
    datasets: [{
      label: "Network download speed KB",
      backgroundColor: 'rgb(255, 99, 132)',
      borderColor: 'rgb(255, 99, 132)',
      data: values,
    }]
  };

  let config = {
    type: 'line',
    data: data,
    options: {}
  };

  Network = new Chart($networkMetricsBox, config);
});

clrXhr.addEventListener("readystatechange", function ()
{
  if (this.readyState === 4)
  {
    console.log(clrXhr.response);
  }

  let metrics = JSON.parse(clrXhr.response);

  let values = [];

  let dates = [];

  metrics.forEach(element =>
  {
    values.push(element.value);
    dates.push(element.time);
  });

  console.log(values);
  console.log(dates);

  let data = {
    labels: dates,
    datasets: [{
      label: "Heap allocated bytes'",
      backgroundColor: 'rgb(255, 99, 132)',
      borderColor: 'rgb(255, 99, 132)',
      data: values,
    }]
  };

  let config = {
    type: 'line',
    data: data,
    options: {}
  };

  CLR = new Chart($clrMetricsBox, config);
});