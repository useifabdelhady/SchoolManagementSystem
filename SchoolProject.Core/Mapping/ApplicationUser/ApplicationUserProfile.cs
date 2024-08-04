﻿using AutoMapper;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            AddUserMapping();
            GetUserByIdMapping();
            GetUserPaginationMapping();
            UpdateUserMapping();
        }
    }
}
