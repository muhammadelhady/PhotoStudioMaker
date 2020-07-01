using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data.Interfaces
{
    public interface IPartnerRepo
    {
        public Task<Partner> AddPartner(PartnerDto partnerDto );
        public Task<bool> DeletePartner(DeletePartnerDto deletePartnerDto);
        public Task<List<Partner>> GetPartners();
    }
}
