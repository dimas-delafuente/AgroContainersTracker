function saveAsFile(filename, bytesBase64) {
  var link = document.createElement("a");
  link.download = filename + ".pdf";
  link.href = "data:application/octect-stream;base64," + bytesBase64;
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);
}

function printFile(byteBase64) {
    printJS({ printable: byteBase64, type: 'pdf', base64: true });
}
