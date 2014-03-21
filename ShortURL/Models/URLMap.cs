using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShortURL.Models
{
    public class URLMap
    {
        public int ID { get; set; }

        [Display(Name = "短域名")]
        [StringLength(6, ErrorMessage = "长度小于6")]
        public string ShortUrl { get; set; }

        [Display(Name = "全域名")]
        [StringLength(100, ErrorMessage = "长度小于100")]
        public string FullUrl { get; set; }
    }
}