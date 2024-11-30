/*script.js*/
document.addEventListener('DOMContentLoaded', () => {
    const createModal = document.getElementById('createModal');
    const createButton = document.getElementById('createButton');
    const closeModal = document.querySelector('#createModal .close');
    const createForm = document.getElementById('createForm');
    const imageFileInput = document.getElementById('imageFile');
    const workSchedulesContainer = document.getElementById('workSchedulesContainer');
    const slider = document.querySelector('.slider');
    const images = document.querySelectorAll('.Slider_UsersPhotos');
    let currentIndex = 0;
    const totalImages = images.length;
    const token = getCookie('token');

    function getCookie(name) {
        const value = "; " + document.cookie;
        const parts = value.split("; " + name + "=");
        if (parts.length === 2) return parts.pop().split(";").shift();
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
                city: getCityName(formData.get('city')) || '',
                region: formData.get('region') || '',
            },
            geoLocation: {
                latitude: parseFloat(formData.get('latitude')),
                longitude: parseFloat(formData.get('longitude')),
            },
        };

        function getCityName(city) {
            if (!city) return ''; // Если города нет, возвращаем пустую строку
            if (city === 'Вся область') return 'Вся область';
            const parts = city.split(' и '); // Разделяем строку на части
            return parts[0]; // Возвращаем только первую часть (название города)
        }

        // Собираем расписание работы
        const workSchedules = Array.from(workSchedulesContainer.querySelectorAll('.work-schedule')).map(scheduleDiv => {
            const day = scheduleDiv.getAttribute('data-day');
            const openTime = scheduleDiv.querySelector('input[name*="openTime"]').value;
            const closeTime = scheduleDiv.querySelector('input[name*="closeTime"]').value;

            if (openTime && closeTime) {
                return {
                    dayOfWeek: day,
                    openTime: formatTime(openTime),
                    closeTime: formatTime(closeTime)
                };
            }
            return null;
        }).filter(schedule => schedule !== null);

        newAttraction.workSchedules = workSchedules.length > 0 ? workSchedules : null;

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
                    'Authorization': `Bearer ${token}`
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

            // Загружаем изображения после создания достопримечательности
            await uploadImages(attractionId);

           //  Если файл изображения был выбран, загружаем его
            //if (imageFileInput.files.length > 0) {
            //    const imageFormData = new FormData();
            //    imageFormData.append('Image', imageFileInput.files[0]);
            //    imageFormData.append('AttractionId', attractionId);
            //    imageFormData.append('IsCover', 'true'); // Подставляем true, если это обложка

            //    try {
            //        console.log('Отправка изображения:', imageFormData.get('file'));
            //        console.log('AttractionId:', imageFormData.get('AttractionId'));
            //        console.log('IsCover:', imageFormData.get('IsCover'));
            //        const uploadResponse = await fetch('https://localhost:7125/Api/Attractions/Image', {
            //            method: 'POST',
            //            headers: {
            //                'Authorization': `Bearer ${token}`
            //            },
            //            body: imageFormData
            //        });

            //        if (!uploadResponse.ok) {
            //            const errorText = await uploadResponse.text();
            //            throw new Error(`HTTP ошибка! Статус: ${uploadResponse.status}, детали: ${errorText}`);
            //        }

            //        console.log('Изображение успешно загружено');

            //    } catch (error) {
            //        console.error('Ошибка при загрузке изображения:', error);
            //        alert('Ошибка при загрузке изображения: ' + error.message);
            //    }
            //}


            alert('Достопримечательность успешно добавлена!');
            createModal.style.display = 'none';
        } catch (error) {
            console.error('Ошибка при добавлении достопримечательности и/или загрузке изображения:', error);
            alert('Ошибка: ' + error.message);
        }
    });

    // Функция для загрузки нескольких изображений
    async function uploadImages(attractionId) {
        const imageFiles = document.getElementById('imageFiles').files;

        if (imageFiles.length === 0) {
            alert('Пожалуйста, выберите хотя бы одно изображение.');
            return;
        }

        const token = getCookie('token');

        for (const file of imageFiles) {
            const imageFormData = new FormData();
            imageFormData.append('Image', file);
            imageFormData.append('AttractionId', attractionId);
            imageFormData.append('IsCover', 'false'); // Устанавливаем, если это не обложка

            try {
                console.log('Отправка изображения:', file.name);
                const uploadResponse = await fetch('https://localhost:7125/Api/Attractions/Image', {
                    method: 'POST',
                    headers: {
                        'Authorization': `Bearer ${token}`
                    },
                    body: imageFormData
                });

                if (!uploadResponse.ok) {
                    const errorText = await uploadResponse.text();
                    throw new Error(`HTTP ошибка! Статус: ${uploadResponse.status}, детали: ${errorText}`);
                }

                console.log(`Изображение ${file.name} успешно загружено`);
            } catch (error) {
                console.error(`Ошибка при загрузке изображения ${file.name}:`, error);
                alert(`Ошибка при загрузке изображения ${file.name}: ` + error.message);
            }
        }

        alert('Все изображения успешно загружены!');
    }

  //   Функция для форматирования времени в формате "hh:mm:ss"
    function formatTime(time) {
        if (!time) return null;
        const [hours, minutes] = time.split(':');
        return `${hours.padStart(2, '0')}:${minutes.padStart(2, '0')}:00`;
    }

    function GetDayOfWeek(day) {
        const days = {
            "Monday": "Понедельник",
            "Tuesday": "Вторник",
            "Wednesday": "Среда",
            "Thursday": "Четверг",
            "Friday": "Пятница",
            "Saturday": "Суббота",
            "Sunday": "Воскресенье"
        };
        return days[day] || day; // Возвращаем день на русском или само значение, если оно не найдено
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



    //Функционал для работы с районами областей

    const regionCities = {
        "Брестская": [
            "",
            "Брест и Брестский район",
            "Барановичи и Барановичский район",
            "Берёза и Берёзовский район",
            "Ганцевичи и Ганцевичский район",
            "Дрогичин и Дрогичинский район",
            "Жабинка и Жабинковский район",
            "Иваново и Ивановский район",
            "Ивацевичи и Ивацевичский район",
            "Каменец и Каменецкий район",
            "Кобрин и Кобринский район",
            "Лунинец и Лунинецкий район",
            "Ляховичи и Ляховичский район",
            "Малорита и Малоритский район",
            "Пинск и Пинский район",
            "Пружаны и Пружанский район",
            "Столин и Столинский район"
        ],
        "Гродненская": [
            "",
            "Гродно и Гродненский район",
            "Берестовица и Берестовицкий район",
            "Волковыск и Волковысский район",
            "Вороново и Вороновский район",
            "Дятлово и Дятловский район",
            "Зельва и Зельвенский район",
            "Ивье и Ивьевский район",
            "Кореличи и Кореличский район",
            "Лида и Лидский район",
            "Мосты и Мостовский район",
            "Новогрудок и Новогрудский район",
            "Ошмяны и Ошмянский район",
            "Островец и Островецкий район",
            "Свислочь и Свислочский район",
            "Слоним и Слонимский район",
            "Сморгонь и Сморгонский район",
            "Щучин и Щучинский район"
        ],
        "Гомельская": [
            "",
            "Гомель и Гомельский район",
            "Брагин и Брагинский район",
            "Буда-Кошелёво и Буда-Кошелевский район",
            "Ветка и Ветковский район",
            "Добруш и Добрушский район",
            "Ельск и Ельский район",
            "Житковичи и Житковичский район",
            "Жлобин и Жлобинский район",
            "Калинковичи и Калинковичский район",
            "Корма и Кормянский район",
            "Лельчицы и Лельчицкий район",
            "Лоев и Лоевский район",
            "Мозырь и Мозырский район",
            "Наровля и Наровлянский район",
            "Октябрьский и Октябрьский район",
            "Петриков и Петриковский район",
            "Речица и Речицкий район",
            "Рогачёв и Рогачёвский район",
            "Светлогорск и Светлогорский район",
            "Хойники и Хойникский район",
            "Чечерск и Чечерский район"
        ],
        "Витебская": [
            "",
            "Витебск и Витебский район",
            "Бешенковичи и Бешенковичский район",
            "Браслав и Браславский район",
            "Верхнедвинск и Верхнедвинский район",
            "Глубокое и Глубокский район",
            "Городок и Городокский район",
            "Докшицы и Докшицкий район",
            "Дубровно и Дубровенский район",
            "Лепель и Лепельский район",
            "Лиозно и Лиозненский район",
            "Миоры и Миорский район",
            "Орша и Оршанский район",
            "Полоцк и Полоцкий район",
            "Поставы и Поставский район",
            "Россоны и Россонский район",
            "Сенно и Сенненский район",
            "Толочин и Толочинский район",
            "Ушачи и Ушачский район",
            "Чашники и Чашникский район",
            "Шарковщина и Шарковщинский район",
            "Шумилино и Шумилинский район"
        ],
        "Минская": [
            "",
            "Минск и Минский район",
            "Березино и Березинский район",
            "Борисов и Борисовский район",
            "Вилейка и Вилейский район",
            "Воложин и Воложинский район",
            "Дзержинск и Дзержинский район",
            "Клецк и Клецкий район",
            "Копыль и Копыльский район",
            "Крупки и Крупский район",
            "Логойск и Логойский район",
            "Любань и Любанский район",
            "Молодечно и Молодечненский район",
            "Мядель и Мядельский район",
            "Несвиж и Несвижский район",
            "Пуховичи и Пуховичский район",
            "Слуцк и Слуцкий район",
            "Смолевичи и Смолевичский район",
            "Солигорск и Солигорский район",
            "Старые Дороги и Стародорожский район",
            "Столбцы и Столбцовский район",
            "Узда и Узденский район",
            "Червень и Червенский район"
        ],
        "Могилевская": [
            "",
            "Могилёв и Могилёвский район",
            "Белыничы и Белыничский район",
            "Бобруйск и Бобруйский район",
            "Быхов и Быховский район",
            "Глуск и Глусский район",
            "Горки и Горецкий район",
            "Дрибин и Дрибинский район",
            "Кировск и Кировский район",
            "Климовичи и Климовичский район",
            "Кличев и Кличевский район",
            "Краснополье и Краснопольский район",
            "Кричев и Кричевский район",
            "Круглое и Круглянский район",
            "Костюковичи и Костюковичский район",
            "Мстиславль и Мстиславский район",
            "Осиповичи и Осиповичский район",
            "Славгород и Славгородский район",
            "Хотимск и Хотимский район",
            "Чаусы и Чаусский район",
            "Чериков и Чериковский район",
            "Шклов и Шкловский район"
        ]
    };

    // Элементы <select>
    const regionSelect = document.getElementById("region");
    const citySelect = document.getElementById("city");
    // Добавляем placeholder в начало списка городов
    const placeholderOption = document.createElement('option');
    placeholderOption.value = '';
    placeholderOption.disabled = true;
    placeholderOption.selected = true;
    placeholderOption.textContent = 'Сначала выберите область';
    citySelect.appendChild(placeholderOption);

    // Обработчик изменения региона
    regionSelect.addEventListener("change", function () {
        const selectedRegion = regionSelect.value;

        // Очистка предыдущего списка городов
        citySelect.innerHTML = "";

        if (regionCities[selectedRegion]) {
            // Активируем select с городами
            citySelect.disabled = false;


            // Добавляем города, соответствующие выбранному региону
            regionCities[selectedRegion].forEach((city) => {
                const option = document.createElement("option");
                option.value = city;
                option.textContent = city === "" ? "Выберите район" : city;
                citySelect.appendChild(option);
            });
        } else {
            // Если регион не выбран или нет соответствующих городов
            citySelect.disabled = true;
            const defaultOption = document.createElement("option");
            defaultOption.value = "";
            defaultOption.textContent = "Сначала выберите область";
            citySelect.appendChild(defaultOption);
        }
    });
});
