// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function toggleVisibility(element) {
    if (element.classList.contains('hidden')) {
        element.classList.remove('hidden');
    } else {
        element.classList.add('hidden');
    }
}

function test(data) {
    console.log(data);

}


function openModal(data) {
    if (data != null) {
        console.log(data);
        const modal = document.getElementById('employees-modal');
        document.getElementById('key').value = data.Id;
        document.getElementById('Cedula').value = data.Cedula;
        document.getElementById('Name').value = data.Name;
        document.getElementById('Department').value = data.Department;
        document.getElementById('Position').value = data.Position;
        document.getElementById('MonthlySalary').value = data.MonthlySalary;
        document.getElementById('PayrollId').value = data.PayrollId;

        var newEmployees = document.getElementById('new-employees');
        var employeesModal = document.getElementById('employees-modal');

        toggleVisibility(newEmployees);
        toggleVisibility(employeesModal);
    }

    var newEmployees = document.getElementById('new-employees');
    var employeesModal = document.getElementById('employees-modal');

    toggleVisibility(newEmployees);
    toggleVisibility(employeesModal);
}

