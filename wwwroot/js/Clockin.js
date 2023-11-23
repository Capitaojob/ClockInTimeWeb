const btnClockIn = document.getElementById("btn-clockin");

btnClockIn.addEventListener("click", async (e) => {
    e.preventDefault();

    const idFuncionario = sessionStorage.getItem("idFuncionario");

    fetch(`/Home/ClockInUser?employeeId=${idFuncionario}`)
        .then(response => {
            console.log(response)
            if (!response.ok) {
                throw new Error(`Erro na requisição: ${response.statusText}`);
            }
            return response.json()
        })
        .then(data => {
            if (data.error) {
                throw new Error(`Erro! ${data.error}`)
            }

            console.log("Deu bom! " + data.success)
        })
        .catch(error => {
            console.error('Erro: ', error);
        });
        
})