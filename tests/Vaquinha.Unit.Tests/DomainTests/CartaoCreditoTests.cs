using FluentAssertions;
using Xunit;
using Vaquinha.Tests.Common.Fixtures;

namespace Vaquinha.Unit.Tests.DomainTests
{
    [Collection(nameof(CartaoCreditoFixtureCollection))]
    public class CartaoCreditoTests: IClassFixture<CartaoCreditoFixture>
    {
        private readonly CartaoCreditoFixture _fixture;

        public CartaoCreditoTests(CartaoCreditoFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("CartaoCredito", "CartaoCredito_CorretamentePreenchido_CartaoCreditoValido")]
        public void CartaoCredito_CorretamentePreenchido_CartaoCreditoValido()
        {
            // Arrange
            var cartaoCredito = _fixture.CartaoCreditoValido();

            // Act
            var valido = cartaoCredito.Valido();

            // Assert
            valido.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            cartaoCredito.ErrorMessages.Should().BeEmpty();
        }

        //

        [Fact]
        [Trait("CartaoCredito", "CartaoCredito_CorretamentePreenchido_CartaoCreditoValido")]
        public void CartaoCredito_CorretamentePreenchido_CartaoCreditoValido2()
        {
            // Arrange
            var cartaoCredito = _fixture.CartaoCreditoValido2();

            // Act
            var valido = cartaoCredito.Valido();

            // Assert
            valido.Should().BeTrue(because: "os campos foram preenchidos corretamente");
            cartaoCredito.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        [Trait("CartaoCredito", "CartaoCredito_NenhumDadoPreenchido_CartaoCreditoInvalido")]
        public void CartaoCredito_NenhumDadoPreenchido_CartaoCreditoInvalido()
        {
            // Arrange
            var cartaoCredito = _fixture.CartaoCreditoVazio();

            // Act
            var valido = cartaoCredito.Valido();

            // Assert
            valido.Should().BeFalse(because: "deve possuir erros de preenchimento");
            cartaoCredito.ErrorMessages.Should().HaveCount(4, because: "nenhum dos 4 campos obrigatórios foi informado ou estão incorretos.");

            cartaoCredito.ErrorMessages.Should().Contain("O campo Nome Titular deve ser preenchido", because: "o campo Nome Titular é obrigatório e não foi preenchido.");
            cartaoCredito.ErrorMessages.Should().Contain("O campo Número de cartão de crédito deve ser preenchido", because: "o campo Número de cartão de crédito é obrigatório e não foi preenchido.");
            cartaoCredito.ErrorMessages.Should().Contain("O campo CVV deve ser preenchido", because: "o campo CVV é obrigatório e não foi preenchido.");
            cartaoCredito.ErrorMessages.Should().Contain("O campo Validade deve ser preenchido", because: "o campo Data de Vencimento do cartão de crédito é obrigatório e não foi preenchido.");
        }

        [Fact]
        [Trait("CartaoCredito", "CartaoCredito_NumeroValidadeCVVInvalido_CartaoCreditoInvalido")]
        public void CartaoCredito_NumeroValidadeCVVInvalido_CartaoCreditoInvalido()
        {
            // Arrange
            var cartaoCredito = _fixture.CartaoCreditoNumeroValidadeCVVInvalido();

            // Act
            var valido = cartaoCredito.Valido();

            // Assert
            valido.Should().BeFalse(because: "deve possuir erros de validação");
            cartaoCredito.ErrorMessages.Should().HaveCount(3, because: "nenhum dos 3 campos obrigatórios foi informado ou estão incorretos.");

            cartaoCredito.ErrorMessages.Should().Contain("Campo Número de cartão de crédito inválido", because: "o campo Nome Titular é obrigatório e não foi preenchido.");
            cartaoCredito.ErrorMessages.Should().Contain("Campo CVV inválido", because: "o campo Nome Titular é obrigatório e não foi preenchido.");
            cartaoCredito.ErrorMessages.Should().Contain("Campo Data de Vencimento do cartão de crédito inválido", because: "o campo Nome Titular é obrigatório e não foi preenchido.");
        }

        [Fact]
        [Trait("CartaoCredito", "CartaoCredito_ValidadeCVVInvalido_CartaoCreditoInvalido")]
        public void CartaoCredito_ValidadeCVVInvalido_CartaoCreditoInvalido()
        {
            // Arrange
            var cartaoCredito = _fixture.CartaoCreditoValidadeCVVInvalido();

            // Act
            var valido = cartaoCredito.Valido();

            // Assert
            // menor que 3 digitos 
            valido.Should().BeFalse(because: "deve possuir erros de validação");
            cartaoCredito.ErrorMessages.Should().HaveCount(2, because: "nenhum dos 2 campos obrigatórios foi informado ou estão incorretos.");

            cartaoCredito.ErrorMessages.Should().Contain("Campo CVV inválido", because: "o campo Nome Titular é obrigatório e não foi preenchido.");
            cartaoCredito.ErrorMessages.Should().Contain("Campo Data de Vencimento do cartão de crédito inválido", because: "o campo Nome Titular é obrigatório e não foi preenchido.");
        }

        [Fact]
        [Trait("CartaoCredito", "CartaoCredito_CVVInvalido_CartaoCreditoInvalido")]
        public void CartaoCredito_CVVInvalido_CartaoCreditoInvalido()
        {
            // Arrange
            var cartaoCredito = _fixture.CartaoCreditoCVVInvalido();

            // Act
            var valido = cartaoCredito.Valido();

            // Assert
            valido.Should().BeFalse(because: "deve possuir erros de validação");
            cartaoCredito.ErrorMessages.Should().HaveCount(1, because: "nenhum do 1 campo obrigatório foi informado ou está incorreto.");

            cartaoCredito.ErrorMessages.Should().Contain("Campo CVV inválido", because: "o campo Nome Titular é obrigatório e não foi preenchido.");
        }

        [Fact]
        [Trait("CartaoCredito", "CartaoCredito_ValidadeExpirada_CartaoCreditoInvalido")]
        public void CartaoCredito_ValidadeExpirada_CartaoCreditoInvalido()
        {
            // Arrange
            var cartaoCredito = _fixture.CartaoCreditoValidadeExpirada();

            // Act
            var valido = cartaoCredito.Valido();

            // Assert
            valido.Should().BeFalse(because: "deve possuir erros de validação");
            cartaoCredito.ErrorMessages.Should().HaveCount(1, because: "data de vencimento expirada");

            cartaoCredito.ErrorMessages.Should().Contain("Cartão de Crédito com data expirada", because: "o campo Data de Vencimento do cartão de crédito está expirado.");
        }

        [Fact]
        [Trait("CartaoCredito", "CartaoCredito_DataInvalidaFalta_CartaoCreditoInvalido")]
        public void CartaoCredito_DataInvalidaFalta_CartaoCreditoInvalido()
        {
            // Arrange
            var cartaoCredito = _fixture.CartaoCreditoDataInvalidaFalta();

            // Act
            var valido = cartaoCredito.Valido();

            // Assert
            valido.Should().BeFalse(because: "deve possuir erros de validação");
            cartaoCredito.ErrorMessages.Should().HaveCount(1, because: "data informada errada");

            cartaoCredito.ErrorMessages.Should().Contain("Cartão de Crédito com data errada", because: "o campo Data de Vencimento do cartão de crédito está faltando dados");
        }

        [Fact]
        [Trait("CartaoCredito", "CartaoCredito_DataInvalidaNumero_CartaoCreditoInvalido")]
        public void CartaoCredito_DataInvalidaNumero_CartaoCreditoInvalido()
        {
            // Arrange
            var cartaoCredito = _fixture.CartaoCreditoValidadeExpirada();

            // Act
            var valido = cartaoCredito.Valido();

            // Assert
            valido.Should().BeFalse(because: "deve possuir erros de validação");
            cartaoCredito.ErrorMessages.Should().HaveCount(1, because: "data informada excede a quantidade esperada");

            cartaoCredito.ErrorMessages.Should().Contain("Cartão de Crédito com data informada maior que o esperado", because: "o campo Data de Vencimento do cartão de crédito está faltando dados.");
        }

        [Fact]
        [Trait("CartaoCredito", "CartaoCredito_NomeTitularMaxLength_CartaoCreditoInvalido")]
        public void CartaoCredito_NomeTitularMaxLength_CartaoCreditoInvalido()
        {
            // Arrange
            var cartaoCredito = _fixture.CartaoCreditoNomeTitularMaxLengthInvalido();

            // Act
            var valido = cartaoCredito.Valido();

            // Assert
            valido.Should().BeFalse(because: "tamanho máximo de campos atingidos");
            cartaoCredito.ErrorMessages.Should().HaveCount(1, because: "o preenchimento de 1 campo ultrapassou tamanho máximo permitido.");

            cartaoCredito.ErrorMessages.Should().Contain("O campo Nome Titular deve possuir no máximo 150 caracteres", because: "o campo Nome Titular é obrigatório e não foi preenchido.");
        }

    }
}
