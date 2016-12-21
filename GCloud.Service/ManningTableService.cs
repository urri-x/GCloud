using System;
using JetBrains.Annotations;
using KP.Storage.Domain;
using KP.Storage.Domain.Context;
using KP.Storage.Repository;

namespace KP.Service
{
    public class ManningTableService: IManningTableService
    {
        private readonly IDbContext context;
        private readonly IRepository<StaffObject> staffRepository;

        public ManningTableService(IDbContextFactory contextFactory, IRepository<StaffObject> staffRepository )
        {
            this.context = contextFactory.GetContext();
            this.staffRepository = staffRepository;
        }

        public StaffObject CreateStaffObject([NotNull] DateTime dateBegin, [NotNull] StaffObjectType objectType)
        {
            StaffObject newStaffObject;
            switch (objectType)
            {
                case StaffObjectType.StaffOrganization:
                    newStaffObject = CreateOrganization(dateBegin);
                    break;
                case StaffObjectType.StaffDepartment:
                    newStaffObject = CreateDepartment(dateBegin);
                    break;
                case StaffObjectType.StaffPosition:
                    newStaffObject = reatePosition(dateBegin);
                    break;
                default:
                    throw new ArgumentException("Unknown ObjectType");
            }
            staffRepository.Add(newStaffObject);
            context.SaveChanges();
            return newStaffObject;
        }

        private static StaffPosition reatePosition(DateTime dateBegin)
        {
            return new StaffPosition(dateBegin);
        }

        private static StaffDepartment CreateDepartment(DateTime dateBegin)
        {
            return new StaffDepartment(dateBegin);
        }

        private static StaffObject CreateOrganization(DateTime dateBegin)
        {
            return new StaffOrganization(dateBegin);
        }
    }
}