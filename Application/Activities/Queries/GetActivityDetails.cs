using System.Diagnostics;
using MediatR;
using Persistence;
using Domain;

namespace Application.Activities.Queries;

public class GetActivityDetails
{
    public class Query : IRequest<Domain.Activity>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, Domain.Activity>
    {
        public async Task<Domain.Activity> Handle(Query request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities.FindAsync([request.Id], cancellationToken);
            if (activity == null) throw new Exception("Activity not found");
            return activity;
        }
    }
}
