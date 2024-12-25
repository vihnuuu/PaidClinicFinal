using Moq;
using Xunit;
using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Tests
{
    public class VisitRepositoryTests
    {
        [Fact]
        public async Task GetVisitsByStatusAsync_ReturnsCorrectVisits_WithMock()
        {
            // Arrange: Налаштування тестового двійника для репозиторію
            var mockVisitRepo = new Mock<IVisitRepository>();

            var visits = new List<Visit>
            {
                new Visit { VisitId = 1, Status = VisitStatus.Completed },
                new Visit { VisitId = 2, Status = VisitStatus.Completed },
                new Visit { VisitId = 3, Status = VisitStatus.Active }
            };

            mockVisitRepo
                .Setup(repo => repo.GetVisitsByStatusAsync(VisitStatus.Completed))
                .ReturnsAsync(visits.Where(v => v.Status == VisitStatus.Completed));

            var repository = mockVisitRepo.Object;

            // Act: Виклик тестованого методу
            var result = await repository.GetVisitsByStatusAsync(VisitStatus.Completed);

            // Assert: Перевірка результатів
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, v => Assert.Equal(VisitStatus.Completed, v.Status));
        }

        [Fact]
        public async Task GetVisitDetailsAsync_ReturnsCorrectVisit_WithMock()
        {
            // Arrange: Налаштування тестового двійника для репозиторію
            var mockVisitRepo = new Mock<IVisitRepository>();

            var visit = new Visit
            {
                VisitId = 1,
                DoctorId = 1,
                PatientId = 1,
                VisitDate = DateTime.Now,
                Status = VisitStatus.Completed,
                Treatments = new List<Treatment> { new Treatment { TreatmentId = 1, Description = "Test Treatment" } }
            };

            mockVisitRepo
                .Setup(repo => repo.GetVisitDetailsAsync(1))
                .ReturnsAsync(visit);

            var repository = mockVisitRepo.Object;

            // Act: Виклик тестованого методу
            var result = await repository.GetVisitDetailsAsync(1);

            // Assert: Перевірка результатів
            Assert.NotNull(result);
            Assert.Equal(1, result.VisitId);
            Assert.Equal(1, result.DoctorId);
            Assert.NotNull(result.Treatments);
            Assert.Single(result.Treatments);
        }

        [Fact]
        public async Task GetVisitCountByStatusAsync_ReturnsCorrectCount_WithMock()
        {
            // Arrange: Налаштування тестового двійника для репозиторію
            var mockVisitRepo = new Mock<IVisitRepository>();

            mockVisitRepo
                .Setup(repo => repo.GetVisitCountByStatusAsync(VisitStatus.Completed))
                .ReturnsAsync(2);

            var repository = mockVisitRepo.Object;

            // Act: Виклик тестованого методу
            var count = await repository.GetVisitCountByStatusAsync(VisitStatus.Completed);

            // Assert: Перевірка результатів
            Assert.Equal(2, count);
        }

        [Fact]
        public async Task GetVisitsByDoctorIdWithAccessCheckAsync_ThrowsUnauthorizedAccessException_WithMock()
        {
            // Arrange: Налаштування тестового двійника для репозиторію
            var mockVisitRepo = new Mock<IVisitRepository>();

            mockVisitRepo
                .Setup(repo => repo.GetVisitsByDoctorIdWithAccessCheckAsync(1, 2))
                .ThrowsAsync(new UnauthorizedAccessException("Access denied: You can only view visits of your patients."));

            var repository = mockVisitRepo.Object;

            // Act & Assert: Перевірка, що виняток кидається
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            {
                await repository.GetVisitsByDoctorIdWithAccessCheckAsync(1, 2);
            });
        }
    }
}
