using Microsoft.AspNetCore.Mvc;
using Application.Activities.Queries;
using Application.Activities.Commands;
using Domain;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Domain.Activity>>> GetActivities()
    {
        return await Mediator.Send(new GetActivityList.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Domain.Activity>> GetActivityDetail(string id)
    {
        var activity = await Mediator.Send(new GetActivityDetails.Query { Id = id });
        if (activity == null) return NotFound();
        return Ok(activity);
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateActivity(Activity activity)
    {
        return await Mediator.Send(new CreateActivity.Command { Activity = activity });
    }

    [HttpPut]
    public async Task<ActionResult> EditActivity(Domain.Activity activity)
    {

        await Mediator.Send(new EditActivity.Command { Activity = activity });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActivity(string id)
    {
        await Mediator.Send(new DeleteActivity.Command { Id = id });
        return Ok();
    }
}