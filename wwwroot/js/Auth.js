btnAuth = document.getElementById("btn-auth")

btnAuth.addEventListener("submit", (e) => {
    e.preventDefault();
    sessionStorage.setItem("userId", userId);
})