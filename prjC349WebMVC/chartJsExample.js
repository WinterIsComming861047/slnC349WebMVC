const xValuesByCategory = [
    '2022/ 5/ 1','2022/ 5/ 2','2022/ 5/ 3','2022/ 5/ 4','2022/ 5/ 5','2022/ 5/ 6','2022/ 5/ 7','2022/ 5/ 8','2022/ 5/ 9','2022/ 5/ 10',
    '2022/ 5/ 11','2022/ 5/ 12','2022/ 5/ 13','2022/ 5/ 14','2022/ 5/ 15','2022/ 5/ 16','2022/ 5/ 17','2022/ 5/ 18','2022/ 5/ 19','2022/ 5/ 20',
    '2022/ 5/ 21', '2022/ 5/ 22', '2022/ 5/ 23', '2022/ 5/ 24', '2022/ 5/ 25', '2022/ 5/ 26', '2022/ 5/ 27', '2022/ 5/ 28', '2022/ 5/ 29', '2022/ 5/ 30', '2022/ 5/ 31',];

const yValuesByCategory_0 = ['0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '0', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0',];
const yValuesByCategory_1 = ['0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0',];
const yValuesByCategory_2 = ['0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0',];
const yValuesByCategory_3 = ['0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0',];
const dataByCategory = {
    labels: xValuesByCategory, datasets: [
        { label: '鋼板', backgroundColor: 'rgb(255,235, 205)', borderColor: 'rgb(255,235, 205)', data: yValuesByCategory_0 },
        { label: '線材', backgroundColor: 'rgb(118, 238, 198)', borderColor: 'rgb(118, 238, 198)', data: yValuesByCategory_1 },
        { label: '熱軋', backgroundColor: 'rgb(255, 106, 106)', borderColor: 'rgb(255, 106, 106)', data: yValuesByCategory_2 },
        { label: '冷軋', backgroundColor: 'rgb(99, 184, 255)', borderColor: 'rgb(99, 184, 255)', data: yValuesByCategory_3 },]};
const configByCategory = { type: 'line', data: dataByCategory, options: { plugins: { colorschemes: { scheme: 'brewer.GnBu8' } } } };