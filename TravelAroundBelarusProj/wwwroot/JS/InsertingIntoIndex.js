// InsertingIntoIndex.js

document.addEventListener('DOMContentLoaded', async () => {
    const content = document.getElementById('content');
    const attractionIds = [32]; // Укажите здесь нужные id достопримечательностей

    // Создаем параметры запроса с несколькими значениями Ids
    const urlParams = new URLSearchParams();
    attractionIds.forEach(id => urlParams.append('Ids', id));

    try {
        const response = await fetch(`https://localhost:7125/Api/Attractions?${urlParams.toString()}`, {
            headers: {
                'Accept': 'application/json'
            }
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`HTTP error! статус: ${response.status}, детали: ${errorText}`);
        }

        const attractions = await response.json();
        displayAttractions(attractions);
    } catch (error) {
        console.error('Ошибка при получении достопримечательностей:', error);
        content.innerHTML = `<p style="color: red;">Ошибка: ${error.message}</p>`;
    }

    // Функция отображения полученных достопримечательностей
    function displayAttractions(attractions) {
        console.log('Отображение достопримечательностей:', attractions);
        content.innerHTML = '';
        if (!Array.isArray(attractions)) {
            content.innerHTML = `<p style="color: red;">Неверный формат ответа</p>`;
            return;
        }

        attractions.forEach(attraction => {
            const div = document.createElement('div');
            div.classList.add('attraction');

            const name = attraction.name || 'Название не предоставлено';
            const description = attraction.description || 'Описание не предоставлено';
            const imagePath = attraction.imagePath ? `<img src="https://localhost:7125/${attraction.imagePath.replace(/\\/g, '/')}" alt="${name}" style="max-width: 100%; height: auto;">` : '';

            div.innerHTML = `
            <a href="/HTML/attractionDetails.html?id=${attraction.id}" class="attraction-link">
                <p><strong>${name}</strong></p>
                ${imagePath}
                <p>${description}</p>
            </a>
        `;


            content.appendChild(div);
        });
    }
});
