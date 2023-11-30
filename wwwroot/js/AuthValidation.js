const currentUrl = window.location.pathname;
const userId = sessionStorage.getItem("userId")

async function checkUserAuthValidity() {
    if (currentUrl.startsWith("/Auth")) {
        if (userId !== null) location.href = "/Home"
    } else {
        if (userId === null) location.href = "/Auth"

        if (!currentUrl.startsWith("/Home") && !currentUrl.startsWith("/Legal")) {
            const response = await fetch(`/Home/GetEmployeeAdministratorRole?userId=${userId}`);

            if (!response.ok) window.history.back()

            const data = await response.json()

            if (!data.isAdministrator) window.history.back()
        }
    }
}

checkUserAuthValidity()