using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataLayer
{
    public class GenreContext : IDB<Genre, int>
    {
        RadoslavMirchev11eDbContext _context;

        public GenreContext(RadoslavMirchev11eDbContext context)
        {
            _context = context;
        }

        public void Create(Genre item)
        {
            try
            {
                _context.Genres.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Genre Read(int key, bool noTracking = false, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Genre> query = _context.Genres;

                if (noTracking)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                if (useNavigationProperties)
                {
                    query = query.Include(g => g.Customers);
                }

                return query.SingleOrDefault(g => g.Id == key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Genre> Read(int skip, int take, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Genre> query = _context.Genres.AsNoTrackingWithIdentityResolution();

                if (useNavigationProperties)
                {
                    query = query.Include(g => g.Customers);
                }

                return query.Skip(skip).Take(take).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Genre> ReadAll(bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Genre> query = _context.Genres.AsNoTracking();

                if (useNavigationProperties)
                {
                    query = query.Include(g => g.Customers);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Genre item, bool useNavigationProperties = false)
        {
            try
            {
                Genre genreFromDB = Read(item.Id);

                _context.Entry(genreFromDB).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int key)
        {
            try
            {
                _context.Genres.Remove(Read(key));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

