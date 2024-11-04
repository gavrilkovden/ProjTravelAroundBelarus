// register.js
document.getElementById('login-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    // Получаем значения из полей формы
    const login = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;

    // Проверяем, что пароль и его подтверждение совпадают
    if (password !== confirmPassword) {
        alert('Passwords do not match.');
        return;
    }

    try {
        // Отправляем запрос на сервер для регистрации
        const response = await fetch('https://localhost:7077/UM/api/v1/Users', { 
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ login, password })
        });

        // Обрабатываем ответ сервера
        if (response.ok) {
            alert('Registration successful! Please log in.');
            // Перенаправление на страницу входа
            window.location.replace('https://localhost:7098/HTML/Login.html');

        } else {
            const errorData = await response.json();
            alert(`Registration failed: ${errorData.message}`);
        }
    } catch (error) {
        console.error('Error:', error);
        alert('An error occurred. Please try again later.');
    }
});

