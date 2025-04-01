using Microsoft.EntityFrameworkCore;
using Moq;
using Setur.Report.Domain.Entities;
using Setur.Report.Persistance;
using Setur.Report.Persistance.Context;
using Setur.Report.Persistance.ReportContacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.ReposTest.ReportContacts
{
    public class ReportContactRepositoryTest
    {
        private readonly ReportDbContext _context;
        private readonly GenericRepository<ReportContact, Guid> _repository;
        private readonly ReportContactRepository _reportContactRepository;

        public ReportContactRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ReportDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())  
                .Options;

            _context = new ReportDbContext(options);
            _repository = new GenericRepository<ReportContact, Guid>(_context);
            _reportContactRepository = new ReportContactRepository(_context);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            SeedTestData();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenFound()
        {
            // Arrange
            SeedTestData();
            var reportContactId = _context.ReportContacts.First().Id;

            // Act
            var result = await _repository.GetByIdAsync(reportContactId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reportContactId, result?.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            SeedTestData();
            var nonExistentId = Guid.NewGuid();

            // Act
            var result = await _repository.GetByIdAsync(nonExistentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            // Arrange
            SeedTestData();
            var newContact = new ReportContact
            {
                Id = Guid.NewGuid(),
                RequestedAt = DateTime.UtcNow,
                Status = ReportStatus.Preparing
            };

            // Act
            await _repository.AddAsync(newContact);
            await _context.SaveChangesAsync();

            // Assert
            var allContacts = await _repository.GetAllAsync();
            Assert.Equal(3, allContacts.Count);
            Assert.Contains(allContacts, c => c.Id == newContact.Id);
        }

        [Fact]
        public void Update_ShouldUpdateEntity()
        {
            // Arrange
            SeedTestData();
            var contactToUpdate = _context.ReportContacts.First();
            contactToUpdate.Status = ReportStatus.Completed;

            // Act
            _repository.Update(contactToUpdate);
            _context.SaveChanges();

            // Assert
            var updatedContact = _context.ReportContacts.First();
            Assert.Equal(ReportStatus.Completed, updatedContact.Status);
        }

        [Fact]
        public async Task GetReportWithDetailsAsync_ShouldReturnReportWithDetails_WhenIdExists()
        {
            // Arrange
            SeedTestData();
            var existingId = _context.ReportContacts.First().Id;

            // Act
            var result = await _reportContactRepository.GetReportWithDetailsAsync(existingId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingId, result?.Id);
            Assert.NotEmpty(result?.Details);
        }

        private void SeedTestData()
        {
            var reportContacts = new List<ReportContact>
            {
                new ReportContact
                {
                    Id = Guid.NewGuid(),
                    RequestedAt = DateTime.UtcNow,
                    Status = ReportStatus.Preparing,
                    Details = new List<ReportDetail>
                    {
                        new ReportDetail { Id = Guid.NewGuid(), Location = "Istanbul" }
                    }
                },
                new ReportContact
                {
                    Id = Guid.NewGuid(),
                    RequestedAt = DateTime.UtcNow,
                    Status = ReportStatus.Completed,
                    Details = new List<ReportDetail>
                    {
                        new ReportDetail { Id = Guid.NewGuid(), Location = "Ankara" }
                    }
                }
            };

            _context.ReportContacts.AddRange(reportContacts);
            _context.SaveChanges();
        }
    }
}
