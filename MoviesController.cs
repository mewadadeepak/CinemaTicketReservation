
namespace ConsoleCinemaTicketbooking
{
    using System.Linq;
    using System.Web.Mvc;

    using Models;

    using Services.Contracts;

    using ViewModels;

    public class MoviesController : Controller
    {
        #region Fields

        private IMoviesService movies;
        private IPeopleService people;
        private IStudiosService studios;

        #endregion Fields

        #region Constructors

        public MoviesController(IMoviesService movies, IStudiosService studios, IPeopleService people)
        {
            this.movies = movies;
            this.studios = studios;
            this.people = people;
        }

        #endregion Constructors

        #region Methods

        [HttpGet]
        public ActionResult AddMovie()
        {
            var addMovieViewModel = new AddMovieViewModel();
            addMovieViewModel.People = this.people
                 .GetAll()
                 .Select(p => new SelectListItem()
                 {
                     Text = p.FirstName + p.LastName,
                     Value = p.Id.ToString()
                 })
                 .ToList();

            addMovieViewModel.Studios = this.studios
                .GetAll()
                .Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                })
                .ToList();

            return this.PartialView("_AddMoviePartial", addMovieViewModel);
        }

        [HttpPost]
        public ActionResult AddMovie(Movie movie)
        {
            if (this.ModelState.IsValid)
            {
                this.movies.Create(movie);
            }

            return this.RedirectToAction("AllMovies");
        }

        public ActionResult AllMovies()
        {
            var movies = this.movies.GetAll().ToList();
            return this.PartialView("_AllMoviesPartial", movies);
        }

        [HttpPost]
        public ActionResult DeleteMovie(int id)
        {
            this.movies.Delete(id);

            return this.RedirectToAction("AllMovies");
        }

        public ActionResult EditMovie(int id)
        {
            var movie = this.movies
                .GetAll()
                .Where(m => m.Id == id)
                .Select(m => new EditMovieViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Year = m.Year,
                    StudioId = m.StudioId,
                    DirectorId = m.DirectorId,
                    LeadingFemaleRoleId = m.LeadingFemaleRoleId,
                    LeadingMaleRoleId = m.LeadingMaleRoleId
                })
                .FirstOrDefault();

            movie.Studios = this.studios
                 .GetAll()
                 .Select(s => new SelectListItem()
                 {
                     Value = s.Id.ToString(),
                     Text = s.Name
                 })
                 .ToList();

            movie.People = this.people
                .GetAll()
                .Select(p => new SelectListItem()
                {
                    Value = p.Id.ToString(),
                    Text = p.FirstName + " " + p.LastName
                })
                .ToList();

            return this.PartialView("_EditMoviePartial", movie);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MovieDetails(int id)
        {
            var movie = this.movies
                .GetAll()
                .Where(m => m.Id == id)
                .Select(m => new MovieDetailsViewModel()
                {
                    Title = m.Title,
                    Year = m.Year,
                    Director = m.Director.FirstName + " " + m.Director.LastName,
                    LeadingMaleRole = m.LeadingMaleRole.FirstName + " " + m.LeadingMaleRole.LastName,
                    LeadingFemaleRole = m.LeadingFemaleRole.FirstName + " " + m.LeadingFemaleRole.LastName
                })
                .FirstOrDefault();

            return this.PartialView("_MovieDetailsPartial", movie);
        }

        public ActionResult UpdateMovie(int id, Movie movie)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("AllMovies");
            }

            this.movies.Update(movie);

            return this.RedirectToAction("AllMovies");
        }

        #endregion Methods
    }
}

//using System.Net;

//namespace Movies.Web.Controllers
//{
  //  using System.Linq;
  //  using System.Web.Mvc;
    //using AutoMapper.QueryableExtensions;
   // using Data.Models;
   // using Data.Services.Contracts;
   // using Models;
  //  using Ninject;

  //  public class MoviesController : Controller
   // {
       // [Inject]
       // public IMoviesService MoviesService { get; set; }

       // [HttpGet]
       // public ActionResult All()
       // {
          //  var movies = this.MoviesService.GetAll().ProjectTo<MovieViewModel>().ToList();

          //  return this.View(movies);
       // }

      //  [HttpGet]
      //  public ActionResult Details(string id)
       // {
       //     var movie = this.MoviesService.GetById(int.Parse(id));

          //  return this.View(movie);
        }

      //  public ActionResult Create()
       // {
          //  return PartialView("_Create");
        //}

       // [HttpPost]
       // [ValidateAntiForgeryToken]
      //  public ActionResult Create(Movie movie)
       // {
         //   if (!ModelState.IsValid)
          //  {
             //   return PartialView("_Create");
          //  }

         //   this.MoviesService.Create(movie);
         //   return RedirectToAction("All");
       // }

        //[HttpGet]
       // public ActionResult Edit(int id)
       // {

        //    var movie = this.MoviesService.GetById(id);

        //    return PartialView("_Edit", movie);
       // }

       // [HttpPost]
       // [ValidateAntiForgeryToken]
      //  public ActionResult Edit(MovieDetailedViewModel model)
      //  {
          //  if (!ModelState.IsValid)
        //    {
        //        return PartialView("_Edit");
         //   }

          //   var movie = MoviesService.GetById(model.Id);
          //   movie.Title = model.Title;
         //    movie.Studio = model.Studio;
         //    movie.StudioId = model.Studio.Id;
         //    movie.ImageUrl = model.ImageUrl;
         //    movie.LeadingFemale = model.LeadingFemale;
        //     movie.LeadingFemaleId = model.LeadingFemale.Id;
        //     movie.LeadingMale = model.LeadingMale;
        //     movie.LeadingMaleId = model.LeadingMale.Id;
        //     movie.Director = model.Director;
       //      movie.Year = model.Year;

         //    this.MoviesService.Update(movie);
          //   return RedirectToAction("Details", movie);
       //  }

      //   [HttpGet]
// // public ActionResult Delete(int id)
// // {

   // //  var movie = this.MoviesService.GetById(id);

        //     return PartialView("_Delete", movie);

       //  }

      //   [HttpPost]
      //   [ValidateAntiForgeryToken]
      //   public ActionResult Delete(Movie movie)
       //  {
    // if (!ModelState.IsValid)
            {
    //  return PartialView("_Delete");
    // }

//  this.MoviesService.Delete(movie);
// return RedirectToAction("All");
// }