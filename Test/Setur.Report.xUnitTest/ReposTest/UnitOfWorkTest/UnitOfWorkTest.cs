using Microsoft.EntityFrameworkCore;
using Setur.Report.Domain.Entities;
using Setur.Report.Persistance;
using Setur.Report.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.ReposTest.UnitOfWorkTest
{
    public class UnitOfWorkTests
    {
        private readonly ReportDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            var options = new DbContextOptionsBuilder<ReportDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ReportDbContext(options);
            _unitOfWork = new UnitOfWork(_context);
        }

        [Fact]
        public async Task SaveChangesAsync_Should_Save_ReportContact()
        {
            // Arrange
            var reportContact = new ReportContact
            {
                Id = Guid.NewGuid(),
                RequestedAt = DateTime.UtcNow,
                Status = ReportStatus.Preparing
            };

            await _context.ReportContacts.AddAsync(reportContact);

            // Act
            var result = await _unitOfWork.SaveChangesAsync();

            // Assert
            Assert.Equal(1, result);

             var inserted = await _context.ReportContacts.FindAsync(reportContact.Id);
            Assert.NotNull(inserted);
            Assert.Equal(reportContact.Id, inserted.Id);
        }
    }
}
