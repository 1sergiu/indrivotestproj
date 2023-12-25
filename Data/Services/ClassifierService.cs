using IndrivoTestProj.Data.Models;
using IndrivoTestProj.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IndrivoTestProj.Exceptions;

namespace IndrivoTestProj.Data.Services
{
    public class ClassifierService
    {
        private readonly AppDbContext _context;

        public ClassifierService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddClassifierAsync(ClassifierVM classifier, CancellationToken cancellationToken = default)
        {
            var _classifier = new Classifier
            {
                Title = classifier.Title,
            };

            _context.Classifiers.Add(_classifier);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Classifier>> GetAllClassifiersAsync(CancellationToken cancellationToken = default)
            => await _context.Classifiers.ToListAsync(cancellationToken);

        public async Task<Classifier> GetClassifierByIdAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            var _classifier = await _context.Classifiers.FirstOrDefaultAsync(c => c.Guid == guid, cancellationToken);

            if (_classifier == null)
            {
                throw new GuidNotFoundException($"Classifier with id: {guid} not found");
            }
            return _classifier;
        }

        public async Task<Classifier> UpdateClassifierByIdAsync(Guid classifierId, ClassifierVM classifier, CancellationToken cancellationToken = default)
        {
            var _classifier = await _context.Classifiers.FirstOrDefaultAsync(c => c.Guid == classifierId, cancellationToken);
            if (_classifier == null)
            {
                throw new GuidNotFoundException($"Classifier with id: {classifierId} not found.");
            }
            if (_classifier != null)
            {
                _classifier.Title = classifier.Title;
                await _context.SaveChangesAsync(cancellationToken);
            }
            return _classifier;
        }

        public async Task DeleteClassifierByIdAsync(Guid classifierId, CancellationToken cancellationToken = default)
        {
            var _classifier = await _context.Classifiers.FirstOrDefaultAsync(n => n.Guid == classifierId, cancellationToken);
            if (_classifier == null)
            {
                throw new GuidNotFoundException($"Classifier with id: {classifierId} not found.");
            }

            if (_classifier != null)
            {
                _context.Classifiers.Remove(_classifier);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}