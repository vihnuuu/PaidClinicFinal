using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class DoctorRepositoryTests
{
    [Fact]
    public async Task GetDoctorsBySpecialtyAsync_ReturnsCorrectDoctors()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ClinicContext>()
            .UseInMemoryDatabase(databaseName: "TestDoctorsDatabase")
            .Options;

        using var context = new ClinicContext(options);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.Doctors.AddRange(
            new Doctor { DoctorId = 1, FirstName = "John", LastName = "Doe", Specialty = "Cardiology" },
            new Doctor { DoctorId = 2, FirstName = "Jane", LastName = "Smith", Specialty = "Cardiology" },
            new Doctor { DoctorId = 3, FirstName = "Mark", LastName = "Jones", Specialty = "Dermatology" }
        );

        await context.SaveChangesAsync();

        var repository = new DoctorRepository(context);

        // Act
        var doctors = await repository.GetDoctorsBySpecialtyAsync("Cardiology");

        // Assert
        Assert.NotNull(doctors);
        Assert.Equal(2, doctors.Count());
        Assert.All(doctors, d => Assert.Equal("Cardiology", d.Specialty));
    }

}
