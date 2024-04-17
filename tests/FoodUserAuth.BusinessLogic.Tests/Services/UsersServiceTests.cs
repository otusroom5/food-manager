using FoodUserAuth.DataAccess.Abstractions;
using Moq;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.BusinessLogic.Dto;
using FoodUserAuth.BusinessLogic.Exceptions;
using FoodUserAuth.BusinessLogic.Tests.Utils;
using FoodUserAuth.DataAccess.Types;
using static FoodUserAuth.BusinessLogic.Services.UserVerificationService;
using FoodUserAuth.BusinessLogic.Interfaces;

namespace FoodUserAuth.BusinessLogic.Services.Tests
{
    [TestFixture()]
    public class UsersServiceTests
    {
        public static string DefaultNewPassword = $"124+4edasd3w";
        public static string DefaultNewPasswordHashed = $"{DefaultNewPassword}!";

        #region GetAll()

        [Test()]
        public void GetAll_CheckResultList_ShouldBeEquals()
        {
            User[] fakeUsers = UsersUtils.CreateFakeUsers();
            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock
                .Setup(f => f.GetAll())
                .Returns(fakeUsers);
            IEnumerable<UserDto> expected = ConvertToUserDto(fakeUsers);
            var userService = new UsersService(usersRepositoryMock.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            IEnumerable<UserDto> actual = userService.GetAll();

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

        #region ChangePassword()

        [Test()]
        public void ChangePassword_PasswordChangedWithHash_True()
        {
            User user = CreateFakeUser();
            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock
                .Setup(f => f.GetById(It.IsAny<Guid>()))
                .Returns(user);
            usersRepositoryMock
                .Setup(f => f.Update(It.IsAny<User>()))
                .Callback<User>(f => user.Password = f.Password);
            var userService = new UsersService(usersRepositoryMock.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            userService.ChangePassword(user.Id, DefaultNewPassword);

            Assert.That(user.Password, Is.EqualTo(DefaultNewPasswordHashed));
        }

        [Test()]
        public void ChangePassword_PutEmptyOrNullPassword_ThrowArgumentException()
        {
            User user = CreateFakeUser();
            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock
                .Setup(f => f.GetAll())
                .Returns(new User[] { user });
            Guid userId = usersRepositoryMock.Object.GetAll().First().Id;
            var userService = new UsersService(usersRepositoryMock.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            Assert.Throws<ArgumentException>(() => userService.ChangePassword(userId, string.Empty));
            Assert.Throws<ArgumentException>(() => userService.ChangePassword(userId, null));
        }

        [Test()]
        public void ChangePassword_PutNotExisingUser_ThrowUserNotFoundException()
        {
            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock
                .Setup(f => f.GetAll())
                .Returns(new User[] { });
            UsersService service = new UsersService(usersRepositoryMock.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            Assert.Throws<UserNotFoundException>(() => service.ChangePassword(Guid.NewGuid(), DefaultNewPassword));
        }

        #endregion

        #region CreateUser()

        [Test()]
        public void CreateUser_PutNullToUserArgument_ThrowArgumentNullException()
        {
            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock
                .Setup(f => f.Create(It.IsAny<User>()));
            var userService = new UsersService(usersRepositoryMock.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            Assert.Throws<ArgumentNullException>(() => userService.CreateUser(null));
        }

        [Test()]
        public void CreateUser_CheckCorrectUserData_True()
        {
            User actual = null;
            Guid newGuid = Guid.NewGuid();
            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock
                .Setup(f => f.Create(It.IsAny<User>()))
                .Callback<User>(f => actual = f);
            var userService = new UsersService(usersRepositoryMock.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());
            var createdUser = userService.CreateUser(CreateFakeUserDto());

            Assert.IsNotNull(actual);
            Assert.That(actual.Id, Is.EqualTo(createdUser.User.Id));
            Assert.That(actual.State, Is.EqualTo(createdUser.User.State));
            Assert.That(actual.LoginName, Is.EqualTo(createdUser.User.LoginName));
            Assert.That(actual.FirstName, Is.EqualTo(createdUser.User.FirstName));
            Assert.That(actual.LastName, Is.EqualTo(createdUser.User.LastName));
            Assert.That(actual.Email, Is.EqualTo(createdUser.User.Email));
        }

        [Test()]
        public void CreateUser_PutNullOrEmptyToLoginName_ThrowRequiredPropertyException()
        {
            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock
                .Setup(f => f.Create(It.IsAny<User>()));
            var userService = new UsersService(usersRepositoryMock.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            Assert.Throws<RequiredPropertyException>(() => userService.CreateUser(new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                LoginName = null
            }));
            Assert.Throws<RequiredPropertyException>(() => userService.CreateUser(new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                LoginName = string.Empty
            }));
        }

        [Test()]
        public void CreateUser_PutNullOrEmptyToFirstName_ThrowRequiredPropertyException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository.Setup(f => f.Create(It.IsAny<User>()));
            var userService = new UsersService(usersRepository.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            Assert.Throws<RequiredPropertyException>(() => userService.CreateUser(new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = string.Empty,
                LastName = Faker.Name.Last(),
                LoginName = Faker.Name.First()
            }));
            Assert.Throws<RequiredPropertyException>(() => userService.CreateUser(new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = null,
                LastName = Faker.Name.Last(),
                LoginName = Faker.Name.First()
            }));
        }

        [Test()]
        public void CreateUser_PutNullOrEmptyToLastName_ThrowRequiredPropertyException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository.Setup(f => f.Create(It.IsAny<User>()));
            var userService = new UsersService(usersRepository.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            Assert.Throws<RequiredPropertyException>(() => userService.CreateUser(new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = Faker.Name.First(),
                LastName = string.Empty,
                LoginName = Faker.Name.First()
            }));
            Assert.Throws<RequiredPropertyException>(() => userService.CreateUser(new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = Faker.Name.First(),
                LastName = null,
                LoginName = Faker.Name.First()
            }));
        }

        [Test()]
        public void CreateUser_PutExistingLoginName_ThrowUserAlreadyExistException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository.Setup(f => f.FindUserByLoginName("test"))
                .Returns(() => new UserDto() { LoginName = "test" });
            var userService = new UsersService(usersRepository.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            Assert.Throws<UserAlreadyExistException>(() => userService.CreateUser(new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = Faker.Name.First(),
                LastName = string.Empty,
                LoginName = "test"
            }));
        }

