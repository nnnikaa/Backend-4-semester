const API_URL = "/api/Currency/rates";

const currencySelect = document.getElementById("currencySelect");
const showRateBtn = document.getElementById("showRateBtn");

const resultBox = document.getElementById("resultBox");
const resultText = document.getElementById("resultText");

const statusDiv = document.getElementById("status");

function showStatus(message) {
    statusDiv.innerText = message;
}

function incrementCurrencyFetchCount() {
    let n = Number(sessionStorage.getItem('requestCount') ?? 0);
    sessionStorage.setItem('requestCount', ++n);
    document.getElementById('sessionCount').textContent = n;
}


// сохраняем выбор валюты в localStorage, но запрос НЕ отправляем
currencySelect.addEventListener("change", () => {
    const code = currencySelect.value;
    if (!code) return;

    localStorage.setItem("currency", code);
});

async function loadCookie()
{
    const renderHistory = (currencies) => {
        document.getElementById('currencyHistory').innerHTML = currencies.length > 0 ? currencies.map(c => `<li>${c}</li>`).join('') : '<li>—</li>';
        console.log(currencies);
    }
    try {
        const cookie = await fetch('/api/Currency/cookie').then(x => x.json());
        renderHistory(cookie.recentCurrencyFromSession ?? []);
        document.getElementById('currencySelected').innerHTML = cookie?.currencyFromCookie ?? '-';

    } catch {
        renderHistory([]);
        document.getElementById('currencySelected').textContent = '-';
    }
}

async function showRate() {
    const code = currencySelect.value;

    if (!code) {
        alert("Выберите валюту!");
        return;
    }

    showStatus("Получаю курс " + code + "...");

    try {
        const response = await fetch(`${API_URL}?code=${code}`, {
            credentials: "include"
        });

        const text = await response.text(); // читаем как текст

        console.log("RAW RESPONSE:", text);

        if (!response.ok) {
            showStatus("Ошибка backend: " + response.status);
            return;
        }

        const data = JSON.parse(text); // пробуем вручную

        resultText.innerHTML = `Курс <b>${data.code}</b> = <b>${data.rate}</b>`;
        resultBox.classList.remove("hidden");

        showStatus("Данные получены из API. Дата: " + data.date);
        loadCookie();
        incrementCurrencyFetchCount();

    } catch (error) {
        showStatus("Ошибка соединения или неверный JSON");
        console.error(error);
    }
}

//showRateBtn.addEventListener("click", showRate);
showRateBtn.addEventListener("click", (e) => {
    e.preventDefault();
    showRate();
});

// восстановление валюты при загрузке страницы



window.addEventListener("load", () => {
    const savedCurrency = localStorage.getItem("currency");
    if (savedCurrency) {
        currencySelect.value = savedCurrency;
    }
});

//const API_URL = "/api/currency/rates";

//const currencySelect = document.getElementById("currencySelect");
//const showRateBtn = document.getElementById("showRateBtn");

//const resultBox = document.getElementById("resultBox");
//const resultText = document.getElementById("resultText");

//const statusDiv = document.getElementById("status");

//let allRates = {};

//function showStatus(message) {
//    statusDiv.innerText = message;
//}

//async function loadRates() {
//    showStatus("Загрузка курсов валют...");

//    try {
//        const response = await fetch(API_URL);

//        if (!response.ok) {
//            showStatus("Ошибка backend: " + response.status);
//            return;
//        }

//        const data = await response.json();

//        allRates = data.rates;

//        showStatus("Курсы успешно загружены. Дата: " + data.date);
//    }
//    catch (error) {
//        showStatus("Ошибка соединения с backend или внешним API");
//        console.error(error);
//    }
//}


//currencySelect.addEventListener("change", async () => {
//    const code = currencySelect.value;

//    if (!code) return;

//    await fetch(`${API_URL}?code=${code}`, {
//        credentials: "include"
//    });

//    localStorage.setItem("currency", code);
//});



//async function showRate() {


//    const code = currencySelect.value;

//    if (!code) {
//        alert("Выберите валюту!");
//        return;
//    }

//    showStatus("Показываю курс " + code + "...");

//    const rate = allRates[code];

//    if (rate === undefined) {
//        resultText.innerHTML = `Нет данных по валюте <b>${code}</b>`;
//    } else {
//        resultText.innerHTML = `Курс <b>${code}</b> = <b>${rate}</b>`;
//    }

//    resultBox.classList.remove("hidden");
//    showStatus("Данные из кеша (последнее обновление)");
    
//}

//showRateBtn.addEventListener("click", showRate);

//loadRates();