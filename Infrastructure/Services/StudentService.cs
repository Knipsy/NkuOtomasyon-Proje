﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specification.StudentSpecs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public StudentService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<StudentInformation> GetStudentInformation(ClaimsPrincipal user)  //memorycache kullan. devamlı kullanılacak.
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (!String.IsNullOrEmpty(email))
            { 
                var id = (await _userManager.Users.SingleOrDefaultAsync(x => x.Email == email)).Id;
                var spec = new StudentEducationInformationSpecification(id); 
                return await _unitOfWork.Repository<StudentInformation>().GetWithSpec(spec);  
            }

            return null;
        }
    }
}
