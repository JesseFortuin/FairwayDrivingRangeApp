using FairwayDrivingRange.Domain.Entities;
using FairwayDrivingRange.Shared.Dtos;

namespace FairwayDrivingRange.Test
{
    public class AdminFacadeTest : BaseTestSetup
    {
        [Fact]
        public void Login_Fails_InvalidAdminDtoNull()
        {
            //Arrange
            var expected = ApiResponseDto<string>.Error("Insert Employee Details");

            //Act
            var actual = adminFacade.Login(null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void Login_Fails_InvalidAdminDtoInvalidName()
        {
            //Arrange
            var adminDto = new AdminDto
            {
                AdminName = "",
                Password = "password"
            };

            var expected = ApiResponseDto<string>.Error("Insert Valid Details");

            //Act
            var actual = adminFacade.Login(adminDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void Login_Fails_InvalidAdminDtoInvalidPassword()
        {
            //Arrange
            var adminDto = new AdminDto
            {
                AdminName = "Jesse",
                Password = "  "
            };

            var expected = ApiResponseDto<string>.Error("Insert Valid Details");

            //Act
            var actual = adminFacade.Login(adminDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void Login_Fails_AdminNameNotFoundInDatabase()
        {
            //Arrange
            var adminDto = new AdminDto
            {
                AdminName = "Jesse",
                Password = "password"
            };

            var expected = ApiResponseDto<string>.Error("Invalid Username Or Password");

            //Act
            var actual = adminFacade.Login(adminDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void Login_Fails_PasswordDoesNotMatch()
        {
            //Arrange
            var admin = new Admin
            {
                AdminName = "Jesse",
                Password = BCrypt.Net.BCrypt.HashPassword("password")
            };

            var adminDto = new AdminDto
            {
                AdminName = "Jesse",
                Password = "passwor"
            };

            context.Admins.Add(admin);

            context.SaveChanges();

            var expected = ApiResponseDto<string>.Error("Invalid Username Or Password");

            //Act
            var actual = adminFacade.Login(adminDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void Login_Succeeds_TokenReturned()
        {
            //Arrange
            var admin = new Admin
            {
                AdminName = "Jesse",
                Password = BCrypt.Net.BCrypt.HashPassword("password")
            };

            var adminDto = new AdminDto
            {
                AdminName = "Jesse",
                Password = "password"
            };

            context.Admins.Add(admin);

            context.SaveChanges();

            var expected = new ApiResponseDto<string>("");

            //Act
            var actual = adminFacade.Login(adminDto);

            //Assert
            Assert.Equal(expected.IsSuccess, actual.IsSuccess);
        }

        [Fact]
        public void RegisterAdmin_Fails_InvalidAdminDtoNull()
        {
            //Arrange
            var expected = ApiResponseDto<string>.Error("Insert Employee Details");

            //Act
            var actual = adminFacade.RegisterAdmin(null);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void RegisterAdmin_Fails_InvalidAdminDtoInvalidName()
        {
            //Arrange
            var adminDto = new AdminDto
            {
                AdminName = "",
                Password = "password"
            };

            var expected = ApiResponseDto<string>.Error("Insert Valid Details");

            //Act
            var actual = adminFacade.RegisterAdmin(adminDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void RegisterAdmin_Fails_InvalidAdminDtoInvalidPassword()
        {
            //Arrange
            var adminDto = new AdminDto
            {
                AdminName = "Jesse",
                Password = "  "
            };

            var expected = ApiResponseDto<string>.Error("Insert Valid Details");

            //Act
            var actual = adminFacade.RegisterAdmin(adminDto);

            //Assert
            Assert.Equal(expected.ErrorMessage, actual.ErrorMessage);
        }

        [Fact]
        public void RegisterAdmin_Succeeds_TokenReturned()
        {
            //Arrange
            var admin = new Admin
            {
                AdminName = "Jesse",
                Password = BCrypt.Net.BCrypt.HashPassword("password")
            };

            var adminDto = new AdminDto
            {
                AdminName = "Jesse",
                Password = "password"
            };

            context.Admins.Add(admin);

            context.SaveChanges();

            var expected = new ApiResponseDto<bool>(true);

            //Act
            var actual = adminFacade.RegisterAdmin(adminDto);

            //Assert
            Assert.Equal(expected.Value, actual.Value);
        }
    }
}