        #endregion

        #region DisableUser()

        [Test()]
        public void DisableUser_TryToDisableUser_True()
        {
            UserState actual = default(UserState);
            User fakeUser = CreateEnabledFakeUser();
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.GetAll())
                .Returns(new User[] { fakeUser });
            usersRepository
                .Setup(f => f.Update(It.IsAny<User>()))
                .Callback<User>(f => actual = f.State);

            var userService = new UsersService(usersRepository.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());
            userService.DisableUser(fakeUser.Id);

            Assert.That(actual, Is.EqualTo(UserState.Disabled));
        }

        public void DisableUser_PutIncorrectUserId_ThrowUserNotFoundException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.GetAll())
                .Returns(new User[] { });
            var userService = new UsersService(usersRepository.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            Assert.Throws<UserNotFoundException>(() => userService.DisableUser(Guid.NewGuid()));
        }

        private User CreateEnabledFakeUser()
        {
            User fakeUser = UsersUtils.CreateFakeUser();
            fakeUser.State = UserState.Enabled;
            return fakeUser;
        }

        #endregion

        #region UpdateUser()

        [Test()]
        public void UpdateUser_PutNullToUserArgument_ThrowArgumentNullException()
        {
            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock
                .Setup(f => f.Create(It.IsAny<User>()));
            var userService = new UsersService(usersRepositoryMock.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            Assert.Throws<ArgumentNullException>(() => userService.UpdateUser(null));
        }

        [Test()]
        public void UpdateUser_PutNonExistUserId_ThrowUserNotFoundException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.GetAll())
                .Returns(new User[] { UsersUtils.CreateFakeUser() });
            usersRepository
                .Setup(f => f.Update(It.IsAny<User>()));
            var userService = new UsersService(usersRepository.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());
            UserDto userDto = CreateFakeUserDto();
            userDto.Id = Guid.NewGuid();

            Assert.Throws<UserNotFoundException>(() => userService.UpdateUser(userDto));
        }

        [Test()]
        public void UpdateUser_PutEmptyOrNullToLoginName_ThrowRequiredPropertyException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.GetAll())
                .Returns(new User[] { UsersUtils.CreateFakeUser() });
            usersRepository
                .Setup(f => f.Update(It.IsAny<User>()));
            var userService = new UsersService(usersRepository.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            Assert.Throws<RequiredPropertyException>(() => userService.UpdateUser(new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                LoginName = null
            }));
            Assert.Throws<RequiredPropertyException>(() => userService.UpdateUser(new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                LoginName = string.Empty
            }));
        }

        [Test()]
        public void UpdateUser_PutEmptyOrNullToFirstName_ThrowArgumentException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.GetAll())
                .Returns(new User[] { UsersUtils.CreateFakeUser() });
            usersRepository
                .Setup(f => f.Update(It.IsAny<User>()));
            var userService = new UsersService(usersRepository.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            Assert.Throws<RequiredPropertyException>(() => userService.UpdateUser(new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = null,
                LastName = Faker.Name.Last(),
                LoginName = Faker.Name.First()
            }));
            Assert.Throws<RequiredPropertyException>(() => userService.UpdateUser(new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = string.Empty,
                LastName = Faker.Name.Last(),
                LoginName = Faker.Name.First()
            }));
        }

        [Test()]
        public void UpdateUser_PutEmptyOrNullToLastName_ThrowArgumentException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.GetAll())
                .Returns(new User[] { UsersUtils.CreateFakeUser() });
            usersRepository
                .Setup(f => f.Update(It.IsAny<User>()));
            var userService = new UsersService(usersRepository.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            Assert.Throws<RequiredPropertyException>(() => userService.UpdateUser(new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = Faker.Name.First(),
                LastName = null,
                LoginName = Faker.Name.First()
            }));
            Assert.Throws<RequiredPropertyException>(() => userService.UpdateUser(new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = Faker.Name.First(),
                LastName = string.Empty,
                LoginName = Faker.Name.First()
            }));
        }

        [Test()]
        public void UpdateUser_PutExistingLoginName_ThrowUserAlreadyExistException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository.Setup(f => f.FindUserByLoginName("test"))
                .Returns(() => new UserDto() { LoginName = "test" });
            var userService = new UsersService(usersRepository.Object,
                CreateDefaultPasswordHasher(),
                CreateDefaultPasswordGenerator());

            Assert.Throws<UserAlreadyExistException>(() => userService.UpdateUser(new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = Faker.Name.First(),
                LastName = string.Empty,
                LoginName = "test"
            }));
        }

        #endregion

        public User CreateFakeUser()
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                Email = Faker.Internet.Email(),
                FirstName = Faker.Name.FullName(),
                LoginName = Faker.Name.First(),
                State = Faker.Enum.Random<UserState>(),
                Password = Faker.Identification.UkNhsNumber()
            };
        }

        private UserDto[] ConvertToUserDto(IEnumerable<User> users)
        {
            var result = new List<UserDto>();
            foreach (var user in users)
            {
                result.Add(new UserDto()
                {
                    Id = user.Id,
                    State = user.State,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    LoginName = user.LoginName
                });
            }
            return result.ToArray();
        }

        private IPasswordHasher CreateDefaultPasswordHasher()
        {
            var passwordHasherMock = new Mock<IPasswordHasher>();
            passwordHasherMock
                .Setup(f => f.ComputeHash(DefaultNewPassword))
                .Returns<string>(f => DefaultNewPasswordHashed);
            return passwordHasherMock.Object;
        }

        private IPasswordGenerator CreateDefaultPasswordGenerator()
        {
            var passwordGenerator = new Mock<IPasswordGenerator>();
            passwordGenerator
                .Setup(f => f.GeneratePassword(It.IsAny<UserDto>()))
                .Returns<string>((userDto) => "test");
            return passwordGenerator.Object;
        }

        private UserDto CreateFakeUserDto()
        {
            return new UserDto()
            {
                State = Faker.Enum.Random<UserState>(),
                Email = Faker.Internet.Email(),
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                LoginName = Faker.Name.First()
            };
        }
    }
}