
namespace ConsoleCinemaTicketbooking
{
    using System.Linq;

    using Data.Repositories;
    using Contracts;
    using Models;

    public class MoviesService : IMoviesService
    {
        private IRepository<Movie> data;

        public MoviesService(IRepository<Movie> data)
        {
            this.data = data;
        }

        public IQueryable<Movie> GetAll()
        {
            return this.data.All();
        }

        public IQueryable<Movie> GetById(int? id)
        {
            return this.data.SearchFor(m => m.Id == id);
        }

        public void Create(Movie movie)
        {
            this.data.Add(movie);
            this.data.SaveChanges();
        }

        public void Update(Movie movie)
        {
            this.data.Update(movie);
            this.data.SaveChanges();
        }

        public void Delete(Movie movie)
        {
            this.data.Delete(movie);
            this.data.SaveChanges();
        }
    }
}
