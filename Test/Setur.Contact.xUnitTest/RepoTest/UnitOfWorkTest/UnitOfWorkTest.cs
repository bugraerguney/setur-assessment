using Microsoft.EntityFrameworkCore;
using Moq;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application.Contracts.Persistance.ContactInfos;
using Setur.Contact.Application.Contracts.Persistance.PersonInfos;
using Setur.Contact.Domain.Entities;
using Setur.Contact.Persistance;
using Setur.Contact.Persistance.ContactInfos;
using Setur.Contact.Persistance.Context;
using Setur.Contact.Persistance.PersonInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.RepoTest.UnitOfWorkTest
{
    public class UnitOfWorkTests
    {
        private readonly ContactDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ContactDbContext(options);
            _unitOfWork = new UnitOfWork(_context);
        }

        [Fact]
        public async Task SaveChangesAsync_Should_Save_Changes()
        {
            // Arrange
            var person = new PersonInfo
            {
                Id = Guid.NewGuid(),
                Name = "Buğra",
                Surname = "Kral",
                Company = "Setur"
            };

            await _context.PersonInfos.AddAsync(person);

            // Act
            var result = await _unitOfWork.SaveChangesAsync();

            // Assert
            Assert.Equal(1, result);
        }
    }
}
