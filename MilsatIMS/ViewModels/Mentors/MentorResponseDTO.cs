﻿using MilsatIMS.Enums;
using MilsatIMS.Models;
using MilsatIMS.ViewModels.Interns;

namespace MilsatIMS.ViewModels.Mentors
{
    public class MentorResponseDTO
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public GenderType Gender { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
        public TeamType Team { get; set; }
        public Guid SessionId { get; set; }
        public List<InternMiniDTO> Interns { get; set; }
    }

    public class MentorResponseUpdateDTO
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public GenderType Gender { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
        public TeamType Team { get; set; }
    }
}

