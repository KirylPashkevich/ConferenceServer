using ConferenceManager.Contracts;
using ConferenceManager.Models;
using ConferenceManager.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConferenceSubscriptionsController : ControllerBase
{
    private readonly IConferenceSubscriptionRepository _subscriptionRepository;

    public ConferenceSubscriptionsController(IConferenceSubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ConferenceSubscription>> GetSubscriptions()
    {
        var subscriptions = _subscriptionRepository.GetAllSubscriptions(false);
        return Ok(subscriptions);
    }

    [HttpGet("conference/{conferenceId}")]
    public ActionResult<IEnumerable<ConferenceSubscription>> GetConferenceSubscriptions(int conferenceId)
    {
        var subscriptions = _subscriptionRepository.GetConferenceSubscriptions(conferenceId, false);
        return Ok(subscriptions);
    }

    [HttpGet("user/{userId}")]
    public ActionResult<IEnumerable<ConferenceSubscription>> GetUserSubscriptions(int userId)
    {
        var subscriptions = _subscriptionRepository.GetUserSubscriptions(userId, false);
        return Ok(subscriptions);
    }

    [HttpGet("{id}")]
    public ActionResult<ConferenceSubscription> GetSubscription(int id)
    {
        var subscription = _subscriptionRepository.GetSubscription(id, false);
        if (subscription == null)
        {
            return NotFound();
        }
        return Ok(subscription);
    }

    [HttpGet("conference/{conferenceId}/user/{userId}")]
    public ActionResult<ConferenceSubscription> GetUserConferenceSubscription(int conferenceId, int userId)
    {
        var subscription = _subscriptionRepository.GetUserConferenceSubscription(userId, conferenceId, false);
        if (subscription == null)
        {
            return NotFound();
        }
        return Ok(subscription);
    }

    [HttpPost]
    public ActionResult<ConferenceSubscription> CreateSubscription(ConferenceSubscriptionDto subscriptionDto)
    {
        // Проверяем, существует ли уже такая подписка
        if (_subscriptionRepository.IsUserSubscribedToConference(subscriptionDto.UserId, subscriptionDto.ConferenceId, false))
        {
            return BadRequest("User is already subscribed to this conference");
        }

        var subscription = new ConferenceSubscription
        {
            ConferenceId = subscriptionDto.ConferenceId,
            UserId = subscriptionDto.UserId
        };

        var createdSubscription = _subscriptionRepository.CreateSubscription(subscription);
        return CreatedAtAction(
            nameof(GetSubscription),
            new { id = createdSubscription.Id },
            createdSubscription);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSubscription(int id)
    {
        var subscription = _subscriptionRepository.GetSubscription(id, false);
        if (subscription == null)
        {
            return NotFound();
        }

        _subscriptionRepository.DeleteSubscription(subscription);
        return NoContent();
    }

    [HttpGet("conference/{conferenceId}/user/{userId}/check")]
    public ActionResult<bool> IsUserSubscribedToConference(int conferenceId, int userId)
    {
        var isSubscribed = _subscriptionRepository.IsUserSubscribedToConference(userId, conferenceId, false);
        return Ok(isSubscribed);
    }
} 