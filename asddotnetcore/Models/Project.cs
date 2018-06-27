using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Url)]
        public string ImgUrl { get; set; }
        public string ImgAlt { get; set; }
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters.")]
        public string description { get; set; }
        //public Tool[] tools { get; set; }
        [StringLength(1000, ErrorMessage = "Details cannot be longer than 1000 characters.")]
        public string details { get; set; }
        public string[] extraimg { get; set; }
    }
}

/**
id: 1, 
name:
imgUrl:
imgAlt: '', 
description:  
tools: ["C++", "QtCreator"], 
details:
extraimg: []
    **/