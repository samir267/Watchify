using back_wachify.Business_Logic_Layer.Interfaces;
using back_wachify.Business_Logic_Layer.Model;
using back_wachify.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace back_wachify.Business_Logic_Layer.Services
{
    public class PackService: IPackService
    {
        private readonly ApplicationDbContext _context;

        public PackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pack>> GetAllPacks()
        {
            return await _context.Pack.ToListAsync();
        }

        public async Task<Pack> GetPackById(int id)
        {
            return await _context.Pack.FindAsync(id);
        }

        public async Task<int> CreatePack(Pack pack)
        {
            _context.Pack.Add(pack);
            await _context.SaveChangesAsync();
            return pack.Id;
        }

        public async Task<bool> UpdatePack(int id, Pack pack)
        {
            var existingPack = await _context.Pack.FindAsync(id);
            if (existingPack == null)
                return false;

            existingPack.Name = pack.Name;
            existingPack.Durations = pack.Durations;

            _context.Pack.Update(existingPack);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePack(int id)
        {
            var pack = await _context.Pack.FindAsync(id);
            if (pack == null)
                return false;

            _context.Pack.Remove(pack);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
