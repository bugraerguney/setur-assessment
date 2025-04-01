using Microsoft.EntityFrameworkCore;
using Setur.Contact.Domain.Entities;
using Setur.Contact.Persistance;
using Setur.Contact.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.RepoTest
{
    public class ContactInfoGenericRepositoryTest
    {
        private readonly ContactDbContext _context;
        private readonly GenericRepository<ContactInfo, Guid> _repository;

        public ContactInfoGenericRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ContactDbContext(options);
            _repository = new GenericRepository<ContactInfo, Guid>(_context);
        }

        [Fact]
        public async Task AddAsync_Should_Add_ContactInfo()
        {
            var contact = new ContactInfo
            {
                Id = Guid.NewGuid(),
                PersonInfoId = Guid.NewGuid(),
                InfoType = InfoType.Email,
                Content = "example@example.com",
                Created = DateTime.UtcNow
            };

            await _repository.AddAsync(contact);
            await _context.SaveChangesAsync();

            Assert.Single(_context.ContactInfos);
        }

        [Fact]
        public async Task Delete_Should_Remove_ContactInfo()
        {
            var contact = new ContactInfo
            {
                Id = Guid.NewGuid(),
                PersonInfoId = Guid.NewGuid(),
                InfoType = InfoType.Phone,
                Content = "123456789",
                Created = DateTime.UtcNow
            };

            await _context.ContactInfos.AddAsync(contact);
            await _context.SaveChangesAsync();

            _repository.Delete(contact);
            await _context.SaveChangesAsync();

            Assert.Empty(_context.ContactInfos);
        }

        [Fact]
        public async Task Update_Should_Modify_ContactInfo()
        {
            var contact = new ContactInfo
            {
                Id = Guid.NewGuid(),
                PersonInfoId = Guid.NewGuid(),
                InfoType = InfoType.Location,
                Content = "Istanbul",
                Created = DateTime.UtcNow
            };

            await _context.ContactInfos.AddAsync(contact);
            await _context.SaveChangesAsync();

            contact.Content = "Ankara";
            _repository.Update(contact);
            await _context.SaveChangesAsync();

            var updated = await _context.ContactInfos.FindAsync(contact.Id);
            Assert.Equal("Ankara", updated.Content);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_ContactInfo()
        {
            var id = Guid.NewGuid();
            var contact = new ContactInfo
            {
                Id = id,
                PersonInfoId = Guid.NewGuid(),
                InfoType = InfoType.Phone,
                Content = "987654321",
                Created = DateTime.UtcNow
            };

            await _context.ContactInfos.AddAsync(contact);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(id);
            Assert.NotNull(result);
            Assert.Equal("987654321", result.Content);
        }

        [Fact]
        public async Task Where_Should_Filter_By_InfoType()
        {
            await _context.ContactInfos.AddRangeAsync(
                new ContactInfo
                {
                    Id = Guid.NewGuid(),
                    PersonInfoId = Guid.NewGuid(),
                    InfoType = InfoType.Email,
                    Content = "a@example.com",
                    Created = DateTime.UtcNow
                },
                new ContactInfo
                {
                    Id = Guid.NewGuid(),
                    PersonInfoId = Guid.NewGuid(),
                    InfoType = InfoType.Phone,
                    Content = "123456789",
                    Created = DateTime.UtcNow
                }
            );
            await _context.SaveChangesAsync();

            var result = _repository.Where(x => x.InfoType == InfoType.Email).ToList();
            Assert.Single(result);
        }

        [Fact]
        public async Task AnyAsync_ById_Should_Return_True_If_Exists()
        {
            var id = Guid.NewGuid();
            await _context.ContactInfos.AddAsync(new ContactInfo
            {
                Id = id,
                PersonInfoId = Guid.NewGuid(),
                InfoType = InfoType.Location,
                Content = "Izmir",
                Created = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            var exists = await _repository.AnyAsync(id);
            Assert.True(exists);
        }

        [Fact]
        public async Task AnyAsync_ByPredicate_Should_Return_True_If_Content_Match()
        {
            await _context.ContactInfos.AddAsync(new ContactInfo
            {
                Id = Guid.NewGuid(),
                PersonInfoId = Guid.NewGuid(),
                InfoType = InfoType.Phone,
                Content = "4444444444",
                Created = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            var exists = await _repository.AnyAsync(x => x.Content == "4444444444");
            Assert.True(exists);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_ContactInfos()
        {
            await _context.ContactInfos.AddRangeAsync(new[]
            {
                new ContactInfo { Id = Guid.NewGuid(), PersonInfoId = Guid.NewGuid(), InfoType = InfoType.Phone, Content = "111", Created = DateTime.UtcNow },
                new ContactInfo { Id = Guid.NewGuid(), PersonInfoId = Guid.NewGuid(), InfoType = InfoType.Email, Content = "b@example.com", Created = DateTime.UtcNow }
            });
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllAsync();
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetAllPagedAsync_Should_Return_Correct_Page()
        {
            var items = Enumerable.Range(1, 10).Select(i =>
                new ContactInfo
                {
                    Id = Guid.NewGuid(),
                    PersonInfoId = Guid.NewGuid(),
                    InfoType = InfoType.Phone,
                    Content = $"Number{i}",
                    Created = DateTime.UtcNow
                });

            await _context.ContactInfos.AddRangeAsync(items);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllPagedAsync(pageNumber: 2, pageSize: 3);
            Assert.Equal(3, result.Count);
        }
    }
}
