using CityProjects.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityProjects.Data.Background_Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IDataHelper<Users> dataHelperUsers;
        private readonly IDataHelper<Mandates> dataHelperMandates;
        private readonly UserManager<IdentityUser> userManager;

        public UserRoleService(IDataHelper<Users> dataHelperUsers,
                               IDataHelper<Mandates> dataHelperMandates,
                               UserManager<IdentityUser> userManager)
        {
            this.dataHelperUsers = dataHelperUsers;
            this.dataHelperMandates = dataHelperMandates;
            this.userManager = userManager;
        }

        public async Task<UpdateResult> UpdateRolesAndMandatsAsync(Mandates expiredMandate)
        {
            // Get the region ID from the expired mandate president
            var regionId = GetRegionIDFromExpiredMandatePresident(expiredMandate);

            // Get the oldest member in the region
            var oldestMember = GetOldestMember(regionId);

            // Update the role from president to member 
            await UpdatePresidentRoleToMember(expiredMandate.PresidentId);

            // Update the role from secretary to president 
            await UpdateSecretaryRoleToPresident(GetSecretaryIDByRegionID(regionId));

            // Update the role from president to member 
            await UpdateMemberRoleToSecretary(oldestMember.UserId);

            // Return a successful result
            return new UpdateResult { Success = true, Message = "Roles and mandates updated successfully." };
        }


        private Users GetOldestMember(int RegionID)
        {
            // Get all memer of this region (the CityUserRoleId  of the member is  4)
            var MemberList = dataHelperUsers.GetAllData().Where(u => u.RegionId == RegionID & u.CityUserRoleId == 4).ToList();

            var OldestMember = new Users();
            int Age = 0;
            //Get the Oldest Member from the liste
            foreach (var Member in MemberList)
            {
                if (Member.age > Age)
                {
                    OldestMember = Member;
                    Age = Member.age;
                }
            }
            return OldestMember;
        }

        private async Task<ActionResult> UpdatePresidentRoleToMember(int PresidentID)
        {
            var president = dataHelperUsers.GetAllData()
                .FirstOrDefault(u => u.UserId == PresidentID);

            if (president == null)
            {
                return new NotFoundResult(); // Return NotFoundResult if president is not found
            }

            //Update the role from president to member in the table Users
            president.CityUserRoleId = 4;

            if (dataHelperUsers.Edit(PresidentID, president) == 0)
            {
                return new NotFoundResult(); // Return NotFoundResult if user edit fails
            }

            //Update the role from president to member in the table AspNetUsers
            var currentUserObject = await userManager.FindByIdAsync(president.AuthenticationUserId);

            if (currentUserObject != null)
            {
                var existingRoles = await userManager.GetRolesAsync(currentUserObject);
                await userManager.RemoveFromRolesAsync(currentUserObject, existingRoles.ToArray());
                await userManager.AddToRoleAsync(currentUserObject, "Member");
                return new OkResult(); // Return OkResult if role update is successful
            }
            else
            {
                // Handle the case where the current user is not found
                return new NotFoundResult();
            }
        }

        private async Task<bool> UpdateSecretaryRoleToPresident(int SecretaryID)
        {
            var secretary = dataHelperUsers.GetAllData()
                .FirstOrDefault(u => u.UserId == SecretaryID);

            if (secretary == null)
            {
                return false; // Indicate that the user with the specified ID is not found
            }

            // Update the role from secretary to president in the table Users
            secretary.CityUserRoleId = 1;

            if (dataHelperUsers.Edit(SecretaryID, secretary) == 0)
            {
                return false; // Indicate that the user edit failed
            }

            // Update the role from secretary to president in the table AspNetUsers
            var currentUserObject = await userManager.FindByIdAsync(secretary.AuthenticationUserId);

            if (currentUserObject != null)
            {
                var existingRoles = await userManager.GetRolesAsync(currentUserObject);
                await userManager.RemoveFromRolesAsync(currentUserObject, existingRoles.ToArray());
                await userManager.AddToRoleAsync(currentUserObject, "President");
                return true; // Indicate that the role update was successful
            }
            else
            {
                // Handle the case where the current user is not found
                return false;
            }
        }

        private async Task<bool> UpdateMemberRoleToSecretary(int MemberID)
        {
            var member = dataHelperUsers.GetAllData()
                .FirstOrDefault(u => u.UserId == MemberID);

            if (member == null)
            {
                return false; // Indicate that the user with the specified ID is not found
            }

            // Update the role from member to secretary in the table Users
            member.CityUserRoleId = 2;

            if (dataHelperUsers.Edit(MemberID, member) == 0)
            {
                return false; // Indicate that the user edit failed
            }

            // Update the role from member to secretary in the table AspNetUsers
            var currentUserObject = await userManager.FindByIdAsync(member.AuthenticationUserId);

            if (currentUserObject != null)
            {
                var existingRoles = await userManager.GetRolesAsync(currentUserObject);
                await userManager.RemoveFromRolesAsync(currentUserObject, existingRoles.ToArray());
                await userManager.AddToRoleAsync(currentUserObject, "Secretary");
                return true; // Indicate that the role update was successful
            }
            else
            {
                // Handle the case where the current user is not found
                return false;
            }
        }

        private int GetSecretaryIDByRegionID(int RegionID)
        {
            // Find the secretary for the specified region with CityUserRoleId == 2
            var secretary = dataHelperUsers.GetAllData()
                .FirstOrDefault(u => u.RegionId == RegionID && u.CityUserRoleId == 2);

            // Check if a secretary was found
            if (secretary != null)
            {
                // Return the ID of the secretary
                return secretary.UserId;
            }
            else
            {
                // Handle the case where there's no secretary for the specified region
                // You might return a default value or throw an exception depending on your requirements
                throw new InvalidOperationException("No secretary found for the specified region.");
            }
        }

        private int GetRegionIDFromExpiredMandatePresident(Mandates expiredMandate)
        {
            if (expiredMandate == null)
            {
                throw new ArgumentNullException(nameof(expiredMandate), "Expired mandate cannot be null.");
            }

            // Find the president based on the expired mandate's PresidentId
            var president = dataHelperUsers.GetAllData()
                .FirstOrDefault(u => u.UserId == expiredMandate.PresidentId);

            // Check if the president was found
            if (president != null)
            {
                // Return the RegionId of the president
                return president.RegionId;
            }
            else
            {
                // Handle the case where the president cannot be found
                // You might return a default value or throw an exception depending on your requirements
                throw new InvalidOperationException("President not found for the expired mandate.");
            }
        }


    }
}
