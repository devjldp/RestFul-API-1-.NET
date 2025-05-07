let table = document.getElementById('table-body')

// Check if there's a JWT token
const token = localStorage.getItem('jwtToken');
if (!token) {
    alert("You're not authenticated. Redirecting to login page.");
    window.location.href = "login.html"; // If no token, redirect to login
}

async function getAdminName(){
    const url = "http://localhost:5025/auth/admin"
    data = {
        user: "josedev"
    }


    const response = await fetch(url)
    const employee = await response.json();
    console.log(employee)

    employee.forEach(admin => {
        if (data.user == admin.userName){
            fullName = `${admin.firstName} ${admin.lastName}`
            console.log(fullName)
            document.getElementById("main-logo").textContent = `Admin: ${admin.firstName} ${admin.lastName}`
            document.getElementById("main-logo").style.fontSize = '1.8rem'

        }
    })

}

async function getData(){
    const url = "http://localhost:5025/employee"

    const response = await fetch(url);
    const employees = await response.json();
    
    console.log(employees)
    employees.forEach(element => {
        const id = element['id']
        const fullName = `${element['firstName']} ${element['lastName']}`
        const email = element['email']
        const city = element['city']
        const role = element['role']

        const row = document.createElement('tr');
        [id, fullName, email, city, role].forEach(prop => {
            const cell = document.createElement('td'); // Create a new table cell
            cell.textContent = prop || 'N/A'; // Display the value or 'N/A' if it's undefined or null
            row.appendChild(cell); // Add the cell to the row
          });
        let button = document.createElement('button');
        button.textContent = 'Update'
        row.appendChild(button)
        button = document.createElement('button');
        button.textContent = 'Delete'
        row.appendChild(button)
        table.appendChild(row)
    });


}


getData();
getAdminName()

// Logout functionality
document.getElementById('logoutButton').addEventListener('click', function() {
    localStorage.removeItem('jwtToken'); // Remove the token from session
    window.location.href = "index.html"; // Redirect to login
})