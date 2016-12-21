using System;
using FluentAssertions;
using KP.FunctionalTests.Context;
using KP.Storage.Domain;
using KP.Storage.Domain.Context;
using KP.Storage.Repository;
using NUnit.Framework;

namespace KP.FunctionalTests
{
    public class TestDbContextFactory : IDbContextFactory
    {
        private StaffContextForTests context;
        public IDbContext GetContext()
        {
            return context ?? (context = new StaffContextForTests("name=TestContext"));
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
    [TestFixture]
    public class RepositoryTest
    {
        [Test]
        public void Create_And_Get_Different_Entities()
        {
            using (var db = new TestDbContextFactory())
            {
                var newOrganization = new StaffOrganization(DateTime.Parse("01.01.2016"));
                var orgRepository = new Repository<StaffOrganization>(db);
                orgRepository.Add(newOrganization);

                var newDepartment = new StaffDepartment(DateTime.Parse("01.01.2016"));
                var departmentRepository = new Repository<StaffDepartment>(db);
                departmentRepository.Add(newDepartment);

                db.GetContext().SaveChanges();

                var savedOrganization = orgRepository.Get(newOrganization.Id);
                savedOrganization.Should().BeSameAs(newOrganization);

                var savedDepartment = departmentRepository.Get(newDepartment.Id);
                savedDepartment.Should().BeSameAs(newDepartment);

            }
        }
    }
}