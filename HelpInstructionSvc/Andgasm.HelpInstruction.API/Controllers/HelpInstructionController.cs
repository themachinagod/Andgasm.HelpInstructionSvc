using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Andgasm.API.Core;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using SE.DynamicHelp.API.Resources;

namespace Andgasm.HelpInstruction.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpInstructionController : ReportableControllerBase
    {
        #region Fields
        private APIDBContext _context;
        #endregion

        #region Constructors
        public HelpInstructionController(APIDBContext context, IMapper datamap, 
                                         DynamicExpressionService expsvc, 
                                         ILogger<HelpInstructionController> logger) : 
            base(datamap, expsvc, logger)
        {
            _context = context;
        }
        #endregion

        #region Get
        [HttpGet(Name = "GetAllHelpInstructions")]
        [SwaggerResponse(typeof(List<HelpInstructionResource>), IsNullable = false,
                         Description = "A collection of HelpInstuctionResources.")]
        public IActionResult GetAll()
        {
            var query = _context.HelpInstructions.AsNoTracking();
            var result = _datamap.Map<List<HelpInstructionResource>>(query.ToList());
            return Ok(result);
        }

        [HttpGet("{internalId}", Name = "GetHelpInstructionById")]
        [SwaggerResponse(typeof(HelpInstructionResource), IsNullable = false,
                         Description = "A single HelpInstuctionResource for the specified id.")]
        public async Task<IActionResult> GetById(int internalId)
        {
            if (internalId <= 0) return BadRequest(string.Format(Constants.InvalidIdProvided, internalId));
            var match = await _context.HelpInstructions.FindAsync(internalId);
            if (match == null) return NotFound(string.Format(Constants.IdNotFound, internalId));
            var result = _datamap.Map<HelpInstructionResource>(match);
            return Ok(result);
        }

        [HttpGet("Lookup/{key}", Name = "GetHelpInstructionByLookupKey")]
        [SwaggerResponse(typeof(HelpInstructionResource), IsNullable = false,
                         Description = "A single HelpInstuctionResource for the specified lookup key.")]
        public async Task<IActionResult> GetByLookupKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return BadRequest(string.Format(Constants.InvalidLookupProvided, key));
            var match = await _context.HelpInstructions.FirstOrDefaultAsync(x => x.LookupKey == key);
            if (match == null) return NotFound(string.Format(Constants.LookupNotFound, key));
            var result = _datamap.Map<HelpInstructionResource>(match);
            return Ok(result);
        }

        [HttpPost("Report", Name = "ReportHelpInstructions")] // DBr: would rather use a REPORT verb but not supported by openapi!
        [SwaggerResponse(typeof(List<HelpInstructionResource>), IsNullable = false,
                         Description = "A collection of HelpInstuctionResources that match the specified reporting parameters.")]
        public IActionResult ReportByOptions([FromBody]ReportOptions options)
        {
            if (options == null) options = new ReportOptions() { skip = 0, take = Constants.DefaultReportPageSize };
            if (options.take < 1) return BadRequest(Constants.InvalidPagingTakeOptions);
            if (options.skip < 0) return BadRequest(Constants.InvalidPagingSkipOptions);
            var query = GetQueryForReportOptions<HelpInstruction, HelpInstructionResource>(_context.HelpInstructions.AsNoTracking(), options);
            var result = _datamap.Map<List<HelpInstructionResource>>(query.ToList());
            return Ok(result);
        }
        #endregion

        #region Create
        [HttpPost(Name = "CreateHelpInstruction")]
        [SwaggerResponse(typeof(HelpInstructionResource), IsNullable = false,
                         Description = "The created HelpInstuctionResource.")]
        public async Task<IActionResult> Create([FromBody]HelpInstructionResource resource)
        {
            if (resource == null) return BadRequest(Constants.InvalidRequestPayload);
            var existsbyid = _context.HelpInstructions.Find(resource.InternalId) != null;
            var existsbykey = _context.HelpInstructions.Any(x => x.LookupKey == resource.LookupKey);
            if (!existsbyid && !existsbykey)
            {
                var result = _datamap.Map<HelpInstruction>(resource);
                _context.HelpInstructions.Add(result);
                await _context.SaveChangesAsync();
                resource.InternalId = result.Id;
                return Ok(resource);
            }
            else if (existsbyid) return Conflict(string.Format(Constants.PrimaryKeyViolation, resource.InternalId));
            else return Conflict(string.Format(Constants.LookupKeyViolation, resource.LookupKey));
        }
        #endregion

        #region Update
        [HttpPut("{id:int}", Name = "UpdateHelpInstruction")]
        [SwaggerResponse(typeof(HelpInstructionResource), IsNullable = false,
                         Description = "The updated HelpInstuctionResource.")]
        public async Task<IActionResult> UpdateById(int id, [FromBody] HelpInstructionResource resource)
        {
            if (id <= 0) return BadRequest(string.Format(Constants.InvalidIdProvided, id));
            if (resource == null) return BadRequest(Constants.InvalidRequestPayload);
            var res = _context.HelpInstructions.Find(id);
            if (res != null)
            {
                res = _datamap.Map(resource, res);
                _context.Entry(res).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(resource);
            }
            else return NotFound(string.Format(Constants.IdNotFound, id));
        }
        #endregion

        #region Delete
        [HttpDelete("{id:int}", Name = "DeleteHelpInstruction")]
        [SwaggerResponse(null, IsNullable = false)]
        public async Task<IActionResult> DeleteById(int id)
        {
            if (id <= 0) return BadRequest(string.Format(Constants.InvalidIdProvided, id));
            var res = _context.HelpInstructions.Find(id);
            if (res == null) return NotFound(string.Format(Constants.IdNotFound, id));
            _context.HelpInstructions.Remove(res);
            await _context.SaveChangesAsync();
            return Ok();
        }
        #endregion

        #region Options
        [HttpOptions]
        public IActionResult Options()
        {
            return Ok();
        }

        [HttpOptions("{id}")]
        public IActionResult Options(int id)
        {
            return Ok();
        }
        #endregion
    }
}