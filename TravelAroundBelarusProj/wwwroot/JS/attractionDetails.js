// attractionDetails.js

document.addEventListener('DOMContentLoaded', async () => {
    const content = document.getElementById('attraction-details');
    const token = getCookie('token'); // Функция getCookie используется для извлечения куки

    // Получение ID из URL
    const urlParams = new URLSearchParams(window.location.search);
    const attractionId = urlParams.get('id');
    console.log('Отображение достопримечательности вначале:', attractionId);

    if (!attractionId) {
        content.innerHTML = '<p style="color: red;">Ошибка: ID достопримечательности не указан</p>';
        return;
    }

    try {
        const response = await fetch(`https://localhost:7125/Api/Attractions/${attractionId}`, {
            headers: {
                'Authorization': `Bearer ${token}`,
                'Accept': 'application/json'
            }
        });

        console.log('Отображение достопримечательности после запроса:', attractionId);

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`HTTP error! статус: ${response.status}, детали: ${errorText}`);
        }

        const attraction = await response.json();
        console.log('Данные достопримечательности от сервера:', attraction);

        displayAttractions(attraction);
    } catch (error) {
        console.error('Ошибка при получении достопримечательностей:', error);
        content.innerHTML = `<p style="color: red;">Ошибка: ${error.message}</p>`;
    }

    // Функция отображения полученных достопримечательностей
    function displayAttractions(attraction) {
        console.log('Отображение достопримечательности:', attraction);
/*        content.innerHTML = '';*/
        const div = document.createElement('div');
/*        div.classList.add('attraction');*/

        const name = attraction.name || 'Название не предоставлено';
        const description = attraction.description || 'Описание не предоставлено';
        const price = attraction.price !== undefined ? attraction.price : 'Цена не указана';
        const averageRating = (attraction.averageRating !== null && attraction.averageRating !== undefined) ? attraction.averageRating : 'У этого места еще нет оценок';
        const address = attraction.address ? `${attraction.address.street} ${attraction.address.city} ${attraction.address.region} область` : 'Адрес не указан';
        const coordinates = (attraction.geoLocation && attraction.geoLocation.latitude !== null && attraction.geoLocation.longitude !== null)
            ? `${attraction.geoLocation.latitude}, ${attraction.geoLocation.longitude}`
            : 'Координаты не указаны';
        const createdDate = attraction.createdDate ? new Date(attraction.createdDate).toLocaleDateString() : 'Дата не указана';
        const workSchedules = attraction.workSchedules && attraction.workSchedules.length > 0
            ? attraction.workSchedules.map(schedule =>
                `<p>${GetDayOfWeek(schedule.dayOfWeek)}: ${schedule.openTime} - ${schedule.closeTime}</p>`
            ).join('')
            : 'Рабочее расписание не указано';

        // Логирование путей
        console.log('Путь к изображению (raw):', attraction.imagePath);
        console.log('Путь для тега img:', attraction.imagePath ? `https://localhost:7125/${attraction.imagePath.replace(/\\/g, '/')}` : '');

       // const imagePath = attraction.imagePath ? `<img src="https://localhost:7125/${attraction.imagePath.replace(/\\/g, '/')}" alt="${name}" style="max-width: 100%; height: auto;">` : '';
        const imagePath = attraction.images && attraction.images.length > 0
            ? attraction.images.map(image =>
                `<img src="https://localhost:7125/${image.imagePath.replace(/\\/g, '/')}" alt="${name}" style="max-width: 100%; height: auto; margin: 5px;">`
            ).join('') // Преобразуем массив в строку
            : '<p style="color: gray;">Изображения отсутствуют</p>';

        div.innerHTML = `
                <h2>${name}</h2>
                    <div class="image-gallery">
                    ${imagePath}
                    </div>
                <p><strong>Описание:</strong> ${description}</p>
                <p><strong>Стоимость посещения:</strong> ${price} руб.</p>
                <p><strong>Рейтинг:</strong> ${averageRating} </p>
                <p><strong>Адрес:</strong> ${address}</p>
                <p><strong>Координаты:</strong> ${coordinates}</p>
                <p><strong>Дата создания:</strong> ${createdDate}</p>
                <div><strong>Рабочее расписание:</strong> ${workSchedules}</div>

                <div class="rating-section" id="rating-section-${attraction.id}" data-attraction-id="${attraction.id}" style="display: flex; align-items: center;">
                <p style="margin-right: 10px;"><strong>Оцените достопримечательность:</strong></p>
                <span class="star" data-rating="1">&#9734;</span>
                <span class="star" data-rating="2">&#9734;</span>
                <span class="star" data-rating="3">&#9734;</span>
                <span class="star" data-rating="4">&#9734;</span>
                <span class="star" data-rating="5">&#9734;</span>
            </div>

    <div class="comment-section" style="margin-top: 15px;">
    <textarea id="comment-${attraction.id}" placeholder="Оставьте комментарий"></textarea>
    <button class="submit-feedback" data-id="${attraction.id}">Отправить</button>
        </div>

               <p> <button class="comment-button" data-id="${attraction.id}"> Посмотреть все комментарии</button></p>
                <div class="comments-section" id="comments-${attraction.id}" style="display: none;">
                    <p>Загрузка комментариев...</p>
                </div>
            `;

        content.appendChild(div);
    }

    // Добавляем функционал для выбора звезд
    document.querySelectorAll('.rating-section').forEach(section => {
        section.addEventListener('click', async (event) => {
            if (event.target.classList.contains('star')) {
                const rating = event.target.getAttribute('data-rating');
                const attractionId = section.getAttribute('data-attraction-id'); // Получаем ID достопримечательности

                if (!attractionId) {
                    console.error('Attraction ID is missing');
                    section.innerHTML = `<p style="color: red;">Ошибка при отправке оценки. Попробуйте позже.</p>`;
                    return;
                }

                // Обновляем звезды
                const stars = section.querySelectorAll('.star');
                stars.forEach(star => {
                    if (star.getAttribute('data-rating') <= rating) {
                        star.innerHTML = '&#9733;'; // Закрашенная звезда (желтая)
                    } else {
                        star.innerHTML = '&#9734;'; // Пустая звезда
                    }
                });

                // Отправка рейтинга на сервер
                try {
                    const response = await fetch('https://localhost:7125/Api/Attractions/AttractionFeedback', {
                        method: 'POST',
                        headers: {
                            'Authorization': `Bearer ${token}`,
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({
                            valueRating: parseInt(rating),
                            attractionId: parseInt(attractionId),
                            comment: null
                        })
                    });

                    if (!response.ok) {
                        const errorMessage = await response.text();
                        throw new Error(`Ошибка HTTP! статус: ${response.status}, детали: ${errorMessage}`);
                    }

                    // Если запрос успешный
                    section.innerHTML = `<p>Вы оценили достопримечательность на ${rating}.</p>`;
                } catch (error) {
                    console.error('Ошибка при отправке оценки:', error);
                    section.innerHTML = `<p style="color: red;">Ошибка при отправке оценки: ${error.message}</p>`;
                }
            }
        });
    });


    // Добавляем обработчик для кнопок "Комментарии"
    document.querySelectorAll('.comment-button').forEach(button => {
        button.addEventListener('click', async (event) => {
            const attractionId = event.target.getAttribute('data-id');
            const commentsSection = document.getElementById(`comments-${attractionId}`);

            // Показываем/скрываем комментарии при повторном нажатии
            if (commentsSection.style.display === 'none') {
                commentsSection.style.display = 'block';
                await loadComments(attractionId, commentsSection);
            } else {
                commentsSection.style.display = 'none';
            }
        });
    });

    // Обработчик для отправки комментария
    document.querySelectorAll('.submit-feedback').forEach(button => {
        button.addEventListener('click', async (event) => {
            const attractionId = event.target.getAttribute('data-id');
            const commentText = document.getElementById(`comment-${attractionId}`).value;

            if (!commentText) {
                alert('Пожалуйста, введите комментарий.');
                return;
            }

            try {
                const response = await fetch('https://localhost:7125/Api/Attractions/AttractionFeedback', {
                    method: 'POST',
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        attractionId: parseInt(attractionId),
                        comment: commentText,
                        valueRating: null // Если нужно отправить только комментарий без рейтинга
                    })
                });

                if (!response.ok) {
                    const errorText = await response.text();
                    throw new Error(`HTTP error! статус: ${response.status}, детали: ${errorText}`);
                }

                // Если запрос успешен, обновим комментарии
                const commentsSection = document.getElementById(`comments-${attractionId}`);

                commentsSection.innerHTML = '<p>Загрузка комментариев...</p>';
                await loadComments(attractionId, commentsSection); // Перезагружаем комментарии

                // Очищаем поле ввода комментариев и добавляем сообщение об успешной отправке
                const commentField = document.getElementById(`comment-${attractionId}`);
                commentField.value = '';
                commentField.placeholder = 'Комментарий успешно добавлен!';


                // Очищаем сообщение через 3 секунды
                setTimeout(() => {
                    commentField.placeholder = 'Оставьте комментарий';
                }, 3000);

            } catch (error) {
                console.error('Ошибка при отправке комментария:', error);
                alert(`Ошибка при отправке комментария: ${error.message}`);
            }
        });
    });
    
});

