
using Esteira.Dto;
using System;

namespace Esteira.Processamento
{
    public static class ProcessarEtapa
    {
        public static PesagemRetornoDto RealizarPesage(PesagemRequisicaoDto requisicao)
        {
            return new PesagemRetornoDto
            {
                Produto = requisicao.Produto,
                Quantidade = requisicao.Quantidade,
                Peso = new Random().Next(0, 2000)
            };
        }

        public static EtiquetaDto GerarEtiqueta(PesagemRetornoDto requisicao)
        {
            return new PesagemRetornoDto
            {
                Produto = requisicao.Produto,
                Quantidade = requisicao.Quantidade,
                Peso = requisicao.Peso,
                CodBarras = Guid.NewGuid().ToString()
            };
        }

        public static bool ValidarDadosCaixa(EtiquetaDto dadosInicial, EtiquetaDto dadosFinal)
        {
            return (dadosFinal.Peso.Equals(dadosInicial.Peso) && dadosFinal.Quantidade.Equals(dadosInicial.Quantidade) && dadosFinal.Produto.Equals(dadosInicial.Produto));
        }

        public static bool Gravar(EtiquetaDto dadosFinal)
        {
            try
            {
                Console.WriteLine(
                    $@"Produto: . . . . . . . . . . {dadosFinal.Produto}\n" +
                    $@"Quantidade: . . . . . . . . {dadosFinal.Quantidade.ToString()}\n" +
                    $@"Peso: . . . . . . . . . . . {dadosFinal.Peso.ToString()}\n" +
                    $@"Codigo de barras: . . . . . {dadosFinal.CodBarras}\n"
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ChecarFuncionamento()
        {
            return new Random().Next() > (Int32.MaxValue / 2);
        }

        public static ErrosDto ExibeErro(string mensagem = "")
        {
            var retorno = new ErrosDto()
            {
                Codigo = new Random().Next(0, 20),
                Mensagem = (mensagem == "" ? "Fora de operação" : mensagem)
            };

            Console.WriteLine(
                $@"Erro: . . . . . . . . . . \n" +
                $@"Codigo: . . . . . . . . . . {retorno.Codigo.ToString()}\n" +
                $@"Mensagem: . . . . . . . . {retorno.Mensagem}\n"
            );

            return retorno;
        }

        public static void OperacaoManual()
        {
            
        }
    }
}


