﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore学习.Models
{
    public class Course
    {
        /// <summary>
        /// 课程ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]//手动指定主键
        public int CourseID { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Credits { get; set; }

        /// <summary>
        /// 导航属性-关联的Enrollment-多个
        /// </summary>
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}