async function LoadAllStudents() {
    const errorEl = document.getElementById('error');
    const body = document.getElementById("studentsBody");
    try {
        const res = await fetch("/students/all");
        if (!res.ok) throw new Error("HTTP" + res.status);
        const students = await res.json();

        body.innerHTML = '';
        for (const s of students) {
            body.innerHTML += `
                        <tr>
                            <td>${s.studentId}</td>
                            <td>${s.firstName}</td>
                            <td>${s.lastName}</td>
                            <td>${s.age}</td>
                            <td>${s.groupId}</td>
                            <td>
                        <button type="button" onclick="EditStudent(${s.studentId})">Изменить</button>
                        <button type="button" onclick="DeleteStudent(${s.studentId})">Удалить</button>
                            </td>
                        </tr>`;
        }
        errorEl.textContent = '';
    } catch (err) {
        errorEl.textContent = "Ошибка загрузки студентов:" + err.message;
    }
}
async function LoadGroups() {
    const errorEl = document.getElementById("error");
    const body = document.getElementById("groupsBody");
    try {
        const res = await fetch("/groups/all");
        if (!res.ok) throw new Error("HTTP" + res.status)
        const groups = await res.json();

        body.innerHTML = '';
        for (const g of groups) {
            body.innerHTML += `<tr>
                    <td>${g.groupId}</td>
                    <td>${g.groupName}</td>
                    </tr>`;
        }
    } catch (err) {
        errorEl.textContent = "Ошибка загрузки групп" + err.message;
    }
}
async function LoadStudentById() {
    const id = document.getElementById("searchId").value;
    const result = document.getElementById("studentResult");
    try {
        const res = await fetch(`/students/${id}`);
        const s = await res.json();

        result.innerHTML = `
                        <strong>Найден студент:</strong>
                        Имя: ${s.firstName}<br>
                        Фамилия: ${s.lastName}<br>
                        Возраст: ${s.age} лет<br>
                        Номер группы: ${s.groupId}
                    `;
    } catch (err) {
        result.textContent = "Ошибка: " + err.message;
    }
}

async function SaveStudent() {
    const errorEl = document.getElementById('error');
    const id = document.getElementById('studentId').value;

    const student = {
        firstName: document.getElementById('firstName').value,
        lastName: document.getElementById('lastName').value,
        age: parseInt(document.getElementById('age').value),
        groupId: parseInt(document.getElementById('groupId').value)
    };

    const url = id ? `/students/${id}` : '/students/add';
    const method = id ? 'PUT' : 'POST';

    try {
        const res = await fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(student)
        });

        if (!res.ok) throw new Error('HTTP ' + res.status);

        document.getElementById('studentId').value = '';
        document.getElementById('firstName').value = '';
        document.getElementById('lastName').value = '';
        document.getElementById('age').value = '';
        document.getElementById('groupId').value = '';

        await LoadAllStudents();
    } catch (err) {
        if (errorEl) errorEl.textContent = 'Ошибка сохранения: ' + err.message;
        else console.error(err);
    }
}

async function EditStudent(id) {
    try {
        const res = await fetch(`/students/${id}`);
        if (!res.ok) throw new Error('Студент не найден');
        const s = await res.json();

        document.getElementById('studentId').value = s.studentId;
        document.getElementById('firstName').value = s.firstName;
        document.getElementById('lastName').value = s.lastName;
        document.getElementById('age').value = s.age;
        document.getElementById('groupId').value = s.groupId;
    } catch (err) {
        console.error(err);
    }
}

async function DeleteStudent(id) {
    if (!confirm(`Вы хотите удалить студента с ID ${id}?`)) return;
    try {
        const res = await fetch(`/students/${id}`, { method: 'DELETE' });
        if (!res.ok) throw new Error('Ошибка при удалении');

        await LoadAllStudents();
    } catch (err) {
        alert(err.message);
    }
}