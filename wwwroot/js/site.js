const logoutBtn = document.querySelector(".btn-logout")

if (logoutBtn !== null) {
    logoutBtn.addEventListener("click", () => {
        sessionStorage.removeItem("userId")
        location.href = '/Auth';
    })
}

async function checkUserAdministrator() {
    const userId = sessionStorage.getItem("userId");

    if (!userId) return;

    const response = await fetch(`/Home/GetEmployeeAdministratorRole/?userId=${userId}`);
    const data = await response.json();

    if (!response.ok || data.error || !data.isAdministrator) return;

    const managementItems = document.getElementById("navbar-link-itens");

    if (managementItems === null) return;

    const funcionariosNavItem = document.createElement("li");
    funcionariosNavItem.className = "nav-item management-item";

    const funcionariosNavLink = document.createElement("a");
    funcionariosNavLink.className = "nav-link text-dark";
    funcionariosNavLink.setAttribute("href", "/Funcionarios/Index");
    funcionariosNavLink.textContent = "Gestão de Funcionários";

    funcionariosNavItem.appendChild(funcionariosNavLink);
    managementItems.appendChild(funcionariosNavItem);

    const cargosNavItem = document.createElement("li");
    cargosNavItem.className = "nav-item management-item";

    const cargosNavLink = document.createElement("a");
    cargosNavLink.className = "nav-link text-dark";
    cargosNavLink.setAttribute("href", "/Cargos/Index");
    cargosNavLink.textContent = "Gestão de Cargos";

    cargosNavItem.appendChild(cargosNavLink);
    managementItems.appendChild(cargosNavItem);
    
}

checkUserAdministrator();