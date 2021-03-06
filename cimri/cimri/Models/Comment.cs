﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cimri.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        public int ItemID { get; set; }
        public int Rating { get; set; }
        public virtual Item Item { get; set; }
    }
}