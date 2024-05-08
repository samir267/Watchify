using back_wachify.Business_Logic_Layer.Interfaces;
using back_wachify.Business_Logic_Layer.Model;
using back_wachify.Business_Logic_Layer.Services;
using back_wachify.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Xml.Linq;

namespace back_wachify.Presentation_Layer.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class FilmController : ControllerBase
	{
		private readonly IFilmService _filmService;
		private readonly string _uploadFolder = "/UploadedVideos"; // Directory to store uploaded videos
		private readonly string _uploadLogo = "/UploadedLogo"; // Directory to store uploaded videos

		public FilmController(IFilmService filmService)
		{
			_filmService = filmService;
		}

		[HttpPost("AddFilm")]
		public async Task<IActionResult>  AddFilm(IFormFile videoFile, IFormFile logoFile, [FromForm] string titre, [FromForm] string description, [FromForm] DateTime AnnéeDeSortie, [FromForm] string Durée, [FromForm] string Genre, [FromForm] bool isfree, [FromForm] int userid)
		{
			var AddFilm = await _filmService.AddFilm(videoFile, logoFile, titre, description, AnnéeDeSortie, Durée, Genre, isfree, userid);
			if (AddFilm != null)
			{
				return Ok(new { AddFilm = AddFilm });
			}
			else
			{
				return BadRequest(new { Message = AddFilm });
			}

		}

		[HttpGet("all")]
		//this methode return getallfilm
		public IActionResult GetAllFilms()
		{
			var films = _filmService.GetAllFilm();
			return Ok(films);
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetFilmparid(int id)
		{

			var film = await _filmService.GetFilmById(id);
			if (film == null)
			{
				return NotFound();
			}
			return Ok(film);
		}

		[HttpGet("titre/{Titre}")]
		public async Task<IActionResult> GetFilmTitre(string Titre)
		{
			return await _filmService.GetFilmTitre(Titre);
		}


		[HttpGet("user/{iduser}")]
		public async Task<IActionResult> GetFilmidP(int iduser)
		{
			try
			{
				var film = await _filmService.GetFilmidP(iduser);

				if (film == null)
				{
					return NotFound();
				}

				return Ok(film);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
			}
		}

		


		[HttpGet("logo/{id}")]
		public async Task<IActionResult> GetLogoFilePathByUserId(string id)
		{
			try
			{
				var filePath = Path.Combine(_uploadLogo, id);

				if (!System.IO.File.Exists(filePath))
				{
					return NotFound(); // Si le fichier image n'existe pas, retourne 404 Not Found
				}

				// Lire l'image en tant que tableau de bytes
				var imageBytes = System.IO.File.ReadAllBytes(filePath);

				// Déterminer le type MIME de l'image
				var contentType = GetContentType(filePath);

				// Retourner le contenu de l'image avec le type de contenu approprié
				return File(imageBytes, contentType);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
			}
		}

	

		// Méthode pour obtenir le type MIME en fonction de l'extension du fichier
		private string GetContentType(string path)
		{
			var types = new Dictionary<string, string>
	{
		{".png", "image/png"},
		{".jpg", "image/jpeg"},
		{".jpeg", "image/jpeg"},
		{".gif", "image/gif"},
		{".bmp", "image/bmp"},
		{".tiff", "image/tiff"}
	};

			var ext = Path.GetExtension(path).ToLowerInvariant();
			return types[ext];
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFilm(int id)
		{
			return await _filmService.DeleteFilm(id);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateFilm(IFormFile videoFile, IFormFile logoFile, [FromForm] int id, [FromForm] string titre, [FromForm] string description, [FromForm] DateTime AnnéeDeSortie, [FromForm] string Durée, [FromForm] string Genre, [FromForm] bool isfree)
		{
			var film = await _filmService.UpdateFilm(videoFile, logoFile, id, titre, description, AnnéeDeSortie, Durée, Genre, isfree);

			return Ok(film);
		}

		/// GET: api/Uploadvedio/{fileName}
		// This method returns video by title
		[HttpGet("video/{titre}")]
		public IActionResult GetVideo(string titre)
		{
			var filePath = Path.Combine(_uploadFolder, titre);

			if (!System.IO.File.Exists(filePath))
			{
				return NotFound(); // If the video does not exist, return 404 Not Found
			}

			// Read the video as a byte array
			var videoBytes = System.IO.File.ReadAllBytes(filePath);

			// Return the video content with the appropriate content type
			return File(videoBytes, "video/mp4"); // Adjust the content type according to your video format
		}

	}
}
