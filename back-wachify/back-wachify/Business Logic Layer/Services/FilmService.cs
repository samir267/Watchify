using back_wachify.Business_Logic_Layer.Interfaces;
using back_wachify.Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace back_wachify.Business_Logic_Layer.Services
{
	public class FilmService : IFilmService
	{
		private readonly IFilmRepo _filmRepos;

		public FilmService(IFilmRepo _filmRepo) 
		{
			_filmRepos = _filmRepo;
		}
		public Task<Film> AddFilm(IFormFile videoFile, IFormFile logoFile, [FromForm] string titre, [FromForm] string description, [FromForm] DateTime AnnéeDeSortie, [FromForm] string Durée, [FromForm] string Genre, [FromForm] bool isfree, [FromForm] int userid)
		{
			var film = _filmRepos.AddFilm(videoFile, logoFile, titre, description, AnnéeDeSortie, Durée, Genre, isfree, userid);
			return film;
		}

		public async Task<IActionResult> DeleteFilm(int id )
		{
			var film = await _filmRepos.GetFilmById(id);

			if (film == null)
			{
				return new NotFoundResult();
			}

			await _filmRepos.DeleteFilm(id);
			return new NoContentResult();
		}

		public IActionResult GetAllFilm()
		{
			var film=_filmRepos.GetAllFilm();
			return new ObjectResult (film);
		}

		public Task<Film> GetFilmById(int id)
		{
			return _filmRepos.GetFilmById(id);
		}

		public async Task<List<Film>> GetFilmidP(int iduser)
		{
			var film= await _filmRepos.GetFilmidP(iduser);
			return film;
		}

		public async Task<IActionResult> GetFilmTitre(String titre)
		{
			var film = await _filmRepos.GetFilmByTitre(titre);

			if (film == null)
			{
				return new NotFoundResult();
			}

			return new OkObjectResult(film);
		}




		public async Task<string> GetLogoFilePathByUserId(int idUser)
		{
			return await _filmRepos.GetLogoFilePathByUserId(idUser);
		}





		public async Task<IActionResult> UpdateFilm(IFormFile videoFile, IFormFile logoFile, [FromForm] int id, [FromForm] string titre, [FromForm] string description, [FromForm] DateTime AnnéeDeSortie, [FromForm] string Durée, [FromForm] string Genre, [FromForm] bool isfree)
		{
			var film = await _filmRepos.UpdateFilm(videoFile, logoFile,id,titre, description, AnnéeDeSortie, Durée, Genre, isfree);

			return new OkObjectResult(film);
		}

		
	}
}
