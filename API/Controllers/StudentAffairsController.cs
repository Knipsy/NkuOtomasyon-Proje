﻿using System.Threading.Tasks;
using API.Dtos;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class StudentAffairsController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinary;
       

        public StudentAffairsController(UserManager<User> userManager,IUnitOfWork unitOfWork,ICloudinaryService cloudinary)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _cloudinary = cloudinary;
        }

        [HttpPost]
        public async Task<ActionResult> UpdateStudent(UpdateStudentDto studentDto)
        {
            
            var spec= new StudentWithIncludesSpecification(studentDto.SchoolNumber);

            var spec2 = new StudentWithIncludesSpecification();

            var student = await _unitOfWork.Repository<Student>().GetWithSpec(spec);
            student.Photo = _cloudinary.UploadPhoto(student.Id, studentDto.FormFile);  
            return null;
        }
    }
}
