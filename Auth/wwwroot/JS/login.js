// login.js
function setCookie(name, value, days) {
    let expires = "";
    if (days) {
        const date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000)); // Количество дней, на которое устанавливается куки
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/; SameSite=None; Secure";
}

document.getElementById('login-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    // Получаем значения из полей ввода
    const login = document.querySelector('input[type="text"]').value;
    const password = document.querySelector('input[type="password"]').value;

    if (!login || !password) {
        alert('Please enter both login and password.');
        return;
    }

    try {
        // Отправляем запрос на сервер для аутентификации
        const response = await fetch('https://localhost:7098/auth/api/v1/LoginJwt', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
                },
            credentials: 'include', //Это укажет браузеру, что нужно сохранять и передавать кросс-доменные куки.
            body: JSON.stringify({ login, password })
        });


        // Обрабатываем ответ сервера
        if (response.ok) {
            const data = await response.json();
            console.log('Response JSON:', JSON.stringify(data, null, 2));

            if (data.jwtToken) {
                setCookie('token', data.jwtToken, 7); // Сохраняем jwtToken в куки
                console.log('Cookie after setting:', document.cookie); // Логируем состояние куки
                alert('Login successful!');
                window.location.replace('https://localhost:7125/HTML/index.html');
            }

        } else {
            alert('Login failed. Please check your login or password.');
        }
    } catch (error) {
        console.error('Error:', error);
        alert('An error occurred. Please try again later.');
    }
});



