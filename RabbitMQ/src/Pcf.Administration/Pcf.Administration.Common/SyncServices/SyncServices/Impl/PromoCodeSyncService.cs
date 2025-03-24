using Microsoft.Extensions.Logging;
using Pcf.Administration.Common.Dto.Sync;
using Pcf.Administration.Core.Abstractions.Repositories;
using Pcf.Administration.Core.Domain.Administration;

namespace Pcf.Administration.Common.SyncServices.SyncServices.Impl;

public class PromoCodeSyncService(ILogger<PromoCodeSyncService> logger, IRepository<Employee> employeeRepository) : ISyncService<PromoCodeSyncDto>
{
    private readonly ILogger<PromoCodeSyncService> _logger = logger;
    private readonly IRepository<Employee> _employeeRepository = employeeRepository;
    
    public async Task SyncAsync(PromoCodeSyncDto message, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(message.PartnerManagerId);

        if (employee is null)
        {
            _logger.LogError($"{nameof(Employee)} with Id: {message.PartnerManagerId} could not be found.");
            
            return;
        }

        employee.AppliedPromocodesCount++;

        await _employeeRepository.UpdateAsync(employee);
    }
}