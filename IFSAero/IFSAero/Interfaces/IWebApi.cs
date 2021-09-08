using IFSAero.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IFSAero.Interfaces
{
    public interface IWebApi
    {
        [Get("/api/technologies")]
        Task<List<Technology>> GetTechnologies();

        [Get("/api/candidates")]
        Task<List<Candidate>> GetCandidates();
    }
}
