using Esteira.Dto;
using Esteira.Processamento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Esteira
{
    public partial class frmPrincipal : Form
    {
        PesagemRetornoDto _PesagemRetornoDto;
        PesagemRetornoDto _PesagemFinal;
        EtiquetaDto _EtiquetaDto;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!ProcessarEtapa.ChecarFuncionamento())
            {
                ProcessarEtapa.ExibeErro();
                ProcessarEtapa.OperacaoManual();
                return;
            }

            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("informe o produto");
                textBox1.Focus();
                return;
            }

            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("informe a quantidade na caixa");
                textBox2.Focus();
                return;
            }

            _PesagemRetornoDto = ProcessarEtapa.RealizarPesage(
                new PesagemRequisicaoDto()
                {
                    Produto = textBox1.Text,
                    Quantidade = Convert.ToInt32(textBox2.Text)
                });

            label6.Text = $@"{_PesagemRetornoDto.Peso.ToString()} Kg";
            button1.Enabled = true;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ProcessarEtapa.ChecarFuncionamento())
            {
                ProcessarEtapa.ExibeErro();
                ProcessarEtapa.OperacaoManual();
                return;
            }

            var _EtiquetaDto = ProcessarEtapa.GerarEtiqueta(_PesagemRetornoDto);

            label9.Text = "Dados Pré Etiqueta";
            label10.Text = $@"Código de Barras - {_EtiquetaDto.CodBarras}";
            label11.Text = $@"Produto - {_EtiquetaDto.Produto}";
            label12.Text = $@"Quantidade - {_EtiquetaDto.Quantidade.ToString()}";
            label13.Text = $@"Peso - {_EtiquetaDto.Peso.ToString()}";

            button6.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!ProcessarEtapa.ChecarFuncionamento())
            {
                ProcessarEtapa.ExibeErro();
                ProcessarEtapa.OperacaoManual();
                return;
            }

            button5.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox5.Text = _PesagemRetornoDto.Quantidade.ToString();
            textBox6.Text = _PesagemRetornoDto.Produto;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!ProcessarEtapa.ChecarFuncionamento())
            {
                ProcessarEtapa.ExibeErro();
                ProcessarEtapa.OperacaoManual();
                return;
            }

            if (String.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("informe o produto");
                textBox6.Focus();
                return;
            }

            if (String.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("informe a quantidade na caixa");
                textBox5.Focus();
                return;
            }

            _PesagemFinal = ProcessarEtapa.RealizarPesage(
                new PesagemRequisicaoDto()
                {
                    Produto = textBox6.Text,
                    Quantidade = Convert.ToInt32(textBox5.Text)
                });

            label14.Text = $@"{_PesagemFinal.Peso.ToString()} Kg";
            button2.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!ProcessarEtapa.ChecarFuncionamento())
            {
                ProcessarEtapa.ExibeErro("Rejeitar caixa");
                return;
            }

            if ((textBox3.Visible) && (textBox3.Text != "Corrigir peso...") && (textBox3.Text != ""))
                _PesagemFinal.Peso = Convert.ToDecimal(textBox3.Text);

            if (ProcessarEtapa.ValidarDadosCaixa(_PesagemRetornoDto, _PesagemFinal))
            {
                _EtiquetaDto = ProcessarEtapa.GerarEtiqueta(_PesagemRetornoDto);

                label9.Text = "Dados Etiqueta final";
                label10.Text = $@"Código de Barras - {_EtiquetaDto.CodBarras}";
                label11.Text = $@"Produto - {_EtiquetaDto.Produto}";
                label12.Text = $@"Quantidade - {_EtiquetaDto.Quantidade.ToString()}";
                label13.Text = $@"Peso - {_EtiquetaDto.Peso.ToString()}";
                button3.Enabled = true;
            }
            else
            {
                button2.Text = "Dados inválidos";
                textBox3.Visible = true;
                button2.BackColor = Color.Azure;
                button2.UseVisualStyleBackColor = false;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!ProcessarEtapa.ChecarFuncionamento())
            {
                ProcessarEtapa.ExibeErro();
                ProcessarEtapa.OperacaoManual();
                return;
            }

            if (ProcessarEtapa.Gravar(_PesagemFinal))
            {
                button7.Enabled = true;
                MessageBox.Show("Dados gravados com sucesso");
            }
            else
            {
                MessageBox.Show("Erro ao gravar dados");
            }
        }

        private void textBox3_MouseEnter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Corrigir peso...")
            {
                textBox3.Text = "";
                button2.Text = "Etapa 5 - Validar dados da caixa";
            }
        }

        private void reset()
        {

            textBox1.Focus();
            label6.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";

            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
            button1.Enabled = false;

            _PesagemRetornoDto = new PesagemRetornoDto();
            _PesagemFinal = new PesagemRetornoDto();
            _EtiquetaDto = new EtiquetaDto();

            textBox5.Text = "";
            textBox6.Text = "";
            label14.Text = "";
            label9.Text = "Dados Etiqueta";
            button2.Text = "Validar dados da caixa";

            textBox5.Enabled = false;
            textBox6.Enabled = false;

            button2.Enabled = false;
            button3.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;

            textBox3.Text = "Corrigir peso...";
            textBox3.Visible = false;
            button2.Text = "Etapa 5 - Validar dados da caixa";
            button2.BackColor = button3.BackColor;
            button2.UseVisualStyleBackColor = button3.UseVisualStyleBackColor;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
}
