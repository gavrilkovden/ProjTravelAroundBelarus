/*results.js*/
document.addEventListener('DOMContentLoaded', async () => {
    const content = document.getElementById('content');
    const query = new URLSearchParams(window.location.search).get('query');
    const region = new URLSearchParams(window.location.search).get('region');
  //  const token = 'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjM1ZjM3MzQwLWY5ZTUtNDExOC1iOTQ5LTA4ZGM1MWNjNTdiNyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzQ5MzIxMTk2LCJpc3MiOiJUcmF2ZWxBcm91bmRCZWxhcnVzIiwiYXVkIjoiVHJhdmVsQXJvdW5kQmVsYXJ1cyJ9.0Q-klqtQBZY-HFaD1UzOLPQRqiFnKEDmS3kQG9Y0z60';
    const token = localStorage.getItem('token'); // Получим токен из localStorage (если он есть)
    // Обработчик события для логотипа
    if (homeLink) {
        homeLink.addEventListener('click', (event) => {
            event.preventDefault();
            window.location.href = 'index.html'; // 
        });
    }

    if (query) {
        // Логика поиска по тексту
        try {
            const response = await fetch(`https://localhost:7125/Api/Attractions?FreeText=${encodeURIComponent(query)}`, {
                headers: {
                    'Authorization': token,
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
    } else if (region) {
        // Логика поиска по региону
        if (region === 'Весь список') {
            // Запрос без параметров
            try {
                const response = await fetch(`https://localhost:7125/Api/Attractions`, {
                    headers: {
                        'Authorization': token,
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
                console.error('Ошибка при получении всех достопримечательностей:', error);
                content.innerHTML = `<p style="color: red;">Ошибка: ${error.message}</p>`;
            }
        } else {
            // Запрос по региону
            try {
                const response = await fetch(`https://localhost:7125/Api/Attractions?Region=${encodeURIComponent(region)}`, {
                    headers: {
                        'Authorization': token,
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
                console.error('Ошибка при получении достопримечательностей по региону:', error);
                content.innerHTML = `<p style="color: red;">Ошибка: ${error.message}</p>`;
            }
        }
    } else {
        content.innerHTML = '<p style="color: red;">Ошибка: отсутствует запрос.</p>';
    }
});



// Функция отображения полученных достопримечательностей
function displayAttractions(attractions) {
   // const token = 'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjM1ZjM3MzQwLWY5ZTUtNDExOC1iOTQ5LTA4ZGM1MWNjNTdiNyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzQ5MzIxMTk2LCJpc3MiOiJUcmF2ZWxBcm91bmRCZWxhcnVzIiwiYXVkIjoiVHJhdmVsQXJvdW5kQmVsYXJ1cyJ9.0Q-klqtQBZY-HFaD1UzOLPQRqiFnKEDmS3kQG9Y0z60';
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
        const price = attraction.price !== undefined ? attraction.price : 'Цена не указана';
        const averageRating = attraction.averageRating !== undefined ? attraction.averageRating : 'Рейтинг не указан';
        /*        const numberOfVisitors = attraction.numberOfVisitors !== undefined ? attraction.numberOfVisitors : 'Количество посетителей не указано';*/
        const address = attraction.address ? `${attraction.address.street}, ${attraction.address.city}, ${attraction.address.region}` : 'Адрес не указан';
        const coordinates = attraction.geoLocation ? `${attraction.geoLocation.latitude}, ${attraction.geoLocation.longitude}` : 'Координаты не указаны';
        const createdDate = attraction.createdDate ? new Date(attraction.createdDate).toLocaleDateString() : 'Дата не указана';
        /*        const isApproved = attraction.isApproved !== undefined ? attraction.isApproved : 'Статус утверждения не указан';*/
/*        const commentsSection = document.getElementById(`comments-${attractionId}`);*/

        const workSchedules = attraction.workSchedules && attraction.workSchedules.length > 0
            ? attraction.workSchedules.map(schedule =>
                `<p>${schedule.dayOfWeek}: ${schedule.openTime} - ${schedule.closeTime}</p>`
            ).join('')
            : 'Рабочее расписание не указано';

        const imagePath = attraction.imagePath ? `<img src="https://localhost:7125/${attraction.imagePath.replace(/\\/g, '/')}" alt="${name}" style="max-width: 100%; height: auto;">` : '';

        div.innerHTML = `
                <h2>${name}</h2>
                ${imagePath}
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

        //  loadComments(attraction.id, document.getElementById(`rating-section-${attraction.id}`));


        //commentsSection.innerHTML = '<p>Загрузка комментариев...</p>';
        //await loadComments(attractionId, commentsSection); // Перезагружаем комментарии

    });

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
                            'Authorization': 'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjM1ZjM3MzQwLWY5ZTUtNDExOC1iOTQ5LTA4ZGM1MWNjNTdiNyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzQ5MzIxMTk2LCJpc3MiOiJUcmF2ZWxBcm91bmRCZWxhcnVzIiwiYXVkIjoiVHJhdmVsQXJvdW5kQmVsYXJ1cyJ9.0Q-klqtQBZY-HFaD1UzOLPQRqiFnKEDmS3kQG9Y0z60',
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
                        'Authorization': 'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjM1ZjM3MzQwLWY5ZTUtNDExOC1iOTQ5LTA4ZGM1MWNjNTdiNyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzQ5MzIxMTk2LCJpc3MiOiJUcmF2ZWxBcm91bmRCZWxhcnVzIiwiYXVkIjoiVHJhdmVsQXJvdW5kQmVsYXJ1cyJ9.0Q-klqtQBZY-HFaD1UzOLPQRqiFnKEDmS3kQG9Y0z60',
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
/*                const commentsSection = document.getElementById(`comments-${attractionId}`);*/
      
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
    const token = 'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjM1ZjM3MzQwLWY5ZTUtNDExOC1iOTQ5LTA4ZGM1MWNjNTdiNyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzQ5MzIxMTk2LCJpc3MiOiJUcmF2ZWxBcm91bmRCZWxhcnVzIiwiYXVkIjoiVHJhdmVsQXJvdW5kQmVsYXJ1cyJ9.0Q-klqtQBZY-HFaD1UzOLPQRqiFnKEDmS3kQG9Y0z60'; // Подставьте свой токен
    try {
        const response = await fetch(`https://localhost:7125/Api/Attractions/AttractionFeedbacks?AttractionId=${attractionId}`, {
            headers: {
                'Authorization': token,
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

        // Извлекаем идентификатор текущего пользователя из токена
      //  const userId = parseJwt(token)["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

        // Проверяем, оставил ли текущий пользователь оценку
        // const userFeedback = comments.find(comment => comment.userId === userId);

        //if (userFeedback && userFeedback.valueRating) {
        //    if (ratingSection) {
        //        ratingSection.innerHTML = `<p>Вы оценили достопримечательность на ${userFeedback.valueRating}.</p>`;
        //    } else {
        //        console.error('Элемент для рейтинга не найден');
        //    }
        //}


    } catch (error) {
        console.error('Ошибка при получении комментариев:', error);
        commentsSection.innerHTML = `<p style="color: red;">Ошибка: ${error.message}</p>`;
    }
}