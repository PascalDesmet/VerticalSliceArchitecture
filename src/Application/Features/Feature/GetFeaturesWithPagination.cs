using AutoMapper;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Sirus.Application.Common;
using Sirus.Application.Common.Mappings;
using Sirus.Application.Common.Models;
using Sirus.Application.Infrastructure.Database;

namespace Sirus.Application.Features.Feature;

public class GetFeaturesWithPaginationController : ApiControllerBase
{
    [HttpGet("/api/features")]
    public Task<PaginatedList<FeatureBriefDto>> GetFeaturesWithPagination([FromQuery] GetFeaturesWithPaginationQuery query)
    {
        return Mediator.Send(query);
    }
}

public class GetFeaturesWithPaginationQuery : IRequest<PaginatedList<FeatureBriefDto>>
{
    // add properties here
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetFeaturesWithPaginationQueryValidator : AbstractValidator<GetFeaturesWithPaginationQuery>
{
    public GetFeaturesWithPaginationQueryValidator()
    {
        // add rules here

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}

internal sealed class GetFeaturesWithPaginationQueryHandler : IRequestHandler<GetFeaturesWithPaginationQuery, PaginatedList<FeatureBriefDto>>
{
    private readonly FeatureDbContext _context;
    private readonly IMapper _mapper;

    public GetFeaturesWithPaginationQueryHandler(FeatureDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<PaginatedList<FeatureBriefDto>> Handle(GetFeaturesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        // get data here and return paginated

        // return _context.Features
            //.Where(x => x.ListId == request.ListId)
            //.OrderBy(x => x.Title)
            //.ProjectTo<FeatureBriefDto>(_mapper.ConfigurationProvider)
            //.PaginatedListAsync(request.PageNumber, request.PageSize);

        return new Task<PaginatedList<FeatureBriefDto>>(() => new PaginatedList<FeatureBriefDto>(new List<FeatureBriefDto>(1), 1, 1, 1));
    }
}

public class FeatureBriefDto : IMapFrom<Entities.Feature>
{
    public int Id { get; set; }

    // add properties here

    public bool Done { get; set; }
}
