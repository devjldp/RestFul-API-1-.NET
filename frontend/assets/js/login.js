document.getElementById('loginForm').addEventListener('submit', async function(event) {
    event.preventDefault();

    const userName = document.getElementById('loginUserName').value;
    const password = document.getElementById('loginPassword').value;

    const loginData = {
        UserName: userName,
        Password: password
    };

    try {
        const response = await fetch('http://localhost:5025/auth/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(loginData)
        });

        const result = await response.json();
        if (response.ok) {
            localStorage.setItem('jwtToken', result.Token); // Store JWT token
            window.location.href = "employees.html"; // Redirect to employees page
        } else {
            alert('Authentication error: ' + result.message);
        }
    } catch (error) {
        alert('Error: ' + error.message);
    }
});