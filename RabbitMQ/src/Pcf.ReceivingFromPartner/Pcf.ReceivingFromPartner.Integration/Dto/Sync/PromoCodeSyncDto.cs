using System;

namespace Pcf.ReceivingFromPartner.Integration.Dto.Sync;

public record PromoCodeSyncDto
{
    public Guid PartnerManagerId { get; init; }
}