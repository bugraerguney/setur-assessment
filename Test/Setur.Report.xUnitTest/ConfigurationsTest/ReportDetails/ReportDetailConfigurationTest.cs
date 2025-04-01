using Microsoft.EntityFrameworkCore;
using Setur.Report.Domain.Entities;
using Setur.Report.Persistance.ReportDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.ConfigurationsTest.ReportDetails
{
    public class ReportDetailConfigurationTest
    {
        [Fact]
        public void Configure_Should_Setup_Properties_Correctly()
        {
            // Arrange
            var modelBuilder = new ModelBuilder();
            modelBuilder.ApplyConfiguration(new ReportDetailConfiguration());

            var entityType = modelBuilder.Model.FindEntityType(typeof(ReportDetail));
            Assert.NotNull(entityType);

            // Assert - Key
            var key = entityType.FindPrimaryKey();
            Assert.Contains("Id", key.Properties.Select(p => p.Name));

            // Assert - Location
            var locationProp = entityType.FindProperty(nameof(ReportDetail.Location));
            Assert.False(locationProp.IsNullable); // IsRequired => IsNullable = false
            Assert.Equal(64, locationProp.GetMaxLength());

            // Assert - PersonCount
            var personCountProp = entityType.FindProperty(nameof(ReportDetail.PersonCount));
            Assert.False(personCountProp.IsNullable); // ✅

            // Assert - PhoneNumberCount
            var phoneProp = entityType.FindProperty(nameof(ReportDetail.PhoneNumberCount));
            Assert.False(phoneProp.IsNullable); // ✅

            // Assert - Navigation
            var navigation = entityType.FindNavigation(nameof(ReportDetail.Report));
            Assert.NotNull(navigation);
            Assert.Equal(DeleteBehavior.Cascade, navigation.ForeignKey.DeleteBehavior);
        }
    }
}
