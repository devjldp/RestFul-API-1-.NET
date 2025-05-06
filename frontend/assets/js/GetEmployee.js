let table = document.getElementById('table-body')

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
        table.appendChild(row)
    });


}


getData();