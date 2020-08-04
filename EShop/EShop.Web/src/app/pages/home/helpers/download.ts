// https://github.com/ag-grid/ag-grid/blob/066683a1cd707906435b03178178522020bfa2a9/community-modules/csv-export/src/csvExport/downloader.ts#L5
export default function download(filename: string, content: string) {
  const element = document.createElement('a');
  const url = window.URL.createObjectURL(packageJsonFile(content));
  element.setAttribute('href', url);
  element.setAttribute('download', filename);
  element.style.display = 'none';
  document.body.appendChild(element);

  element.dispatchEvent(
    new MouseEvent('click', {
      bubbles: false,
      cancelable: true,
      view: window,
    })
  );

  document.body.removeChild(element);

  window.setTimeout(() => {
    window.URL.revokeObjectURL(url);
  }, 0);
}

const packageJsonFile = (data: string): Blob => {
  return new Blob(['\ufeff', data], {
    type: 'application/json',
  });
};
