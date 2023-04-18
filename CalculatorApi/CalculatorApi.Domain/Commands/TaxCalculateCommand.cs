using Flunt.Notifications;
using Flunt.Validations;

namespace CalculatorApi.Domain.Commands;

public class TaxCalculateCommand : Notifiable<Notification>, ICommand
{
    public double ValorInicial { get; set; }
    public int Meses { get; set; }

    public bool Validate()
    {
        AddNotifications(new Contract<TaxCalculateCommand>().Requires()
            .IsGreaterThan(ValorInicial, 0, nameof(ValorInicial), "Valor Inicial precisa ser maior que 0")
            .IsGreaterThan(Meses, 0, nameof(Meses), "Quantidade de meses precisa ser maior que 0")
        );
        return IsValid;
    }
}
