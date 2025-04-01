using Microsoft.EntityFrameworkCore;
using Moq;
using Setur.Report.Domain.Entities;
using Setur.Report.Persistance;
using Setur.Report.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.ReposTest.ReportDetails
{
    public class ReportDetailRepositoryTest
    {
        private ReportDbContext _context;
        private GenericRepository<ReportDetail, Guid> _repository;

        private void InitializeTestDatabase()
        {
            // InMemory veritabanı her test için ayrı oluşturuluyor
            var options = new DbContextOptionsBuilder<ReportDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ReportDbContext(options);
            _repository = new GenericRepository<ReportDetail, Guid>(_context);
        }

        private void SeedTestData()
        {
            // Test için başlangıç verileri
            var reportDetails = new List<ReportDetail>
            {
                new ReportDetail
                {
                    Id = Guid.NewGuid(),
                    Location = "Istanbul",
                    PersonCount = 10,
                    PhoneNumberCount = 5,
                },
                new ReportDetail
                {
                    Id = Guid.NewGuid(),
                    Location = "Ankara",
                    PersonCount = 20,
                    PhoneNumberCount = 15,
                }
            };

            _context.ReportDetails.AddRange(reportDetails);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            InitializeTestDatabase();
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
            InitializeTestDatabase();
            SeedTestData();
            var entityId = _context.ReportDetails.First().Id;

            // Act
            var result = await _repository.GetByIdAsync(entityId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entityId, result?.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            InitializeTestDatabase();
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
            InitializeTestDatabase();
            SeedTestData();
            var newDetail = new ReportDetail
            {
                Id = Guid.NewGuid(),
                Location = "Izmir",
                PersonCount = 5,
                PhoneNumberCount = 3,
            };

            // Act
            await _repository.AddAsync(newDetail);
            await _context.SaveChangesAsync();

            // Assert
            var allEntities = await _repository.GetAllAsync();
            Assert.Equal(3, allEntities.Count);
            Assert.Contains(allEntities, d => d.Location == "Izmir");
        }

        [Fact]
        public void Update_ShouldUpdateEntity()
        {
            // Arrange
            InitializeTestDatabase();
            SeedTestData();
            var entityToUpdate = _context.ReportDetails.First();
            entityToUpdate.Location = "Updated Location";

            // Act
            _repository.Update(entityToUpdate);
            _context.SaveChanges();

            // Assert
            var updatedEntity = _context.ReportDetails.First();
            Assert.Equal("Updated Location", updatedEntity.Location);
        }
    }
}
