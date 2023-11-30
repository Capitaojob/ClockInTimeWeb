export function getINSS(salario) {
    let faixa1 = 1320.00;
    let faixa2 = 2571.29;
    let faixa3 = 3856.94;
    let faixa4 = 7507.49;

    let aliquota1 = 0.075;
    let aliquota2 = 0.09;
    let aliquota3 = 0.12;
    let aliquota4 = 0.14;

    let desconto = 0;

    if (salario > faixa1) {
        desconto += faixa1 * aliquota1;

        if (salario > faixa2) {
            desconto += (faixa2 - faixa1) * aliquota2;

            if (salario > faixa3) {
                desconto += (faixa3 - faixa2) * aliquota3;

                if (salario > faixa4) {
                    desconto += (faixa4 - faixa3) * aliquota4;
                } else {
                    desconto += (salario - faixa3) * aliquota4
                }
            } else {
                desconto += (salario - faixa2) * aliquota3;
            }
        } else {
            desconto += (salario - faixa1) * aliquota2;
        }
    } else {
        desconto = salario * aliquota1;
    }

    return {
        totalLiquido: salario - desconto,
        desconto: desconto.toFixed(2),
        percentual: ((desconto / salario) * 100).toFixed(2) + "%"
    };
}

export function getIRRF(baseDeCalculo) {
    let aliquota;
    let deducao;

    if (baseDeCalculo <= 2112.00) {
        aliquota = 0;
        deducao = 0;
    } else if (baseDeCalculo <= 2826.65) {
        aliquota = 7.5;
        deducao = 158.40;
    } else if (baseDeCalculo <= 3751.05) {
        aliquota = 15;
        deducao = 370.40;
    } else if (baseDeCalculo <= 4664.68) {
        aliquota = 22.5;
        deducao = 651.73;
    } else {
        aliquota = 27.5;
        deducao = 884.96;
    }

    const imposto = (baseDeCalculo * (aliquota / 100)) - deducao;
    return {
        totalLiquido: (baseDeCalculo - imposto).toFixed(2),
        desconto: imposto.toFixed(2),
        aliquota: `${aliquota}%`,
        deducao
    };
}
