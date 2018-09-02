﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private RestaurantContext _context;

        public JobController(RestaurantContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult<JobApplication> GetById(int id)
        {
            var apply = _context.JobApplications.FirstOrDefault(p => p.Id == id);
            if (apply == null)
            {
                return NotFound();
            }

            return apply;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(JobApplication))]
        [ProducesResponseType(400)]
        public ActionResult<JobApplication> Create(JobApplication jobApplication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Add(jobApplication);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = jobApplication.Id }, jobApplication);
        }
    }
}