﻿using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;
using SchoolProject.Infrustucture.Abstracts;
using SchoolProject.Service.Abstracts;
using Serilog;


namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
    {
        #region Fields

        private readonly IStudentRepository _studentRepository;
        #endregion
        #region Constructors
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        #endregion
        #region Handel Functions
        public async Task<List<Student>> GetStudentListAsync()
        {
            return await _studentRepository.GetStudentsListAsync();
        }
        public async Task<Student> GetStudentByIDWithIncludeAsync(int id)
        {
            /* var student = await _studentRepository.GetByIdAsync(id);*/
            var student = _studentRepository.GetTableNoTracking()
                                                  .Include(x => x.Department)
                                                  .Where(x => x.StudID.Equals(id))
                                                  .FirstOrDefault();
            return student;
        }

        public async Task<string> AddAsync(Student student)
        {
            //check if name is Exist Or not
            var studentResult = _studentRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(student.NameAr)).FirstOrDefault();
            if (studentResult != null) return "Exist";
            //Add Student
            await _studentRepository.AddAsync(student);
            return "Success";
        }

        public async Task<bool> IsNameEnExist(string nameEn)
        {
            //Check if the name is Exist Or not
            var student = _studentRepository.GetTableNoTracking().Where(x => x.NameEn.Equals(nameEn)).FirstOrDefault();
            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExistExcludeSelf(string nameEn, int id)
        {
            //Check if the name is Exist Or not
            var student = await _studentRepository.GetTableNoTracking().Where(x => x.NameEn.Equals(nameEn) & !x.StudID.Equals(id)).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameArExist(string nameAr)
        {
            //Check if the name is Exist Or not
            var student = _studentRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(nameAr)).FirstOrDefault();
            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameArExistExcludeSelf(string nameAr, int id)
        {
            //Check if the name is Exist Or not
            var student = await _studentRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(nameAr) & !x.StudID.Equals(id)).FirstOrDefaultAsync();
            if (student == null) return false;
            return true;
        }

        public async Task<string> EditAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "Success";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                Log.Error(ex.Message);
                return "Falied";
            }

        }

        public Task<Student> GetByIDAsync(int id)
        {
            var student = _studentRepository.GetByIdAsync(id);
            return student;
        }

        public IQueryable<Student> GetStudentsQuerable()
        {
            return _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum, string search)
        {
            var querable = _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.NameAr.Contains(search) || x.Address.Contains(search));
            }
            switch (orderingEnum)
            {
                case StudentOrderingEnum.StudID:
                    querable.OrderBy(x => x.StudID);
                    break;
                case StudentOrderingEnum.Name:
                    querable.OrderBy(x => x.NameAr);
                    break;
                case StudentOrderingEnum.Address:
                    querable.OrderBy(x => x.Address);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    querable.OrderBy(x => x.Department.DNameAr);
                    break;

            }


            return querable;
        }

        public IQueryable<Student> GetStudentsByDepartmentIDQuerable(int DID)
        {
            return _studentRepository.GetTableNoTracking().Where(x => x.DID.Equals(DID)).AsQueryable();
        }


        #endregion
    }
}
