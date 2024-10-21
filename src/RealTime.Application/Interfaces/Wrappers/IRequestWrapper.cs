using MediatR;

namespace RealTime.Application.Interfaces.Wrappers;

public interface IRequestWrapper<T> : IRequest<T>
{
}