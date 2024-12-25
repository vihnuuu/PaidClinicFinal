using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DAL.Tests
{
    public class PatientRepositoryTests
    {
        [Fact]
        public async Task FindByEmailAsync_ReturnsCorrectPatient()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ClinicContext>()
                .UseInMemoryDatabase(databaseName: "TestPatientsDatabase")
                .Options;

            using var context = new ClinicContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Patients.AddRange(
                new Patient { PatientId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Patient { PatientId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
            );

            await context.SaveChangesAsync();

            var repository = new PatientRepository(context);

            // Act
            var patient = await repository.FindByEmailAsync("john.doe@example.com");

            // Assert
            Assert.NotNull(patient);
            Assert.Equal("John", patient.FirstName);
            Assert.Equal("Doe", patient.LastName);
        }

    }
}
