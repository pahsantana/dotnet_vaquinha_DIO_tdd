using Bogus;
using Xunit;
using Vaquinha.Domain.Entities;
using Vaquinha.Domain.ViewModels;

namespace Vaquinha.Tests.Common.Fixtures
{
    [CollectionDefinition(nameof(CartaoCreditoFixtureCollection))]
    public class CartaoCreditoFixtureCollection : ICollectionFixture<CartaoCreditoFixture>
    {
    }
    public class CartaoCreditoFixture
    {
        public CartaoCreditoViewModel CartaoCreditoModelValido()
        {
            var cartaoCredito = new Faker().Finance;

            var faker = new Faker<CartaoCreditoViewModel>("pt_BR");

            faker.RuleFor(c => c.CVV, (f, c) => cartaoCredito.CreditCardCvv());
            faker.RuleFor(c => c.NomeTitular, (f, c) => f.Person.FullName);
            faker.RuleFor(c => c.NumeroCartaoCredito, (f, c) => cartaoCredito.CreditCardNumber());
            faker.RuleFor(c => c.Validade, (f, c) => "06/28");

            return faker.Generate();
        }

        public CartaoCredito CartaoCreditoValido()
        {
            var cartaoCredito = new Faker("pt_BR").Finance;
            var pessoa = new Faker("pt_BR").Person;

            var faker = new Faker<CartaoCredito>("pt_BR");

            faker.CustomInstantiator(f =>
                new CartaoCredito(pessoa.FullName, cartaoCredito.CreditCardNumber(), "06/28", cartaoCredito.CreditCardCvv()));

            return faker.Generate();
        }

        public CartaoCreditoViewModel CartaoCreditoModelValido2()
        {
            var cartaoCredito = new Faker().Finance;

            var faker = new Faker<CartaoCreditoViewModel>("pt_BR");

            faker.RuleFor(c => c.CVV, (f, c) => cartaoCredito.CreditCardCvv());
            faker.RuleFor(c => c.NomeTitular, (f, c) => "Paloma Cristina Santana");
            faker.RuleFor(c => c.NumeroCartaoCredito, (f, c) => cartaoCredito.CreditCardNumber());
            faker.RuleFor(c => c.Validade, (f, c) => "08/27");

            return faker.Generate();
        }
        // Meu cartão retirando o número e cvv
        public CartaoCredito CartaoCreditoValido2()
        {
            var cartaoCredito = new Faker("pt_BR").Finance;

            var faker = new Faker<CartaoCredito>("pt_BR");

            faker.CustomInstantiator(f =>
                new CartaoCredito("Paloma Cristina Santana", cartaoCredito.CreditCardNumber(), "08/27", cartaoCredito.CreditCardCvv()));

            return faker.Generate();
        }

        public CartaoCredito CartaoCreditoVazio()
        {
            return new CartaoCredito(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public CartaoCredito CartaoCreditoNumeroValidadeCVVInvalido()
        {
            var cartaoCredito = new Faker("pt_BR").Finance;
            var pessoa = new Faker("pt_BR").Person;

            var faker = new Faker<CartaoCredito>("pt_BR");

            faker.CustomInstantiator(f =>
                 new CartaoCredito(pessoa.FullName, "21125684", "14/25", "312q"));

            return faker.Generate();
        }

        public CartaoCredito CartaoCreditoValidadeCVVInvalido()
        {
            var cartaoCredito = new Faker("pt_BR").Finance;
            var pessoa = new Faker("pt_BR").Person;

            var faker = new Faker<CartaoCredito>("pt_BR");

            faker.CustomInstantiator(f =>
                new CartaoCredito(pessoa.FullName, "5032933437649046", "-01/2100", "31"));

            return faker.Generate();
        }

        public CartaoCredito CartaoCreditoCVVInvalido()
        {
            var cartaoCredito = new Faker("pt_BR").Finance;
            var pessoa = new Faker("pt_BR").Person;

            var faker = new Faker<CartaoCredito>("pt_BR");

            faker.CustomInstantiator(f =>
                new CartaoCredito(pessoa.FullName, "5032933437649046", "04/22", "4567"));

            return faker.Generate();
        }

        public CartaoCredito CartaoCreditoValidadeExpirada()
        {
            var cartaoCredito = new Faker("pt_BR").Finance;
            var pessoa = new Faker("pt_BR").Person;

            var faker = new Faker<CartaoCredito>("pt_BR");

            faker.CustomInstantiator(f =>
                 new CartaoCredito(pessoa.FullName, cartaoCredito.CreditCardNumber(), "06/19", cartaoCredito.CreditCardCvv()));

            return faker.Generate();
        }

        public CartaoCredito CartaoCreditoDataInvalidaFalta()
        {
            var cartaoCredito = new Faker("pt_BR").Finance;
            var pessoa = new Faker("pt_BR").Person;

            var faker = new Faker<CartaoCredito>("pt_BR");

            faker.CustomInstantiator(f =>
                 new CartaoCredito(pessoa.FullName, cartaoCredito.CreditCardNumber(), "06/", cartaoCredito.CreditCardCvv()));

            return faker.Generate();
        }

        public CartaoCredito CartaoCreditoDataInvalidaNumero()
        {
            var cartaoCredito = new Faker("pt_BR").Finance;
            var pessoa = new Faker("pt_BR").Person;

            var faker = new Faker<CartaoCredito>("pt_BR");

            faker.CustomInstantiator(f =>
                 new CartaoCredito(pessoa.FullName, cartaoCredito.CreditCardNumber(), "078/21009", cartaoCredito.CreditCardCvv()));

            return faker.Generate();
        }

        public CartaoCredito CartaoCreditoNomeTitularMaxLengthInvalido()
        {
            const string TEXTO_COM_MAIS_DE_150_CARACTERES = "AHIUDHASHOIFJOASJPFPOKAPFOKPKQPOFKOPQKWPOFEMMVIMWPOVPOQWPMVPMQOPIPQMJEOIPFMOIQOIFMCOKQMEWVMOPMQEOMVOPMWQOEMVOWMEOMVOIQMOIVMQEHISUAHDUIHASIUHDIHASIUHDUIHIAUSHIDUHAIUSDQWMFMPEQPOGFMPWEMGVWEM";
            var cartaoCredito = new Faker("pt_BR").Finance;

            var faker = new Faker<CartaoCredito>("pt_BR");

            faker.CustomInstantiator(f =>
                 new CartaoCredito(TEXTO_COM_MAIS_DE_150_CARACTERES, cartaoCredito.CreditCardNumber(), "06/28", cartaoCredito.CreditCardCvv()));

            return faker.Generate();
        }

    }
}
