const btnAuth = document.getElementById("btn-auth");

btnAuth.addEventListener("click", async (e) => {
    e.preventDefault();

    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    const response = await fetch(`/Auth/ValidateUser`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
    });

    try {
        if (!response.ok) {
            throw new Error(`Requisição: ${response.statusText}`);
        }

        const data = await response.json();

        if (data.error) {
            throw new Error(`${data.error}`);
        }

        sessionStorage.setItem("userId", data.userId);
        location.href = '/Home';

    } catch (error) {
        console.error("Erro: ", error);
    }
});
