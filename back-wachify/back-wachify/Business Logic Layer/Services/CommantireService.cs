using back_wachify.Business_Logic_Layer.Interfaces;
using back_wachify.Business_Logic_Layer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace back_wachify.Business_Logic_Layer.Services
{
	public class CommantireService : ICommantireService
	{
		private readonly ICommantireRepo _commantireRepo;

		public CommantireService(ICommantireRepo commantireRepo)
		{
			_commantireRepo = commantireRepo;
		}
		public   void AddCommantire(Commantire commantire)
		{
		  _commantireRepo.AddCommantire(commantire);
			

		}

		/*public void DeleteCommantire(int id)
		{
		_commantireRepo.DeleteCommantire(id);

		}

		public async Task<List<Commantire>> GetAllCommantires()
		{
			return await _commantireRepo.GetAllCommantires();
		}

		public async Task<Commantire> GetCommantireparid(int id)
		{
			return await _commantireRepo.GetCommantireparid(id);

		}

		public async Task<List<Commantire>> GetCommantireParIdVedio(int id)
		{
			return await _commantireRepo.GetCommantireParIdVedio(id);

		}

		public void UpdateCommantire(int id, Commantire commantire)
		{
			_commantireRepo.UpdateCommantire(id, commantire);

		}*/
	}
}
