﻿using MediatR;

namespace TicketIvoire.Shared.Application.Queries;

public interface IQuery<out TResult> : IRequest<TResult>
{
}
