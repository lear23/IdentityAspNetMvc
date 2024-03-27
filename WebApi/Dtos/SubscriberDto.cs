using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos;

public class SubscriberDto
{
    [Required]
    public string Email { get; set; } = null!;

    public bool DailyNewsLatter { get; set; }
    public bool AdvertisingUpdates { get; set; }
    public bool WeekinReview { get; set; }
    public bool EventUpdates { get; set; }
    public bool StartupWeekly { get; set; }
    public bool Podcasts { get; set; }



}
