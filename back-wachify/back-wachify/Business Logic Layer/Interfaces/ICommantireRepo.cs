using back_wachify.Business_Logic_Layer.Model;
using Microsoft.AspNetCore.Mvc;

namespace back_wachify.Business_Logic_Layer.Interfaces
{
	public interface ICommantireRepo
	{
		public Task<Commantire> AddCommantire(Commantire commantire);

		//public Task<List< Commantire>> GetAllCommantires();
		//public Task<Commantire> GetCommantireparid(int id);

	/*	public Task<List<Commantire>> GetCommantireParIdVedio(int id);

		public void UpdateCommantire(int id, Commantire commantire);
		public void DeleteCommantire(int id);
		
		*/
	}
}
