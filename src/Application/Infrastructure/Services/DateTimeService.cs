using Sirus.Application.Common.Interfaces;

namespace Sirus.Application.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
