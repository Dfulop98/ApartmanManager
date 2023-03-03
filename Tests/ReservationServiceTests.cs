using DataAccessLayer.DbAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ReservationServiceTests
    {
        private AMDbContext _db;

        [SetUp]

        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AMDbContext>()
                .UseInMemoryDatabase(databaseName: "TestBd_" + DateTime.Now.ToFileTimeUtc())
                .Options;
            _db = new AMDbContext(options);
        }
        [Test]
        public async Task GetReservationsAsync_ListIsEmpty_ReturnNotFound()
        {
            using (_db)
            {
                ReservationService service = new(_db);
                //Act
                HttpResponseMessage result = await service.GetReservationsAsync();
                //Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("The reservation succefully added."));
                    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                });
            }
        }

        [Test]
        public async Task GetReservationsAsync_()
        {

        }

        [Test]
        public async Task AddReservationAsync_WithValidReservationData_ReturnsSuccess()
        {

        }
        
        [Test]
        public async Task AddReservationAsync_WithExistingReservation_ReturnsConflict()
        {

        }
        
        [Test]
        public async Task AddReservationAsync_WithInvalidReservationData_ReturnsBadRequest()
        {

        }
        
        [Test]
        public async Task AddReservationAsync_WithNonExistentRoom_ReturnsNotFound()
        {

        }
    }
}