// Функция для получения куки по имени
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
    return null;
}

function parseJwt(token) {
    try {
        const base64Url = token.split('.')[1];
        const base64 = decodeURIComponent(atob(base64Url).split('').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));

        return JSON.parse(base64);
    } catch (error) {
        console.error('Ошибка при разборе токена:', error);
        return null;
    }
}

// Функция для загрузки комментариев
async function loadComments(attractionId, commentsSection) {
    const token = getCookie('token');
    try {
        const response = await fetch(`https://localhost:7125/Api/Attractions/AttractionFeedbacks?AttractionId=${attractionId}`, {
            headers: {
                'Authorization': `Bearer ${token}`,
                'Accept': 'application/json'
            }
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`HTTP error! статус: ${response.status}, детали: ${errorText}`);
        }

        const comments = await response.json();

        if (!commentsSection) {
            console.error('Элемент комментариев не найден');
            return;
        }

        if (!Array.isArray(comments)) {
            commentsSection.innerHTML = `<p style="color: red;">Неверный формат ответа</p>`;
            return;
        }

        const ratingSection = document.getElementById(`rating-section-${attractionId}`);


        if (ratingSection) {
            commentsSection.innerHTML = comments
                .filter(comment => comment.comment) // Фильтруем только те, у которых есть текст комментария
                .map(comment => `
            <div class="comment">
                <p>Пользователь: ${comment.userId}</p>
                <p>${comment.comment}</p>
            </div>
        `).join('');
        }

    } catch (error) {
        console.error('Ошибка при получении комментариев:', error);
        commentsSection.innerHTML = `<p style="color: red;">Ошибка: ${error.message}</p>`;
    }
}
