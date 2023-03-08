

using DataAccessLayer.DbAccess;
using DataModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services;
using System.Net;
using System.Runtime.Intrinsics.X86;

namespace Tests
{
    public class GuestServiceTests
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
        public async Task GetAllGuestsAsync_ListIsNotEmpty_ReturnNotNull()
        {
            using (_db)
            {
                _db.Guests.AddRange(new List<Guest>
                {
                    new Guest
                    {
                        Id = 1,
                        Name = "Zachary Reyes",
                        Nationatily = "Ireland",
                        Address = "142-2117 Proin St.",
                        Email = "phasellus.nulla@outlook.net",
                        Phone = "1-293-569-8473"

                    },
                    new Guest
                    {
                        Id = 2,
                        Name = "Raja Curtis",
                        Nationatily = "Australia",
                        Address = "9267 Sit Ave",
                        Email = "sagittis@outlook.ca",
                        Phone = "(336) 523-5251"

                    }
                });
                _db.SaveChanges();
                GuestService service = new(_db);

                // Act
                IEnumerable<Guest> result = await service.GetAllGuestsAsync();
                // Assert
                Assert.That(result, Is.Not.Null);

            }

        }

        [Test]
        public async Task GetAllGuestsAsync_ListIsEmpty_ReturnEmpty()
        {
            using (_db)
            {
                GuestService service = new(_db);
                //Act

                IEnumerable<Guest> result = await service.GetAllGuestsAsync();
                //Assert
                Assert.That(result, Is.Empty);
            }
        }

        [Test]
        public async Task GetAllGuestsAsync_HaveOneReservation_ReturnOneReservation()
        {
            using (_db)
            {
                _db.Guests.AddRange(new List<Guest>
                {
                    new Guest
                    {
                        Id = 1,
                        Name = "Zachary Reyes",
                        Nationatily = "Ireland",
                        Address = "142-2117 Proin St.",
                        Email = "phasellus.nulla@outlook.net",
                        Phone = "1-293-569-8473"

                    }
                });
                _db.SaveChanges();
                GuestService service = new(_db);

                //Act

                IEnumerable<Guest> result = await service.GetAllGuestsAsync();
                //Assert
                Assert.That(result.Count(), Is.EqualTo(1));

            }
        }
        [Test]
        public async Task GetAllGuestsAsync_HaveFourReservation_ReturnFourReservation()
        {
            using (_db)
            {
                _db.Guests.AddRange(new List<Guest>
                {
                    new Guest
                    {
                        Id = 1,
                        Name = "Zachary Reyes",
                        Nationatily = "Ireland",
                        Address = "142-2117 Proin St.",
                        Email = "phasellus.nulla@outlook.net",
                        Phone = "1-293-569-8473"

                    },
                    new Guest
                    {
                        Id = 2,
                        Name = "Raja Curtis",
                        Nationatily = "Australia",
                        Address = "9267 Sit Ave",
                        Email = "sagittis@outlook.ca",
                        Phone = "(336) 523-5251"

                    },
                    new Guest
                    {
                        Id = 3,
                        Name = "Erasmus Casey",
                        Nationatily = "Canada",
                        Address = "Ap #495-7382 Nisi Rd.",
                        Email = "ridiculus.mus.donec@protonmail.couk",
                        Phone = "1-693-618-5241"

                    },
                    new Guest
                    {
                        Id = 4,
                        Name = "Chester Cervantes",
                        Nationatily = "Turkey",
                        Address = "9877 Duis St.",
                        Email = "bibendum.sed.est@outlook.org",
                        Phone = "1-912-936-3465"
                    }
                });
                _db.SaveChanges();
                GuestService service = new(_db);
                //Act
                IEnumerable<Guest> result = await service.GetAllGuestsAsync();
                //Assert
                Assert.That(result.Count(), Is.EqualTo(4));
            }
        }

        [Test]
        public async Task GetAllGuestsAsync_IsCorrectReservationId_ReturnSameReservation()
        {
            using (_db)
            {

                _db.Guests.AddRange(new List<Guest>
                {
                    new Guest
                    {
                        Id = 1,
                        Name = "Zachary Reyes",
                        Nationatily = "Ireland",
                        Address = "142-2117 Proin St.",
                        Email = "phasellus.nulla@outlook.net",
                        Phone = "1-293-569-8473"

                    }
                });
                _db.SaveChanges();
                GuestService service = new(_db);

                var expectedGuest = new List<Guest>
                {
                    new Guest
                    {
                        Id = 1,
                        Name = "Zachary Reyes",
                        Nationatily = "Ireland",
                        Address = "142-2117 Proin St.",
                        Email = "phasellus.nulla@outlook.net",
                        Phone = "1-293-569-8473"
                    }
                };
                //Act
                IEnumerable<Guest> result = await service.GetAllGuestsAsync();
                //Assert
                Assert.That(result.First().Id, Is.EqualTo(expectedGuest.First().Id));
            }
        }

