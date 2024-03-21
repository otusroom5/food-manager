using FoodUserAuth.DataAccess.Abstractions;
using Moq;
using FoodUserAuth.DataAccess.Entities;
using FoodUserAuth.BusinessLogic.Dto;
using static FoodUserAuth.BusinessLogic.Services.UserVerificationService;
using FoodUserAuth.BusinessLogic.Exceptions;
using FoodUserAuth.BusinessLogic.Tests.Utils;

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
            var userService = new UsersService(usersRepositoryMock.Object, CreateDefaultPasswordHasher());

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
                .Setup(f => f.GetAll())
                .Returns(new User[] { user });
            usersRepositoryMock
                .Setup(f => f.Update(It.IsAny<User>()))
                .Callback<User>(f => user.Password = f.Password);
            var userService = new UsersService(usersRepositoryMock.Object, CreateDefaultPasswordHasher());

            userService.ChangePassword(user.Id, DefaultNewPassword);

            Assert.That(user.Password, Is.EqualTo(DefaultNewPasswordHashed));
        }

        [Test()]
        public void ChangePassword_PutNullPassword_ThrowArgumentNullException()
        {
            User user = CreateFakeUser();
            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock
                .Setup(f => f.GetAll())
                .Returns(new User[] { user });
            Guid userId = usersRepositoryMock.Object.GetAll().First().Id;
            var userService = new UsersService(usersRepositoryMock.Object, CreateDefaultPasswordHasher());

            Assert.Throws<ArgumentNullException>(() => userService.ChangePassword(userId, null));
        }

        [Test()]
        public void ChangePassword_PutEmptyPassword_ThrowArgumentException()
        {
            User user = CreateFakeUser();
            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock
                .Setup(f => f.GetAll())
                .Returns(new User[] { user });
            Guid userId = usersRepositoryMock.Object.GetAll().First().Id;
            var userService = new UsersService(usersRepositoryMock.Object, CreateDefaultPasswordHasher());

            Assert.Throws<ArgumentException>(() => userService.ChangePassword(userId, string.Empty));
        }

        [Test()]
        public void ChangePassword_PutNotExisingUser_ThrowUserNotFoundException()
        {
            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock
                .Setup(f => f.GetAll())
                .Returns(new User[] { });
            UsersService service = new UsersService(usersRepositoryMock.Object, CreateDefaultPasswordHasher());

            Assert.Throws<UserNotFoundException>(() => service.ChangePassword(Guid.NewGuid(), DefaultNewPassword));
        }

        #endregion

        #region CreateUser()

        [Test()]
        public void CreateUser_CheckCorrectUserDetails_True()
        {
            User actual = null;
            Guid newGuid = Guid.NewGuid();
            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock
                .Setup(f => f.Create(It.IsAny<User>()))
                .Callback<User>(f => actual = f)
                .Returns(newGuid);
            UserDto newUser = CreateFakeUserDto();
            var userService = new UsersService(usersRepositoryMock.Object, CreateDefaultPasswordHasher());

            Guid createdId = userService.CreateUser(newUser);

            Assert.That(actual?.Id, Is.EqualTo(newGuid)); // Id
            Assert.That(createdId, Is.EqualTo(newGuid)); // Check result of CreateUser method
            Assert.That(actual?.State, Is.EqualTo(newUser.State));
            Assert.That(actual?.UserName, Is.EqualTo(newUser.UserName));
            Assert.That(actual?.FullName, Is.EqualTo(newUser.FullName));
            Assert.That(actual?.Email, Is.EqualTo(newUser.Email));
        }

        [Test()]
        public void CreateUser_PutNullToUserName_ThrowArgumentNullException()
        {
            var usersRepositoryMock = new Mock<IUsersRepository>();
            usersRepositoryMock
                .Setup(f => f.Create(It.IsAny<User>()))
                .Returns(Guid.NewGuid());
            var userService = new UsersService(usersRepositoryMock.Object, CreateDefaultPasswordHasher());
            UserDto newUser = CreateFakeUserDto();
            newUser.UserName = null;

            Assert.Throws<ArgumentNullException>(() => userService.CreateUser(newUser));
        }

        [Test()]
        public void CreateUser_PutEmptyToUserName_ThrowArgumentException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.Create(It.IsAny<User>()))
                .Returns(Guid.NewGuid());
            var userService = new UsersService(usersRepository.Object, CreateDefaultPasswordHasher());
            UserDto newUser = CreateFakeUserDto();
            newUser.UserName = string.Empty;

            Assert.Throws<ArgumentException>(() => userService.CreateUser(newUser));
        }

        [Test()]
        public void CreateUser_PutNullToFullName_ThrowArgumentNullException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.Create(It.IsAny<User>()))
                .Returns(Guid.NewGuid());
            var userService = new UsersService(usersRepository.Object, CreateDefaultPasswordHasher());
            UserDto newUser = CreateFakeUserDto();
            newUser.FullName = null;
            
            Assert.Throws<ArgumentNullException>(() => userService.CreateUser(newUser));
        }

        [Test()]
        public void CreateUser_PutEmptyToFullName_ThrowArgumentException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.Create(It.IsAny<User>()))
                .Returns(Guid.NewGuid());
            var userService = new UsersService(usersRepository.Object, CreateDefaultPasswordHasher());
            UserDto newUser = CreateFakeUserDto();
            newUser.FullName = string.Empty;
            
            Assert.Throws<ArgumentException>(() => userService.CreateUser(newUser));
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

            var userService = new UsersService(usersRepository.Object, CreateDefaultPasswordHasher());
            userService.DisableUser(fakeUser.Id);

            Assert.That(actual, Is.EqualTo(UserState.Disabled));
        }

        public void DisableUser_PutIncorrectUserId_ThrowUserNotFoundException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.GetAll())
                .Returns(new User[] { });
            var userService = new UsersService(usersRepository.Object, CreateDefaultPasswordHasher());

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
        public void UpdateUser_PutIncorrectUserId_ThrowUserNotFoundException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.GetAll())
                .Returns(new User[] { UsersUtils.CreateFakeUser() });
            usersRepository
                .Setup(f => f.Update(It.IsAny<User>()));
            var userService = new UsersService(usersRepository.Object, CreateDefaultPasswordHasher());
            UserDto userDto = new UserDto(Guid.NewGuid(), UserState.Enabled);

            Assert.Throws<UserNotFoundException>(() => userService.UpdateUser(userDto));
        }

        [Test()]
        public void UpdateUser_PutNullToUserNameForExistUser_ThrowArgumentNullException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.GetAll())
                .Returns(new User[] { UsersUtils.CreateFakeUser() });
            usersRepository
                .Setup(f => f.Update(It.IsAny<User>()));
            var userService = new UsersService(usersRepository.Object, CreateDefaultPasswordHasher());
            UserDto userDto = userService.GetAll().First();
            userDto.UserName = null;

            Assert.Throws<ArgumentNullException>(() => userService.UpdateUser(userDto));
        }

        [Test()]
        public void UpdateUser_PutEmptyToUserNameForExistUser_ThrowArgumentException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.GetAll())
                .Returns(new User[] { UsersUtils.CreateFakeUser() });
            usersRepository
                .Setup(f => f.Update(It.IsAny<User>()));
            var userService = new UsersService(usersRepository.Object, CreateDefaultPasswordHasher());
            UserDto userDto = userService.GetAll().First();
            userDto.UserName = string.Empty;

            Assert.Throws<ArgumentException>(() => userService.UpdateUser(userDto));
        }

        [Test()]
        public void UpdateUser_PutNullToFullNameForExistUser_ThrowArgumentNullException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.GetAll())
                .Returns(new User[] { UsersUtils.CreateFakeUser() });
            usersRepository
                .Setup(f => f.Update(It.IsAny<User>()));
            var userService = new UsersService(usersRepository.Object, CreateDefaultPasswordHasher());
            UserDto userDto = userService.GetAll().First();
            userDto.FullName = null;

            Assert.Throws<ArgumentNullException>(() => userService.UpdateUser(userDto));
        }

        [Test()]
        public void UpdateUser_PutEmptyToFullNameForExistUser_ThrowArgumentException()
        {
            var usersRepository = new Mock<IUsersRepository>();
            usersRepository
                .Setup(f => f.GetAll())
                .Returns(new User[] { UsersUtils.CreateFakeUser() });
            usersRepository
                .Setup(f => f.Update(It.IsAny<User>()));
            var userService = new UsersService(usersRepository.Object, CreateDefaultPasswordHasher());
            UserDto userDto = userService.GetAll().First();
            userDto.FullName = string.Empty;

            Assert.Throws<ArgumentException>(() => userService.UpdateUser(userDto));
        }

        #endregion


        public User CreateFakeUser()
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                Email = Faker.Internet.Email(),
                FullName = Faker.Name.FullName(),
                UserName = Faker.Name.First(),
                State = Faker.Enum.Random<UserState>(),
                Password = Faker.Identification.UkNhsNumber()
            };
        }
        private UserDto[] ConvertToUserDto(IEnumerable<User> users)
        {
            var result = new List<UserDto>();
            foreach (var user in users)
            {
                result.Add(new UserDto(user.Id, user.State)
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    UserName = user.UserName
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

        private UserDto CreateFakeUserDto()
        {
            return new UserDto(Faker.Enum.Random<UserState>())
            {
                Email = Faker.Internet.Email(),
                FullName = Faker.Name.FullName(),
                UserName = Faker.Name.First()
            };
        }
    }
}