﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DBObjects
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int CommentScore { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentCreateDate { get; set; }
        public int PostId { get; set; }
        //public User CommentUser { get; set; }
        public Post Post { get; set; }


    }
}
