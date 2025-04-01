using Microsoft.EntityFrameworkCore;
using Setur.Contact.Domain.Entities;
using Setur.Contact.Persistance.Context;
using Setur.Contact.Persistance.PersonInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.RepoTest.PersonInfos
{
    public class PersonInfoRepositoryTest
    {
        private readonly ContactDbContext _context;
        private readonly PersonInfoRepository _repository;

        public PersonInfoRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new ContactDbContext(options);
            _repository = new PersonInfoRepository(_context);

            SeedTestData().Wait();
        }

        private async Task SeedTestData()
        {
            var person1 = new PersonInfo
            {
                Id = Guid.NewGuid(),
                Name = "Ali",
                Surname = "Veli",
                Company = "A",
                ContactInfos = new List<ContactInfo>
                {
                    new ContactInfo { InfoType = InfoType.Location, Content = "Istanbul" },
                    new ContactInfo { InfoType = InfoType.Phone, Content = "+905555555555" }
                }
            };

            var person2 = new PersonInfo
            {
                Id = Guid.NewGuid(),
                Name = "Ayşe",
                Surname = "Kaya",
                Company = "B",
                ContactInfos = new List<ContactInfo>
                {
                    new ContactInfo { InfoType = InfoType.Location, Content = "Istanbul" },
                    new ContactInfo { InfoType = InfoType.Phone, Content = "+905555555556" },
                    new ContactInfo { InfoType = InfoType.Phone, Content = "+905555555557" }
                }
            };

            await _context.PersonInfos.AddRangeAsync(person1, person2);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetPersonStatisticsAsync_Should_Return_Correct_Statistics()
        {
            var stats = await _repository.GetPersonStatisticsAsync();

            Assert.Single(stats);
            var stat = stats.First();
            Assert.Equal("Istanbul", stat.Location);
            Assert.Equal(2, stat.PersonCount);
            Assert.Equal(3, stat.PhoneNumberCount); // 1 phone for person1, 2 phones for person2
        }

        [Fact]
        public async Task GetPersonWithContactInfosAsync_Should_Return_Person_And_ContactInfos()
        {
            var person = _context.PersonInfos.First();
            var result = await _repository.GetPersonWithContactInfosAsync(person.Id);

            Assert.NotNull(result);
            Assert.Equal(person.Name, result.Name);
            Assert.NotEmpty(result.ContactInfos);
        }
    }
}
