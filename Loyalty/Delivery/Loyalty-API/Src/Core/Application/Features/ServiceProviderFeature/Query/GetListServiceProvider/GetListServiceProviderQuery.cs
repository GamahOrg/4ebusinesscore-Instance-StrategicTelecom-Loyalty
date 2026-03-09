using Domain.Utilities.Enums;
using DTO;
using MediatR;

namespace _4EPlatform.Application.Features.ServiceProviderFeature.Query.GetListServiceProvider
{
    public class GetListServiceProviderQuery : IRequest<ResponseDTO>
    {
        public Status? Status { get; set; }
    }
}
