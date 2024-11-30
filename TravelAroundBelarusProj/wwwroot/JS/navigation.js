// navigation.js
document.addEventListener('DOMContentLoaded', () => {
    const content = document.getElementById('content');
    const searchButton = document.getElementById('searchButton');
    const searchInput = document.getElementById('searchInput');
    const regionLinks = document.querySelectorAll('.region-link'); // Получаем все ссылки регионов
    const cityLinks = document.querySelectorAll('.city-link');
    const loginButton = document.getElementById('loginButton'); // Найдем кнопку Войти
    const token = getCookie('token');

    // Обработчик события для логотипа
    if (homeLink) {
        homeLink.addEventListener('click', (event) => {
            event.preventDefault();
           // window.location.reload();
            window.location.href = 'index.html'; // Замените на URL вашей стартовой страницы
        });
    }

    // Обработчик события для поиска достопримечательностей по региону
    regionLinks.forEach(link => {
        link.addEventListener('click', async (event) => {
            event.preventDefault(); // Предотвращаем переход по ссылке
            const region = link.getAttribute('region'); // Получаем значение региона
            
            try {
                // Перенаправление на results.html с параметром региона
                window.location.href = `results.html?region=${encodeURIComponent(region)}`;
            } catch (error) {
                console.error('Ошибка при формировании ссылки по региону:', error);
                content.innerHTML = `<p style="color: red;">Ошибка: ${error.message}</p>`;
            }
        });
    });

    // Обработчик события для поиска достопримечательностей по городу
    cityLinks.forEach(link => {
        link.addEventListener('click', async (event) => {
            event.preventDefault(); // Предотвращаем переход по ссылке
            const city = link.getAttribute('city'); // Получаем значение региона

            try {
                // Перенаправление на results.html с параметром региона
                window.location.href = `results.html?city=${encodeURIComponent(city)}`;
            } catch (error) {
                console.error('Ошибка при формировании ссылки по city:', error);
                content.innerHTML = `<p style="color: red;">Ошибка: ${error.message}</p>`;
            }
        });
    });


    // Обработчик события для поиска достопримечательностей по тексту
    searchButton.addEventListener('click', async () => {
        const query = searchInput.value;
        console.log('Поиск с запросом:', query);
        try {
            window.location.href = `results.html?query=${encodeURIComponent(query)}`;
        } catch (error) {
            console.error('Ошибка при получении достопримечательностей:', error);
            content.innerHTML = `<p style="color: red;">Ошибка: ${error.message}</p>`;
        }
    });

        // Логика для кнопки "Войти/Выйти"
    if (token) {
        loginButton.textContent = 'Выйти'; // Если токен существует, меняем текст на "Выйти"
        loginButton.addEventListener('click', handleLogout); // Привязываем обработчик выхода
    } else {
        loginButton.textContent = 'Войти'; // Если токена нет, кнопка остается "Войти"
        loginButton.addEventListener('click', () => {
            window.location.replace('https://localhost:7098/HTML/Login.html'); // Переход на страницу входа
        });
    }

    function getCookie(name) {
        const value = "; " + document.cookie;
        const parts = value.split("; " + name + "=");
        if (parts.length === 2) return parts.pop().split(";").shift();
    }

    function handleLogout(event) {
        event.preventDefault();

        // Удаляем токен из куки, установив пустое значение и прошедшую дату истечения
        document.cookie = "token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/; SameSite=None; Secure";

        window.location.replace('https://localhost:7098/HTML/Login.html');// Переход на страницу входа после выхода
    }
});
