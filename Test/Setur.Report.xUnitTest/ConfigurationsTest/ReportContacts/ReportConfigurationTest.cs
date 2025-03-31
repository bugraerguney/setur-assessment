using Microsoft.EntityFrameworkCore;
using Setur.Report.Domain.Entities;
using Setur.Report.Persistance.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.ConfigurationsTest.ReportContacts
{
    public class ReportConfigurationTest
    {
        [Fact]
        public void Configure_Should_Configure_Entity_Correctly()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            modelBuilder.ApplyConfiguration(new ReportConfiguration());

            var entity = modelBuilder.Model.FindEntityType(typeof(ReportContact));
            Assert.NotNull(entity);

            // Assert - Key
            var key = entity.FindPrimaryKey();
            Assert.Single(key.Properties);
            Assert.Equal("Id", key.Properties.First().Name);

            // Assert - RequestedAt is required
            var requestedAt = entity.FindProperty(nameof(ReportContact.RequestedAt));
            Assert.False(requestedAt.IsNullable);

            // Assert - CompletedAt is nullable (since not IsRequired)
            var completedAt = entity.FindProperty(nameof(ReportContact.CompletedAt));
            Assert.True(completedAt.IsNullable);

            // Assert - Status is required and converted to int
            var status = entity.FindProperty(nameof(ReportContact.Status));
            Assert.False(status.IsNullable);
            Assert.Equal(typeof(int), status.GetProviderClrType());


            // Assert - Navigation: Report.Details → One-to-Many
            var navigation = entity.GetNavigations().FirstOrDefault(n => n.Name == nameof(ReportContact.Details));
            Assert.NotNull(navigation);
            Assert.True(navigation.IsCollection);
            Assert.Equal(DeleteBehavior.Cascade, navigation.ForeignKey.DeleteBehavior);
        }
    }
}
