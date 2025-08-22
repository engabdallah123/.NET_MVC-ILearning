// --------time down exam ------------------ 
let minutesSpan = document.querySelector(".minuts");
let secondsSpan = document.querySelector(".seconds");

if (minutesSpan && secondsSpan) {
    let totalTime = parseInt(minutesSpan.innerText) * 60;

    let countDownInterval = setInterval(function () {
        let mins = Math.floor(totalTime / 60);
        let secs = totalTime % 60;

        minutesSpan.innerHTML = mins < 10 ? `0${mins}` : mins;
        secondsSpan.innerHTML = secs < 10 ? `0${secs}` : secs;

        totalTime--;

        if (totalTime < 0) {
            clearInterval(countDownInterval);
            let formExam = document.getElementById("form-send-exam");
            if (formExam) formExam.submit();
        }
    }, 1000);
}

//---------------------alert--------------------------- 
document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () {
        let alert = document.querySelector(".alert");
        if (alert) alert.remove();
    }, 3000);
});

//--------------------delete exam--------- 
document.addEventListener("DOMContentLoaded", function () {
    let btnExam = document.getElementById("delete-exam");
    let form = document.getElementById("delete-exam-form");
    if (btnExam && form) {
        btnExam.addEventListener("click", function (e) {
            e.preventDefault();
            Swal.fire({
                title: "Are you sure?",
                text: "This exam will be permanently deleted!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Yes, delete it!",
                cancelButtonText: "Cancel"
            }).then((result) => {
                if (result.isConfirmed) form.submit();
            });
        });
    }
});

//--------------------send exam---------
document.addEventListener("DOMContentLoaded", function () {
    let btnExam = document.getElementById("sent-exam");
    let form = document.getElementById("form-send-exam");
    if (btnExam && form) {
        btnExam.addEventListener("click", function (e) {
            e.preventDefault();
            Swal.fire({
                title: "Are you sure?",
                text: "This exam will be permanently Sent!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Yes, send it!",
                cancelButtonText: "Cancel"
            }).then((result) => {
                if (result.isConfirmed) form.submit();
            });
        });
    }
});

//----------------Role---------------------- 
document.addEventListener("DOMContentLoaded", function () {
    let btnRole = document.getElementById("btn-role");
    if (btnRole) {
        btnRole.addEventListener("click", function (e) {
            e.preventDefault();
            Swal.fire({
                title: "Do you want to save the changes?",
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: "Save",
                denyButtonText: `Don't save`
            }).then((result) => {
                if (result.isConfirmed) {
                    document.getElementById("add-role").submit();
                } else if (result.isDenied) {
                    Swal.fire("Changes are not saved", "", "info");
                }
            });
        })
    }
});

// --------------Degree--------------------- 
document.addEventListener("DOMContentLoaded", function () {
    let btnDegree = document.getElementById("degree-btn");
    if (btnDegree) {
        btnDegree.addEventListener("click", function (e) {
            e.preventDefault();
            Swal.fire({
                title: "Do you want to save the changes?",
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: "Save",
                denyButtonText: `Don't save`
            }).then((result) => {
                if (result.isConfirmed) {
                    document.getElementById("degree-post").submit();
                } else if (result.isDenied) {
                    Swal.fire("Changes are not saved", "", "info");
                }
            });
        })
    }
});

// --------------------------Active page-------- 
document.addEventListener("DOMContentLoaded", function () {
    let currentPath = window.location.pathname.toLowerCase();
    let btns = document.querySelectorAll(".btn-active");
    if (btns.length > 0) {
        btns.forEach(btn => {
            let link = btn.querySelector("a");
            if (link && currentPath.includes(link.getAttribute("href").toLowerCase())) {
                btns.forEach(b => b.classList.remove("active"));
                btn.classList.add("active");
            }
            btn.addEventListener("click", function () {
                btns.forEach(b => b.classList.remove("active"));
                this.classList.add("active");
            });
        });
    }
});

// -------------------Search------------------ 
document.addEventListener("DOMContentLoaded", function () {
    const searchInput = document.getElementById("menuSearch");
    const menuLinks = document.querySelectorAll("#menuList a");
    if (searchInput) {
        searchInput.addEventListener("keypress", function (e) {
            if (e.key === "Enter") {
                const keyword = this.value.toLowerCase();
                let found = false;
                menuLinks.forEach(link => {
                    const text = link.innerText.toLowerCase();
                    if (text.includes(keyword)) {
                        window.location.href = link.href;
                        found = true;
                    }
                });
                if (!found) alert("No matching page found!");
            }
        });
    }
});

// -----------------Courses----------------- 
document.addEventListener("DOMContentLoaded", function () {
    let manageCrsBtn = document.getElementById("manage-btn");
    if (manageCrsBtn) {
        manageCrsBtn.addEventListener("click", function (e) {
            e.preventDefault();
            Swal.fire({
                title: "Do you want to save the changes?",
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: "Save",
                denyButtonText: `Don't save`
            }).then((result) => {
                if (result.isConfirmed) {
                    document.getElementById("manage-form").submit();
                } else if (result.isDenied) {
                    Swal.fire("Changes are not saved", "", "info");
                }
            });
        })
    }
});

// -----------------Student------------- 
document.addEventListener("DOMContentLoaded", function () {
    let addStu = document.getElementById("add-btn");
    if (addStu) {
        addStu.addEventListener("click", function (e) {
            e.preventDefault();
            Swal.fire({
                title: "Do you want to save the changes?",
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: "Save",
                denyButtonText: `Don't save`
            }).then((result) => {
                if (result.isConfirmed) {
                    document.getElementById("creat-stu").submit();
                } else if (result.isDenied) {
                    Swal.fire("Changes are not saved", "", "info");
                }
            });
        })
    }

    let editStu = document.getElementById("edit-btn");
    if (editStu) {
        editStu.addEventListener("click", function (e) {
            e.preventDefault();
            Swal.fire({
                title: "Do you want to save the changes?",
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: "Save",
                denyButtonText: `Don't save`
            }).then((result) => {
                if (result.isConfirmed) {
                    document.getElementById("edit-stu").submit();
                } else if (result.isDenied) {
                    Swal.fire("Changes are not saved", "", "info");
                }
            });
        })
    }
});

// ------------------------------------------------------------------------------ 
document.addEventListener("DOMContentLoaded", function () {
    let delettBtn = document.querySelector("#delete-btn");
    if (delettBtn) {
        delettBtn.addEventListener("click", function (e) {
            e.preventDefault();
            Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Yes, delete it!"
            }).then((result) => {
                if (result.isConfirmed) {
                    document.getElementById("formBtnDelete").submit();
                }
            });
        });
    }
});

// -----------------------------------Department------------------------------------------- 
document.addEventListener("DOMContentLoaded", function () {
    let btn = document.querySelector("#deletebtn");
    if (btn) {
        btn.addEventListener("click", function () {
            Swal.fire({
                title: "Do you want to save the changes?",
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: "Save",
                denyButtonText: `Don't save`
            }).then((result) => {
                if (result.isConfirmed) {
                    document.getElementById("formEdite").submit();
                } else if (result.isDenied) {
                    Swal.fire("Changes are not saved", "", "info");
                }
            });
        });
    }
});
