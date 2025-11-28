using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.SyncAPI.Data;
using MyApp.SyncAPI.Dtos;
using MyApp.SyncAPI.Models;
using System.Reflection;
using System.Text.Json;

namespace MyApp.SyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SyncController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Sync([FromBody] SyncRequestDto request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.EntityType))
                return BadRequest("Invalid request.");

            var entityType = Assembly.GetExecutingAssembly()
                                     .GetTypes()
                                     .FirstOrDefault(t => t.Namespace == "MyApp.SyncAPI.Models"
                                                      && t.Name.Equals(request.EntityType, StringComparison.OrdinalIgnoreCase));

            if (entityType == null)
                return BadRequest($"Entity '{request.EntityType}' does not exist.");

            var listType = typeof(List<>).MakeGenericType(entityType);
            var entities = request.Data.Deserialize(listType,
                                                    new JsonSerializerOptions
                                                    {
                                                        PropertyNameCaseInsensitive = true
                                                    }) as System.Collections.IList;

            if (entities == null || entities.Count == 0)
                return BadRequest("No entities to sync.");

            var entityTypeModel = _context.Model.FindEntityType(entityType);
            var keyProps = entityTypeModel.FindPrimaryKey().Properties; 

            var setMethod = typeof(DbContext).GetMethod("Set", Type.EmptyTypes)!.MakeGenericMethod(entityType);
            dynamic dbSet = setMethod.Invoke(_context, null)!;

            foreach (var entity in entities)
            {
                var keyValues = keyProps
                    .Select(k => entityType.GetProperty(k.Name)!.GetValue(entity))
                    .ToArray();

                var existing = await dbSet.FindAsync(keyValues);

                if (existing == null)
                {
                    await dbSet.AddAsync((dynamic)entity);
                }
                else
                {
                    _context.Entry(existing).CurrentValues.SetValues(entity);
                }
            }

            await _context.SaveChangesAsync();

            return Ok($"Synced {entities.Count} {request.EntityType}(s) successfully.");
        }
    }
}
