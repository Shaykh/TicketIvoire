﻿using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Domain.Membres.Events;
public record MembreValideEvent(Guid MembreId) : DomainEventBase
{
}
