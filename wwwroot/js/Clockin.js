const btnClockIn = document.getElementById("btn-clockin");
const idFuncionario = sessionStorage.getItem("userId");

btnClockIn.addEventListener("click", async (e) => {
    e.preventDefault();

    const response = await fetch(`/Home/ClockInUser`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(idFuncionario),
    });

    try {
        if (!response.ok) {
            throw new Error(`Erro na requisição: ${response.statusText}`);
        }

        const data = await response.json();

        if (data.error) {
            throw new Error(`Erro! ${data.error}`);
        }

        console.log("Deu bom! " + data.success + ` ${data.entry ?? ""}`);

        location.reload();
    } catch (error) {
        console.error("Erro: ", error);
    }
});

document.addEventListener("DOMContentLoaded", async () => {
    const response = await fetch(`/Home/GetClockInRegistersForUser?idFuncionario=${idFuncionario}`);

    try {
        if (!response.ok) {
            throw new Error(`Erro na requisição: ${response.statusText}`);
        }

        const data = await response.json();

        if (data.error) {
            throw new Error(`Erro! ${data.error}`);
        }

        const clockInRegisters = document.getElementById("clockin__registers");

        data.clockInRegisters.forEach(register => {
            const ul = clockInRegisters.appendChild(document.createElement("ul"));
            const adicionarItem = (label, data) => {
                const li = document.createElement("li");
                li.innerHTML = `${label}: ${formatDate(data)}`;
                ul.appendChild(li);
            };

            register.saida && adicionarItem("Saída", register.saida);
            register.entradaAl && adicionarItem("Volta do Almoço", register.entradaAl);
            register.saidaAl && adicionarItem("Saída para Almoço", register.saidaAl);
            register.entrada && adicionarItem("Entrada", register.entrada);
        });
        //console.log("Deu bom! ", data.clockInRegisters);
    } catch (error) {
        console.error("Erro: ", error);
    }
})

function formatDate(dateString) {
    const date = new Date(dateString);

    const hours = addZeroToTheLeft(date.getHours());
    const minutes = addZeroToTheLeft(date.getMinutes());
    const seconds = addZeroToTheLeft(date.getSeconds());
    const day = addZeroToTheLeft(date.getDate());
    const month = addZeroToTheLeft(date.getMonth() + 1);
    const year = date.getFullYear();

    return `${hours}:${minutes}:${seconds} ${day}/${month}/${year}`;
}

function addZeroToTheLeft(value) {
    return value < 10 ? `0${value}` : value;
}