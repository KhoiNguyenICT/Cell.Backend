using Cell.Helpers.Extensions;
using Cell.Model.Entities.SettingTableEntity;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;

namespace Cell.UnitTest
{
    public class SettingTableTest
    {
        private Mock<ISettingTableService> _settingTableService;

        [SetUp]
        public void Setup()
        {
            _settingTableService = new Mock<ISettingTableService>();
        }

        [Test]
        public void InitializeData()
        {
            var settingTables = Builder<SettingTable>.CreateListOfSize(1000)
                .All()
                .With(x => x.Name = Faker.Name.FullName())
                .With(x => x.Description = Faker.Lorem.Paragraph())
                .With(x => x.Code == "LIST")
                .Build();
            foreach (var settingTable in settingTables)
            {
                _settingTableService.Object.AddAsync(new SettingTable
                {
                    Name = settingTable.Name,
                    Description = settingTable.Description,
                    Code = settingTable.Code,
                    BasedTable = $"T_DATA_{settingTable.Name.ToUpper().Split(" ").JoinString(",")}"
                });
            }
            _settingTableService.Object.CommitAsync();
        }
    }
}