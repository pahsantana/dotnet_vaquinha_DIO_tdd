using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Vaquinha.Tests.Common.Fixtures;
using Xunit;

namespace Vaquinha.AutomatedUITests
{
	public class DoacaoTests : IDisposable, IClassFixture<DoacaoFixture>, 
                                               IClassFixture<EnderecoFixture>, 
                                               IClassFixture<CartaoCreditoFixture>
	{
		private DriverFactory _driverFactory = new DriverFactory();
		private IWebDriver _driver;
		private IWebElement valorDoado;
        private IWebElement nomeCompleto;
        private IWebElement email;
        private IWebElement mensagem;
        private IWebElement doadorAnonimo;
        private IWebElement numero_endereco;
        private IWebElement cidade;
        private IWebElement estado;
        private IWebElement cep;
        private IWebElement complemento;
        private IWebElement endereco;
        private IWebElement telefone;
        private IWebElement nomeTitular;
		private IWebElement numero_cartao;
        private IWebElement validade;
		private IWebElement cvv;
		private IWebElement btn;
        private readonly DoacaoFixture _doacaoFixture;
		private readonly EnderecoFixture _enderecoFixture;
		private readonly CartaoCreditoFixture _cartaoCreditoFixture;

		public DoacaoTests(DoacaoFixture doacaoFixture, EnderecoFixture enderecoFixture, CartaoCreditoFixture cartaoCreditoFixture)
        {
            _doacaoFixture = doacaoFixture;
            _enderecoFixture = enderecoFixture;
            _cartaoCreditoFixture = cartaoCreditoFixture;
        }
		public void Dispose()
		{
			_driverFactory.Close();
		}

		[Fact]
		public void DoacaoUI_AcessoTelaHome()
		{
			// Arrange
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			// Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("vaquinha-logo"));

			// Assert
			webElement.Displayed.Should().BeTrue(because:"logo exibido");
		}
		[Fact]
		public void DoacaoUI_CriacaoDoacao()
		{
			//Arrange
			var doacao = _doacaoFixture.DoacaoValida();
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());
			_driverFactory.NavigateToUrl("https://vaquinha.azurewebsites.net/");
			_driver = _driverFactory.GetWebDriver();

			//Act
			IWebElement webElement = null;
			webElement = _driver.FindElement(By.ClassName("btn-yellow"));
			webElement.Click();

			//Assert
			_driver.Url.Should().Contain("/Doacoes/Create");
			valorDoado = _driver.FindElement(By.Id("valor"));
			valorDoado.SendKeys("10");
			nomeCompleto = _driver.FindElement(By.Id("DadosPessoais_Nome"));
			nomeCompleto.SendKeys("Paloma Cristina Santana");
			email = _driver.FindElement(By.Id("DadosPessoais_Email"));
			email.SendKeys("palomacsantana24@gmail.com");
			mensagem = _driver.FindElement(By.Id("DadosPessoais_MensagemApoio"));
			mensagem.SendKeys("Eu apoiarei a causa que me comoveu extremamente. Adorei a iniciativa!");
			doadorAnonimo = _driver.FindElement(By.Id("DadosPessoais_Anonima"));
			doadorAnonimo.Click();

			endereco =  _driver.FindElement(By.Id("EnderecoCobranca_TextoEndereco"));
			endereco.SendKeys(" Av. Reg. Feijó");
			numero_endereco= _driver.FindElement(By.Id("EnderecoCobranca_Numero"));
			numero_endereco.SendKeys("1739");
			cidade= _driver.FindElement(By.Id("EnderecoCobranca_Cidade"));
			cidade.SendKeys("São Paulo");
			estado= _driver.FindElement(By.Id("estado"));
			estado.SendKeys("SP");
			cep = _driver.FindElement(By.Id("cep"));
			cep.SendKeys("03342-900");
			complemento = _driver.FindElement(By.Id("EnderecoCobranca_CEP"));
			complemento.SendKeys("Bairro Tatuapé");
			telefone = _driver.FindElement(By.Id("telefone"));
			telefone.SendKeys("(11)4003-4133");
			nomeTitular=_driver.FindElement(By.Id("FormaPagamento_NomeTitular"));
			nomeTitular.SendKeys("Paloma C Santana");
			numero_cartao = _driver.FindElement(By.Id("cardNumber"));
			numero_cartao.SendKeys("1234567890123456");
			validade = _driver.FindElement(By.Id("validade"));
			validade.SendKeys("10/22");
			cvv= _driver.FindElement(By.Id("cvv"));
			cvv.SendKeys("437");
			btn= _driver.FindElement(By.ClassName("btn yellow"));
			btn.Click();
			_driver.Url.Should().Contain("/Home/Index");
		}
	}
}