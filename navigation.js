// navigation.js
document.addEventListener('DOMContentLoaded', () => {
    const content = document.getElementById('content');
    const searchButton = document.getElementById('searchButton');
    const searchInput = document.getElementById('searchInput');
    const regionLinks = document.querySelectorAll('.region-link'); // �������� ��� ������ ��������

    // ���������� ������� ��� ��������
    if (homeLink) {
        homeLink.addEventListener('click', (event) => {
            event.preventDefault();
            window.location.href = 'index.html'; // �������� �� URL ����� ��������� ��������
        });
    }

    // ���������� ������� ��� ������ ���������������������� �� �������
    regionLinks.forEach(link => {
        link.addEventListener('click', async (event) => {
            event.preventDefault(); // ������������� ������� �� ������
            const region = link.getAttribute('data-region'); // �������� �������� �������

            try {
                // ��������������� �� results.html � ���������� �������
                window.location.href = `results.html?region=${encodeURIComponent(region)}`;
            } catch (error) {
                console.error('������ ��� ������������ ������ �� �������:', error);
                content.innerHTML = `<p style="color: red;">������: ${error.message}</p>`;
            }
        });
    });

    // ���������� ������� ��� ������ ���������������������� �� ������
    searchButton.addEventListener('click', async () => {
        const query = searchInput.value;
        console.log('����� � ��������:', query);
        try {
            window.location.href = `results.html?query=${encodeURIComponent(query)}`;
        } catch (error) {
            console.error('������ ��� ��������� ����������������������:', error);
            content.innerHTML = `<p style="color: red;">������: ${error.message}</p>`;
        }
    });
});
