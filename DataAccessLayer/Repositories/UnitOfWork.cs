using DataAccessLayer.Abstract;
using DataAccessLayer.Identity;
using DataAccessLayer.Models;
using Entities.Entities;
using Library.Web.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationContext _context;
        private bool _disposed = false;
        private Dictionary<string, object> repositories;
        private IGenericRepository<Magazine> _magazineRepository;
        private IGenericRepository<PublicationHouse> _houseRepository;
        private IGenericRepository<Brochure> _brochureRepository;
        private IGenericRepository<Book> _bookRepository;

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IClientManager _clientManager;

        public IGenericRepository<Magazine> Magazine
        {
            get
            {
                if (_magazineRepository == null)
                    _magazineRepository = this.Repository<Magazine>();
                return _magazineRepository;
            }
        }

        public IGenericRepository<Book> Book
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = this.Repository<Book>();
                return _bookRepository;
            }
        }

        public IGenericRepository<Brochure> Brochure
        {
            get
            {
                if (_brochureRepository == null)
                    _brochureRepository = this.Repository<Brochure>();
                return _brochureRepository;
            }
        }

        public IGenericRepository<PublicationHouse> PublicationHouse
        {
            get
            {
                if (_houseRepository == null)
                    _houseRepository = this.Repository<PublicationHouse>();
                return _houseRepository;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    CreateIdentity();
                }
                return _userManager;
            }
        }

        public IClientManager ClientManager
        {
            get
            {
                if (_clientManager == null)
                {
                    CreateIdentity();
                }
                return _clientManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (_roleManager == null)
                {
                    CreateIdentity();
                }
                return _roleManager;
            }
        }

        public UnitOfWork(string connectionString)
        {
            _context = new ApplicationContext(connectionString);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public IGenericRepository<T> Repository<T>() where T : TEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(EntityRepository<>);
                if (typeof(T) == typeof(Book))
                {
                    repositoryType = typeof(EntityBookRepository<>);
                }
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                repositories.Add(type, repositoryInstance);
            }
            return (IGenericRepository<T>) repositories[type];
        }

        private void CreateIdentity()
        {
            _clientManager = new ClientManager(_context);
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
        }
    }
}
