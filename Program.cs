using System;


namespace ProjetoFaturamentoUN5
{
    public class Fatura //Nessa classe define-se os dados que teremos na fatura
    {
        public int codV;
        public string prod;
        public double prodV;
        public int codC;
        public string cDate;
        public string cNom;
        public Fatura(int codVendedor, string produto, double prodValor, int codCliente, string nomClient, string actDate)
        {
            codV = codVendedor;
            prod = produto;
            prodV = prodValor;
            codC = codCliente;
            cNom = nomClient;
            cDate = actDate;
            
        }
    }

    public class FatBuild //Aqui obtemos dados do usuário referente a fatura
    {
        public static void Main()
        {
            int cv = 0;
            Console.WriteLine("Digite o código de vendedor (Seis dígitos): ");
            try
            {
                int cv_A = Convert.ToInt32(Console.ReadLine());
                cv = cv_A;
            }
            catch (FormatException)
            {
                Console.WriteLine("Formato Inválido!\nDefinido como padrão: '987654'.");
                cv = 987654;
            }

            int cl = 0;
            Console.WriteLine("Selecione o registro:\n   1 - Pessoa Física; \n   2 - Pessoa Jurídica;\n   3 - Governamental.");
            try 
            {
                int cl_A = Convert.ToInt32(Console.ReadLine());
                cl = cl_A;
            }
            catch (FormatException)
            {
                Console.WriteLine("Formato Inválido!\nDefinido como padrão: '1' - CPF.");
                cl = 1;
            }
            
            Console.WriteLine("Cobrar a: ");
            string nm = Console.ReadLine();

            Console.WriteLine("Descreva o serviço a ser faturado: (Ex: Suporte Técnico)");
            string prd = Console.ReadLine();

            double vl = 0.0;
            bool isThereVal = false;
            while (isThereVal == false) //Valor do serviço é essencial, por isso o while.
            {
                try
                {
                    Console.WriteLine("Valor: ");
                    double vl_A = Convert.ToDouble(Console.ReadLine());
                    vl = vl_A;
                    isThereVal = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Formato inválido!\nInsira o valor do serviço em decimais (Ex: 14.99)");
                }
                
            }

            Fatura nota = new Fatura(cv, prd, vl, cl, nm, "01/01/1900"); //nota gerada

            var dtn = DateTime.Now;
            
            switch (nota.codC) //A partir do codC (Código do cliente) definimos o tipo de processamento (CPF, CNPJ ou Governamental)
            {
                case 1:
                    {
                        nota.prodV *= 0.85;
                        Console.WriteLine("Selecionado: Pessoa Física.\nMétodo de pagamento definido: -15% bruto, 30 dias p/ pagar.");
                        DateTime dVenc = dtn.AddDays(30);
                        nota.cDate = dVenc.ToString();
                        break;
                    }
                case 2:
                    {
                        nota.prodV *= 0.8;
                        Console.WriteLine("Selecionado: Pessoa Jurídica.\nMétodo de pagamento definido: -20% bruto, 60 dias p/ pagar.");
                        DateTime dVenc = dtn.AddDays(60);
                        nota.cDate = dVenc.ToString();
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Selecionado tipo: Governamental.\nMétodo de pagamento definido: À Vista.");
                        nota.cDate = dtn.ToString();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Registro de Identificação inválido!\nMétodo de pagamento definido: À Vista.");
                        nota.cDate = dtn.ToString();
                        break;
                    }
            }
            static void NBuilder(int cv_B, string prd_B, double vl_B, int cl_B, string nm_B, string dv_B) //Formatação e impressão da Fatura na tela.
            {
                Console.WriteLine($"COD. Vendedor:....................{cv_B,0:d}");
                Console.WriteLine($"Serviço:..........................{prd_B,1:c}");
                Console.WriteLine($"Valor total:......................{vl_B,0:c}");
                Console.WriteLine($"Tipo de cliente:..................{cl_B,0:d}");
                Console.WriteLine("(1 - PF, 2 - PJ, 3 - GOV)");
                Console.WriteLine($"Enviar Para:......................{nm_B,0:c}");
                var dtn = DateTime.Now;
                Console.WriteLine($"Data de Emissão:..................{dtn.ToString(),0:c}");
                Console.WriteLine($"Vencimento:.......................{dv_B,1:c}");
            }
            NBuilder(nota.codV, nota.prod, nota.prodV, nota.codC, nota.cNom, nota.cDate); 
        }
    }
}
