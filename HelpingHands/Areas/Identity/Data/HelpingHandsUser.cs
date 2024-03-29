﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using HelpingHands.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Areas.Identity.Data;

// Add profile data for application users by adding properties to the HelpingHandsUser class
public class HelpingHandsUser : IdentityUser
{
    [Required]
    [StringLength(20)]
    public string FirstName { get; set; }
    [Required]
    [StringLength(30)]
    public string Surname { get; set; }
}

