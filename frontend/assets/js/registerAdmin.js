document.getElementById('registerForm').addEventListener('submit', async function(event) {
    event.preventDefault();

    const firstName = document.getElementById('firstName').value;
    const lastName = document.getElementById('lastName').value;
    const userName = document.getElementById('userName').value;
    const password = document.getElementById('password').value;
    const email = document.getElementById('email').value;

    const data = { 
        FirstName: firstName, 
        LastName: lastName, 
        UserName: userName, 
        PasswordHash: password, // Password will be hashed in backend
        Email: email
    };

    try {
        const response = await fetch('http://localhost:5025/auth/register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        });

        const result = await response.json();
        if (response.ok) {
            alert('Admin registered successfully');
            window.location.href = "index.html"; // Redirect to index page after registration
        } else {
            alert('Error registering admin: ' + result.message);
        }
    } catch (error) {
        alert('Error: ' + error.message);
    }
});