        [Test]
        public Task GetGuestByIdAsync_EmptyGuestList_ThrowsException()
        {
            //Arrage
            using (_db)
            {
                // Arrage
                GuestService service = new(_db);
                // Act & Assert
                Assert.ThrowsAsync<InvalidOperationException>(async () => await service.GetGuestByIdAsync(1));

            }
            return Task.CompletedTask;
        }

        [Test]
        public Task GetGuestByIdAsync_GuestNotExists_ThrowsException()
        {
            //Arrage
            using (_db)
            {
                // Arrage
                _db.Guests.AddRange(new List<Guest>
                {
                    new Guest
                    {
                        Id = 1,
                        Name = "Zachary Reyes",
                        Nationatily = "Ireland",
                        Address = "142-2117 Proin St.",
                        Email = "phasellus.nulla@outlook.net",
                        Phone = "1-293-569-8473"

                    }
                });
                _db.SaveChanges();
                GuestService service = new(_db);
                // Act & Assert
                Assert.ThrowsAsync<InvalidOperationException>(async () => await service.GetGuestByIdAsync(5));

            }
            return Task.CompletedTask;
        }

        [Test]
        public async Task GetGuestByIdAsync_GuestIdIsCorrect_ReturnCorrectGuestId()
        {
            //Arrage
            using (_db)
            {
                // Arrage
                _db.Guests.AddRange(new List<Guest>
                {
                    new Guest
                    {
                        Id = 1,
                        Name = "Zachary Reyes",
                        Nationatily = "Ireland",
                        Address = "142-2117 Proin St.",
                        Email = "phasellus.nulla@outlook.net",
                        Phone = "1-293-569-8473"

                    }
                });
                _db.SaveChanges();
                GuestService service = new(_db);
                // Act
                var result = await service.GetGuestByIdAsync(1);
                var expectedGuest = new Guest
                {
                    Id = 1,
                    Name = "Zachary Reyes",
                    Nationatily = "Ireland",
                    Address = "142-2117 Proin St.",
                    Email = "phasellus.nulla@outlook.net",
                    Phone = "1-293-569-8473"

                };

                // Assert
                Assert.That(result.Id, Is.EqualTo(expectedGuest.Id));

            }
        }

