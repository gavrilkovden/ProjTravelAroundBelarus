/*script.js*/
document.addEventListener('DOMContentLoaded', () => {
    const createModal = document.getElementById('createModal');
    const createButton = document.getElementById('createButton');
    const closeModal = document.querySelector('#createModal .close');
    const createForm = document.getElementById('createForm');
    const workSchedulesContainer = document.getElementById('workSchedules');
    const addScheduleButton = document.getElementById('addSchedule');
    const imageFileInput = document.getElementById('imageFile');
   // const token = 'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjM1ZjM3MzQwLWY5ZTUtNDExOC1iOTQ5LTA4ZGM1MWNjNTdiNyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzQ5MzIxMTk2LCJpc3MiOiJUcmF2ZWxBcm91bmRCZWxhcnVzIiwiYXVkIjoiVHJhdmVsQXJvdW5kQmVsYXJ1cyJ9.0Q-klqtQBZY-HFaD1UzOLPQRqiFnKEDmS3kQG9Y0z60'; // Подставьте свой токен

    let scheduleIndex = 1;

    const slider = document.querySelector('.slider');
    const images = document.querySelectorAll('.Slider_UsersPhotos');
    let currentIndex = 0;
    const totalImages = images.length;

    // Логика для кнопки "Войти/Выйти"
    const loginButton = document.getElementById('loginButton'); // Найдем кнопку Войти
//    const token = localStorage.getItem('token'); // Получим токен из localStorage (если он есть)
    const token = getCookie('token');

    if (token) {
        loginButton.textContent = 'Выйти'; // Если токен существует, меняем текст на "Выйти"
        loginButton.addEventListener('click', handleLogout); // Привязываем обработчик выхода
    } else {
        loginButton.textContent = 'Войти'; // Если токена нет, кнопка остается "Войти"
        loginButton.addEventListener('click', () => {
            window.location.href = 'Login.html'; // Переход на страницу входа
        });
    }

    function getCookie(name) {
        const value = "; " + document.cookie;
        const parts = value.split("; " + name + "=");
        if (parts.length === 2) return parts.pop().split(";").shift();
    }

    // Обработчик выхода из системы
    function handleLogout(event) {
        event.preventDefault();
        localStorage.removeItem('token'); // Удаляем токен
        window.location.href = 'login.html'; // Переход на страницу входа после выхода
    }

    // Открытие модального окна для создания достопримечательности
    createButton.addEventListener('click', () => {
        createModal.style.display = 'block';
    });

    // Закрытие модального окна при нажатии на кнопку "закрыть"
    closeModal.addEventListener('click', () => {
        createModal.style.display = 'none';
    });

    // Закрытие модального окна при клике вне его области
    window.addEventListener('click', (event) => {
        if (event.target === createModal) {
            createModal.style.display = 'none';
        }
    });

    // Добавление нового расписания работы
    addScheduleButton.addEventListener('click', () => {
        const scheduleDiv = document.createElement('div');
        scheduleDiv.classList.add('work-schedule');
        scheduleDiv.innerHTML = `
            <label for="workDay${scheduleIndex}">День недели:</label>
            <select id="workDay${scheduleIndex}" name="workSchedules[${scheduleIndex}].dayOfWeek">
                <option value="Sunday">Воскресенье</option>
                <option value="Monday">Понедельник</option>
                <option value="Tuesday">Вторник</option>
                <option value="Wednesday">Среда</option>
                <option value="Thursday">Четверг</option>
                <option value="Friday">Пятница</option>
                <option value="Saturday">Суббота</option>
            </select>
            <label for="openTime${scheduleIndex}">Время открытия:</label>
            <input type="time" id="openTime${scheduleIndex}" name="workSchedules[${scheduleIndex}].openTime">
            <label for="closeTime${scheduleIndex}">Время закрытия:</label>
            <input type="time" id="closeTime${scheduleIndex}" name="workSchedules[${scheduleIndex}].closeTime">
        `;
        workSchedulesContainer.appendChild(scheduleDiv);
        scheduleIndex++;
    });

    // Обработка формы создания достопримечательности
    createForm.addEventListener('submit', async (event) => {
        event.preventDefault();

        const formData = new FormData(createForm);

        // Проверка обязательных полей
        if (!formData.get('name') || !formData.get('description') || !formData.get('price') || !formData.get('region')) {
            alert('Пожалуйста, заполните все обязательные поля: название, описание и регион.');
            return;
        }

        // Подготовка объекта для отправки на сервер
        const newAttraction = {
            name: formData.get('name'),
            description: formData.get('description'),
            price: parseFloat(formData.get('price')),
            /* numberOfVisitors: parseInt(formData.get('numberOfVisitors'), 10),*/
            address: {
                street: formData.get('street') || '',
                city: formData.get('city') || '',
                region: formData.get('region') || '',
            },
            geoLocation: {
                latitude: parseFloat(formData.get('latitude')),
                longitude: parseFloat(formData.get('longitude')),
            },
        };

        // Собираем расписание работы из формы, если оно указано
        const workSchedules = Array.from(formData.entries())
            .filter(([key]) => key.startsWith('workSchedules'))
            .reduce((acc, [key, value]) => {
                const index = parseInt(key.match(/\d+/)[0], 10);
                const field = key.split('.')[1];
                if (!acc[index]) acc[index] = {};
                acc[index][field] = value;
                return acc;
            }, []);

        // Преобразование времени и дней недели только если расписание полностью заполнено
        const filledWorkSchedules = workSchedules.filter(schedule =>
            schedule.openTime && schedule.closeTime && schedule.dayOfWeek
        );

        // Преобразование времени и дней недели только если расписание заполнено
        if (filledWorkSchedules.length > 0) {
            newAttraction.workSchedules = workSchedules.map(schedule => ({
                ...schedule,
                openTime: formatTime(schedule.openTime),
                closeTime: formatTime(schedule.closeTime),
                dayOfWeek: convertDayOfWeek(schedule.dayOfWeek)
            }));
        } else {
            newAttraction.workSchedules = null;
        }

        try {
            // Отправка запроса на создание новой достопримечательности
            const response = await fetch('https://localhost:7125/Api/Attractions', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': token
                },
                body: JSON.stringify(newAttraction)
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`HTTP ошибка! Статус: ${response.status}, детали: ${errorText}`);
            }

            const addedAttraction = await response.json();
            console.log('Добавленная достопримечательность:', addedAttraction);

            const attractionId = addedAttraction.id;

            // Если файл изображения был выбран, загружаем его
            if (imageFileInput.files.length > 0) {
                const imageFormData = new FormData();
                imageFormData.append('Image', imageFileInput.files[0]);
                imageFormData.append('AttractionId', attractionId);
                imageFormData.append('IsCover', 'true'); // Подставляем true, если это обложка

                try {
                    console.log('Отправка изображения:', imageFormData.get('file'));
                    console.log('AttractionId:', imageFormData.get('AttractionId'));
                    console.log('IsCover:', imageFormData.get('IsCover'));
                    const uploadResponse = await fetch('https://localhost:7125/Api/Attractions/Image', {
                        method: 'POST',
                        headers: {
                            'Authorization': token
                        },
                        body: imageFormData
                    });

                    if (!uploadResponse.ok) {
                        const errorText = await uploadResponse.text();
                        throw new Error(`HTTP ошибка! Статус: ${uploadResponse.status}, детали: ${errorText}`);
                    }

                    console.log('Изображение успешно загружено');

                } catch (error) {
                    console.error('Ошибка при загрузке изображения:', error);
                    alert('Ошибка при загрузке изображения: ' + error.message);
                }
            }

            alert('Достопримечательность успешно добавлена!');
            createModal.style.display = 'none';
        } catch (error) {
            console.error('Ошибка при добавлении достопримечательности и/или загрузке изображения:', error);
            alert('Ошибка: ' + error.message);
        }
    });

    // Функция для форматирования времени в формате "hh:mm:ss"
    function formatTime(time) {
        if (!time) return null;
        const [hours, minutes] = time.split(':');
        return `${hours.padStart(2, '0')}:${minutes.padStart(2, '0')}:00`;
    }

    // Функция для конвертации дня недели в английский формат
    function convertDayOfWeek(day) {
        const days = {
            "Понедельник": "Monday",
            "Вторник": "Tuesday",
            "Среда": "Wednesday",
            "Четверг": "Thursday",
            "Пятница": "Friday",
            "Суббота": "Saturday",
            "Воскресенье": "Sunday"
        };
        return days[day] || day;
    }

    // Функция для показа следующего изображения
    function showNextImage() {
        currentIndex++;

        if (currentIndex < totalImages) {
            // Если не на последнем изображении, плавно перемещаемся к следующему
            slider.style.transition = 'transform 0.5s ease';
            slider.style.transform = `translateX(-${currentIndex * 100}%)`;
        }

        if (currentIndex === totalImages - 1) {
            // Когда достигаем дубликата первого изображения
            setTimeout(() => {
                // Переход к настоящему первому изображению без анимации
                slider.style.transition = 'none';
                currentIndex = 0;
                slider.style.transform = 'translateX(0)';
            }, 500); // Ожидание завершения анимации (500мс)
        }
    }
    // Запуск интервала для автоматической смены изображений
    setInterval(showNextImage, 3000); // Меняем изображение каждые 3 секунды

});
