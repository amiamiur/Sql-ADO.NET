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
                            <td>${s.firstName}</td>
                            <td>${s.lastName}</td>
                            <td>${s.age}</td>
                            <td>${s.groupId}</td>
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
        const res = await fetch(`students/${id}`);

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