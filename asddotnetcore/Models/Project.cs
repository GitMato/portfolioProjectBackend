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
        [StringLength(256, ErrorMessage = "Description cannot be longer than 256 characters.")]
        public string Description { get; set; }
        //public Tool[] tools { get; set; }
        [StringLength(512, ErrorMessage = "Details cannot be longer than 512 characters.")]
        public string Details { get; set; }
        public string[] Extraimg { get; set; }
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