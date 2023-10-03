using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                serialNumber = 0,

                isAvailable = false,
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
                serialNumber = 5550000,

                isAvailable = false,
            };

            var expected = new ApiResponseDto<bool>(true);
            //Act

            var actual = golfClubFacade.AddGolfClub(golfClubDto);

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
                    SerialNumber = 1220000,

                    IsAvailable = false
                },

                new GolfClub
                {
                    SerialNumber = 1250000,

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
                    SerialNumber = 1220000,

                    IsAvailable = false
                },

                new GolfClub
                {
                    SerialNumber = 1250000,

                    IsAvailable = true
                }
            };

            context.GolfClubs.AddRange(golfClubs);

            context.SaveChanges();

            var golfClubDtos = new List<GolfClubDto>
            {
                new GolfClubDto
                {
                    id = 1,

                    serialNumber = 1220000,

                    isAvailable = false
                },

                new GolfClubDto
                {
                    id = 2,

                    serialNumber = 1250000,

                    isAvailable = true
                }
            };

            var expected = new ApiResponseDto<IEnumerable<GolfClubDto>>(golfClubDtos.AsEnumerable());

            //Act
            var actual = golfClubFacade.GetGolfClubs();

            //Assert

            Assert.Equal(expected.Value.ToList()[0].id, actual.Value.ToList()[0].id);
            Assert.Equal(expected.Value.ToList()[0].serialNumber, actual.Value.ToList()[0].serialNumber);
            Assert.Equal(expected.Value.ToList()[0].isAvailable, actual.Value.ToList()[0].isAvailable);
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
                SerialNumber = 5550000,

                IsAvailable = false,
            };

            var golfClubDto = new GolfClubDto
            {
                id = 1,

                serialNumber = 5550000,

                isAvailable = false,
            };

            context.GolfClubs.Add(golfClub);

            context.SaveChanges();

            var expected = new ApiResponseDto<GolfClubDto>(golfClubDto);

            var id = 1;

            //Act
            var actual = golfClubFacade.GetGolfClubById(id);

            //Assert
            Assert.Equal(expected.Value.id, actual.Value.id);
            Assert.Equal(expected.Value.serialNumber, actual.Value.serialNumber);
            Assert.Equal(expected.Value.isAvailable, actual.Value.isAvailable);
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
                serialNumber = -1,
                isAvailable = false,
            };
            
            var golfClub = new GolfClub
            {
                SerialNumber = 5550000,

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
        public void UpdateGolfClub_Fails_InvalidBookingId()
        {
            //Arrange
            var golfClubDto = new UpdateGolfClubDto
            {
                bookingId = -1,
                serialNumber = 0005463,
                isAvailable = true
            };

            var golfClub = new GolfClub
            {
                SerialNumber = 5550000,

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
                bookingId = 1,
                serialNumber = 0005463,
                isAvailable = true
            };

            var golfClub = new GolfClub
            {
                SerialNumber = 5550000,

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
                DateBooked = DateTime.Today,
                //Lane = 4,
                CustomerId = 1,
            };

            var customer = new CustomerInformation
            {
                Name = "J",
                Email = "e@gmail.com",
                IsPaid = false
            };

            var golfClubDto = new UpdateGolfClubDto
            {
                bookingId = 1,
                serialNumber = 0005463,
                isAvailable = true
            };

            var golfClub = new GolfClub
            {
                SerialNumber = 5550000,

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
            Assert.Equal(updatedClub.Value.bookingId, golfClubDto.bookingId);
            Assert.Equal(updatedClub.Value.serialNumber, golfClubDto.serialNumber);
            Assert.Equal(updatedClub.Value.isAvailable, golfClubDto.isAvailable);
            Assert.Equal(expected.Value, actual.Value);
        }
    }
}
