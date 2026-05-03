const resultBlock = document.getElementById("result");

function showResult(data) {
    resultBlock.textContent = JSON.stringify(data, null, 2);
}
function showResultInsertStudent(message) {
    document.getElementById("result").innerHTML = message;
}

async function loadStudents() {
    const response = await fetch("https://localhost:7142/api/students");

    if (!response.ok) {
        showResult("Ошибка: " + response.status);
        return;
    }

    const data = await response.json();
    showResult(data);
}

async function loadCourses() {
    const response = await fetch("https://localhost:7142/api/courses");

    if (!response.ok) {
        showResult("Ошибка: " + response.status);
        return;
    }

    const data = await response.json();
    showResult(data);
}

async function loadEnrollments() {
    const response = await fetch("https://localhost:7142/api/enrollments");

    if (!response.ok) {
        showResult("Ошибка: " + response.status);
        return;
    }

    const data = await response.json();
    showResult(data);
}

async function addStudent() {
    const courseUid = document.getElementById("courseUid").value;
    const name = document.getElementById("studentName").value;
    const surname = document.getElementById("studentSurname").value;
    const age = parseInt(document.getElementById("studentAge").value);
    const group = parseInt(document.getElementById("studentGroup").value);

    const body = {
        courseUid: courseUid,
        name: name,
        surname: surname,
        age: age,
        group: group
    };

    const response = await fetch("https://localhost:7142/api/students", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(body)
    });

    if (response.status === 201 || response.status === 200) {
        //showResult("Студент успешно добавлен на курс! courseUid: " + body.courseUid + " Имя: " + body.name + " Фамилия: " + body.surname + " Группа: " + body.group);
        showResultInsertStudent(
            "Студент успешно добавлен на курс!<br>" +
            "courseUid: " + body.courseUid + "<br>" +
            "Имя: " + body.name + "<br>" +
            "Фамилия: " + body.surname + "<br>" +
            "Группа: " + body.group
        );
        //showResult(response.text);
        //loadStudents();
    }
    else {
        const errorText = await response.text();
        showResult("Ошибка при добавлении: " + response.status + "\n" + errorText);
    }
}

document.getElementById("btnStudents").addEventListener("click", loadStudents);
document.getElementById("btnCourses").addEventListener("click", loadCourses);
document.getElementById("btnEnrollments").addEventListener("click", loadEnrollments);
document.getElementById("btnAddStudent").addEventListener("click", addStudent);
