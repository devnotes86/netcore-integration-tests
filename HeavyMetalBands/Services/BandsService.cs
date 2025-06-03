using HeavyMetalBands.Models;
using HeavyMetalBands.Repositories;
using AutoMapper;

namespace HeavyMetalBands.Services
{
  
    public class BandsService: IBandsService
    {
        private readonly IBandsRepository _bandsRepository;
        private readonly IMapper _mapper;

        public BandsService(IBandsRepository bandRepository, IMapper mapper)
        {
            _bandsRepository = bandRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BandDTO>> GetAllAsync()
        {
            var bands = await _bandsRepository.GetAllAsync();
            var bandDTOs = _mapper.Map<List<BandDTO>>(bands);
            return bandDTOs.ToList();
        }

        public async Task<BandDTO> GetByIdAsync(int id)
        {
            var band = await _bandsRepository.GetByIdAsync(id);
            var bandDTO = _mapper.Map<BandDTO>(band);
            return bandDTO;
        }

        public async Task AddAsync(BandDTO bandDTO)
        {
            var bandDAO = _mapper.Map<BandDAO>(bandDTO);
            await _bandsRepository.AddAsync(bandDAO);
        }

        public async Task UpdateAsync(BandDTO bandDTO)
        { 
            var bandDAO = _mapper.Map<BandDAO>(bandDTO);
            await _bandsRepository.UpdateAsync(bandDAO);
        }

        public async Task DeleteAsync(int id) => await _bandsRepository.DeleteAsync(id);
    }
}
