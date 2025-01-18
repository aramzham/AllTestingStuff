using CA_Jason_Taylor_template.Application.Common.Interfaces;

namespace CA_Jason_Taylor_template.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
