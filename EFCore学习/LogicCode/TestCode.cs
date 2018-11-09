﻿using EFCore学习.Data;
using EFCore学习.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCore学习.LogicCode
{
    public class TestCode
    {
        private readonly SchoolContext schoolContext;
        private readonly ILogger logger;

        public TestCode(SchoolContext schoolContext, ILoggerFactory loggerFactory)
        {
            this.schoolContext = schoolContext;
            this.logger = loggerFactory.CreateLogger("EF-Demo");
        }

        public void Run()
        {
            //起步
            this.GetAsync();

            //CRUD
            this.ReadData();
            this.CreateAndUpdateData();
            this.Update();

            //排序-筛选-分页-分组
            this.SortByStudentID();
            this.SearchName();
            this.Pagination();
            this.Group();

            Console.WriteLine("验证结束");
            Console.ReadLine();
        }

        public void Log(string str)
        {
            this.logger.LogInformation(str);
        }

        //起步
        public void GetAsync()
        {
            System.Console.WriteLine("R-获取集合数据");
            List<Student> students = this.schoolContext.Students.ToList();

            this.Log(students.ToJson());
        }

        #region CRUD

        //R
        public void ReadData()
        {
            this.logger.LogInformation("R-查找ID为2的相关学员数据");
            Student student = this.schoolContext.Students
                .Include(t => t.Enrollments)
                    .ThenInclude(e => e.Courses)
                .AsNoTracking()
                .FirstOrDefault(m => m.ID == 2);

            this.Log(student.ToJson());
        }

        //C 和 D
        public void CreateAndUpdateData()
        {
            this.logger.LogInformation("C D-添加学员");

            Student student = new Student()
            {
                EnrollmentDate = DateTime.Now,
                FirstName = "现",
                LastName = "在"
            };

            this.schoolContext.Students.Add(student);
            this.schoolContext.SaveChanges();

            var result = this.schoolContext.Students
                .FirstOrDefault(t => t.FirstName == "现");

            this.logger.LogInformation("新添加的成员：");
            this.Log(result?.ToJson());

            this.Log("添加成功");

            this.schoolContext.Students.Remove(result);
            this.schoolContext.SaveChanges();
        }

        //U
        public void Update()
        {
            this.logger.LogInformation("U-更新学员");

            var result = this.schoolContext.Students
                .Find(1);
            string tempStr = result.FirstName;

            result.FirstName = "AAAAAA";
            this.schoolContext.SaveChanges();

            this.logger.LogInformation("还原属性");
            result.FirstName = tempStr;

            this.schoolContext.SaveChanges();
        }

        #endregion CRUD

        #region 排序-筛选-分页

        //排序
        public void SortByStudentID()
        {
            this.Log("排序-StudentID");

            List<Student> students = this.schoolContext.Students
                .AsNoTracking()
                .OrderByDescending(t => t.ID)
                .ToList();
            this.Log(students.ToJson());
        }

        //筛选
        public void SearchName()
        {
            this.Log("筛选-姓为李的");

            Student student = this.schoolContext.Students
                  .AsNoTracking()
                  .SingleOrDefault(t => t.FirstName == "李");

            this.Log(student.ToJson());
        }

        //分页
        public void Pagination()
        {
            this.Log("分页--第二页--页大小为2--ID升序");

            int pageIndex = 2;
            int pageSize = 2;

            List<Student> students = this.schoolContext.Students
              .AsNoTracking()
              .OrderBy(t => t.ID)
              .Skip((pageIndex - 1) * pageSize)
              .Take(pageSize)
              .ToList();
            this.Log(students.ToJson());
        }

        //分组
        public void Group()
        {
            this.Log("分组-同一年的为一组");

            //Student student = this.schoolContext.Students
            //      .AsNoTracking()
            //      .SingleOrDefault(t => t.FirstName == "李");

            this.Log("错误分组：");
            var result = from a in this.schoolContext.Students
                         group a by a.EnrollmentDate.Year into g
                         select new KeyValuePair<int, List<Student>>(g.Key, g.ToList());
            var resultInfo = result.ToDictionary(t => t.Key, t => t.Value);

            this.Log("正确作法应该不使用EF，使用原生SQL.数据量少时使用错误的也可以");

            this.Log(resultInfo.ToJson());
        }

        #endregion 排序-筛选-分页
    }

    //显示测试结果用的
    public static class Extensions
    {
        private static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }
    }
}