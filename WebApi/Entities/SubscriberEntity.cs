using System.ComponentModel.DataAnnotations;
using WebApi.Dtos;

namespace WebApi.Entities;

public class SubscriberEntity
{

    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Email { get; set; } = null!;

    public bool DailyNewsLatter { get; set; }
    public bool AdvertisingUpdates { get; set; }
    public bool WeekinReview { get; set; }
    public bool EventUpdates { get; set; }
    public bool StartupWeekly { get; set; }
    public bool Podcasts { get; set; }

    public static implicit operator SubscriberEntity(SubscriberDto Dto)
    {
        return new SubscriberEntity
        {
            Email = Dto.Email,
            DailyNewsLatter = Dto.DailyNewsLatter,
            AdvertisingUpdates = Dto.AdvertisingUpdates,
            WeekinReview = Dto.WeekinReview,
            EventUpdates = Dto.EventUpdates,
            StartupWeekly = Dto.StartupWeekly,
            Podcasts = Dto.Podcasts,

        };
    }

}
