using Moq;
using Xunit;
using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using DAL.Repositories.Impl;
using System.Threading.Tasks;

namespace DAL.Tests
{
    public class TreatmentRepositoryTests
    {
        [Fact]
        public async Task GetTreatmentsByVisitIdAsync_ReturnsCorrectTreatments_WithMock()
        {
            // Arrange: Налаштування тестового двійника для репозиторію
            var mockTreatmentRepo = new Mock<ITreatmentRepository>();

            var treatments = new List<Treatment>
            {
                new Treatment { TreatmentId = 1, VisitId = 1, Description = "Treatment 1", Cost = 100 },
                new Treatment { TreatmentId = 2, VisitId = 1, Description = "Treatment 2", Cost = 200 },
                new Treatment { TreatmentId = 3, VisitId = 2, Description = "Treatment 3", Cost = 300 }
            };

            mockTreatmentRepo
                .Setup(repo => repo.GetTreatmentsByVisitIdAsync(1))
                .ReturnsAsync(treatments.Where(t => t.VisitId == 1));

            var repository = mockTreatmentRepo.Object;

            // Act: Виклик тестованого методу
            var result = await repository.GetTreatmentsByVisitIdAsync(1);

            // Assert: Перевірка результатів
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, t => Assert.Equal(1, t.VisitId));
        }

        [Fact]
        public async Task GetExpensiveTreatmentsAsync_ReturnsCorrectTreatments_WithMock()
        {
            // Arrange: Налаштування тестового двійника для репозиторію
            var mockTreatmentRepo = new Mock<ITreatmentRepository>();

            var treatments = new List<Treatment>
            {
                new Treatment { TreatmentId = 1, VisitId = 1, Description = "Treatment 1", Cost = 100 },
                new Treatment { TreatmentId = 2, VisitId = 1, Description = "Treatment 2", Cost = 200 },
                new Treatment { TreatmentId = 3, VisitId = 2, Description = "Treatment 3", Cost = 300 }
            };

            mockTreatmentRepo
                .Setup(repo => repo.GetExpensiveTreatmentsAsync(150))
                .ReturnsAsync(treatments.Where(t => t.Cost > 150).ToList()); // Преобразование к списку для совместимости с возвращаемым типом Task<List<Treatment>>

            var repository = mockTreatmentRepo.Object;

            // Act: Виклик тестованого методу
            var result = await repository.GetExpensiveTreatmentsAsync(150);

            // Assert: Перевірка результатів
            Assert.NotNull(result); 
            Assert.Equal(2, result.Count());
            Assert.All(result, t => Assert.True(t.Cost > 150));
        }

    }
}
