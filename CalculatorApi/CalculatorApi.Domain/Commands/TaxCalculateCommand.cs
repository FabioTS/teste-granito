using System.Text.Json.Serialization;
using Flunt.Notifications;
using Flunt.Validations;

namespace CalculatorApi.Domain.Commands;

public abstract class TaxCalculateCommand : Notifiable<Notification>, ICommand
{
    public int? ValorInicial { get; set; } = default;
    public int? Meses { get; set; } = default;

    public virtual bool Validate()
    {
        // AddNotifications(new Contract<TaxCalculateCommand>().Requires()
        //     .IsNotNullOrWhiteSpace(ValorInicial, nameof(ValorInicial))
        // );
        return IsValid;
    }
}
