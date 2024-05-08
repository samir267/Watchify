using back_wachify.Business_Logic_Layer.Interfaces;
using back_wachify.Data;
using back_wachify.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace back_wachify.Data_Layer.Repositroy
{
	public class FilmRepo : IFilmRepo
	{
		private readonly string _uploadFolder = "/UploadedVideos"; // Directory to store uploaded videos
		private readonly string _uploadLogo = "/UploadedLogo"; // Directory to store uploaded videos


		//private readonly List<Film> _films = new List<Film>(); // Simulation des données en mémoire (vous devrez utiliser une base de données réelle)
		private readonly ApplicationDbContext _db;
		public FilmRepo(ApplicationDbContext _db)
		{
			this._db = _db;
		}

		//methode ajouter film en table filme
		public async Task<Film> AddFilm(IFormFile videoFile, IFormFile logoFile, [FromForm] string titre, [FromForm] string description, [FromForm] DateTime DateDeSortie, [FromForm] string Duree, [FromForm] string Genre, [FromForm] bool IsFree, [FromForm] int UserId)
		{
			try
			{
				if (videoFile == null || videoFile.Length == 0)
				{
					throw new ArgumentException("No video file detected.");
				}
				// Create directory if it doesn't exist
				if (!Directory.Exists(_uploadFolder))
				{
					Console.WriteLine("dossier video");
					Directory.CreateDirectory(_uploadFolder);   
				}
				// Create directory if it doesn't exist logo
				if (!Directory.Exists(_uploadLogo))
				{
					Console.WriteLine("taswira");

					Directory.CreateDirectory(_uploadLogo);
				}

				var uniqueFileName = Guid.NewGuid().ToString() + "_" + videoFile.FileName;
				var uniqueFileNamelogo = Guid.NewGuid().ToString() + "_" + logoFile.FileName;

				// Combine the upload folder path with the unique file name
				var filePath = Path.Combine(_uploadFolder, uniqueFileName);
				var filePathlogo = Path.Combine(_uploadLogo, uniqueFileNamelogo);


				// Save the video file to the server
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await videoFile.CopyToAsync(fileStream);

				}

				using (var fileStreamlogo = new FileStream(filePathlogo, FileMode.Create))
				{
					await logoFile.CopyToAsync(fileStreamlogo);

				}

				var film = new Film
				{
					VideoFilePath = uniqueFileName, // Ajustez selon comment vous voulez enregistrer le chemin
					Titre = titre,
					Description = description,
					LogoFilePath = uniqueFileNamelogo,
					DateDeSortie = DateDeSortie,
					Duree = Duree,
					Genre = Genre,
					IsFree = IsFree,
					UserId = UserId,

				};
				_db.Film.Add(film); // Assurez-vous que votre DbSet s'appelle Films ou ajustez selon le nom réel
				await _db.SaveChangesAsync();
				// You may save the file path to a database or return it as response
				//var fileUrl = Path.Combine("~", filePath);

				return film;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task DeleteFilm(int id)
		{
			var film = await _db.Film.FindAsync(id);
			if (film != null)
			{
				_db.Film.Remove(film);
				await _db.SaveChangesAsync();
			}
		}



		public List<Film> GetAllFilm()
		{
			var films = _db.Film.ToList();
			return films;
		}



		public async Task<Film> GetFilmByTitre(string name)
		{
			var film = await _db.Film.FirstOrDefaultAsync(f => f.Titre == name);
			return film;
		}



		public async Task<Film> UpdateFilm(IFormFile videoFile, IFormFile logoFile, [FromForm] int id, [FromForm] string titre, [FromForm] string description, [FromForm] DateTime AnnéeDeSortie, [FromForm] string Durée, [FromForm] string Genre, [FromForm] bool isfree)
		{
			try
			{
				var film = await _db.Film.FindAsync(id);

				if (film != null)
				{
					throw new ArgumentException("No film  detected.");

				}
				// Mettre à jour les propriétés du film avec les nouvelles valeurs
				film.Titre = titre;
				film.Description = description;
				film.DateDeSortie = AnnéeDeSortie;
				film.Genre = Genre;
				film.IsFree = isfree;

				// Vérifier s'il y a un nouveau fichier vidéo
				if (videoFile != null && videoFile.Length > 0)
				{
					// Supprimer l'ancien fichier vidéo s'il existe
					if (System.IO.File.Exists(film.VideoFilePath))
					{
						System.IO.File.Delete(film.VideoFilePath);
					}

					// Enregistrer le nouveau fichier vidéo
					var uniqueVideoFileName = Guid.NewGuid().ToString() + "_" + videoFile.FileName;
					var videoFilePath = Path.Combine(_uploadFolder, uniqueVideoFileName);
					film.VideoFilePath = videoFilePath;

					using (var fileStream = new FileStream(videoFilePath, FileMode.Create))
					{
						await videoFile.CopyToAsync(fileStream);
					}
				}

				// Vérifier s'il y a un nouveau fichier logo
				if (logoFile != null && logoFile.Length > 0)
				{
					// Supprimer l'ancien fichier logo s'il existe
					if (System.IO.File.Exists(film.LogoFilePath))
					{
						System.IO.File.Delete(film.LogoFilePath);
					}

					// Enregistrer le nouveau fichier logo
					var uniqueLogoFileName = Guid.NewGuid().ToString() + "_" + logoFile.FileName;
					var logoFilePath = Path.Combine(_uploadLogo, uniqueLogoFileName);
					film.LogoFilePath = logoFilePath;

					using (var fileStream = new FileStream(logoFilePath, FileMode.Create))
					{
						await logoFile.CopyToAsync(fileStream);
					}
				}

				await _db.SaveChangesAsync();

				return film; // Retourner le film mis à jour

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	






		public async Task<Film> GetFilmById(int id)
		{
			var film = await _db.Film.FindAsync(id);
			return film;
		}

		

		public async Task<List<Film>> GetFilmidP(int iduser)
		{
			var film = await _db.Film.Where(f => f.UserId == iduser).ToListAsync();
			
			return film;
		}

		public async Task<string> GetLogoFilePathByUserId(int idUser)
		{
			var film = await _db.Film.FirstOrDefaultAsync(f => f.UserId == idUser);
			return film != null ? film.LogoFilePath : null;
		}



		/*public async Task<Film> GetFilmByidpart(int idp)
		{
			var film = await _db.Film.FirstOrDefaultAsync(f => f.UserId == idp);
			return film;

		}*/
	}
}
