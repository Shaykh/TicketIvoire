﻿using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Domain.LieuEvenements.Events;

public record LieuModifieeEvent(Guid LieuId, uint? Capacite, string Nom, string Description, string Adresse, string Ville) : DomainEventBase;
