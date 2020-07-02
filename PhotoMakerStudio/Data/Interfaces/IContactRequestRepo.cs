using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data.Interfaces
{
    public interface IContactRequestRepo
    {
        public Task<List<ContactRequest>> GetAllRequests();
        public Task<bool> DeleteContactRequest(DeleteContactRequestDto deleteContactRequestDto);
        public Task<ContactRequest> AddNewContactRequest(ContactRequestDto contactRequestDto);
    }
}
