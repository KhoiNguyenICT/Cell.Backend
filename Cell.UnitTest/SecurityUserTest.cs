using Cell.Helpers.Extensions;
using Cell.Model;
using Cell.Model.Entities.SecurityUserEntity;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;

namespace Cell.UnitTest
{
    public class SecurityUserTest
    {
        private Mock<AppDbContext> _context;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<AppDbContext>();
        }

        [Test]
        public void InitializeData()
        {
            var users = Builder<SecurityUser>.CreateListOfSize(1000)
                .All()
                .With(x => x.Code == "USER")
                .With(x => x.Description = Faker.Lorem.Paragraph())
                .With(x => x.Account = Faker.Name.FullName().Split(" ").JoinString("_"))
                .With(x => x.Email = Faker.Internet.Email())
                .With(x => x.EncryptedPassword == "02fcdb81781c12d13ee67bbf56c0fba02af236f41aefb8e3bda4331132fb5971")
                .With(x => x.Phone = Faker.Phone.Number())
                .With(x => x.Settings == "{\"Information\":{\"ProfileImage\":null,\"FullName\":\"Super Admin\",\"BirthDay\":\"2019-07-12T00:00:00Z\",\"Address\":\"Số 102, Thái Thịnh, Đống Đa, Hà Nội\"},\"Departments\":[],\"DefaultDepartmentData\":null,\"Roles\":[{\"Id\":\"70886cc0-5566-4b48-8b2a-e9818cfcb5d8\",\"Name\":\"System\",\"Description\":\"System\"}],\"DefaultRoleData\":null}")
                .Build();
            foreach (var securityUser in users)
            {
                _context.Object.Add(new SecurityUser
                {
                    Code = securityUser.Code,
                    Description = securityUser.Description,
                    Account = securityUser.Account,
                    Email = securityUser.Email,
                    EncryptedPassword = securityUser.EncryptedPassword,
                    Phone = securityUser.Phone,
                    Settings = securityUser.Settings
                });
            }

            _context.Object.SaveChanges();
        }
    }
}