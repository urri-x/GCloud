using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Common;
using KP.FunctionalTests.Context;
using KP.Storage.Domain;
using NUnit.Framework;

namespace KP.FunctionalTests
{
    public class StaffContextTest
    {
        [Test]
        public void Create_Object_With_ShortName()
        {
            using (var db = new StaffContextForTests("name=TestContext"))
            {
                var newObject = db.Set<StaffObject>().Add(new StaffOrganization(DateTime.Parse("01.01.2016")));
                var shortName = new StaffObjectShortName(DateTime.Parse("01.01.2016"), "ООО Рога и Копыта", newObject);
                db.Set<StaffObjectShortName>().Add(shortName);

                db.SaveChanges();
                var savedObject = db.Set<StaffObject>().Find(newObject.Id);
                savedObject.ShortNames.Single().Should().BeSameAs(shortName);
            }
        }

        [Test]
        public void Create_Object_With_Many_ShortNames()
        {
            using (var db = new StaffContextForTests("name=TestContext"))
            {
                var newObject = db.Set<StaffObject>().Add(new StaffOrganization(DateTime.Parse("01.01.2016")));
                var shortNames = new List<StaffObjectShortName>()
                {
                    new StaffObjectShortName(DateTime.Parse("01.01.2015"), "ООО Рога и Копыта", newObject),
                    new StaffObjectShortName(DateTime.Parse("01.01.2016"), "ООО Легион", newObject)
                };
                db.Set<StaffObjectShortName>().AddRange(shortNames);

                db.SaveChanges();
                var savedObject = db.Set<StaffObject>().Find(newObject.Id);
                savedObject.ShortNames.ShouldBeEquivalentTo(shortNames);
                Assert.That(savedObject.ShortNames.SequenceEqual(shortNames));
            }
        }

        [Test]
        public void Create_ManyObjects_With_ShortName()
        {
            using (var db = new StaffContextForTests("name=TestContext"))
            {
                var firstObject = db.Set<StaffObject>().Add(new StaffOrganization(DateTime.Parse("01.01.2015")));
                var secondObject = db.Set<StaffObject>().Add(new StaffOrganization(DateTime.Parse("01.01.2016")));
                var firstShortName = new StaffObjectShortName(DateTime.Parse("01.01.2015"), "ООО Рога и Копыта",
                    firstObject);
                var secondShortName = new StaffObjectShortName(DateTime.Parse("01.01.2015"), "ООО Рога и Копыта",
                    secondObject);
                db.Set<StaffObjectShortName>().AddRange(new List<StaffObjectShortName>()
                {
                    firstShortName,
                    secondShortName
                });

                db.SaveChanges();
                var savedfirstObject = db.Set<StaffObject>().Find(firstObject.Id);
                var savedsecondObject = db.Set<StaffObject>().Find(secondObject.Id);

                Assert.That(savedfirstObject.ShortNames.Single().IsSameOrEqualTo(firstShortName));
                Assert.That(savedsecondObject.ShortNames.Single().IsSameOrEqualTo(secondShortName));
            }
        }

        [Test]
        public void Create_ManyObjects_With_ManyShortNames()
        {
            using (var db = new StaffContextForTests("name=TestContext"))
            {
                var firstObject = db.Set<StaffObject>().Add(new StaffOrganization(DateTime.Parse("01.01.2015")));
                var secondObject = db.Set<StaffObject>().Add(new StaffOrganization(DateTime.Parse("01.01.2016")));
                var firstShortNames = new List<StaffObjectShortName>()
                {
                    new StaffObjectShortName(DateTime.Parse("01.01.2015"), "ООО Рога и Копыта", firstObject),
                    new StaffObjectShortName(DateTime.Parse("01.01.2016"), "ООО Легион", firstObject)
                };
                var secondShortNames = new List<StaffObjectShortName>()
                {
                    new StaffObjectShortName(DateTime.Parse("01.01.2015"), "ЗАО Рога и Копыта", secondObject),
                    new StaffObjectShortName(DateTime.Parse("01.01.2016"), "ЗАО Легион", secondObject)
                };
                db.Set<StaffObjectShortName>().AddRange(firstShortNames.Concat(secondShortNames));

                db.SaveChanges();
                var savedfirstObject = db.Set<StaffObject>().Find(firstObject.Id);
                var savedsecondObject = db.Set<StaffObject>().Find(secondObject.Id);

                Assert.That(savedfirstObject.ShortNames.SequenceEqual(firstShortNames));
                Assert.That(savedsecondObject.ShortNames.SequenceEqual(secondShortNames));
            }
        }
    }
}