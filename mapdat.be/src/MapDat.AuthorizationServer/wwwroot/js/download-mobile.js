
document.getElementById("download").addEventListener("click", download);

function download() {  
    var url = document.getElementById("download").value;
    //console.log(url);

    fetchAsync(url)
        .then(async response => ({
            filename: fnGetFileNameFromContentDispostionHeader(response.headers.get('content-disposition')),
            blob: await response.blob()
        }))
        .then(blob => openDownloadWindow(blob))
        .catch(reason => console.log(reason.message));
}

async function fetchAsync(url) {
    let response = await fetch(url, { mode: 'cors' });
    let data = await response;
    return data;
}

function openDownloadWindow(response) {
    const blob = new Blob([response.blob]);
    var url = window.URL.createObjectURL(blob);
    var a = document.createElement('a');
    //console.log(blob)
    a.href = url;
    a.download = response.filename;
    document.body.appendChild(a);
    a.click();
    a.remove(); 
}

let fnGetFileNameFromContentDispostionHeader = function (header) {
    let contentDispostion = header.split(';');
    const fileNameToken = `filename*=UTF-8''`;

    let fileName = 'itm_service.apk';
    for (let thisValue of contentDispostion) {
        if (thisValue.trim().indexOf(fileNameToken) === 0) {
            fileName = decodeURIComponent(thisValue.trim().replace(fileNameToken, ''));
            break;
        }
    }

    return fileName;
};