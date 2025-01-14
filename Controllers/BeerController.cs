using BackendLearnUdemy.DataTransferObjects;
using BackendLearnUdemy.Models;
using BackendLearnUdemy.Services.BeerStore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BackendLearnUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private IValidator<BeerInsertDTO> _beerInsertValidator;
        private IValidator<BeerUpdateDTO> _beerUpdateValidator;

        private ICommonService<BeerDTO, BeerInsertDTO,BeerUpdateDTO> _beerService;
        public BeerController(StoreContext storeContext, 
                              IValidator<BeerInsertDTO> beerInsertValidator,
                              IValidator<BeerUpdateDTO> beerUpdateValidator,
                              [FromKeyedServices("beerService")]ICommonService<BeerDTO, BeerInsertDTO, BeerUpdateDTO> beerService)
        {
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator= beerUpdateValidator;
            _beerService = beerService;
        }


        [HttpGet]
        public async Task<IEnumerable<BeerDTO>> Get()
        {
            return await _beerService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDTO>> GetById(int id)
        {
           var beerDto= await _beerService.GetById(id);

            return beerDto is not null ? Ok(beerDto) : NotFound();
        }


        [HttpPost]
        public async Task<ActionResult<BeerDTO>> Post(BeerInsertDTO beerInsertDTO)
        {
            //Format validation can be in the controller
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDTO);

            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            var beerDTO= await _beerService.Add(beerInsertDTO);

            //First param: URL to get resource
            //Second param: param needed for the first param (the URL route) to work
            //Third param: final object

            return CreatedAtAction(nameof(GetById), new { id = beerDTO.Id }, beerDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDTO>> Update(int id, BeerUpdateDTO beerUpdateDTO)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDTO);

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            
            BeerDTO beerDTO= await _beerService.Update(id, beerUpdateDTO);

            if (beerDTO is null) return NotFound();

            return Ok(beerDTO);
        }


        [HttpDelete("{id}")]
        public async Task <ActionResult<BeerDTO>> Delete(int id)
        {

            var beerDTO = await _beerService.Delete(id);
            if (beerDTO is null) return NotFound();
            return Ok(beerDTO);
        }
    }

}
