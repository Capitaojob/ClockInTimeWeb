async function downloadPDF() {
    try {
        const response = await fetch(`/Pdf/GetPdf?userId=${sessionStorage.getItem("userId")}`);

        if (!response.ok) {
            throw new Error(`Erro na solicitação: ${response.statusText}`);
        }

        const blob = await response.blob();

        const link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);

        link.download = 'relatorio.pdf';

        document.body.appendChild(link);

        link.click();

        document.body.removeChild(link);
    } catch (error) {
        console.error('Erro durante o download do PDF:', error);
    }
}

document.querySelector("#generate__report").addEventListener("click", async (e) => {
    e.preventDefault();
    //Promise.resolve(downloadPDF())
        //.then(location.href = '/Home/Report');
})

//function generatePDF() {
//    var pdfData = {
//        nomeFuncionario: "Joao Bataglia",
//        emailFuncionario: "jpangottibataglia@gmail.com",
//        data: "2023-11-11"
//    };

//    var props = {
//        outputType: jsPDFInvoiceTemplate.OutputType.Save,
//        returnJsPDFDocObject: true,
//        fileName: `Relatorio ${pdfData.nomeFuncionario} - ${pdfData.data}`,
//        orientationLandscape: false,
//        compress: true,
//        logo: {
//            src: "../images/logo.png",
//            type: 'PNG',
//            height: 26.66,
//            height: 53.66,
//            margin: {
//                top: 0,
//                left: 0
//            }
//        },
//        stamp: {
//            inAllPages: true, //by default = false, just in the last page
//            src: "https://raw.githubusercontent.com/edisonneza/jspdf-invoice-template/demo/images/qr_code.jpg",
//            type: 'JPG', 
//            width: 20, //aspect ratio = width/height
//            height: 20,
//            margin: {
//                top: 0, //negative or positive num, from the current position
//                left: 0 //negative or positive num, from the current position
//            }
//        },
//        business: {
//            name: "Tz Solucoes",
//            address: "R. Chile, 967 - Jardim Iraja, Ribeirao Preto - SP, 14020-610",
//            phone: "(16) 99158-9900",
//            email: "contato@tzsolucoes.com.br",
//            website: "www.tzsolucoes.com.br/",
//        },
//        contact: {
//            label: "Relatorio para o funcionario:",
//            name: pdfData.nomeFuncionario,
//            email: pdfData.emailFuncionario
//        },
//        invoice: {
//            label: "Relatorio de Pagamento:",
//            invDate: `Data do Pagamento: 05/11/2023`,
//            headerBorder: false,
//            tableBodyBorder: false,
//            header: [
//                {
//                    title: "#",
//                    style: {
//                        width: 10
//                    }
//                },
//                {
//                    title: "Titulo",
//                    style: {
//                        width: 30
//                    }
//                },
//                {
//                    title: "Descricao",
//                    style: {
//                        width: 80
//                    }
//                },
//                { title: "Entrada" },
//                { title: "Saida" },
//                { title: "Total" }
//            ],
//            table: Array.from(Array(5), (item, index) => ([
//                index + 1,
//                "Tipo",
//                "Desc",
//                400.5,
//                1.5,
//                399
//            ])),
//            additionalRows: [{
//                col1: 'Total:',
//                col2: '145,250.50',
//                col3: 'ALL',
//                style: {
//                    fontSize: 14 //optional, default 12
//                }
//            },
//            {
//                col1: 'VAT:',
//                col2: '20',
//                col3: '%',
//                style: {
//                    fontSize: 10 //optional, default 12
//                }
//            },
//            {
//                col1: 'SubTotal:',
//                col2: '116,199.90',
//                col3: 'ALL',
//                style: {
//                    fontSize: 10 //optional, default 12
//                }
//            }],
//            //invDescLabel: "Invoice Note",
//            //invDesc: "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary.",
//        },
//        footer: {
//            text: "The invoice is created on a computer and is valid without the signature and stamp.",
//        },
//        pageEnable: true,
//        pageLabel: "Pagina ",
//    };

//    var pdfObject = jsPDFInvoiceTemplate.default(props);
//    console.log("Object Created: ", pdfObject);
//}

//document.addEventListener("click", () => {
//    generatePDF();
//})