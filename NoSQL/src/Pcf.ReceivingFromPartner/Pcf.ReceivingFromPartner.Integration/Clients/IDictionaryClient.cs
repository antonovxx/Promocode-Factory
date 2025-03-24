using System.Collections.Generic;
using System.Threading.Tasks;
using Pcf.ReceivingFromPartner.Core.Domain;

namespace Pcf.ReceivingFromPartner.Integration.Clients;

public interface IDictionaryClient
{
    Task<List<Preference>> GetPreferencesAsync();
}