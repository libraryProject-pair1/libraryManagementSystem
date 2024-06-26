using NArchitecture.Core.Application.Responses;

namespace Application.Features.Publishers.Queries.GetById;

public class GetByIdPublisherResponse : IResponse
{
    public Guid Id { get; set; }
    public string PublisherName { get; set; }
    public string Adress { get; set; }
    public string Phone { get; set; }
}