using IndrivoTestProj.Data.Models;
using IndrivoTestProj.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndrivoTestProj.Data.Services
{
    public class EntityService
    {
        private readonly AppDbContext _context;

        public EntityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddEntityAsync(EntityVM entity, CancellationToken cancellationToken = default)
        {
            //Verifing if Classfier exists
            var classifierExists = await _context.Classifiers.AnyAsync(c => c.Guid == entity.TypeGuid);
            if (!classifierExists)
            {
                throw new InvalidOperationException("Classifier with this TypeGuid not found");
            }

            var _entity = new Entity
            {
                Title = entity.Title,
                Description = entity.Description,
                TypeGuid = entity.TypeGuid,
            };

            _context.Entities.Add(_entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Entity>> GetAllEntitiesAsync(CancellationToken cancellationToken = default)
            => await _context.Entities.ToListAsync(cancellationToken);

        public async Task<Entity> GetEntityByIdAsync(Guid guid, CancellationToken cancellationToken = default)
            => await _context.Entities.FirstOrDefaultAsync(n => n.Guid == guid, cancellationToken);

        public async Task<Entity> UpdateEntityByIdAsync(Guid entityId, EntityVM entity, CancellationToken cancellationToken = default)
        {
            var _entity = await _context.Entities.FirstOrDefaultAsync(c => c.Guid == entityId, cancellationToken);
            if (_entity != null)
            {
                _entity.Title = entity.Title;
                _entity.Description = entity.Description;
                _entity.TypeGuid = entity.TypeGuid;
                await _context.SaveChangesAsync(cancellationToken);
            }
            return _entity;
        }
        public async Task DeleteEntityByIdAsync(Guid entityId, CancellationToken cancellationToken = default)
        {
            var _entity = await _context.Entities.FirstOrDefaultAsync(n => n.Guid == entityId, cancellationToken);
            if (_entity != null)
            {
                _context.Entities.Remove(_entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
