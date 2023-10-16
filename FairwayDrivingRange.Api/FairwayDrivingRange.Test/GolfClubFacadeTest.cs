using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Test
{
    public class GolfClubFacadeTest : BaseTestSetup
    {
        [Fact]
        public void AddGolfClub_Fails_InvalidSerialNumber()
        {
            //Arrange
            var golfClubDto = new AddGolfClubDto
            {
                SerialNumber = "",

                ClubType = "Driver",

                IsAvailable = false,
            };

            var expected = ApiResponseDto<bool>.Error("Invalid Serial Number");
            //Act

            var actual = golfClubFacade.AddGolfClub(golfClubDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddGolfClub_Succeeds_GolfClubAdded()
        {
            //Arrange
            var golfClubDto = new AddGolfClubDto
            {
                SerialNumber = "5550000",

                ClubType = "Wedge",

                IsAvailable = false,
            };

            var expected = new ApiResponseDto<bool>(true);
            //Act

            var actual = golfClubFacade.AddGolfClub(golfClubDto);

            //Assert
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public void AddAllGolfClub_Fails_InvalidSerialNumber()
        {
            //Arrange
            var golfClubDtos = new List<AddGolfClubDto>
            {
                new AddGolfClubDto
                {
                    SerialNumber = "65674743",

                    ClubType = "Putter",

                    IsAvailable = false,
                },

                new AddGolfClubDto
                {
                    SerialNumber = "",

                    ClubType = "Iron",

                    IsAvailable = false,
                }
            };

            var expected = ApiResponseDto<bool>.Error("Invalid Serial Number");
            //Act

            var actual = golfClubFacade.AddAllGolfClubs(golfClubDtos.ToArray());

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddAllGolfClub_Fails_InvalidClubType()
        {
            //Arrange
            var golfClubDtos = new List<AddGolfClubDto>
            {
                new AddGolfClubDto
                {
                    SerialNumber = "65674743",

                    ClubType = "",

                    IsAvailable = false,
                },

                new AddGolfClubDto
                {
                    SerialNumber = "65674743",

                    ClubType = " ",

                    IsAvailable = false,
                }
            };

            var expected = ApiResponseDto<bool>.Error("Invalid Club Type");
            //Act

            var actual = golfClubFacade.AddAllGolfClubs(golfClubDtos.ToArray());

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void AddAllGolfClubs_Succeeds_GolfClubAdded()
        {
            //Arrange
            var golfClubDtos = new List<AddGolfClubDto>
            {
                new AddGolfClubDto
                {
                    SerialNumber = "65674743",

                    ClubType = "Iron",

                    IsAvailable = false,
                },

                new AddGolfClubDto
                {
                    SerialNumber = "96456346",

                    ClubType = "Iron",

                    IsAvailable = false,
                }
            };

            var expected = new ApiResponseDto<bool>(true);
            //Act

            var actual = golfClubFacade.AddAllGolfClubs(golfClubDtos.ToArray());

            //Assert
            Assert.Equal(expected.Value, actual.Value);
        }

        [Fact]
        public void DeleteGolfClub_Fails_InvalidGolfClubId()
        {
            //Arrange
            var golfClubId = -1;

            var expected = ApiResponseDto<bool>.Error("Invalid Golf Club Id");

            //Act
            var actual = golfClubFacade.DeleteGolfClub(golfClubId);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteGolfClub_Fails_GolfClubNotFound()
        {
            //Arrange
            var golfClubId = 1;

            var expected = ApiResponseDto<bool>.Error("Golf Club Not Found");

            //Act
            var actual = golfClubFacade.DeleteGolfClub(golfClubId);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void DeleteGolfClub_Succeeds_GolfClubDeleted()
        {
            //Arrange
            var golfClubId = 1;

            var golfClubCount = 1;

            var golfClubs = new List<GolfClub>
            {
                new GolfClub
                {
                    SerialNumber = "1220000",

                    ClubType = "Iron",

                    IsAvailable = false
                },

                new GolfClub
                {
                    SerialNumber = "1250000",

                    ClubType = "Iron",

                    IsAvailable = true
                }
            };

            context.GolfClubs.AddRange(golfClubs);

            context.SaveChanges();

            //Act
            var actual = golfClubFacade.DeleteGolfClub(golfClubId);

            var clubs = golfClubFacade.GetGolfClubs();

            var actualCount = clubs.Value.Count();

            //Assert
            Assert.Equal(golfClubCount, actualCount);
        }

        [Fact]
        public void GetGolfClubs_Succeeds_ReturnsClubs()
        {
            //Arrange 
            var golfClubs = new List<GolfClub>
            {
                new GolfClub
                {
                    SerialNumber = "1220000",

                    ClubType = "Iron",

                    IsAvailable = false
                },

                new GolfClub
                {
                    SerialNumber = "1250000",

                    ClubType = "Iron",

                    IsAvailable = true
                }
            };

            context.GolfClubs.AddRange(golfClubs);

            context.SaveChanges();

            var golfClubDtos = new List<GolfClubDto>
            {
                new GolfClubDto
                {
                    Id = 1,

                    SerialNumber = "1220000",

                    ClubType = "Iron",

                    IsAvailable = false
                },

                new GolfClubDto
                {
                    Id = 2,

                    SerialNumber = "1250000",

                    ClubType = "Iron",

                    IsAvailable = true
                }
            };

            var expected = new ApiResponseDto<IEnumerable<GolfClubDto>>(golfClubDtos.AsEnumerable());

            //Act
            var actual = golfClubFacade.GetGolfClubs();

            //Assert

            Assert.Equal(expected.Value.ToList()[0].Id, actual.Value.ToList()[0].Id);
            Assert.Equal(expected.Value.ToList()[0].SerialNumber, actual.Value.ToList()[0].SerialNumber);
            Assert.Equal(expected.Value.ToList()[0].IsAvailable, actual.Value.ToList()[0].IsAvailable);
        }

        [Fact]
        public void GetGolfClubById_Fails_InvalidIdZero()
        {
            //Arrange
            var expected = ApiResponseDto<GolfClubDto>.Error("Invalid Golf Club Id");

            var id = 0;

            //Act
            var actual = golfClubFacade.GetGolfClubById(id);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void GetGolfClubById_Fails_InvalidIdNegativeNumber()
        {
            //Arrange
            var expected = ApiResponseDto<GolfClubDto>.Error("Invalid Golf Club Id");

            var id = -1;

            //Act
            var actual = golfClubFacade.GetGolfClubById(id);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void GetGolfClubById_Fails_GolfClubNotFound()
        {
            //Arrange 
            var expected = ApiResponseDto<GolfClubDto>.Error("Golf Club Not Found");

            var id = 1;

            //Act
            var actual = golfClubFacade.GetGolfClubById(id);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void GetGolfClubById_Succeeds_GolfClubFound()
        {
            //Arrange 
            var golfClub = new GolfClub
            {
                SerialNumber = "5550000",

                ClubType = "Iron",

                IsAvailable = false,
            };

            var golfClubDto = new GolfClubDto
            {
                Id = 1,

                SerialNumber = "5550000",

                ClubType = "Iron",

                IsAvailable = false,
            };

            context.GolfClubs.Add(golfClub);

            context.SaveChanges();

            var expected = new ApiResponseDto<GolfClubDto>(golfClubDto);

            var id = 1;

            //Act
            var actual = golfClubFacade.GetGolfClubById(id);

            //Assert
            Assert.Equal(expected.Value.Id, actual.Value.Id);
            Assert.Equal(expected.Value.SerialNumber, actual.Value.SerialNumber);
            Assert.Equal(expected.Value.IsAvailable, actual.Value.IsAvailable);
        }

        [Fact]
        public void UpdateGolfClub_Fails_InvalidGolfClubId()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Invalid Golf Club Id");

            var golfClubId = 0;

            //Act
            var actual = golfClubFacade.UpdateGolfClub(golfClubId, null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateGolfClub_Fails_GolfClubNotFound()
        {
            //Arrange
            var expected = ApiResponseDto<bool>.Error("Golf Club Not Found");

            var golfClubId = 1;

            //Act
            var actual = golfClubFacade.UpdateGolfClub(golfClubId, null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateGolfClub_Fails_InvalidSerialNumber()
        {
            //Arrange
            var golfClubDto = new UpdateGolfClubDto
            {
                SerialNumber = "  ",

                ClubType = "Iron",

                IsAvailable = false,
            };

            var golfClub = new GolfClub
            {
                SerialNumber = "5550000",

                ClubType = "Iron",

                IsAvailable = false,
            };

            context.GolfClubs.Add(golfClub);

            context.SaveChanges();

            var id = 1;

            var expected = ApiResponseDto<bool>.Error("Invalid Serial Number");

            //Act
            var actual = golfClubFacade.UpdateGolfClub(id, golfClubDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateGolfClub_Fails_InvalidClubType()
        {
            //Arrange
            var golfClubDto = new UpdateGolfClubDto
            {
                SerialNumber = "5550000",

                ClubType = "  ",

                IsAvailable = false,
            };

            var golfClub = new GolfClub
            {
                SerialNumber = "5550000",

                ClubType = "Iron",

                IsAvailable = false,
            };

            context.GolfClubs.Add(golfClub);

            context.SaveChanges();

            var id = 1;

            var expected = ApiResponseDto<bool>.Error("Invalid Club Type");

            //Act
            var actual = golfClubFacade.UpdateGolfClub(id, golfClubDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateGolfClub_Fails_InvalidBookingId()
        {
            //Arrange
            var golfClubDto = new UpdateGolfClubDto
            {
                BookingId = -1,
                SerialNumber = "0005463",
                ClubType = "Iron",
                IsAvailable = true
            };

            var golfClub = new GolfClub
            {
                SerialNumber = "5550000",

                ClubType = "Iron",

                IsAvailable = false
            };

            context.GolfClubs.Add(golfClub);

            context.SaveChanges();

            var id = 1;

            var expected = ApiResponseDto<bool>.Error("Invalid Booking Id");

            //Act
            var actual = golfClubFacade.UpdateGolfClub(id, golfClubDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateGolfClub_Fails_BookingNotFound()
        {
            //Arrange
            var golfClubDto = new UpdateGolfClubDto
            {
                BookingId = 1,

                SerialNumber = "0005463",

                ClubType = "Iron",

                IsAvailable = true
            };

            var golfClub = new GolfClub
            {
                SerialNumber = "5550000",

                ClubType = "Iron",

                IsAvailable = false
            };

            context.GolfClubs.Add(golfClub);

            context.SaveChanges();

            var id = 1;

            var expected = ApiResponseDto<bool>.Error("Booking Not Found");

            //Act
            var actual = golfClubFacade.UpdateGolfClub(id, golfClubDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void UpdateGolfClub_Succeeds_GolfClubUpdated()
        {
            //Arrange
            var booking = new Booking
            {
                Start = DateTime.Today,
                //Lane = 4,
                CustomerId = 1,
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com"
            };

            var golfClubDto = new UpdateGolfClubDto
            {
                BookingId = 1,
                SerialNumber = "0005463",
                ClubType = "Iron",
                IsAvailable = true
            };

            var golfClub = new GolfClub
            {
                SerialNumber = "5550000",

                ClubType = "Iron",

                IsAvailable = false
            };

            context.CustomerInformation.Add(customer);

            context.Bookings.Add(booking);

            context.GolfClubs.Add(golfClub);

            context.SaveChanges();

            var id = 1;

            var expected = new ApiResponseDto<bool>(true);

            //Act
            var actual = golfClubFacade.UpdateGolfClub(id, golfClubDto);

            var updatedClub = golfClubFacade.GetGolfClubById(id);

            //Assert
            Assert.Equal(updatedClub.Value.BookingId, golfClubDto.BookingId);
            Assert.Equal(updatedClub.Value.SerialNumber, golfClubDto.SerialNumber);
            Assert.Equal(updatedClub.Value.IsAvailable, golfClubDto.IsAvailable);
            Assert.Equal(expected.Value, actual.Value);
        }
    }
}