        [Test]
        public async Task AddGuestAsync_WithValidGuestData_ReturnsSuccess()
        {
            using (_db)
            {
                //Arrage
                _db.Guests.AddRange(new List<Guest>
                {
                    new Guest
                    {
                        Id = 1,
                        Name = "Zachary Reyes",
                        Nationatily = "Ireland",
                        Address = "142-2117 Proin St.",
                        Email = "phasellus.nulla@outlook.net",
                        Phone = "1-293-569-8473"

                    },
                    new Guest
                    {
                        Id = 2,
                        Name = "Raja Curtis",
                        Nationatily = "Australia",
                        Address = "9267 Sit Ave",
                        Email = "sagittis@outlook.ca",
                        Phone = "(336) 523-5251"

                    },
                    new Guest
                    {
                        Id = 3,
                        Name = "Erasmus Casey",
                        Nationatily = "Canada",
                        Address = "Ap #495-7382 Nisi Rd.",
                        Email = "ridiculus.mus.donec@protonmail.couk",
                        Phone = "1-693-618-5241"

                    }
                });

                _db.SaveChanges();
                GuestService service = new(_db);
                
                Guest newGuest = new()
                {
                    Id = 4,
                    Name = "Chester Cervantes",
                    Nationatily = "Turkey",
                    Address = "9877 Duis St.",
                    Email = "bibendum.sed.est@outlook.org",
                    Phone = "1-912-936-3465"
                };
                //Act
                HttpResponseMessage result = await service.AddGuestAsync(newGuest);
                Assert.Multiple(() =>
                {
                    //Assert
                    Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("The guest successfully added."));
                    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                });
            }
        }

        [Test]
        public async Task AddGuestAsync_WithExistingGuest_ReturnsConflict()
        {
            using (_db)
            {
                //Arrage
                _db.Guests.AddRange(new List<Guest>
                {
                    new Guest
                    {
                        Id = 1,
                        Name = "Zachary Reyes",
                        Nationatily = "Ireland",
                        Address = "142-2117 Proin St.",
                        Email = "phasellus.nulla@outlook.net",
                        Phone = "1-293-569-8473"

                    },
                    new Guest
                    {
                        Id = 2,
                        Name = "Raja Curtis",
                        Nationatily = "Australia",
                        Address = "9267 Sit Ave",
                        Email = "sagittis@outlook.ca",
                        Phone = "(336) 523-5251"

                    },
                    new Guest
                    {
                        Id = 3,
                        Name = "Erasmus Casey",
                        Nationatily = "Canada",
                        Address = "Ap #495-7382 Nisi Rd.",
                        Email = "ridiculus.mus.donec@protonmail.couk",
                        Phone = "1-693-618-5241"

                    }
                });

                _db.SaveChanges();
                GuestService service = new(_db);

                
                Guest newGuest = new()
                {
                    Id = 3,
                    Name = "Erasmus Casey",
                    Nationatily = "Canada",
                    Address = "Ap #495-7382 Nisi Rd.",
                    Email = "ridiculus.mus.donec@protonmail.couk",
                    Phone = "1-693-618-5241"
                };
                //Act
                HttpResponseMessage result = await service.AddGuestAsync(newGuest);
                Assert.Multiple(() =>
                {
                    //Assert
                    Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("The guest already exists."));
                    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
                });
            }
        }
        [Test]
        public async Task UpdateGuestAsync_GuestDoesntExists_ReturnsNotFound()
        {
            // Arrage
            Guest newGuest = new()
            {
                Id = 1,
                Name = "Erasmus Casey",
                Nationatily = "Canada",
                Address = "Ap #495-7382 Nisi Rd.",
                Email = "ridiculus.mus.donec@protonmail.couk",
                Phone = "1-693-618-5241"
            };
            GuestService service = new(_db);

            // Act
            var result = await service.UpdateGuestAsync(newGuest);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("Guest doesnt exists."));
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            });
        }

        [Test]
        public async Task UpdateGuestAsync_GuestSuccesfullyUpdate_ReturnsOk()
        {
            //Arrage
            _db.Guests.AddRange(new List<Guest>
                {
                    new Guest
                    {
                        Id = 1,
                        Name = "Zachary Reyes",
                        Nationatily = "Ireland",
                        Address = "142-2117 Proin St.",
                        Email = "phasellus.nulla@outlook.net",
                        Phone = "1-293-569-8473"

                    },
                    new Guest
                    {
                        Id = 2,
                        Name = "Raja Curtis",
                        Nationatily = "Australia",
                        Address = "9267 Sit Ave",
                        Email = "sagittis@outlook.ca",
                        Phone = "(336) 523-5251"

                    },
                    new Guest
                    {
                        Id = 3,
                        Name = "Erasmus Casey",
                        Nationatily = "Canada",
                        Address = "Ap #495-7382 Nisi Rd.",
                        Email = "ridiculus.mus.donec@protonmail.couk",
                        Phone = "1-693-618-5241"

                    }
                });

            _db.SaveChanges();
            GuestService service = new(_db);

            Guest newGuest = new()
            {
                Id = 1,
                Name = "Erasmus Test",
                Nationatily = "UK",
                Address = "Ap #495-7382 Nisi Rd.",
                Email = "ridiculus.mus.donec@protonmail.couk",
                Phone = "1-693-618-5241"
            };

            var result = await service.UpdateGuestAsync(newGuest);

            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("Guest successfully updated."));
            });
        }

        [Test]
        public async Task UpdateGuestAsync_GuestSuccesfullyUpdate_ReturnsCorrectGuest()
        {//Arrage
            _db.Guests.AddRange(new List<Guest>
                {
                    new Guest
                    {
                        Id = 1,
                        Name = "Zachary Reyes",
                        Nationatily = "Ireland",
                        Address = "142-2117 Proin St.",
                        Email = "phasellus.nulla@outlook.net",
                        Phone = "1-293-569-8473"

                    },
                    new Guest
                    {
                        Id = 2,
                        Name = "Raja Curtis",
                        Nationatily = "Australia",
                        Address = "9267 Sit Ave",
                        Email = "sagittis@outlook.ca",
                        Phone = "(336) 523-5251"

                    },
                    new Guest
                    {
                        Id = 3,
                        Name = "Erasmus Casey",
                        Nationatily = "Canada",
                        Address = "Ap #495-7382 Nisi Rd.",
                        Email = "ridiculus.mus.donec@protonmail.couk",
                        Phone = "1-693-618-5241"

                    }
                });

            _db.SaveChanges();
            GuestService service = new(_db);



            Guest newGuest = new()
            {
                Id = 1,
                Name = "Erasmus Test",
                Nationatily = "UK",
                Address = "Ap #495-7382 Nisi Rd.",
                Email = "ridiculus.mus.donec@protonmail.couk",
                Phone = "1-693-618-5241"
            };

            await service.UpdateGuestAsync(newGuest);
            var result = await service.GetGuestByIdAsync(newGuest.Id);

            Assert.That(result.Name, Is.EqualTo(newGuest.Name));
        }

        [Test]
        public async Task RemoveGuestByIdAsync_GuestDoesntExist_ReturnNotFound()
        {
            using (_db)
            {
                // Arrage
                _db.Guests.AddRange(new List<Guest>
                {
                    new Guest
                    {
                        Id = 1,
                        Name = "Zachary Reyes",
                        Nationatily = "Ireland",
                        Address = "142-2117 Proin St.",
                        Email = "phasellus.nulla@outlook.net",
                        Phone = "1-293-569-8473"

                    },
                    new Guest
                    {
                        Id = 2,
                        Name = "Raja Curtis",
                        Nationatily = "Australia",
                        Address = "9267 Sit Ave",
                        Email = "sagittis@outlook.ca",
                        Phone = "(336) 523-5251"

                    },
                    new Guest
                    {
                        Id = 3,
                        Name = "Erasmus Casey",
                        Nationatily = "Canada",
                        Address = "Ap #495-7382 Nisi Rd.",
                        Email = "ridiculus.mus.donec@protonmail.couk",
                        Phone = "1-693-618-5241"

                    }
                });

                _db.SaveChanges();
                GuestService service = new(_db);
                Guest guestToRemove = new()
                {
                    Id = 4,
                    Name = "Zachary test",
                    Nationatily = "Ireland",
                    Address = "142-2117 Proin St.",
                    Email = "phasellus.nulla@outlook.net",
                    Phone = "1-293-569-8473"
                };
                // Act
                HttpResponseMessage result = await service.RemoveGuestByIdAsync(guestToRemove.Id);
                Assert.Multiple(() =>
                {
                    // Assert
                    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                    Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("Guest doesnt exists."));
                });
            }
        }


        [Test]
        public async Task RemoveGuestByIdAsync_CheckGuestIsRemoved_ReturnNotFound()
        {
            using (_db)
            {
                // Arrage
                _db.Guests.AddRange(new List<Guest>
                {
                    new Guest
                    {
                        Id = 1,
                        Name = "Zachary Reyes",
                        Nationatily = "Ireland",
                        Address = "142-2117 Proin St.",
                        Email = "phasellus.nulla@outlook.net",
                        Phone = "1-293-569-8473"

                    },
                    new Guest
                    {
                        Id = 2,
                        Name = "Raja Curtis",
                        Nationatily = "Australia",
                        Address = "9267 Sit Ave",
                        Email = "sagittis@outlook.ca",
                        Phone = "(336) 523-5251"

                    },
                    new Guest
                    {
                        Id = 3,
                        Name = "Erasmus Casey",
                        Nationatily = "Canada",
                        Address = "Ap #495-7382 Nisi Rd.",
                        Email = "ridiculus.mus.donec@protonmail.couk",
                        Phone = "1-693-618-5241"

                    }
                });
                _db.SaveChanges();
                GuestService service = new(_db);
                Guest guestToRemove = new()
                {
                    Id = 1,
                    Name = "Zachary Reyes",
                    Nationatily = "Ireland",
                    Address = "142-2117 Proin St.",
                    Email = "phasellus.nulla@outlook.net",
                    Phone = "1-293-569-8473"
                };
                // Act
                await service.RemoveGuestByIdAsync(guestToRemove.Id);
                HttpResponseMessage result = await service.RemoveGuestByIdAsync(guestToRemove.Id);
                // Assert
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [Test]
        public async Task RemoveGuestByIdAsync_GuestRemove_ReturOK()
        {
            using (_db)
            {
                // Arrage
                _db.Guests.AddRange(new List<Guest>
                {
                    new Guest
                    {
                        Id = 1,
                        Name = "Zachary Reyes",
                        Nationatily = "Ireland",
                        Address = "142-2117 Proin St.",
                        Email = "phasellus.nulla@outlook.net",
                        Phone = "1-293-569-8473"

                    },
                    new Guest
                    {
                        Id = 2,
                        Name = "Raja Curtis",
                        Nationatily = "Australia",
                        Address = "9267 Sit Ave",
                        Email = "sagittis@outlook.ca",
                        Phone = "(336) 523-5251"

                    },
                    new Guest
                    {
                        Id = 3,
                        Name = "Erasmus Casey",
                        Nationatily = "Canada",
                        Address = "Ap #495-7382 Nisi Rd.",
                        Email = "ridiculus.mus.donec@protonmail.couk",
                        Phone = "1-693-618-5241"

                    }
                });
                _db.SaveChanges();
                GuestService service = new(_db);
                Guest guestToRemove = new()
                {
                    Id = 1,
                    Name = "Zachary Reyes",
                    Nationatily = "Ireland",
                    Address = "142-2117 Proin St.",
                    Email = "phasellus.nulla@outlook.net",
                    Phone = "1-293-569-8473"
                };
                // Act
                HttpResponseMessage result = await service.RemoveGuestByIdAsync(guestToRemove.Id);
                Assert.Multiple(() =>
                {
                    // Assert
                    Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("Guest successfully deleted."));
                    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                });
            }
        }
    }
}
