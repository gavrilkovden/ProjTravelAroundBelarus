// navigation.js
document.addEventListener('DOMContentLoaded', () => {
    const content = document.getElementById('content');
    const searchButton = document.getElementById('searchButton');
    const searchInput = document.getElementById('searchInput');
    const regionLinks = document.querySelectorAll('.region-link'); // Получаем все ссылки регионов

    // Обработчик события для логотипа
    if (homeLink) {
        homeLink.addEventListener('click', (event) => {
            event.preventDefault();
            window.location.href = 'index.html'; // Замените на URL вашей стартовой страницы
        });
    }

    // Обработчик события для поиска достопримечательностей по региону
    regionLinks.forEach(link => {
        link.addEventListener('click', async (event) => {
            event.preventDefault(); // Предотвращаем переход по ссылке
            const region = link.getAttribute('data-region'); // Получаем значение региона

            try {
                // Перенаправление на results.html с параметром региона
                window.location.href = `results.html?region=${encodeURIComponent(region)}`;
            } catch (error) {
                console.error('Ошибка при формировании ссылки по региону:', error);
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
});
