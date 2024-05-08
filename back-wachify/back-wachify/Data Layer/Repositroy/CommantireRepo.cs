using back_wachify.Business_Logic_Layer.Interfaces;
using back_wachify.Business_Logic_Layer.Model;
using back_wachify.Data;
using Microsoft.EntityFrameworkCore;

namespace back_wachify.Data_Layer.Repositroy
{
	public class CommantireRepo : ICommantireRepo
	{

		private readonly ApplicationDbContext _db;
		public  CommantireRepo(ApplicationDbContext _db)
		{
			this._db = _db;
		}
		public async Task<Commantire> AddCommantire(Commantire commantire)
		{
			_db.Commantire.Add(commantire);
			await _db.SaveChangesAsync();
			return commantire;
			
		}
		/*
		public async void DeleteCommantire(int id)
		{
			var com= await  _db.Commantire.FindAsync(id);
			if(com == null)
			{
				throw new ArgumentException("Commantire not found");
			}
			_db.Commantire.Remove(com);
			await _db.SaveChangesAsync();
		}

		public async  Task<List<Commantire>> GetAllCommantires()
		{
			var com =  await _db.Commantire.ToListAsync();
			
			return com;
		}

		public async Task<Commantire> GetCommantireparid(int id)
		{
			var com = await _db.Commantire.FindAsync(id);
			if (com == null)
			{
				throw new ArgumentException("Commantire not found");
			}
			else
			{
				return com;
			}
		}

		public async Task<List<Commantire>> GetCommantireParIdVedio(int id)
		{
			return await _db.Commantire.Where(c => c.IdFilm == id).ToListAsync();
			

		}

		public async void UpdateCommantire(int id, Commantire commantire)
		{
			if (id != commantire.Id)
			{
				throw new ArgumentException("Id mismatch");
			}

			_db.Entry(commantire).State = EntityState.Modified;

			try
			{
				await _db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CommantireExists(id))
				{
					throw new ArgumentException("Commantire not found");
				}
				else
				{
					throw;
				}
			};
		}


		private bool CommantireExists(int id)
		{
			return _db.Commantire.Any(e => e.Id == id);
		}*/
	}
}
