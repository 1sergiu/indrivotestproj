using IndrivoTestProj.Data.Models;
using IndrivoTestProj.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndrivoTestProj.Data.Services
{
    public class ClassifierService
    {
        private readonly AppDbContext _context;

        public ClassifierService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddClassifierAsync(ClassifierVM classifier)
        {
            var _classifier = new Classifier
            {
                Title = classifier.Title,
            };

            _context.Classifiers.Add(_classifier);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Classifier>> GetAllClassifiersAsync() => await _context.Classifiers.ToListAsync();

        public async Task<Classifier> GetClassifierByIdAsync(Guid guid) => await _context.Classifiers.FirstOrDefaultAsync(c => c.Guid == guid);

        public async Task<Classifier> UpdateClassifierByIdAsync(Guid classifierId, ClassifierVM classifier)
        {
            var _classifier = await _context.Classifiers.FirstOrDefaultAsync(c => c.Guid == classifierId);
            if (_classifier != null)
            {
                _classifier.Title = classifier.Title;
                await _context.SaveChangesAsync();
            }
            return _classifier;
        }

        public async Task DeleteClassifierByIdAsync(Guid classifierId)
        {
            var _classifier = await _context.Classifiers.FirstOrDefaultAsync(n => n.Guid == classifierId);
            if (_classifier != null)
            {
                _context.Classifiers.Remove(_classifier);
                await _context.SaveChangesAsync();
            }
        }
    }
}