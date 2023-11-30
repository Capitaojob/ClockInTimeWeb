import { getINSS, getIRRF } from './TaxesUtils.js'
import { normalizeSpecialCharactersOnString } from './StringUtils.js'

async function gerarPDF() {
    try {
        const monthDate = document.getElementById("month__date").value
        const response = await fetch(`/Home/GetReportDataForUser?idFuncionario=${sessionStorage.getItem("userId")}&month=${monthDate.split('-')[1]}&year=${monthDate.split('-')[0]}`);

        if (!response.ok) {
            throw new Error(`Erro na solicitação: ${response.statusText}`);
        }

        const data = await response.json(); //nome, horasTotaisTrabalhadas, horasTotaisBase, salarioBruto

        const salarioBaseComDescontoDeHoras = (data.horasTotaisTrabalhadas * data.salarioBruto) / data.horasTotaisBase;
        const descontoHoras = (data.salarioBruto - salarioBaseComDescontoDeHoras).toFixed(2)

        const inssData = getINSS(salarioBaseComDescontoDeHoras); //totalLiquido, desconto, percentual
        const irrfData = getIRRF(inssData.totalLiquido); //totalLiquido, aliquota, deducao, desconto

        const doc = new jsPDF();
        doc.setFont("times");
        doc.setFontSize(12);

        let y = 20;

        doc.text("Clock In Time - Relatorio De Pagamento", 10, y);
        y += 10;

        doc.line(10, 22, 200, 22);

        doc.setFontSize(22);
        doc.text("Tz Solucoes", 10, y);
        y += 15;

        doc.setFontSize(12);

        doc.text("Endereco: R. Chile, 967 - Jardim Iraja, Ribeirao Preto - SP", 10, y);
        y += 10;

        doc.text("Telefone: (16) 99158-9900", 10, y);
        y += 15;

        // Funcionário
        doc.setFontSize(22);
        doc.text(`Funcionario: ${normalizeSpecialCharactersOnString(data.nome)}`, 10, y);
        doc.setFontSize(12);

        y += 10;
        doc.text(`E-mail: ${normalizeSpecialCharactersOnString(data.email)}`, 10, y);
        y += 10;
        doc.text(`Endereco: ${normalizeSpecialCharactersOnString(data.endereco)}`, 10, y);
        y += 10;
        doc.text(`Telefone: ${normalizeSpecialCharactersOnString(data.telefone)}`, 10, y);

        y += 20;

        const columns = ['Descricao', 'Valor Somado', 'Valor Subtraido', 'Valor Total'];
        const columnWidths = [80, 60, 60, 40];

        const tableData = [
            ['Salário Bruto', data.salarioBruto.toFixed(2), '0.00', data.salarioBruto],
            ['Desconto de Horas', '0.00', descontoHoras, descontoHoras],
            ['INSS', '0.00', inssData.desconto, inssData.desconto],
            ['IRRF', '0.00', irrfData.desconto, irrfData.desconto],
            [
                'Total',
                data.salarioBruto.toFixed(2),
                (+descontoHoras + +inssData.desconto + +irrfData.desconto).toFixed(2),
                (+data.salarioBruto - +descontoHoras - +inssData.desconto - +irrfData.desconto).toFixed(2)
            ]
        ];

        // Adiciona o cabeçalho da tabela
        for (let i = 0; i < columns.length; i++) {
            doc.text(normalizeSpecialCharactersOnString(columns[i]), 10 + i * 50, y);
        }

        doc.line(10, y + 2, 200, y + 2);

        // Adiciona as linhas da tabela
        for (let row of tableData) {
            y += 10; // Ajusta a posição vertical
            for (let i = 0; i < row.length; i++) {
                doc.text(normalizeSpecialCharactersOnString((row[i].toString())), 10 + i * 50, y);
            }
        }

        // Salva ou exibe o PDF
         doc.save('relatorio.pdf');
    } catch (error) {
        console.error('Erro ao gerar o PDF:', error);
    }
}

document.querySelector("#generate__report").addEventListener("click", async (e) => {
    e.preventDefault();
    Promise.resolve(gerarPDF());
})