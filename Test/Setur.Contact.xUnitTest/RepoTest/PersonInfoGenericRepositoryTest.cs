using Microsoft.EntityFrameworkCore;
using Moq;
using Setur.Contact.Domain.Entities;
using Setur.Contact.Domain.Entities.Common;
using Setur.Contact.Persistance;
using Setur.Contact.Persistance.Context;
using Setur.Contact.xUnitTest.RepoTest.ContextTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.RepoTest
{


    public class PersonInfoGenericRepositoryTest
    {
        private readonly ContactDbContext _context;
        private readonly GenericRepository<PersonInfo, Guid> _repository;

        public PersonInfoGenericRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ContactDbContext(options);
            _repository = new GenericRepository<PersonInfo, Guid>(_context);
        }

        [Fact]
        public async Task AddAsync_Should_Add_Entity()
        {
            var person = new PersonInfo
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Surname = "Doe",
                Company = "Setur",
                Created = DateTime.UtcNow
            };

            await _repository.AddAsync(person);
            await _context.SaveChangesAsync();

            Assert.Single(_context.PersonInfos);
        }

        [Fact]
        public async Task Delete_Should_Remove_Entity()
        {
            var person = new PersonInfo
            {
                Id = Guid.NewGuid(),
                Name = "Delete",
                Surname = "Me",
                Company = "Setur",
                Created = DateTime.UtcNow
            };

            await _context.PersonInfos.AddAsync(person);
            await _context.SaveChangesAsync();

            _repository.Delete(person);
            await _context.SaveChangesAsync();

            Assert.Empty(_context.PersonInfos);
        }

        [Fact]
        public async Task Update_Should_Modify_Entity()
        {
            var person = new PersonInfo
            {
                Id = Guid.NewGuid(),
                Name = "OldName",
                Surname = "LastName",
                Company = "Setur",
                Created = DateTime.UtcNow
            };

            await _context.PersonInfos.AddAsync(person);
            await _context.SaveChangesAsync();

            person.Name = "NewName";
            _repository.Update(person);
            await _context.SaveChangesAsync();

            var updated = await _context.PersonInfos.FindAsync(person.Id);
            Assert.Equal("NewName", updated.Name);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Entity()
        {
            var id = Guid.NewGuid();
            var person = new PersonInfo
            {
                Id = id,
                Name = "Find",
                Surname = "Me",
                Company = "Setur",
                Created = DateTime.UtcNow
            };

            await _context.PersonInfos.AddAsync(person);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(id);
            Assert.NotNull(result);
            Assert.Equal("Find", result.Name);
        }

        [Fact]
        public async Task Where_Should_Filter_Entities()
        {
            await _context.PersonInfos.AddRangeAsync(
                new PersonInfo { Id = Guid.NewGuid(), Name = "Ali", Surname = "A", Company = "Setur", Created = DateTime.UtcNow },
                new PersonInfo { Id = Guid.NewGuid(), Name = "Veli", Surname = "B", Company = "Other", Created = DateTime.UtcNow }
            );
            await _context.SaveChangesAsync();

            var result = _repository.Where(x => x.Company == "Setur").ToList();
            Assert.Single(result);
        }

        [Fact]
        public async Task AnyAsync_ById_Should_Return_True_If_Exists()
        {
            var id = Guid.NewGuid();
            await _context.PersonInfos.AddAsync(new PersonInfo
            {
                Id = id,
                Name = "Test",
                Surname = "One",
                Company = "Setur",
                Created = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            var exists = await _repository.AnyAsync(id);
            Assert.True(exists);
        }

        [Fact]
        public async Task AnyAsync_ByPredicate_Should_Return_True_If_Match()
        {
            await _context.PersonInfos.AddAsync(new PersonInfo
            {
                Id = Guid.NewGuid(),
                Name = "Exists",
                Surname = "X",
                Company = "Setur",
                Created = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            var exists = await _repository.AnyAsync(x => x.Name == "Exists");
            Assert.True(exists);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Entities()
        {
            await _context.PersonInfos.AddRangeAsync(new[]
            {
                new PersonInfo { Id = Guid.NewGuid(), Name = "1", Surname = "A", Company = "Setur", Created = DateTime.UtcNow },
                new PersonInfo { Id = Guid.NewGuid(), Name = "2", Surname = "B", Company = "Setur", Created = DateTime.UtcNow }
            });
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllAsync();
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllPagedAsync_Should_Return_Correct_Page()
        {
            var items = Enumerable.Range(1, 10).Select(i =>
                new PersonInfo
                {
                    Id = Guid.NewGuid(),
                    Name = $"Person{i}",
                    Surname = $"Surname{i}",
                    Company = "Setur",
                    Created = DateTime.UtcNow
                });

            await _context.PersonInfos.AddRangeAsync(items);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllPagedAsync(pageNumber: 2, pageSize: 3);
            Assert.Equal(3, result.Count);
        }
    }
}
